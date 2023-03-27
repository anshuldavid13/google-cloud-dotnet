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

using System;
using Xunit;

namespace Google.Cloud.Storage.V1.Tests;
public class RetryTimingsTest
{
    [Fact]
    public void Check_MaxBackOffConstraint() => Assert.Throws<ArgumentOutOfRangeException>(() =>
        RetryTimings.Default.WithMaxBackoff(maxBackoff: TimeSpan.FromSeconds(0)));

    [Fact]
    public void Check_InitialBackOffConstraint() => Assert.Throws<ArgumentOutOfRangeException>(() =>
        RetryTimings.Default.WithInitialBackoff(initialBackoff: TimeSpan.FromSeconds(-1)));

    [Fact]
    public void Check_BackOffMultiplierConstraint() => Assert.Throws<ArgumentOutOfRangeException>(() =>
        RetryTimings.Default.WithBackoffMultiplier(backoffMultiplier: 0));
}
