using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace CVRLua.Lua.LuaDefs
{
    static class UiSelectableDefs
    {
        const string c_destroyed = "Selectable is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsSelectable), IsSelectable));

            ms_instanceProperties.Add(("animator", (GetAnimator, null)));
            ms_instanceProperties.Add(("interactable", (GetInteractable, SetInteractable)));
            ms_instanceProperties.Add(("transition", (GetTransition, SetTransition)));
            //ms_instanceProperties.Add(("navigation", (?, ?));
            //ms_instanceProperties.Add(("animationTriggers", (?, ?));
            //ms_instanceProperties.Add(("colors", (?, ?));
            //ms_instanceProperties.Add(("spriteState", (?, ?));
            //ms_instanceProperties.Add(("targetGraphic", (?, ?));
            //ms_instanceProperties.Add(("image", (?, ?));
            //ms_instanceProperties.Add(("image", (?, ?));

            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Selectable), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
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
        static int IsSelectable(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Selectable l_select = null;
            l_argReader.ReadNextObject(ref l_select);
            l_argReader.PushBoolean(l_select != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAnimator(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Selectable l_select = null;
            l_argReader.ReadObject(ref l_select);
            if(!l_argReader.HasErrors())
            {
                if(l_select != null)
                {
                    if(l_select.animator != null)
                        l_argReader.PushObject(l_select.animator);
                    else
                        l_argReader.PushBoolean(false);
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
            return 1;
        }

        static int GetInteractable(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Selectable l_select = null;
            l_argReader.ReadObject(ref l_select);
            if(!l_argReader.HasErrors())
            {
                if(l_select != null)
                    l_argReader.PushBoolean(l_select.interactable);
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
        static int SetInteractable(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Selectable l_select = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_select);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_select != null)
                    l_select.interactable = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTransition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Selectable l_select = null;
            l_argReader.ReadObject(ref l_select);
            if(!l_argReader.HasErrors())
            {
                if(l_select != null)
                    l_argReader.PushString(l_select.transition.ToString());
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
        static int SetTransition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Selectable l_select = null;
            Selectable.Transition l_value = Selectable.Transition.None;
            l_argReader.ReadObject(ref l_select);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_select != null)
                    l_select.transition = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
    }
}
