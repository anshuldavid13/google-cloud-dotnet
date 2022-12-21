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
/// These options can be used to pass custom retry configuration for each API request in case of failure.
/// These determine whether and how the retry would happen.
/// </summary>
public sealed class RetryOptions
{
    /// <summary>
    /// The default configuration used for retry upon failure.
    /// It has all retry timings reset to defaults and retry predicate based on default error codes.
    /// </summary>
    public static RetryOptions IdempotentRetryOptions { get; } = new RetryOptions(RetryTimings.Default, RetryPredicate.DefaultErrorCodes);

    /// <summary>
    /// This configuration ensures that the api request is never retried on failure.
    /// </summary>
    public static RetryOptions Never { get; } = new RetryOptions(new RetryTimings(), RetryPredicate.EmptyPredicate);

    internal RetryPredicate RetryPredicate { get; }
    internal RetryTimings RetryTimings { get; }

    internal static RetryOptions MaybeIdempotent(object condition) =>
        condition is null ? Never : IdempotentRetryOptions;

    /// <summary>
    /// Constructor to pass the custom configurations for retrying on failure.
    /// </summary>
    /// <param name="retryTimings">Custom retry timings configurations in case of retrying.</param>
    /// <param name="retryPredicate">Custom retry predicate configurations to determine when to retry.</param>
    public RetryOptions(RetryTimings retryTimings, RetryPredicate retryPredicate)
    {
        RetryTimings = retryTimings ?? RetryTimings.Default;
        RetryPredicate = retryPredicate ?? RetryPredicate.EmptyPredicate;
    }
}
