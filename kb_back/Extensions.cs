using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.TypeConverter;

namespace kb_back
{ 
    public static class Extensions
    {
        internal class ValueCache
        {
            private static readonly Dictionary<Type, object> maxValues = new Dictionary<Type, object>()
            {
                { typeof(byte), int.MaxValue },
                { typeof(int), int.MaxValue },
                { typeof(long), long.MaxValue },
                { typeof(float), float.MaxValue },
                { typeof(double), double.MaxValue },
                { typeof(decimal), decimal.MaxValue },
                { typeof(DateOnly), DateOnly.MaxValue },
            };

            private static readonly Dictionary<Type, object> minValues = new Dictionary<Type, object>()
            {
                { typeof(byte), int.MinValue },
                { typeof(int), int.MinValue },
                { typeof(long), long.MinValue },
                { typeof(float), float.MinValue },
                { typeof(double), double.MinValue },
                { typeof(decimal), decimal.MinValue },
                { typeof(DateOnly), DateOnly.MinValue },
            };

            public static string GetMaxValue(Type type)
            {
                return maxValues[type].ToString();
            }

            public static string GetMinValue(Type type)
            {
                return minValues[type].ToString();
            }
        }

        public static T ChangeType<T>(this string s)
        {
            if(typeof(T) == typeof(DateOnly))
            {
                return (T)Convert.ChangeType(DateOnly.Parse(s), typeof(T));
            }

            return (T)Convert.ChangeType(s, typeof(T));
        }

        public static (T, T) GetEnds<T>(string s)
        {
            if (s.Contains('-'))
            {
                int i = s.IndexOf('-');
                T a, b;

                try
                {
                    if (s[..i] == "") { a = ValueCache.GetMinValue(typeof(T)).ChangeType<T>(); }
                    else { a = s[..i].ChangeType<T>(); }

                    if (s[(i+1)..] == "") {  b = ValueCache.GetMaxValue(typeof(T)).ChangeType<T>(); }
                    else { b = s[(i + 1)..].ChangeType<T>(); }
                    
                    return (a, b);
                }
                catch (Exception ex)
                {
                    throw new Exception("received invalid search input");
                }
            }
            else
            {
                try
                {
                    return (s.ChangeType<T>(), s.ChangeType<T>());
                }
                catch
                {
                    throw new Exception("received invalid search input");
                }
            }
        }
    }
}
