﻿using System.Collections.Generic;
using System.Linq;

namespace LiteDB
{
    /// <summary>
    ///     Contains query do not work with index, only full scan
    /// </summary>
    internal class QueryContains : Query
    {
        private readonly BsonValue _value;

        public QueryContains(string field, BsonValue value)
            : base(field)
        {
            _value = value;
        }

        internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        {
            var v = _value.Normalize(index.Options);

            return indexer
                .FindAll(index, Ascending)
                .Where(x => x.Key.IsString && x.Key.AsString.Contains(v));
        }
    }
}