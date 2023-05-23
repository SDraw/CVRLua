using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class ColliderDefs
    {
        const string c_destroyed = "Collider is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(IsCollider), IsCollider);

            //ms_instanceProperties.Add("attachedRigidbody", (?, ?)); // Requires RigidBody defs
            //ms_instanceProperties.Add("bounds", (?, ?)); // Requires Bound defs
            ms_instanceProperties.Add("contactOffset", (GetContactOffset, SetContactOffset));
            ms_instanceProperties.Add("enabled", (GetEnabled, SetEnabled));
            ms_instanceProperties.Add("isTrigger", (GetTrigger, SetTrigger));
            //ms_instanceProperties.Add("material", (?, ?)); // Requires Material defs
            //ms_instanceProperties.Add("sharedMaterial", (?, ?)); // Requires Material defs

            ms_instanceMethods.Add(nameof(ClosestPoint), ClosestPoint);
            ms_instanceMethods.Add(nameof(ClosestPointOnBounds), ClosestPointOnBounds);
            //ms_instanceMethods.Add(nameof(Raycast), Raycast); // Requires some defs

            ComponentDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Collider), null, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
        }

        // Static methods
        static int IsCollider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            l_argReader.ReadNextObject(ref l_col);
            l_argReader.PushBoolean(l_col != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static void GetContactOffset(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Collider).contactOffset);
        }
        static void SetContactOffset(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as Collider).contactOffset = l_val;
        }

        static void GetEnabled(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Collider).enabled);
        }
        static void SetEnabled(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as Collider).enabled = l_state;
        }

        static void GetTrigger(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Collider).isTrigger);
        }
        static void SetTrigger(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as Collider).isTrigger = l_state;
        }

        // Instance methods
        static int ClosestPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_col.ClosestPoint(l_vec.m_vec)));
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

        static int ClosestPointOnBounds(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_col.ClosestPointOnBounds(l_vec.m_vec)));
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
            Collider l_obj = null;
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
            Collider l_obj = null;
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
