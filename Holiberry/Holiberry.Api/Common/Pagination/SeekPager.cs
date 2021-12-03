using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holiberry.Api.Common.Pagination
{
    public class SeekPager<TKey> where TKey : struct
    {
        public TKey? LastId { get; set; }
        public int Size { get; set; } = 10;


        public bool HasNext { get; set; }

        public string First { get; set; }
        public string Self { get; set; }
        public string Next { get; set; }


        public SeekPager(TKey? lastId, int? size, HttpContext httpContext)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (size.HasValue && size.Value > 0)
            {
                Size = size.Value;
            }
            LastId = lastId;

            var uri = new Uri(httpContext.Request.GetEncodedUrl());
            var baseUri = uri.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);

            if (baseUri.StartsWith("http://"))
            {
                baseUri = baseUri.Replace("http://", "https://");
            }

            var query = QueryHelpers.ParseQuery(uri.Query);
            var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();

            items.RemoveAll(x => x.Key.ToUpper() == nameof(LastId).ToUpper());
            var qb = new QueryBuilder(items);
            var fullUri = baseUri + qb.ToQueryString();


            Self = baseUri + uri.Query;

            First = fullUri;

            Next = null;
        }

        public static void UpdatePager<T>(SeekPager<TKey> pager, IEnumerable<T> items, Func<T, TKey> primaryKey)
        {
            if (pager is null)
            {
                throw new ArgumentNullException(nameof(pager));
            }
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            items.Select(primaryKey);

            pager.LastId = items.Select(primaryKey).LastOrDefault();

            pager.HasNext = items.Count() == pager.Size;

            pager.Next = pager.HasNext ? QueryHelpers.AddQueryString(pager.First, nameof(LastId), pager.LastId.ToString()) : null;
        }


    }


}
