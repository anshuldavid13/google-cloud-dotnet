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
using Google.Type;
using System;
using System.Collections.Generic;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types;

namespace Google.Cloud.Firestore;

/// <summary>
/// An immutable snapshot of aggregate query results.
/// </summary>
public sealed class AggregateQuerySnapshot : IEquatable<AggregateQuerySnapshot>
{
    /// <summary>
    /// The query producing this snapshot.
    /// </summary>
    public AggregateQuery Query { get; }

    /// <summary>
    /// The time at which the snapshot was read.
    /// </summary>
    public Timestamp ReadTime { get; }

    /// <summary>
    /// Number of documents that matches the query. May be null when count aggregation is not applied on the Query.
    /// Note that when the query contains a limit, the count is restricted by that limit.
    /// </summary>
    public long? Count { get; }

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, Value> Data { get; }

    internal AggregateQuerySnapshot(AggregateQuery query, Timestamp readTime, long? count, Dictionary<string, Value> data)
    {
        Query = query;
        ReadTime = readTime;
        Count = count;
        //TODO USE DIRECT GETTER
        //Count = getCount();
        Data = data;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public long getCount()
    {
        Aggregation countField = Aggregates.CreateCountAggregate();
        Object value = countField.Count;
        if (value == null)
        {
            // TODO WITH NAME OF VARIABLE
            throw new ArgumentNullException("count");
        }
        /*
         * TODO FIX IT!!!!
        else if (typeof(value) != long) {
            throw new ArgumentException("Count should be long");
        }
        */
        return (long) value;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="aggregateField"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public object getData(Aggregation aggregateField)
    {
        if (!Data.ContainsKey(aggregateField.Alias))
        {
            // TODO Detail
            throw new ArgumentException();
        }
        Value value = Data[aggregateField.Alias];// (aggregateField.Alias);//.get(aggregateField.getAlias());
        return value;
    }
        /*
       if (value is null)
       {
           return null;
       }

        * 
        * TODO TYPES
       else if (value.DoubleValue)
       {
           return value.getDoubleValue();
       }
       else if (value.hasIntegerValue())
       {
           return value.getIntegerValue();
       }
       else
       {
           //TODO
           throw new ArgumentException();
           //throw new IllegalStateException("Found aggregation result that is not an integer nor double");
       }*/
   



    /// <summary> 
    /// Determines whether <paramref name="other"/> is equal to this instance.
    /// </summary>
    /// <returns><c>true</c> if the specified object is equal to this instance; otherwise <c>false</c>.</returns>
    public bool Equals(AggregateQuerySnapshot other) =>
        other != null && Query.Equals(other.Query) && ReadTime.Equals(other.ReadTime) && Count == other.Count;

    /// <inheritdoc />
    public override bool Equals(object obj) => Equals(obj as AggregateQuerySnapshot);

    /// <inheritdoc />
    public override int GetHashCode() =>
        GaxEqualityHelpers.CombineHashCodes(Query.GetHashCode(), ReadTime.GetHashCode(), Count?.GetHashCode() ?? 0);
}
