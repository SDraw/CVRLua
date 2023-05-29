using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class LuaScriptDefs
    {
        const string c_destroyed = "LuaScript is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsLuaScript), IsLuaScript));

            ms_instanceMethods.Add((nameof(SendMessage), SendMessage));

            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(LuaScript), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsLuaScript(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            LuaScript l_script = null;
            l_argReader.ReadNextObject(ref l_script);
            l_argReader.PushBoolean(l_script != null);
            return l_argReader.GetReturnValue();
        }

        // Instance methods
        static int SendMessage(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            LuaScript l_script = null;
            l_argReader.ReadObject(ref l_script);
            if(!l_argReader.HasErrors())
            {
                if(l_script != null)
                {
                    List<object> l_args = new List<object>();
                    List<object> l_results = new List<object>();
                    l_argReader.ReadArguments(l_args); // Never errors
                    l_script.SendScriptMessage(l_args, l_results);
                    l_argReader.PushTable(l_results);
                }
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
