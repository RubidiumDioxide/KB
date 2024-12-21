using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb_back
{ 
    public static class Extensions
    {
        public static T ChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static (T, T) GetEnds<T>(string s)
        {
            if (s.Contains('-'))
            {
                int i = s.IndexOf('-');

                try
                {
                    T a = s[..i].ChangeType<T>();
                    //string B = s.Substring(i + 1, s.Length);
                    T b = s[(i + 1)..].ChangeType<T>();
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
