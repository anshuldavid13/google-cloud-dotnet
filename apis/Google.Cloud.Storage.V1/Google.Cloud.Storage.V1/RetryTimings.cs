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

namespace Google.Cloud.Storage.V1;

/// <summary>
/// These specify the custom timing configurations for retrying in case of failure of the API request.
/// </summary>
public sealed class RetryTimings
{
    /// <summary>
    /// The backoff time between the first attempt and the first retry. It must be non-negative. 
    /// </summary>
    public TimeSpan InitialBackoff { get; private set; }

    /// <summary>
    /// Maximum backoff time between retries. It must be at least as much as initialBackoff.
    /// </summary>
    public TimeSpan MaxBackoff { get; private set; }

    /// <summary>
    /// The multiplier to apply to the backoff on each iteration. It is always greater than or equal to 1.0.
    /// </summary>
    public double BackoffMultiplier { get; private set; }

    /// <summary>
    /// The default initial backoff time between the first attempt and the first retry. It is set to 1 second by default.
    /// </summary>
    public static TimeSpan DefaultInitialBackoff { get; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// The default maximum backoff time between retries. It is set to 32 seconds by default.
    /// </summary>
    public static TimeSpan DefaultMaxBackoff { get; } = TimeSpan.FromSeconds(32);

    /// <summary>
    /// The default maximum backoff multiplier to be applied on each iteration. It is set to 2 by default.
    /// </summary>
    public static double DefaultBackoffMultiplier { get; } = 2;

    /// <summary>
    /// Returns the retry timing configurations all set to default.
    /// </summary>
    public static RetryTimings Default { get; } = new();

    internal RetryTimings Clone() => new()
    {
        InitialBackoff = InitialBackoff,
        MaxBackoff = MaxBackoff,
        BackoffMultiplier = BackoffMultiplier
    };

    /// <summary>
    /// Returns the existing retry timings configurations using the specified initial backoff.
    /// </summary>
    /// <param name="initialBackoff">Custom initial Backoff for retry.</param>
    /// <returns>Retry timings with updated initial backoff.</returns>
    public RetryTimings WithInitialBackoff(TimeSpan initialBackoff)
    {
        var clone = Clone();
        clone.InitialBackoff = initialBackoff;
        return clone;
    }

    /// <summary>
    /// Returns the existing retry timings configurations using the specified maximum backoff.
    /// </summary>
    /// <param name="maxBackoff">Custom maximum Backoff for retry.</param>
    /// <returns>Retry timings with updated maximum backoff timing.</returns>
    public RetryTimings WithMaxBackoff(TimeSpan maxBackoff)
    {
        var clone = Clone();
        clone.MaxBackoff = maxBackoff;
        return clone;
    }

    /// <summary>
    /// Returns the existing retry timings configurations using the specified backoff multiplier.
    /// </summary>
    /// <param name="backoffMultiplier">Custom retry delay Backoff Multiplier for retry.</param>
    /// <returns>Retry timings with updated backoff multiplier.</returns>
    public RetryTimings WithBackoffMultiplier(double backoffMultiplier)
    {
        var clone = Clone();
        clone.BackoffMultiplier = backoffMultiplier;
        return clone;
    }

    /// <summary>
    /// Retry Timings constructor which sets all values to their respective defaults.
    /// </summary>
    public RetryTimings()
    {
        InitialBackoff = RetryTimings.DefaultInitialBackoff;
        MaxBackoff = RetryTimings.DefaultMaxBackoff;
        BackoffMultiplier = RetryTimings.DefaultBackoffMultiplier;
    }

    /// <summary>
    /// Constructor to pass the custom timing configurations for retrying.
    /// </summary>
    /// <param name="initialBackoff">Initial Backoff for retry.</param>
    /// <param name="maxBackoff">Maximum Backoff for retry.</param>
    /// <param name="backoffMultiplier">Retry delay Backoff Multiplier for retry.</param>
    public RetryTimings(TimeSpan initialBackoff, TimeSpan maxBackoff, double backoffMultiplier)
    {
        InitialBackoff = initialBackoff >= TimeSpan.Zero ?initialBackoff : throw new ArgumentOutOfRangeException("InitialBackoff value must be non-negative.") ;
        MaxBackoff = maxBackoff >= initialBackoff ? maxBackoff : throw new ArgumentOutOfRangeException("MaxBackoff value must be at least as much as initialBackoff.");
        BackoffMultiplier = backoffMultiplier >= 1 ? backoffMultiplier : throw new ArgumentOutOfRangeException("BackoffMultiplier value must atleast be 1.");
    }
}
