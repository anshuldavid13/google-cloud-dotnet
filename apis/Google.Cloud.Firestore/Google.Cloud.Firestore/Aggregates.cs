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

using Google.Cloud.Firestore.V1;
using System.Collections.Generic;
using System.Reflection;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types.Aggregation.Types;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types.Aggregation.OperatorOneofCase;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;
using System;
using Google.Api.Gax;
using Google.Cloud.Firestore.Converters;

namespace Google.Cloud.Firestore;

/// <summary>
/// Factory for different <see cref="Aggregation"/>.
/// </summary>
internal static class Aggregates
{
    /// <summary>
    /// The "alias" to specify in the <see cref="RunAggregationQueryRequest"/> proto when running a count query.
    /// The actual value is not meaningful, but will be used to get the count out of the <see cref="RunAggregationQueryResponse"/>.
    /// </summary>
    internal const string CountAlias = "Count";

    internal const string AvgAlias = "Avg";

    internal const string SumAlias = "Sum";

    internal static Aggregation CreateCountAggregate()
    {
        return new Aggregation { Count = new Count(), Alias = CountAlias };
    }

    internal static Aggregation CreateSumAggregate(string field)
    {
        GaxPreconditions.CheckNotNullOrEmpty(field, nameof(field));
        return new Aggregation { Sum = new Sum() { Field = FieldPath.FromDotSeparatedString(field).ToFieldReference() }, Alias = SumAlias + "_" + field };
    }

    internal static Aggregation CreateSumAggregate(FieldPath fieldPath)
    {
        GaxPreconditions.CheckNotNull(fieldPath, nameof(fieldPath));
        return new Aggregation { Sum = new Sum() { Field = fieldPath.ToFieldReference() }, Alias = SumAlias + "_" + fieldPath.EncodedPath };
    }

    internal static Aggregation CreateAvgAggregate(string field)
    {
        GaxPreconditions.CheckNotNullOrEmpty(field, nameof(field));
        return new Aggregation { Avg = new Avg() { Field = FieldPath.FromDotSeparatedString(field).ToFieldReference() }, Alias = AvgAlias + "_" + field };
    }

    internal static Aggregation CreateAvgAggregate(FieldPath fieldPath)
    {
        GaxPreconditions.CheckNotNull(fieldPath, nameof(fieldPath));
        return new Aggregation { Avg = new Avg() { Field = fieldPath.ToFieldReference() }, Alias = AvgAlias + "_" + fieldPath.EncodedPath };
    }
}
