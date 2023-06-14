using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class NavMeshHitDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();

        internal static void Init()
        {
            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("distance", (GetDistance, SetDistance)));
            ms_instanceProperties.Add(("hit", (GetHit, SetHit)));
            ms_instanceProperties.Add(("mask", (GetMask, SetMask)));
            ms_instanceProperties.Add(("normal", (GetNormal, SetNormal)));
            ms_instanceProperties.Add(("position", (GetPosition, SetPosition)));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.NavMeshHit), Create, null, null, ms_metaMethods, ms_instanceProperties, null);
            p_vm.RegisterFunction(nameof(IsNavMeshHit), IsNavMeshHit);
        }

        // Constructor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.NavMeshHit(new UnityEngine.AI.NavMeshHit()));
            return l_argReader.GetReturnValue();
        }

        // Static methods
        static int IsNavMeshHit(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            l_argReader.ReadNextObject(ref l_hit);
            l_argReader.PushBoolean(l_hit != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Equal(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hitA = null;
            Wrappers.NavMeshHit l_hitB = null;
            l_argReader.ReadObject(ref l_hitA);
            l_argReader.ReadObject(ref l_hitB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_hitA == l_hitB);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int ToString(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_hit.m_hit.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance properties
        static int GetDistance(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_hit.m_hit.distance);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetDistance(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_hit);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_hit.m_hit.distance = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetHit(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_hit.m_hit.hit);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetHit(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_hit);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
                l_hit.m_hit.hit = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetMask(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(l_hit.m_hit.mask);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetMask(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_hit);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
                l_hit.m_hit.mask = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetNormal(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_hit.m_hit.normal));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetNormal(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_hit);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
                l_hit.m_hit.normal = l_value.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetPosition(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_hit.m_hit.position));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetPosition(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.NavMeshHit l_hit = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_hit);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
                l_hit.m_hit.position = l_value.m_vec;

            l_argReader.LogError();
            return 0;
        }
    }
}
