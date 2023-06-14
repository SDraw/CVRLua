using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class ComponentDefs
    {
        const string c_destroyed = "Component is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_instanceProperties.Add(("gameObject", (GetGameObject, null)));
            ms_instanceProperties.Add(("tag", (GetTag, SetTag)));
            ms_instanceProperties.Add(("transform", (GetTransform, null)));

            ms_instanceMethods.Add((nameof(CompareTag), CompareTag));
            ms_instanceMethods.Add((nameof(GetComponent), GetComponent));

            ObjectDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
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

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Component), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsComponent), IsComponent);
        }

        // Static methods
        static int IsComponent(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Component l_component = null;
            l_argReader.ReadNextObject(ref l_component);
            l_argReader.PushBoolean(l_component != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetGameObject(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Component l_component = null;
            l_argReader.ReadObject(ref l_component);
            if(!l_argReader.HasErrors())
            {
                if(l_component != null)
                    l_argReader.PushObject(l_component.gameObject);
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

        static int GetTag(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Component l_component = null;
            l_argReader.ReadObject(ref l_component);
            if(!l_argReader.HasErrors())
            {
                if(l_component != null)
                    l_argReader.PushString(l_component.tag);
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
        static int SetTag(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Component l_component = null;
            string l_name = "";
            l_argReader.ReadObject(ref l_component);
            l_argReader.ReadString(ref l_name);
            if(!l_argReader.HasErrors())
            {
                if(l_component != null)
                    l_component.name = l_name;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTransform(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Component l_component = null;
            l_argReader.ReadObject(ref l_component);
            if(!l_argReader.HasErrors())
            {
                if(l_component != null)
                    l_argReader.PushObject(l_component.transform);
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

        // Instance methods
        static int CompareTag(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Component l_component = null;
            string l_tag = "";
            l_argReader.ReadObject(ref l_component);
            l_argReader.ReadString(ref l_tag);
            if(!l_argReader.HasErrors())
            {
                if(l_component != null)
                    l_argReader.PushBoolean(l_component.CompareTag(l_tag));
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

        static int GetComponent(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Component l_comp = null;
            string l_typeName = "";
            l_argReader.ReadObject(ref l_comp);
            l_argReader.ReadString(ref l_typeName);
            if(!l_argReader.HasErrors())
            {
                if(l_comp != null)
                {
                    var l_component = l_comp.GetComponent(l_typeName);
                    if(l_component != null)
                        l_argReader.PushObject(l_component);
                    else
                        l_argReader.PushBoolean(false);
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
