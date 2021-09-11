using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.Groups
{
    class SearchInArrays
    {
        public bool GetBoolDictonary<T, C>(in Dictionary<T, C> dictonary, T key)
        {
            foreach (var value in dictonary)
            {
                if (value.Key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        public C GetValueDictonary<T, C>(in Dictionary<T, C> dictonary, T key)
        {
            foreach (var value in dictonary)
            {
                if (value.Key.Equals(key))
                {
                    return value.Value;
                }
            }
            return default;
        }
    }
}
