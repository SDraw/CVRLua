using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    class RaycastHitDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsRaycastHit), IsRaycastHit));

            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            //ms_instanceProperties.Add(("articulationBody", (?,?)));
            ms_instanceProperties.Add(("barycentricCoordinate", (GetBarycentricCoordinate, SetBarycentricCoordinate)));
            ms_instanceProperties.Add(("collider", (GetCollider, null)));
            ms_instanceProperties.Add(("distance", (GetDistance, SetDistance)));
            ms_instanceProperties.Add(("lightmapCoord", (GetLightmapCoord, null)));
            ms_instanceProperties.Add(("normal", (GetNormal, SetNormal)));
            ms_instanceProperties.Add(("point", (GetPoint, SetPoint)));
            ms_instanceProperties.Add(("rigidbody", (GetRigidbody, null)));
            ms_instanceProperties.Add(("textureCoord", (GetTextureCoord, null)));
            ms_instanceProperties.Add(("textureCoord2", (GetTextureCoord2, null)));
            ms_instanceProperties.Add(("transform", (GetTransform, null)));
            ms_instanceProperties.Add(("triangleIndex", (GetTriangleIndex, null)));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(RaycastHit), Create, null, ms_staticMethods, ms_metaMethods, ms_instanceProperties, null);
        }

        // Ctor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.RaycastHit(new RaycastHit()));
            return l_argReader.GetReturnValue();
        }

        // Static methods
        static int IsRaycastHit(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadNextObject(ref l_hit);
            l_argReader.PushBoolean(l_hit != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Equal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hitA = null;
            Wrappers.RaycastHit l_hitB = null;
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
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_hit.m_hit.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetBarycentricCoordinate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_hit.m_hit.barycentricCoordinate));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetBarycentricCoordinate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            Wrappers.Vector3 l_coord = null;
            l_argReader.ReadObject(ref l_hit);
            l_argReader.ReadObject(ref l_coord);
            if(!l_argReader.HasErrors())
                l_hit.m_hit.barycentricCoordinate = l_coord.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetCollider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
            {
                if(l_hit.m_hit.collider != null)
                    l_argReader.PushObject(l_hit.m_hit.collider);
                else
                    l_argReader.PushBoolean(false);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
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
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            float l_distance = 0f;
            l_argReader.ReadObject(ref l_hit);
            l_argReader.ReadNumber(ref l_distance);
            if(!l_argReader.HasErrors())
                l_hit.m_hit.distance = l_distance;

            l_argReader.LogError();
            return 0;
        }

        static int GetLightmapCoord(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector2(l_hit.m_hit.lightmapCoord));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetNormal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
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
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            Wrappers.Vector3 l_normal = null;
            l_argReader.ReadObject(ref l_hit);
            l_argReader.ReadObject(ref l_normal);
            if(!l_argReader.HasErrors())
                l_hit.m_hit.normal = l_normal.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_hit.m_hit.point));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            Wrappers.Vector3 l_point = null;
            l_argReader.ReadObject(ref l_hit);
            l_argReader.ReadObject(ref l_point);
            if(!l_argReader.HasErrors())
                l_hit.m_hit.point = l_point.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetRigidbody(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
            {
                if(l_hit.m_hit.rigidbody != null)
                    l_argReader.PushObject(l_hit.m_hit.rigidbody);
                else
                    l_argReader.PushBoolean(false);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetTextureCoord(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector2(l_hit.m_hit.textureCoord));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetTextureCoord2(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector2(l_hit.m_hit.textureCoord2));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetTransform(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
            {
                if(l_hit.m_hit.transform != null)
                    l_argReader.PushObject(l_hit.m_hit.transform);
                else
                    l_argReader.PushBoolean(false);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetTriangleIndex(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.RaycastHit l_hit = null;
            l_argReader.ReadObject(ref l_hit);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(l_hit.m_hit.triangleIndex);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
    }
}
