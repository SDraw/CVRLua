using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class BoxColliderDefs
    {
        const string c_destroyed = "BoxCollider is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(IsBoxCollider), IsBoxCollider);

            ms_instanceProperties.Add("center", (GetCenter, SetCenter));
            ms_instanceProperties.Add("size", (GetSize, SetSize));

            ColliderDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(BoxCollider), null, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
        }

        // Static methods
        static int IsBoxCollider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            BoxCollider l_col = null;
            l_argReader.ReadNextObject(ref l_col);
            l_argReader.PushBoolean(l_col != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static void GetCenter(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as BoxCollider).center));
        }
        static void SetCenter(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = null;
            p_reader.ReadObject(ref l_vec);
            if(!p_reader.HasErrors())
            {
                (p_obj as BoxCollider).center = l_vec.m_vec;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetSize(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as BoxCollider).size));
        }
        static void SetSize(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = null;
            p_reader.ReadObject(ref l_vec);
            if(!p_reader.HasErrors())
            {
                (p_obj as BoxCollider).size = l_vec.m_vec;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
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
            BoxCollider l_obj = null;
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
            BoxCollider l_obj = null;
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
