using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class CollisionDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(IsCollision), IsCollision);

            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add("collider", (GetCollider, null));
            ms_instanceProperties.Add("contactCount", (GetContactCount, null));
            ms_instanceProperties.Add("impulse", (GetImpulse, null));
            ms_instanceProperties.Add("relativeVelocity", (GetRelativeVelocity, null));

            ms_instanceMethods.Add(nameof(GetContact), GetContact);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Collision), null, ms_metaMethods, StaticGet, null, InstanceGet, null);
        }

        // Static methods
        static int IsCollision(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_colA = null;
            l_argReader.ReadNextObject(ref l_colA);
            l_argReader.PushBoolean(l_colA != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Equal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_colA = null;
            Collision l_colB = null;
            l_argReader.ReadObject(ref l_colA);
            l_argReader.ReadObject(ref l_colB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_colA == l_colB);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int ToString(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_col.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static void GetCollider(object p_obj, LuaArgReader p_reader)
        {
            Collider l_col = (p_obj as Collision).collider;
            if(l_col != null)
                p_reader.PushObject(l_col);
            else
                p_reader.PushBoolean(false);
        }

        static void GetContactCount(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushInteger((p_obj as Collision).contactCount);
        }

        static void GetImpulse(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as Collision).impulse));
        }

        static void GetRelativeVelocity(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as Collision).relativeVelocity));
        }

        // Instance methods
        static int GetContact(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_col = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_index < l_col.contactCount)
                    l_argReader.PushObject(new Wrappers.ContactPoint(l_col.GetContact(l_index)));
                else
                    l_argReader.PushBoolean(false);
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
            Collision l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(ms_instanceMethods.TryGetValue(l_key, out var l_func))
                    l_argReader.PushFunction(l_func); // Lua handles it by itself
                else if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item1 != null))
                    l_pair.Item1.Invoke(l_obj, l_argReader);
                else
                    l_argReader.PushNil();
            }
            else
                l_argReader.PushNil();

            return l_argReader.GetReturnValue();
        }
    }
}
