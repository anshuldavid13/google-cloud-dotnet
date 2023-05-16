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

using static Google.Cloud.Datastore.V1.AggregationQuery.Types;
using static Google.Cloud.Datastore.V1.AggregationQuery.Types.Aggregation.Types;

namespace Google.Cloud.Datastore.V1;

/// <summary>
///  Factory for different types of Aggregations.
/// </summary>
public static class Aggregations
{
    /// <summary>
    /// Returns an instance of count(*) aggregation.
    /// </summary>
    /// <param name="alias">A string used to retrieve the result of this aggregation.
    /// If not provided, Datastore will pick a default name following the format `property_&lt;incremental_id++>`
    /// eg. property_1
    /// </param>
    /// <returns>A count(*) aggregation.</returns>
    public static Aggregation Count(string alias = "") =>
        new Aggregation { Count = new Count(), Alias = alias };

    /// <summary>
    /// Sum of the values of the requested property.
    ///
    /// * Only numeric values will be aggregated. All non-numeric values
    /// including `NULL` are skipped.
    ///
    /// * If the aggregated values contain `NaN`, returns `NaN`.
    ///
    /// * If the aggregated value set is empty, returns 0.
    ///
    /// * Returns a 64-bit integer if the sum result is an integer value and does
    /// not overflow. Otherwise, the result is returned as a double. Note that
    /// even if all the aggregated values are integers, the result is returned
    /// as a double if it cannot fit within a 64-bit signed integer. When this
    /// occurs, the returned value will lose precision.
    ///
    /// * When underflow occurs, floating-point aggregation is non-deterministic.
    /// This means that running the same query repeatedly without any changes to
    /// the underlying values could produce slightly different results each
    /// time. In those cases, values should be stored as integers over
    /// floating-point numbers.
    /// </summary>
    /// <param name="property">Property for which Sum is to calculated</param>
    /// <param name="alias">A string used to retrieve the result of this aggregation.</param>
    /// <returns></returns>
    public static Aggregation Sum(string property, string alias = "") =>
        new() { Sum = new Sum() { Property = new PropertyReference(property) }, Alias = alias };

    /// <summary>
    /// Average of the values of the requested property.
    ///
    /// * Only numeric values will be aggregated. All non-numeric values
    /// including `NULL` are skipped.
    ///
    /// * If the aggregated values contain `NaN`, returns `NaN`.
    ///
    /// * If the aggregated value set is empty, returns `NULL`.
    ///
    /// * Always returns the result as a double.
    /// </summary>
    /// <param name="property">Property for which Average is to calculated</param>
    /// <param name="alias">A string used to retrieve the result of this aggregation.</param>
    /// <returns></returns>
    public static Aggregation Average(string property, string alias = "") =>
        new() { Avg = new Avg() { Property = new PropertyReference(property) }, Alias = alias };
}
