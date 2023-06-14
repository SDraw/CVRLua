using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class ContactPointDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();

        internal static void Init()
        {
            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("normal", (GetNormal, null)));
            ms_instanceProperties.Add(("otherCollider", (GetOtherCollider, null)));
            ms_instanceProperties.Add(("point", (GetPoint, null)));
            ms_instanceProperties.Add(("separation", (GetSeparation, null)));
            ms_instanceProperties.Add(("thisCollider", (GetThisCollider, null)));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.ContactPoint), Create, null, null, ms_metaMethods, ms_instanceProperties, null);
            p_vm.RegisterFunction(nameof(IsContactPoint), IsContactPoint);
        }

        // Ctor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.ContactPoint(new UnityEngine.ContactPoint()));
            return l_argReader.GetReturnValue();
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
        static int Equal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.ContactPoint l_pointA = null;
            Wrappers.ContactPoint l_pointB = null;
            l_argReader.ReadObject(ref l_pointA);
            l_argReader.ReadObject(ref l_pointB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_pointA == l_pointB);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

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
        static int GetNormal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.ContactPoint l_point = null;
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_point.m_point.normal));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetOtherCollider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.ContactPoint l_point = null;
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(l_point.m_point.otherCollider);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.ContactPoint l_point = null;
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_point.m_point.point));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetSeparation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.ContactPoint l_point = null;
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_point.m_point.separation);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetThisCollider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.ContactPoint l_point = null;
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(l_point.m_point.thisCollider);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
    }
}
