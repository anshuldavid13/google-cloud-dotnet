﻿// Copyright 2017 Google Inc. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Cloud.Diagnostics.Common;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Google.Cloud.Diagnostics.AspNetCore3.Tests
{
    public class CloudTraceMiddlewareTest
    {
        private static readonly TraceIdFactory _traceIdFactory = TraceIdFactory.Create();
        private static readonly ITraceContext _traceContext = new SimpleTraceContext(_traceIdFactory.NextId(), 0, true);

        /// <summary>
        /// Creates a <see cref="Mock{IManagedTracer}"/> that is set up to start and end a span as well as
        /// annotate the span.
        /// </summary>
        private static Mock<IManagedTracer> CreateIManagedTracerMock(HttpContext context)
        {
            var tracerMock = new Mock<IManagedTracer>();
            tracerMock.Setup(t => t.GetCurrentTraceId()).Returns(_traceContext.TraceId);
            tracerMock.Setup(t => t.StartSpan(context.Request.Path, null)).Returns(new NullManagedTracer.Span());
            tracerMock.Setup(t => t.AnnotateSpan(It.IsAny<Dictionary<string, string>>()));
            return tracerMock;
        }

        /// <summary>
        /// Create a basic <see cref="HttpContext"/> with a request and request path.
        /// </summary>
        private static HttpContext CreateHttpContext()
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Path = new PathString("/api/trace");
            return context;
        }

        private static void CustomTraceContextPropagator(HttpResponse response, ITraceContext context) =>
                response.Headers.Add("custom_trace", context.TraceId);

        private static void AssertCustomTraceContext(HttpResponse response)
        {
            // Let's make sure that we don't add the Google trace (unless the propagator does).
            Assert.False(response.Headers.ContainsKey(TraceHeaderContext.TraceHeader));
            Assert.True(response.Headers.ContainsKey("custom_trace"));
            Assert.Equal(_traceContext.TraceId, response.Headers["custom_trace"]);
        }

        private static void AssertNoTraceContext(HttpResponse response)
        {
            // Let's make sure that we don't add the Google trace (unless the propagator does).
            Assert.False(response.Headers.ContainsKey(TraceHeaderContext.TraceHeader));
            Assert.False(response.Headers.ContainsKey("custom_trace"));
        }

        [Fact]
        public async Task Invoke_Trace()
        {
            var context = CreateHttpContext();
            var tracerMock = CreateIManagedTracerMock(context);

            var delegateMock = new Mock<RequestDelegate>();
            delegateMock.Setup(d => d(context)).Returns(Task.CompletedTask);

            Func<ITraceContext, IManagedTracer> fakeFactory = f => tracerMock.Object;

            Assert.Equal(NullManagedTracer.Instance, ContextTracerManager.GetCurrentTracer());

            var middleware = new CloudTraceMiddleware(delegateMock.Object, fakeFactory, new DefaultCloudTraceNameProvider());
            await middleware.Invoke(context, _traceContext, TraceDecisionPredicate.Default, CustomTraceContextPropagator);

            // Since the current tracer is AsyncLocal<>, it will be back to the default after awaiting the middleware invoke
            Assert.Equal(NullManagedTracer.Instance, ContextTracerManager.GetCurrentTracer());

            AssertCustomTraceContext(context.Response);

            delegateMock.VerifyAll();
            tracerMock.VerifyAll();
        }

        [Fact]
        public async Task Invoke_TraceException()
        {
            var context = CreateHttpContext();
            var tracerMock = CreateIManagedTracerMock(context);
            tracerMock.Setup(t => t.SetStackTrace(It.IsAny<StackTrace>()));

            var delegateMock = new Mock<RequestDelegate>();
            delegateMock.Setup(d => d(context)).Throws(new DivideByZeroException());

            Func<ITraceContext, IManagedTracer> fakeFactory = f => tracerMock.Object;

            var middleware = new CloudTraceMiddleware(delegateMock.Object, fakeFactory, new DefaultCloudTraceNameProvider());
            await Assert.ThrowsAsync<DivideByZeroException>(
                () => middleware.Invoke(context, _traceContext, TraceDecisionPredicate.Default, CustomTraceContextPropagator));

            AssertCustomTraceContext(context.Response);

            delegateMock.VerifyAll();
            tracerMock.VerifyAll();
        }

        [Fact]
        public async Task Invoke_TraceThrowsAndException()
        {
            var context = CreateHttpContext();
            var tracerMock = CreateIManagedTracerMock(context);
            tracerMock.Setup(t => t.SetStackTrace(It.IsAny<StackTrace>()))
                .Throws(new ArgumentOutOfRangeException());

            var delegateMock = new Mock<RequestDelegate>();
            delegateMock.Setup(d => d(context)).Throws(new DivideByZeroException());

            Func<ITraceContext, IManagedTracer> fakeFactory = f => tracerMock.Object;

            var middleware = new CloudTraceMiddleware(delegateMock.Object, fakeFactory, new DefaultCloudTraceNameProvider());
            await Assert.ThrowsAsync<AggregateException>(
                () => middleware.Invoke(context, _traceContext, TraceDecisionPredicate.Default, CustomTraceContextPropagator));

            AssertCustomTraceContext(context.Response);

            delegateMock.VerifyAll();
            tracerMock.VerifyAll();
        }

        [Fact]
        public async Task Invoke_NoTrace()
        {
            var context = new DefaultHttpContext();
            var delegateMock = new Mock<RequestDelegate>();
            var tracerMock = new Mock<IManagedTracer>();

            Func<ITraceContext, IManagedTracer> fakeFactory = f => tracerMock.Object;

            var middleware = new CloudTraceMiddleware(delegateMock.Object, fakeFactory, new DefaultCloudTraceNameProvider());
            await middleware.Invoke(context, _traceContext, TraceDecisionPredicate.Default, CustomTraceContextPropagator);

            // Since the current tracer is AsyncLocal<>, it will be back to the default after awaiting the middleware invoke
            Assert.Equal(NullManagedTracer.Instance, ContextTracerManager.GetCurrentTracer());

            AssertNoTraceContext(context.Response);

            delegateMock.Verify(d => d(context), Times.Once());
            tracerMock.Verify(t => t.StartSpan(It.IsAny<string>(), null), Times.Never());
        }
    }
}
