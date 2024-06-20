// Copyright 2022 Google LLC
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

using Google.Api.Gax;
using Google.Cloud.Firestore.V1;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types.Aggregation.Types;

namespace Google.Cloud.Firestore;

/// <summary>
/// Factory for different <see cref="Aggregation"/>.
/// </summary>
public static class Aggregates
{
    /// <summary>
    /// The "alias" to specify in the <see cref="RunAggregationQueryRequest"/> proto when running a count query.
    /// The actual value is not meaningful, but will be used to get the count out of the <see cref="RunAggregationQueryResponse"/>.
    /// </summary>
    internal const string CountAlias = "Count";

    internal const string AvgAliasPrefix = "Avg";

    internal const string SumAliasPrefix = "Sum";

    /// <summary>
    /// Calculates the count of the values of the requested field.
    /// It uses the server side aggregation <see cref="Aggregation.Count"/> to get the count.
    ///
    /// The `COUNT(*)` aggregation function operates on the entire document so it does not require a field reference.
    /// 
    /// </summary>
    /// <returns>Returns the count.</returns>
    public static Aggregation Count() => new() { Count = new Count(), Alias = CountAlias };

    /// <summary>
    /// 
    /// Calculates the sum of the values of the requested field.
    /// It uses the server side aggregation <see cref="Aggregation.Sum"/> to get the sum value.
    /// 
    /// * Only numeric values will be aggregated. All non-numeric values including `NULL` are skipped.
    /// * If the aggregated values contain `NaN`, returns `NaN`.
    /// * If the aggregated value set is empty, returns 0.
    /// * Returns a 64-bit integer if the sum result is an integer value and does
    /// not overflow or underflow. Otherwise, the result is returned as a double.
    /// 
    /// </summary>
    /// <param name="field">The mandatory field on which the Sum aggregation is performed.</param>
    /// <param name="alias" cref="Aggregation.Alias">
    ///
    /// An optional parameter to store the alias.
    /// If none is provided by user , one is generated based on the field names formatted as
    /// "operatorName"_"fieldName". Example: Sum_marks
    /// 
    /// </param>
    /// <returns>Returns the sum of the values of the requested field.</returns>
    public static Aggregation Sum(string field, string alias = null)
    {
        GaxPreconditions.CheckNotNullOrEmpty(field, nameof(field));
        return new Aggregation { Sum = new Sum() { Field = FieldPath.FromDotSeparatedString(field).ToFieldReference() }, Alias = alias ?? SumAliasPrefix + "_" + field };
    }

    /// <summary>
    /// 
    /// Calculates the sum of the values of the requested field.
    /// It uses the server side aggregation <see cref="Aggregation.Sum"/> to get the sum value.
    /// 
    /// * Only numeric values will be aggregated. All non-numeric values including `NULL` are skipped.
    /// * If the aggregated values contain `NaN`, returns `NaN`.
    /// * If the aggregated value set is empty, returns 0.
    /// * Returns a 64-bit integer if the sum result is an integer value and does
    /// not overflow or underflow. Otherwise, the result is returned as a double.
    /// 
    /// </summary>
    /// <param name="fieldPath">The fieldpath for the mandatory field on which the Sum aggregation is performed.</param>
    /// <param name="alias" cref="Aggregation.Alias">
    ///
    /// An optional parameter to store the alias.
    /// If none is provided by user , one is generated based on the field names formatted as
    /// "operatorName"_"fieldName". Example: Sum_marks
    /// 
    /// </param>
    /// <returns>Returns the sum of the values of the requested field.</returns>
    public static Aggregation Sum(FieldPath fieldPath, string alias = null)
    {
        GaxPreconditions.CheckNotNull(fieldPath, nameof(fieldPath));
        return new Aggregation { Sum = new Sum() { Field = fieldPath.ToFieldReference() }, Alias = alias ?? SumAliasPrefix + "_" + fieldPath.EncodedPath };
    }

    /// <summary>
    /// 
    /// Calculates the average of the values of the requested field.
    /// It uses the server side aggregation <see cref="Aggregation.Avg"/> to get the average value.
    ///
    /// * Only numeric values will be aggregated. All non-numeric values including `NULL` are skipped.
    /// * If the aggregated values contain `NaN`, returns `NaN`.
    /// * If the aggregated value set is empty, returns `NULL`.
    /// * Always returns the result as a double.
    /// 
    /// </summary>
    /// <param name="field">The mandatory field on which the Average aggregation is performed.</param>
    /// <param name="alias" cref="Aggregation.Alias">
    ///
    /// An optional parameter to store the alias.
    /// If none is provided by user , one is generated based on the field names formatted as
    /// "operatorName"_"fieldName". Example: Avg_age
    /// 
    /// </param>
    /// <returns>Returns the average of the values of the requested field.</returns>
    public static Aggregation Average(string field, string alias = null)
    {
        GaxPreconditions.CheckNotNullOrEmpty(field, nameof(field));
        return new Aggregation { Avg = new Avg() { Field = FieldPath.FromDotSeparatedString(field).ToFieldReference() }, Alias = alias ?? AvgAliasPrefix + "_" + field };
    }

    /// <summary>
    /// 
    /// Calculates the average of the values of the requested field.
    /// It uses the server side aggregation <see cref="Aggregation.Avg"/> to get the average value.
    ///
    /// * Only numeric values will be aggregated. All non-numeric values including `NULL` are skipped.
    /// * If the aggregated values contain `NaN`, returns `NaN`.
    /// * If the aggregated value set is empty, returns `NULL`.
    /// * Always returns the result as a double.
    /// 
    /// </summary>
    /// <param name="fieldPath">The fieldpath for the mandatory field on which the Average aggregation is performed.</param>
    /// <param name="alias" cref="Aggregation.Alias">
    ///
    /// An optional parameter to store the alias.
    /// If none is provided by user , one is generated based on the field names formatted as
    /// "operatorName"_"fieldName". Example: Avg_age
    /// 
    /// </param>
    /// <returns>Returns the average of the values of the requested field.</returns>
    public static Aggregation Average(FieldPath fieldPath, string alias = null)
    {
        GaxPreconditions.CheckNotNull(fieldPath, nameof(fieldPath));
        return new Aggregation { Avg = new Avg() { Field = fieldPath.ToFieldReference() }, Alias = alias ?? AvgAliasPrefix + "_" + fieldPath.EncodedPath };
    }
}
