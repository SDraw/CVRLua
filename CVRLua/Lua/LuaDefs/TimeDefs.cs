using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class TimeDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticProperties.Add(("deltaTime", (GetDeltaTime, null)));
            ms_staticProperties.Add(("fixedDeltaTime", (GetFixedDeltaTime, null)));
            ms_staticProperties.Add(("fixedTime", (GetFixedTime, null)));
            ms_staticProperties.Add(("fixedUnscaledDeltaTime", (GetFixedUnscaledDeltaTime, null)));
            ms_staticProperties.Add(("fixedUnscaledTime", (GetFixedUnscaledTime, null)));
            ms_staticProperties.Add(("frameCount", (GetFrameCount, null)));
            ms_staticProperties.Add(("inFixedTimeStep", (GetInFixedTimeStep, null)));
            ms_staticProperties.Add(("maximumDeltaTime", (GetMaximumDeltaTime, null)));
            ms_staticProperties.Add(("maximumParticleDeltaTime", (GetMaximumParticleDeltaTime, null)));
            ms_staticProperties.Add(("realtimeSinceStartup", (GetRealtimeSinceStartup, null)));
            ms_staticProperties.Add(("smoothDeltaTime", (GetSmoothDeltaTime, null)));
            ms_staticProperties.Add(("time", (GetTime, null)));
            ms_staticProperties.Add(("timeSinceLevelLoad", (GetTimeSinceLevelLoad, null)));
            ms_staticProperties.Add(("unscaledDeltaTime", (GetUnscaledDeltaTime, null)));
            ms_staticProperties.Add(("unscaledTime", (GetUnscaledTime, null)));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Time), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // static properties
        static int GetDeltaTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.deltaTime);
            return 1;
        }

        static int GetFixedDeltaTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.fixedDeltaTime);
            return 1;
        }

        static int GetFixedTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.fixedTime);
            return 1;
        }

        static int GetFixedUnscaledDeltaTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.fixedUnscaledDeltaTime);
            return 1;
        }

        static int GetFixedUnscaledTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.fixedUnscaledTime);
            return 1;
        }

        static int GetFrameCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushInteger(Time.frameCount);
            return 1;
        }

        static int GetInFixedTimeStep(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushBoolean(Time.inFixedTimeStep);
            return 1;
        }

        static int GetMaximumDeltaTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.maximumDeltaTime);
            return 1;
        }

        static int GetMaximumParticleDeltaTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.maximumParticleDeltaTime);
            return 1;
        }

        static int GetRealtimeSinceStartup(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.realtimeSinceStartup);
            return 1;
        }

        static int GetSmoothDeltaTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.smoothDeltaTime);
            return 1;
        }

        static int GetTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.time);
            return 1;
        }

        static int GetTimeSinceLevelLoad(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.timeSinceLevelLoad);
            return 1;
        }

        static int GetUnscaledDeltaTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.unscaledDeltaTime);
            return 1;
        }

        static int GetUnscaledTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Time.unscaledTime);
            return 1;
        }
    }
}
