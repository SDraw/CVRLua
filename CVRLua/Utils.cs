using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CVRLua
{
    static class Utils
    {
        public static int GetInt(this IntPtr p_source)
        {
            int l_result = 0;
            try
            {
                l_result = Marshal.ReadInt32(p_source);
            }
            catch(AccessViolationException) { }
            return l_result;
        }

        public static void SetInt(this IntPtr p_source, int p_value)
        {
            try
            {
                Marshal.WriteInt32(p_source, p_value);
            }
            catch(AccessViolationException) { }
        }

        public static void MergeInto<T, V>(this Dictionary<T, V> p_source, Dictionary<T, V> p_target)
        {
            p_source.ToList().ForEach(p => p_target.Add(p.Key, p.Value));
        }

        public static void MergeInto<T>(this List<T> p_source, List<T> p_target)
        {
            p_target.ForEach(p => p_target.Add(p));
        }
    }
}
