﻿using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class DateTimeDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(Now), Now));
            ms_staticMethods.Add((nameof(UtcNow), UtcNow));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(DateTime), null, null, ms_staticMethods, null, null, null);
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
    }
}
