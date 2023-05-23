using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class ComponentDefs
    {
        const string c_destroyed = "Component is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(IsComponent), IsComponent);

            ms_instanceProperties.Add("gameObject", (GetGameObject, null));
            ms_instanceProperties.Add("tag", (GetTag, SetTag));
            ms_instanceProperties.Add("transform", (GetTransform, null));

            ms_instanceMethods.Add(nameof(CompareTag), CompareTag);
            ms_instanceMethods.Add(nameof(GetComponent), GetComponent);

            ObjectDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void InheritTo(
            List<(string, LuaInterop.lua_CFunction)> p_metaMethods,
            Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> p_staticProperties,
            Dictionary<string, LuaInterop.lua_CFunction> p_staticMethods,
            Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> p_instanceProperties,
            Dictionary<string, LuaInterop.lua_CFunction> p_instanceMethods
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
            p_vm.RegisterClass(typeof(Component), null, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
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
        static void GetGameObject(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject((p_obj as Component).gameObject);
        }

        static void GetTag(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushString((p_obj as Component).tag);
        }
        static void SetTag(object p_obj, LuaArgReader p_reader)
        {
            string l_tag = "";
            p_reader.ReadString(ref l_tag);
            if(!p_reader.HasErrors())
            {
                (p_obj as Component).tag = l_tag;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetTransform(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject((p_obj as Component).transform);
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
                        l_argReader.PushObject(l_component, l_typeName);
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

        // Instance getter
        static int InstanceGet(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Component l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    if(ms_instanceMethods.TryGetValue(l_key, out var l_func))
                        l_argReader.PushFunction(l_func); // Lua handles it by itself
                    else if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item1 != null))
                        l_pair.Item1.Invoke(l_obj, l_argReader);
                    else
                        l_argReader.PushNil();
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushNil();
                }
            }
            else
                l_argReader.PushNil();

            return l_argReader.GetReturnValue();
        }

        // Instance setter
        static int InstanceSet(IntPtr p_state)
        {
            // Our value is on stack top
            var l_argReader = new LuaArgReader(p_state);
            Component l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item2 != null))
                        l_pair.Item2.Invoke(l_obj, l_argReader);
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
