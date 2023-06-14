using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class RayDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("direction", (GetDirection, SetDirection)));
            ms_instanceProperties.Add(("origin", (GetOrigin, SetOrigin)));

            ms_instanceMethods.Add((nameof(GetPoint), GetPoint));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.Ray), Create, null, null, ms_metaMethods, ms_instanceProperties, null);
            p_vm.RegisterFunction(nameof(IsRay), IsRay);
        }

        // Ctor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_pos = new Wrappers.Vector3();
            Wrappers.Vector3 l_dir = new Wrappers.Vector3();
            l_argReader.Skip(); // Metatable
            l_argReader.ReadNextObject(ref l_pos);
            l_argReader.ReadNextObject(ref l_dir);
            l_argReader.PushObject(new Wrappers.Ray(new UnityEngine.Ray(l_pos.m_vec, l_dir.m_vec)));
            return l_argReader.GetReturnValue();
        }

        // Static methods
        static int IsRay(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Ray l_ray = null;
            l_argReader.ReadNextObject(ref l_ray);
            l_argReader.PushBoolean(l_ray != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Equal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Ray l_rayA = null;
            Wrappers.Ray l_rayB = null;
            l_argReader.ReadObject(ref l_rayA);
            l_argReader.ReadObject(ref l_rayB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_rayA == l_rayB);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int ToString(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Ray l_ray = null;
            l_argReader.ReadObject(ref l_ray);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_ray.m_ray.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetDirection(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Ray l_ray = null;
            l_argReader.ReadObject(ref l_ray);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_ray.m_ray.direction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetDirection(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Ray l_ray = null;
            Wrappers.Vector3 l_dir = null;
            l_argReader.ReadObject(ref l_ray);
            l_argReader.ReadObject(ref l_dir);
            if(!l_argReader.HasErrors())
                l_ray.m_ray.direction = l_dir.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetOrigin(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Ray l_ray = null;
            l_argReader.ReadObject(ref l_ray);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_ray.m_ray.origin));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetOrigin(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Ray l_ray = null;
            Wrappers.Vector3 l_origin = null;
            l_argReader.ReadObject(ref l_ray);
            l_argReader.ReadObject(ref l_origin);
            if(!l_argReader.HasErrors())
                l_ray.m_ray.origin = l_origin.m_vec;

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int GetPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Ray l_ray = null;
            float l_distance = 0f;
            l_argReader.ReadObject(ref l_ray);
            l_argReader.ReadNumber(ref l_distance);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_ray.m_ray.GetPoint(l_distance)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
