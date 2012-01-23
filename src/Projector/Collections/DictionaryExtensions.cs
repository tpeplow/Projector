using System.Collections.Generic;

namespace Projector.Collections
{
    public static class DictionaryExtensions
    {
         public static TValue FindItem<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue @default = default(TValue))
         {
             TValue value;
             return !dictionary.TryGetValue(key, out value) ? @default : value;
         }
    }
}