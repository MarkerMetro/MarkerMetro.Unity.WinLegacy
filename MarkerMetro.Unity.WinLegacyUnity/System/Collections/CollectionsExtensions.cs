using System;
using System.Collections.Generic;

namespace MarkerMetro.Unity.WinLegacy.Collections
{
    /** 
     * Converter is not supported in Win8 or WP8.1, but supported in WP8.
     */
    public delegate TOutput Converter<in TInput, out TOutput>(TInput input);

    /**
    * Helpers for missing functions of classes belonging Collections namespace.
    */
    public static class CollectionsExtensions
    {
        /**
         * List<TOutput>.ConvertAll<TOutput>(Converter) is neither implement in WSA nor in WP8.
         */
        public static List<TOutput> ConvertAll<T, TOutput>(this List<T> list, Converter<T, TOutput> converter)
        {
            if (converter == null)
                throw new ArgumentNullException("converter is null.");

            List<TOutput> result = new List<TOutput>(list.Count);
            foreach (T t in list)
                result.Add(converter(t));
            return result;
        }
    }
}
