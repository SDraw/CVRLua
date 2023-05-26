using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class CapsuleColliderDefs
    {
        const string c_destroyed = "CapsuleCollider is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsCapsuleCollider), IsCapsuleCollider));

            ms_instanceProperties.Add(("center", (GetCenter, SetCenter)));
            ms_instanceProperties.Add(("direction", (GetDirection, SetDirection)));
            ms_instanceProperties.Add(("height", (GetHeight, SetHeight)));
            ms_instanceProperties.Add(("radius", (GetRadius, SetRadius)));
            
            ColliderDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CapsuleCollider), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsCapsuleCollider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CapsuleCollider l_col = null;
            l_argReader.ReadNextObject(ref l_col);
            l_argReader.PushBoolean(l_col != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetCenter(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CapsuleCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_col.center));
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
        static int SetCenter(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CapsuleCollider l_col = null;
            Wrappers.Vector3 l_center = null;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadObject(ref l_center);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.center = l_center.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDirection(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CapsuleCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushInteger(l_col.direction);
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
        static int SetDirection(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CapsuleCollider l_col = null;
            int l_dir = 0;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadInteger(ref l_dir);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.direction = l_dir;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetHeight(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CapsuleCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.height);
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
        static int SetHeight(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CapsuleCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.height = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRadius(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CapsuleCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.radius);
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
        static int SetRadius(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CapsuleCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.radius = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
    }
}
