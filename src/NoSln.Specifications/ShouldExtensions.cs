using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace NoSln.Specifications
{
    public static class ShouldExtensions
    {
         public static void ShouldBeEquivalentTo<T>(this IEnumerable<T> first, IEnumerable<T> second)
         {
             var firstArray = first.ToArray();
             var secondArray = second.ToArray();
             if (firstArray.Length != secondArray.Length)
             {
                 throw new SpecificationException(string.Format("Expected equivalent lists but counts are no the same.  Expected count of {0} but got {1}", firstArray.Length, secondArray.Length));
             }

             var i = 0;
             foreach (var item in firstArray)
             {
                 if (!EqualityComparer<T>.Default.Equals(item, secondArray[i]))
                 {
                     throw new SpecificationException(string.Format("Expected element at {0} to equal {1} but was {2}", i, item, secondArray[i]));
                 }
                 i++;
             }
         }
    }
}