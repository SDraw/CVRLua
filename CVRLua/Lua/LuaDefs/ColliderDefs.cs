using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class ColliderDefs
    {
        const string c_destroyed = "Collider is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsCollider), IsCollider));

            ms_instanceProperties.Add(("attachedRigidbody", (GetAttachedRigidBody, null)));
            ms_instanceProperties.Add(("bounds", (GetBounds, null)));
            ms_instanceProperties.Add(("contactOffset", (GetContactOffset, SetContactOffset)));
            ms_instanceProperties.Add(("enabled", (GetEnabled, SetEnabled)));
            ms_instanceProperties.Add(("isTrigger", (GetTrigger, SetTrigger)));
            //ms_instanceProperties.Add("material", (?, ?)); // Requires Material defs
            //ms_instanceProperties.Add("sharedMaterial", (?, ?)); // Requires Material defs

            ms_instanceMethods.Add((nameof(ClosestPoint), ClosestPoint));
            ms_instanceMethods.Add((nameof(ClosestPointOnBounds), ClosestPointOnBounds));
            ms_instanceMethods.Add((nameof(Raycast), Raycast));

            ComponentDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Collider), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void InheritTo(
            List<(string, LuaInterop.lua_CFunction)> p_metaMethods,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_staticProperties,
            List<(string, LuaInterop.lua_CFunction)> p_staticMethods,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_instanceProperties,
            List<(string, LuaInterop.lua_CFunction)> p_instanceMethods
        )
        {
            if(p_metaMethods != null)
                ms_metaMethods.MergeInto(p_metaMethods);

            if(p_staticProperties != null)
                ms_staticProperties.MergeInto(p_staticProperties);

            if(p_staticMethods != null)
                ms_staticMethods.MergeInto(p_staticMethods);

            if(p_instanceProperties != null)
                ms_instanceProperties.MergeInto(p_instanceProperties);

            if(p_instanceMethods != null)
                ms_instanceMethods.MergeInto(p_instanceMethods);
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
        static int GetAttachedRigidBody(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                {
                    if(l_col.attachedRigidbody != null)
                        l_argReader.PushObject(l_col.attachedRigidbody);
                    else
                        l_argReader.PushBoolean(false);
                }
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetBounds(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushObject(new Wrappers.Bounds(l_col.bounds));
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetContactOffset(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.contactOffset);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetContactOffset(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.contactOffset = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetEnabled(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushBoolean(l_col.enabled);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetEnabled(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.enabled = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTrigger(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushBoolean(l_col.isTrigger);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetTrigger(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.isTrigger = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
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

        static int Raycast(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collider l_col = null;
            Wrappers.Ray l_ray = null;
            float l_maxDistance = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadObject(ref l_ray);
            l_argReader.ReadNumber(ref l_maxDistance);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                {
                    if(l_col.Raycast(l_ray.m_ray, out RaycastHit l_hit, l_maxDistance))
                        l_argReader.PushObject(new Wrappers.RaycastHit(l_hit));
                    else
                        l_argReader.PushBoolean(false);
                }
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
    }
}
