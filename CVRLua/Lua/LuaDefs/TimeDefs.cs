using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class TimeDefs
    {
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticProperties.Add("deltaTime", (GetDeltaTime, null));
            ms_staticProperties.Add("fixedDeltaTime", (GetFixedDeltaTime, null));
            ms_staticProperties.Add("fixedTime", (GetFixedTime, null));
            ms_staticProperties.Add("fixedUnscaledDeltaTime", (GetFixedUnscaledDeltaTime, null));
            ms_staticProperties.Add("fixedUnscaledTime", (GetFixedUnscaledTime, null));
            ms_staticProperties.Add("frameCount", (GetFrameCount, null));
            ms_staticProperties.Add("inFixedTimeStep", (GetInFixedTimeStep, null));
            ms_staticProperties.Add("maximumDeltaTime", (GetMaximumDeltaTime, null));
            ms_staticProperties.Add("maximumParticleDeltaTime", (GetMaximumParticleDeltaTime, null));
            ms_staticProperties.Add("realtimeSinceStartup", (GetRealtimeSinceStartup, null));
            ms_staticProperties.Add("smoothDeltaTime", (GetSmoothDeltaTime, null));
            ms_staticProperties.Add("time", (GetTime, null));
            ms_staticProperties.Add("timeSinceLevelLoad", (GetTimeSinceLevelLoad, null));
            ms_staticProperties.Add("unscaledDeltaTime", (GetUnscaledDeltaTime, null));
            ms_staticProperties.Add("unscaledTime", (GetUnscaledTime, null));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Time), null, null, StaticGet, null, null, null);
        }

        // static properties
        static void GetDeltaTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.deltaTime);
        static void GetFixedDeltaTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.fixedDeltaTime);
        static void GetFixedTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.fixedTime);
        static void GetFixedUnscaledDeltaTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.fixedUnscaledDeltaTime);
        static void GetFixedUnscaledTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.fixedUnscaledTime);
        static void GetFrameCount(LuaArgReader p_reader) => p_reader.PushInteger(Time.frameCount);
        static void GetInFixedTimeStep(LuaArgReader p_reader) => p_reader.PushBoolean(Time.inFixedTimeStep);
        static void GetMaximumDeltaTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.maximumDeltaTime);
        static void GetMaximumParticleDeltaTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.maximumParticleDeltaTime);
        static void GetRealtimeSinceStartup(LuaArgReader p_reader) => p_reader.PushNumber(Time.realtimeSinceStartup);
        static void GetSmoothDeltaTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.smoothDeltaTime);
        static void GetTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.time);
        static void GetTimeSinceLevelLoad(LuaArgReader p_reader) => p_reader.PushNumber(Time.timeSinceLevelLoad);
        static void GetUnscaledDeltaTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.unscaledDeltaTime);
        static void GetUnscaledTime(LuaArgReader p_reader) => p_reader.PushNumber(Time.unscaledTime);

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
