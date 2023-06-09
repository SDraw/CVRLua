using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace CVRLua.Lua.LuaDefs
{
    static class UiButtonDefs
    {
        const string c_destroyed = "Button is destroyed"; // _(big thonk)_

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsButton), IsButton));

            UiSelectableDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Button), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsButton(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Button l_button = null;

            l_argReader.ReadNextObject(ref l_button);
            l_argReader.PushBoolean(l_button != null);
            return l_argReader.GetReturnValue();
        }
    }
}
