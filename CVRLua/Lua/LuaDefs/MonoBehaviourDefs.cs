using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class MonoBehaviourDefs
    {
        const string c_destroyed = "MonoBehaviour is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_instanceProperties.Add(("useGUILayout", (GetUseGUILayout, SetUseGUILayout)));

            BehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(MonoBehaviour), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsMonoBehaviour), IsMonoBehaviour);
        }

        internal static void InheritTo(
            List<(string, LuaInterop.lua_CFunction)> p_metaMethods,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_staticProperties,
            List<(string, LuaInterop.lua_CFunction)> p_staticMethods,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_instanceProperties,
            List<(string, LuaInterop.lua_CFunction)> p_instanceMethods
        )
        {
            if(p_metaMethods != null)
                ms_metaMethods.MergeInto(p_metaMethods);

            if(p_staticProperties != null)
                ms_staticProperties.MergeInto(p_staticProperties);

            if(p_staticMethods != null)
                ms_staticMethods.MergeInto(p_staticMethods);

            if(p_instanceProperties != null)
                ms_instanceProperties.MergeInto(p_instanceProperties);

            if(p_instanceMethods != null)
                ms_instanceMethods.MergeInto(p_instanceMethods);
        }

        // Static methods
        static int IsMonoBehaviour(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            MonoBehaviour l_behaviour = null;
            l_argReader.ReadNextObject(ref l_behaviour);
            l_argReader.PushBoolean(l_behaviour != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetUseGUILayout(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            MonoBehaviour l_mono = null;
            l_argReader.ReadObject(ref l_mono);
            if(!l_argReader.HasErrors())
            {
                if(l_mono != null)
                    l_argReader.PushBoolean(l_mono.useGUILayout);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetUseGUILayout(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            MonoBehaviour l_mono = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_mono);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_mono != null)
                    l_mono.useGUILayout = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
    }
}
