using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace CVRLua.Lua.LuaDefs
{
    static class UiToggleDefs
    {
        const string c_destroyed = "Toggle is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsToggle), IsToggle));

            ms_instanceProperties.Add(("toggleTransition", (GetToggleTransition, SetToggleTransition)));
            ms_instanceProperties.Add(("isOn", (GetIsOn, SetIsOn)));

            ms_instanceMethods.Add((nameof(SetIsOnWithoutNotify), SetIsOnWithoutNotify));

            UiSelectableDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Toggle), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsToggle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Toggle l_toggle = null;
            l_argReader.ReadNextObject(ref l_toggle);
            l_argReader.PushBoolean(l_toggle != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetToggleTransition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Toggle l_toggle = null;
            l_argReader.ReadObject(ref l_toggle);
            if(!l_argReader.HasErrors())
            {
                if(l_toggle != null)
                    l_argReader.PushString(l_toggle.toggleTransition.ToString());
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
        static int SetToggleTransition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Toggle l_toggle = null;
            Toggle.ToggleTransition l_value = Toggle.ToggleTransition.None;
            l_argReader.ReadObject(ref l_toggle);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_toggle != null)
                    l_toggle.toggleTransition = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIsOn(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Toggle l_toggle = null;
            l_argReader.ReadObject(ref l_toggle);
            if(!l_argReader.HasErrors())
            {
                if(l_toggle != null)
                    l_argReader.PushBoolean(l_toggle.isOn);
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
        static int SetIsOn(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Toggle l_toggle = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_toggle);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_toggle != null)
                    l_toggle.isOn = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int SetIsOnWithoutNotify(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Toggle l_toggle = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_toggle);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_toggle != null)
                {
                    l_toggle.SetIsOnWithoutNotify(l_value);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
