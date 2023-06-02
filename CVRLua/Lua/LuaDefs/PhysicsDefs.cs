using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class PhysicsDefs
    {
        const string c_destroyedCollider = "Collider is destroyed";

        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticProperties.Add(("AllLayers", (GetAllLayers, null)));
            ms_staticProperties.Add(("DefaultRaycastLayers", (GetDefaultRaycastLayers, null)));
            ms_staticProperties.Add(("gravity", (GetGravity, SetGravity)));
            ms_staticProperties.Add(("IgnoreRaycastLayer", (GetIgnoreRaycastLayer, null)));

            //ms_staticMethods.Add((nameof(BakeMesh), BakeMesh));
            ms_staticMethods.Add((nameof(BoxCast), BoxCast));
            ms_staticMethods.Add((nameof(BoxCastAll), BoxCastAll));
            ms_staticMethods.Add((nameof(CapsuleCast), CapsuleCast));
            ms_staticMethods.Add((nameof(CapsuleCastAll), CapsuleCastAll));
            ms_staticMethods.Add((nameof(CheckBox), CheckBox));
            ms_staticMethods.Add((nameof(CheckCapsule), CheckCapsule));
            ms_staticMethods.Add((nameof(CheckSphere), CheckSphere));
            ms_staticMethods.Add((nameof(ClosestPoint), ClosestPoint));
            ms_staticMethods.Add((nameof(ComputePenetration), ComputePenetration));
            ms_staticMethods.Add((nameof(GetIgnoreCollision), GetIgnoreCollision));
            ms_staticMethods.Add((nameof(GetIgnoreLayerCollision), GetIgnoreLayerCollision));
            ms_staticMethods.Add((nameof(IgnoreCollision), IgnoreCollision));
            ms_staticMethods.Add((nameof(Linecast), Linecast));
            ms_staticMethods.Add((nameof(OverlapBox), OverlapBox));
            ms_staticMethods.Add((nameof(OverlapCapsule), OverlapCapsule));
            ms_staticMethods.Add((nameof(OverlapSphere), OverlapSphere));
            ms_staticMethods.Add((nameof(Raycast), Raycast));
            ms_staticMethods.Add((nameof(RaycastAll), RaycastAll));
            ms_staticMethods.Add((nameof(SphereCast), SphereCast));
            ms_staticMethods.Add((nameof(SphereCastAll), SphereCastAll));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Physics), null, ms_staticProperties, ms_staticMethods, null, null, null);
        }

        // Static properties
        static int GetAllLayers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushInteger(Physics.AllLayers);
            return 1;
        }

        static int GetDefaultRaycastLayers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushInteger(Physics.DefaultRaycastLayers);
            return 1;
        }

        static int GetGravity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Vector3(Physics.gravity));
            return 1;
        }
        static int SetGravity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_gravity = null;
            l_argReader.ReadObject(ref l_gravity);
            if(!l_argReader.HasErrors())
                Physics.gravity = l_gravity.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetIgnoreRaycastLayer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushInteger(Physics.IgnoreRaycastLayer);
            return 1;
        }

        // Static methods
        static int BoxCast(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_center = null;
            Wrappers.Vector3 l_halfExtents = null;
            Wrappers.Vector3 l_dir = null;
            Wrappers.Quaternion l_orientation = new Wrappers.Quaternion(Quaternion.identity);
            float l_maxDistance = Mathf.Infinity;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_center);
            l_argReader.ReadObject(ref l_halfExtents);
            l_argReader.ReadObject(ref l_dir);
            l_argReader.ReadNextObject(ref l_orientation);
            l_argReader.ReadNextNumber(ref l_maxDistance);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Physics.BoxCast(l_center.m_vec, l_halfExtents.m_vec, l_dir.m_vec, l_orientation.m_quat, l_maxDistance, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int BoxCastAll(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_center = null;
            Wrappers.Vector3 l_halfExtents = null;
            Wrappers.Vector3 l_dir = null;
            Wrappers.Quaternion l_orientation = new Wrappers.Quaternion(Quaternion.identity);
            float l_maxDistance = Mathf.Infinity;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_center);
            l_argReader.ReadObject(ref l_halfExtents);
            l_argReader.ReadObject(ref l_dir);
            l_argReader.ReadNextObject(ref l_orientation);
            l_argReader.ReadNextNumber(ref l_maxDistance);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
            {
                RaycastHit[] l_hits = Physics.BoxCastAll(l_center.m_vec, l_halfExtents.m_vec, l_dir.m_vec, l_orientation.m_quat, l_maxDistance, l_layerMask, l_triggerInteraction);
                List<Wrappers.RaycastHit> l_list = new List<Wrappers.RaycastHit>();
                foreach(RaycastHit l_hit in l_hits)
                    l_list.Add(new Wrappers.RaycastHit(l_hit));
                l_list.Sort((a, b) => ((a.m_hit.distance < b.m_hit.distance) ? -1 : 1));
                l_argReader.PushTable(l_list);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int CapsuleCast(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_pointA = null;
            Wrappers.Vector3 l_pointB = null;
            float l_radius = 0f;
            Wrappers.Vector3 l_dir = null;
            float l_maxDistance = Mathf.Infinity;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_pointA);
            l_argReader.ReadObject(ref l_pointB);
            l_argReader.ReadNumber(ref l_radius);
            l_argReader.ReadObject(ref l_dir);
            l_argReader.ReadNextNumber(ref l_maxDistance);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Physics.CapsuleCast(l_pointA.m_vec, l_pointB.m_vec, l_radius, l_dir.m_vec, l_maxDistance, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int CapsuleCastAll(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_pointA = null;
            Wrappers.Vector3 l_pointB = null;
            float l_radius = 0f;
            Wrappers.Vector3 l_dir = null;
            float l_maxDistance = Mathf.Infinity;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_pointA);
            l_argReader.ReadObject(ref l_pointB);
            l_argReader.ReadNumber(ref l_radius);
            l_argReader.ReadObject(ref l_dir);
            l_argReader.ReadNextNumber(ref l_maxDistance);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
            {
                RaycastHit[] l_hits = Physics.CapsuleCastAll(l_pointA.m_vec, l_pointB.m_vec, l_radius, l_dir.m_vec, l_maxDistance, l_layerMask, l_triggerInteraction);
                List<Wrappers.RaycastHit> l_list = new List<Wrappers.RaycastHit>();
                foreach(RaycastHit l_hit in l_hits)
                    l_list.Add(new Wrappers.RaycastHit(l_hit));
                l_list.Sort((a, b) => ((a.m_hit.distance < b.m_hit.distance) ? -1 : 1));
                l_argReader.PushTable(l_list);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int CheckBox(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_center = null;
            Wrappers.Vector3 l_halfExtents = null;
            Wrappers.Quaternion l_orientation = new Wrappers.Quaternion(Quaternion.identity);
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_center);
            l_argReader.ReadObject(ref l_halfExtents);
            l_argReader.ReadNextObject(ref l_orientation);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Physics.CheckBox(l_center.m_vec, l_halfExtents.m_vec, l_orientation.m_quat, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int CheckCapsule(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_pointA = null;
            Wrappers.Vector3 l_pointB = null;
            float l_radius = 0f;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_pointA);
            l_argReader.ReadObject(ref l_pointB);
            l_argReader.ReadNumber(ref l_radius);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Physics.CheckCapsule(l_pointA.m_vec, l_pointB.m_vec, l_radius, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int CheckSphere(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_point = null;
            float l_radius = 0f;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_point);
            l_argReader.ReadNumber(ref l_radius);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Physics.CheckSphere(l_point.m_vec, l_radius, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int ClosestPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_point = null;
            Collider l_collider = null;
            Wrappers.Vector3 l_pos = null;
            Wrappers.Quaternion l_rot = null;
            l_argReader.ReadObject(ref l_point);
            l_argReader.ReadObject(ref l_collider);
            l_argReader.ReadObject(ref l_pos);
            l_argReader.ReadObject(ref l_rot);
            if(!l_argReader.HasErrors())
            {
                if(l_collider != null)
                    l_argReader.PushObject(new Wrappers.Vector3(Physics.ClosestPoint(l_point.m_vec, l_collider, l_pos.m_vec, l_rot.m_quat)));
                else
                {
                    l_argReader.SetError(c_destroyedCollider);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int ComputePenetration(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collider l_colA = null;
            Wrappers.Vector3 l_posA = null;
            Wrappers.Quaternion l_rotA = null;
            Collider l_colB = null;
            Wrappers.Vector3 l_posB = null;
            Wrappers.Quaternion l_rotB = null;
            l_argReader.ReadObject(ref l_colA);
            l_argReader.ReadObject(ref l_posA);
            l_argReader.ReadObject(ref l_rotA);
            l_argReader.ReadObject(ref l_colB);
            l_argReader.ReadObject(ref l_posB);
            l_argReader.ReadObject(ref l_rotB);
            if(!l_argReader.HasErrors())
            {
                if((l_colA != null) && (l_colB != null))
                {
                    bool l_result = Physics.ComputePenetration(l_colA, l_posA.m_vec, l_rotA.m_quat, l_colB, l_posB.m_vec, l_rotB.m_quat, out Vector3 l_dir, out float l_dist);
                    if(l_result)
                    {
                        l_argReader.PushObject(new Wrappers.Vector3(l_dir));
                        l_argReader.PushNumber(l_dist);
                    }
                    else
                        l_argReader.PushBoolean(false);
                }
                else
                {
                    l_argReader.SetError(c_destroyedCollider);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GetIgnoreCollision(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collider l_colA = null;
            Collider l_colB = null;
            l_argReader.ReadObject(ref l_colA);
            l_argReader.ReadObject(ref l_colB);
            if(!l_argReader.HasErrors())
            {
                if((l_colA != null) && (l_colB != null))
                    l_argReader.PushBoolean(Physics.GetIgnoreCollision(l_colA, l_colB));
                else
                {
                    l_argReader.SetError(c_destroyedCollider);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GetIgnoreLayerCollision(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            int l_layerA = 0;
            int l_layerB = 0;
            l_argReader.ReadInteger(ref l_layerA);
            l_argReader.ReadInteger(ref l_layerB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Physics.GetIgnoreLayerCollision(l_layerA, l_layerB));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int IgnoreCollision(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collider l_colA = null;
            Collider l_colB = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_colA);
            l_argReader.ReadObject(ref l_colB);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if((l_colA != null) && (l_colB != null))
                {
                    Physics.IgnoreCollision(l_colA, l_colB, l_state);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyedCollider);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Linecast(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_pointA = null;
            Wrappers.Vector3 l_pointB = null;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_pointA);
            l_argReader.ReadObject(ref l_pointB);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Physics.Linecast(l_pointA.m_vec, l_pointB.m_vec, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int OverlapBox(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_center = null;
            Wrappers.Vector3 l_halfExtents = null;
            Wrappers.Quaternion l_orientation = new Wrappers.Quaternion(Quaternion.identity);
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_center);
            l_argReader.ReadObject(ref l_halfExtents);
            l_argReader.ReadNextObject(ref l_orientation);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushTable(Physics.OverlapBox(l_center.m_vec, l_halfExtents.m_vec, l_orientation.m_quat, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int OverlapCapsule(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_pointA = null;
            Wrappers.Vector3 l_pointB = null;
            float l_radius = 0f;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_pointA);
            l_argReader.ReadObject(ref l_pointB);
            l_argReader.ReadNumber(ref l_radius);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushTable(Physics.OverlapCapsule(l_pointA.m_vec, l_pointB.m_vec, l_radius, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int OverlapSphere(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_point = null;
            float l_radius = 0f;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_point);
            l_argReader.ReadNumber(ref l_radius);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushTable(Physics.OverlapSphere(l_point.m_vec, l_radius, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Raycast(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_origin = null;
            Wrappers.Vector3 l_dir = null;
            float l_maxDist = Mathf.Infinity;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_origin);
            l_argReader.ReadObject(ref l_dir);
            l_argReader.ReadNextNumber(ref l_maxDist);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Physics.Raycast(l_origin.m_vec, l_dir.m_vec, l_maxDist, l_layerMask, l_triggerInteraction));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int RaycastAll(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_origin = null;
            Wrappers.Vector3 l_dir = null;
            float l_maxDist = Mathf.Infinity;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_origin);
            l_argReader.ReadObject(ref l_dir);
            l_argReader.ReadNextNumber(ref l_maxDist);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
            {
                RaycastHit[] l_hits = Physics.RaycastAll(l_origin.m_vec, l_dir.m_vec, l_maxDist, l_layerMask, l_triggerInteraction);
                List<Wrappers.RaycastHit> l_list = new List<Wrappers.RaycastHit>();
                foreach(RaycastHit l_hit in l_hits)
                    l_list.Add(new Wrappers.RaycastHit(l_hit));
                l_list.Sort((a, b) => ((a.m_hit.distance < b.m_hit.distance) ? -1 : 1));
                l_argReader.PushTable(l_list);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SphereCast(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_origin = null;
            float l_radius = 0f;
            Wrappers.Vector3 l_dir = null;
            float l_maxDist = Mathf.Infinity;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_origin);
            l_argReader.ReadNumber(ref l_radius);
            l_argReader.ReadObject(ref l_dir);
            l_argReader.ReadNextNumber(ref l_maxDist);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
            {
                bool l_result = Physics.SphereCast(l_origin.m_vec, l_radius, l_dir.m_vec, out RaycastHit l_hit, l_maxDist, l_layerMask, l_triggerInteraction);
                if(l_result)
                    l_argReader.PushObject(new Wrappers.RaycastHit(l_hit));
                else
                    l_argReader.PushBoolean(false);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SphereCastAll(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_origin = null;
            float l_radius = 0f;
            Wrappers.Vector3 l_dir = null;
            float l_maxDist = Mathf.Infinity;
            int l_layerMask = Physics.DefaultRaycastLayers;
            QueryTriggerInteraction l_triggerInteraction = QueryTriggerInteraction.UseGlobal;
            l_argReader.ReadObject(ref l_origin);
            l_argReader.ReadNumber(ref l_radius);
            l_argReader.ReadObject(ref l_dir);
            l_argReader.ReadNextNumber(ref l_maxDist);
            l_argReader.ReadNextInteger(ref l_layerMask);
            l_argReader.ReadNextEnum(ref l_triggerInteraction);
            if(!l_argReader.HasErrors())
            {
                RaycastHit[] l_hits = Physics.SphereCastAll(l_origin.m_vec, l_radius, l_dir.m_vec, l_maxDist, l_layerMask, l_triggerInteraction);
                List<Wrappers.RaycastHit> l_list = new List<Wrappers.RaycastHit>();
                foreach(RaycastHit l_hit in l_hits)
                    l_list.Add(new Wrappers.RaycastHit(l_hit));
                l_list.Sort((a, b) => ((a.m_hit.distance < b.m_hit.distance) ? -1 : 1));
                l_argReader.PushTable(l_list);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
