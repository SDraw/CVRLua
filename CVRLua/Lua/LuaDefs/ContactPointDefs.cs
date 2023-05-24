using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class ContactPointDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(IsContactPoint), IsContactPoint);

            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add("normal", (GetNormal, null));
            ms_instanceProperties.Add("otherColliderName", (GetOtherColliderName, null));
            ms_instanceProperties.Add("otherColliderID", (GetOtherColliderID, null));
            ms_instanceProperties.Add("point", (GetPoint, null));
            ms_instanceProperties.Add("separation", (GetSeparation, null));
            ms_instanceProperties.Add("thisColliderName", (GetThisColliderName, null));
            ms_instanceProperties.Add("thisColliderID", (GetThisColliderID, null));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.ContactPoint), null, ms_metaMethods, StaticGet, null, InstanceGet, null);
        }

        // Static methods
        static int IsContactPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.ContactPoint l_point = null;
            l_argReader.ReadNextObject(ref l_point);
            l_argReader.PushBoolean(l_point != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int ToString(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.ContactPoint l_point = null;
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_point.m_point.ToString());
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static void GetNormal(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as Wrappers.ContactPoint).m_point.normal));
        }

        static void GetOtherColliderName(object p_obj, LuaArgReader p_reader)
        {
            UnityEngine.Collider l_col = (p_obj as Wrappers.ContactPoint).m_point.otherCollider;
            if(l_col != null)
                p_reader.PushString(l_col.name);
            else
                p_reader.PushBoolean(false);
        }

        static void GetOtherColliderID(object p_obj, LuaArgReader p_reader)
        {
            UnityEngine.Collider l_col = (p_obj as Wrappers.ContactPoint).m_point.otherCollider;
            if(l_col != null)
                p_reader.PushInteger(l_col.GetInstanceID());
            else
                p_reader.PushBoolean(false);
        }

        static void GetPoint(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as Wrappers.ContactPoint).m_point.point));
        }

        static void GetSeparation(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.ContactPoint).m_point.separation);
        }

        static void GetThisColliderName(object p_obj, LuaArgReader p_reader)
        {
            UnityEngine.Collider l_col = (p_obj as Wrappers.ContactPoint).m_point.thisCollider;
            if(l_col != null)
                p_reader.PushString(l_col.name);
            else
                p_reader.PushBoolean(false);
        }

        static void GetThisColliderID(object p_obj, LuaArgReader p_reader)
        {
            UnityEngine.Collider l_col = (p_obj as Wrappers.ContactPoint).m_point.thisCollider;
            if(l_col != null)
                p_reader.PushInteger(l_col.GetInstanceID());
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
            Wrappers.ContactPoint l_obj = null;
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
