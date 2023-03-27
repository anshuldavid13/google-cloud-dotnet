// Copyright 2023 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License").
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at 
//
// https://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and 
// limitations under the License.

using Google.Apis.Requests;
using Google.Apis.Storage.v1;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Google.Cloud.Storage.V1.Tests;
public class CustomRetryImplTest
{
    [Fact]
    public void Check_TimingsNull()
    {
        RetryTimings retryTimings = null;
        RetryPredicate retryPredicate = RetryPredicate.DefaultErrorCodes;

        RetryOptions retryOptions = new RetryOptions(retryTimings, retryPredicate);

        Assert.Equal(RetryTimings.Default, retryOptions.Timing);
    }

    [Fact]
    public void Check_PredicateNull()
    {
        RetryTimings retryTimings = RetryTimings.Default;
        RetryPredicate retryPredicate = null;

        RetryOptions retryOptions = new RetryOptions(retryTimings, retryPredicate);

        Assert.Equal(RetryPredicate.Never, retryOptions.Predicate);
    }

    [Fact]
    public void Check_RetryfromErrorCodes()
    {
        var retryPredicate = RetryPredicate.FromErrorCodes(429, 502);

        Assert.True(retryPredicate.ShouldRetry(429));
        Assert.True(retryPredicate.ShouldRetry(502));
        Assert.False(retryPredicate.ShouldRetry(500));
    }

    [Fact]
    public void Check_RetryfromErrorPredicate()
    {
        var retryPredicate = RetryPredicate.FromErrorCodePredicate(errorCode => errorCode >= 500);

        Assert.False(retryPredicate.ShouldRetry(429));
        Assert.True(retryPredicate.ShouldRetry(502));
        Assert.True(retryPredicate.ShouldRetry(500));
    }

    [Fact]
    public void Check_ParellelRun()
    {
        var retryOptionsList = new List<RetryOptions>
            {
                new RetryOptions(
            retryTimings: new RetryTimings(initialBackoff: TimeSpan.FromSeconds(1),
               maxBackoff: TimeSpan.FromSeconds(6), backoffMultiplier: 2),
            retryPredicate: RetryPredicate.FromErrorCodes(429, 502)),

                new RetryOptions(
            retryTimings: new RetryTimings(initialBackoff: TimeSpan.FromSeconds(4),
               maxBackoff: TimeSpan.FromSeconds(20), backoffMultiplier: 5),
            retryPredicate: RetryPredicate.FromErrorCodes(501, 502)),

                new RetryOptions(
            retryTimings: RetryTimings.Default.WithMaxBackoff(TimeSpan.FromSeconds(3)),
            retryPredicate: RetryPredicate.FromErrorCodes(502))
            };

        Parallel.For(0, 2, i => RetryHelper(service => service.Buckets.Get("bucket"), client => client.GetBucket("bucket", new GetBucketOptions
        { RetryOptions = retryOptionsList[i] }), retryOptionsList[i]));
    }

    #region Helper Methods

    private static void Retry<T>(Func<StorageService, ClientServiceRequest<T>> requestProvider, Action<StorageClient> clientAction, T response,
        RetryOptions retryOptions, HttpStatusCode errorCode = HttpStatusCode.BadGateway)
    {
        var service = new FakeStorageService();
        var client = new StorageClientImpl(service);
        var request = requestProvider(service);
        retryOptions ??= RetryOptions.IdempotentRetryOptions;

        if (retryOptions.Predicate.ShouldRetry((int) errorCode))
        {
            int retrycount = 0;
            TimeSpan delay = retryOptions.Timing.InitialBackoff;
            while (delay < retryOptions.Timing.MaxBackoff && retrycount < 3)
            {
                delay = retryOptions.Timing.InitialBackoff + TimeSpan.FromSeconds((retrycount) * retryOptions.Timing.BackoffMultiplier);
                service.ExpectRequest(request, errorCode);
                retrycount++;
            }
        }
        else
        {
            service.ExpectRequest(request, errorCode);
        }

        Assert.Throws<GoogleApiException>(() => clientAction(client));
        service.Verify();
    }

    private static void RetryHelper<T>(Func<StorageService, ClientServiceRequest<T>> requestProvider, Action<StorageClient> clientAction, RetryOptions retryOptions,
       HttpStatusCode errorCode = HttpStatusCode.BadGateway)
       where T : new() => Retry(requestProvider, clientAction, new T(), retryOptions, errorCode);

    #endregion

}
