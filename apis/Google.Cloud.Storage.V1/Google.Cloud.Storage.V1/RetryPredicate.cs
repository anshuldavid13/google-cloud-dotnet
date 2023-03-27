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
using System.Linq;

namespace Google.Cloud.Storage.V1;

/// <summary>
/// It specifies the configurations based on which the user might want to retry the operations in case of failure. These are immutable.
/// </summary>
public sealed class RetryPredicate
{
    /// <summary>
    /// Default retriable error codes for which the operation would retry unless otherwise specified by the user. 
    /// </summary>
    public static RetryPredicate DefaultErrorCodes { get; } = RetryPredicate.FromErrorCodes(
                408, // Request timeout
                429, // Too many requests
                500, // Internal server error
                502, // Bad gateway
                503, // Service unavailable
                504 // Gateway timeout
        );

    private readonly Func<int, bool> _predicate;

    /// <summary>
    /// Returns a Retry Predicate which will ensure that the operation will never retry in case of failure.
    /// </summary>
    public static RetryPredicate Never { get; } = RetryPredicate.FromErrorCodes();

    /// <summary>
    /// It configures retrying based on the specified custom retriable error codes.
    /// In case the API request fails with an error code provided in this list, it is retried.
    /// These error codes completely override the default error codes provided and are not appended to it.
    /// </summary>
    /// <param name="errorCodes">Error codes on which to retry.</param>
    /// <returns>Returns the retry predicate with the custom error codes specified.</returns>
    public static RetryPredicate FromErrorCodes(params int[] errorCodes) => new(x => errorCodes.Contains(x));

    /// <summary>
    /// It configures the conditions based on which the operation is retried in case of failure.
    /// If configured, it completely override the default error codes provided and they are not considered anymore for retrying.
    /// </summary>
    /// <param name="predicate">Predicate based on which to decide whether to retry or not.</param>
    /// <returns>Returns the retry predicate with the conditions specified for retrying.</returns>
    public static RetryPredicate FromErrorCodePredicate(Func<int, bool> predicate) => new(predicate);

    internal bool ShouldRetry(int statusCode) => _predicate.Invoke(statusCode);

    private RetryPredicate(Func<int, bool> predicate) => _predicate = predicate;
}
