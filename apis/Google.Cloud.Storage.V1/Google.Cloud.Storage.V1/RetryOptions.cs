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

namespace Google.Cloud.Storage.V1;

/// <summary>
/// Options used to indicate the retry configuration for an API request in case of failure.
/// These determine whether and how the request should be retried.
/// </summary>
public sealed class RetryOptions
{
    /// <summary>
    /// Options used by default for idempotent operations upon failure.
    /// It has all retry timings reset to defaults and retry predicate based on default error codes.
    /// </summary>
    public static RetryOptions IdempotentRetryOptions { get; } = new RetryOptions(RetryTimings.Default, RetryPredicate.DefaultErrorCodes);

    /// <summary>
    /// This configuration ensures that the API request is never retried on failure.
    /// </summary>
    public static RetryOptions Never { get; } = new RetryOptions(RetryTimings.Default, RetryPredicate.Never);

    internal RetryPredicate Predicate { get; }
    internal RetryTimings Timing { get; }

    internal static RetryOptions MaybeIdempotent(object condition) =>
        condition is null ? Never : IdempotentRetryOptions;

    /// <summary>
    /// Creates an instance based on the given timing and predicate.
    /// </summary>
    /// <param name="retryTimings">Custom retry timings configurations in case of retrying. It can be null in which case default retry timings <see cref="RetryTimings.Default"/> will be used.</param>
    /// <param name="retryPredicate">Custom retry predicate configurations to determine when to retry.It can be null in which case default error codes <see cref="RetryPredicate.DefaultErrorCodes"/> will be used for retrying.</param>
    public RetryOptions(RetryTimings retryTimings, RetryPredicate retryPredicate)
    {
        Timing = retryTimings ?? RetryTimings.Default;
        Predicate = retryPredicate ?? RetryPredicate.Never;
    }
}
