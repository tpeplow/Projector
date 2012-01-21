﻿using System;
using System.Collections.Generic;

namespace Projector.Collections
{
    public static class EnumerableExtensions
    {
         public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
         {
             foreach (var item in enumerable)
             {
                 action(item);
             }
         }
    }
}