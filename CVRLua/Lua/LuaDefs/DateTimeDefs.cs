using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class DateTimeDefs
    {
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(Now), Now);
            ms_staticMethods.Add(nameof(UtcNow), UtcNow);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(DateTime), null, null, StaticGet, null, null, null);
        }

        // Static methods
        static int Now(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            DateTime l_dt = DateTime.Now;
            l_argReader.PushInteger(l_dt.Hour);
            l_argReader.PushInteger(l_dt.Minute);
            l_argReader.PushInteger(l_dt.Second);
            l_argReader.PushInteger(l_dt.Millisecond);
            l_argReader.PushInteger(l_dt.Day);
            l_argReader.PushInteger(l_dt.Month);
            l_argReader.PushInteger(l_dt.Year);
            return l_argReader.GetReturnValue();
        }

        static int UtcNow(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            DateTime l_dt = DateTime.UtcNow;
            l_argReader.PushInteger(l_dt.Hour);
            l_argReader.PushInteger(l_dt.Minute);
            l_argReader.PushInteger(l_dt.Second);
            l_argReader.PushInteger(l_dt.Millisecond);
            l_argReader.PushInteger(l_dt.Day);
            l_argReader.PushInteger(l_dt.Month);
            l_argReader.PushInteger(l_dt.Year);
            return l_argReader.GetReturnValue();
        }

        // Static getter
        static int StaticGet(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            string l_key = "";
            l_argReader.Skip(); // Metatable
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(ms_staticMethods.TryGetValue(l_key, out var l_func))
                    l_argReader.PushFunction(l_func);
                else if(ms_staticProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item1 != null))
                    l_pair.Item1.Invoke(l_argReader);
                else
                    l_argReader.PushNil();
            }
            else
                l_argReader.PushNil();

            return l_argReader.GetReturnValue();
        }
    }
}
