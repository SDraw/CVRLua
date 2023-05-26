using ABI.CCK.Components;
using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class CVRPointerDefs
    {
        const string c_destroyed = "CVRPointer is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsCVRPointer), IsCVRPointer));

            ms_instanceProperties.Add(("isInternalPointer", (GetIsInternalPointer, null)));
            ms_instanceProperties.Add(("isLocalPointer", (GetIsLocalPointer, null)));
            ms_instanceProperties.Add(("limitToFilteredTriggers", (GetLimitToFilteredTriggers, null)));
            ms_instanceProperties.Add(("type", (GetPointerType, null)));

            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        static internal void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CVRPointer), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsCVRPointer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPointer l_pointer = null;
            l_argReader.ReadNextObject(ref l_pointer);
            l_argReader.PushBoolean(l_pointer != null);
            return l_argReader.GetReturnValue();
        }

        // Instance methods
        static int GetIsInternalPointer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPointer l_pointer = null;
            l_argReader.ReadObject(ref l_pointer);
            if(!l_argReader.HasErrors())
            {
                if(l_pointer != null)
                    l_argReader.PushBoolean(l_pointer.isInternalPointer);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetIsLocalPointer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPointer l_pointer = null;
            l_argReader.ReadObject(ref l_pointer);
            if(!l_argReader.HasErrors())
            {
                if(l_pointer != null)
                    l_argReader.PushBoolean(l_pointer.isLocalPointer);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLimitToFilteredTriggers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPointer l_pointer = null;
            l_argReader.ReadObject(ref l_pointer);
            if(!l_argReader.HasErrors())
            {
                if(l_pointer != null)
                    l_argReader.PushBoolean(l_pointer.limitToFilteredTriggers);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetPointerType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPointer l_pointer = null;
            l_argReader.ReadObject(ref l_pointer);
            if(!l_argReader.HasErrors())
            {
                if(l_pointer != null)
                    l_argReader.PushString(l_pointer.type);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
    }
}
