using System;
using System.Collections.Generic;

namespace DotMaster.Core.Utils
{
    public static class Extensions
    {
        public static Dictionary<TKey, TValue>
            ToDictionaryIgnoringNullValue<TSource, TKey, TValue>(this IEnumerable<TSource> sources,
                                                                 Func<TSource, TKey> keySelector,
                                                                 Func<TSource, TValue> valueSelector)
            where TValue : class
        {
            if (sources == null)
            {
                throw new ArgumentNullException("sources");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            if (valueSelector == null)
            {
                throw new ArgumentNullException("valueSelector");
            }

            var result = new Dictionary<TKey, TValue>();
            foreach (var source in sources)
            {
                var value = valueSelector(source);
                if (value != null)
                {
                    result.Add(keySelector(source), value);
                }
            }
            return result;
        }
    }
}
