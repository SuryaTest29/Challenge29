using System;
using System.Collections.Generic;

using Pokemon.Models;

namespace Pokemon.Search.Test
{
    public class TranslateResultComparer : IEqualityComparer<IShakespeareResult>
    {
        public bool Equals(IShakespeareResult left, IShakespeareResult right)
        {
            return left != null &&
                   right != null &&
                   left.Description == right.Description;
        }

        public int GetHashCode(IShakespeareResult shakespeareResult)
        {
            unchecked
            {
                var hash = 17;
                hash *= GetHashCode(shakespeareResult.Name);
                hash *= GetHashCode(shakespeareResult.Description);
                return hash;
            }
        }

        private static int GetHashCode(object obj) => 23 + obj.GetHashCode();
    }
}