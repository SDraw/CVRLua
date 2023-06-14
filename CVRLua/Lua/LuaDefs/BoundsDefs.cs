using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class BoundsDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("center", (GetCenter, SetCenter)));
            ms_instanceProperties.Add(("extents", (GetExtents, SetExtents)));
            ms_instanceProperties.Add(("max", (GetMax, SetMax)));
            ms_instanceProperties.Add(("min", (GetMin, SetMin)));
            ms_instanceProperties.Add(("size", (GetSize, SetSize)));

            ms_instanceMethods.Add((nameof(ClosestPoint), ClosestPoint));
            ms_instanceMethods.Add((nameof(Contains), Contains));
            ms_instanceMethods.Add((nameof(Encapsulate), Encapsulate));
            ms_instanceMethods.Add((nameof(Expand), Expand));
            ms_instanceMethods.Add((nameof(IntersectRay), IntersectRay));
            ms_instanceMethods.Add((nameof(Intersects), Intersects));
            ms_instanceMethods.Add((nameof(SetMinMax), SetMinMax));
            ms_instanceMethods.Add((nameof(SqrDistance), SqrDistance));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.Bounds), Create, null, null, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsBounds), IsBounds);
        }

        // Ctor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_center = new Wrappers.Vector3();
            Wrappers.Vector3 l_size = new Wrappers.Vector3();
            l_argReader.Skip(); // Metatable
            l_argReader.ReadNextObject(ref l_center);
            l_argReader.ReadNextObject(ref l_size);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Bounds(new UnityEngine.Bounds(l_center.m_vec, l_size.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Static methods
        static int IsBounds(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            l_argReader.ReadNextObject(ref l_bounds);
            l_argReader.PushBoolean(l_bounds != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Equal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_boundsA = null;
            Wrappers.Bounds l_boundsB = null;
            l_argReader.ReadObject(ref l_boundsA);
            l_argReader.ReadObject(ref l_boundsB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_boundsA == l_boundsB);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int ToString(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_ray = null;
            l_argReader.ReadObject(ref l_ray);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_ray.m_bounds.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetCenter(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            l_argReader.ReadObject(ref l_bounds);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_bounds.m_bounds.center));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetCenter(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_center = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_center);
            if(!l_argReader.HasErrors())
                l_bounds.m_bounds.center = l_center.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetExtents(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            l_argReader.ReadObject(ref l_bounds);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_bounds.m_bounds.extents));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetExtents(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_ext = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_ext);
            if(!l_argReader.HasErrors())
                l_bounds.m_bounds.extents = l_ext.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetMax(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            l_argReader.ReadObject(ref l_bounds);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_bounds.m_bounds.max));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetMax(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_max = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_max);
            if(!l_argReader.HasErrors())
                l_bounds.m_bounds.max = l_max.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetMin(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            l_argReader.ReadObject(ref l_bounds);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_bounds.m_bounds.min));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetMin(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_min = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_min);
            if(!l_argReader.HasErrors())
                l_bounds.m_bounds.min = l_min.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            l_argReader.ReadObject(ref l_bounds);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_bounds.m_bounds.size));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_size = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_size);
            if(!l_argReader.HasErrors())
                l_bounds.m_bounds.size = l_size.m_vec;

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int ClosestPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_point = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_bounds.m_bounds.ClosestPoint(l_point.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Contains(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_point = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_bounds.m_bounds.Contains(l_point.m_vec));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Encapsulate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_point = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
            {
                l_bounds.m_bounds.Encapsulate(l_point.m_vec);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Expand(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            float l_amount = 0f;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadNumber(ref l_amount);
            if(!l_argReader.HasErrors())
            {
                l_bounds.m_bounds.Expand(l_amount);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int IntersectRay(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Ray l_ray = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_ray);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_bounds.m_bounds.IntersectRay(l_ray.m_ray));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Intersects(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_boundsA = null;
            Wrappers.Bounds l_boundsB = null;
            l_argReader.ReadObject(ref l_boundsA);
            l_argReader.ReadObject(ref l_boundsB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_boundsA.m_bounds.Intersects(l_boundsB.m_bounds));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetMinMax(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_min = null;
            Wrappers.Vector3 l_max = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_min);
            l_argReader.ReadObject(ref l_max);
            if(!l_argReader.HasErrors())
            {
                l_bounds.m_bounds.SetMinMax(l_min.m_vec, l_max.m_vec);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SqrDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Bounds l_bounds = null;
            Wrappers.Vector3 l_point = null;
            l_argReader.ReadObject(ref l_bounds);
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_bounds.m_bounds.SqrDistance(l_point.m_vec));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
