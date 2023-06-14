using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class MeshColliderDefs
    {
        const string c_destroyed = "MeshCollider is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_instanceProperties.Add(("convex", (GetConvex, SetConvex)));
            ms_instanceProperties.Add(("cookingOptions", (GetCookingOptions, SetCookingOptions)));
            //ms_instanceProperties.Add("sharedMesh", (GetSharedMesh, SetSharedMesh));

            ColliderDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(MeshCollider), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsMeshCollider), IsMeshCollider);
        }

        // Static methods
        static int IsMeshCollider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            MeshCollider l_col = null;
            l_argReader.ReadNextObject(ref l_col);
            l_argReader.PushBoolean(l_col != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetConvex(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            MeshCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushBoolean(l_col.convex);
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
        static int SetConvex(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            MeshCollider l_col = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.convex = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCookingOptions(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            MeshCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushString(l_col.cookingOptions.ToString());
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
        static int SetCookingOptions(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            MeshCollider l_col = null;
            MeshColliderCookingOptions l_options = MeshColliderCookingOptions.None;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadEnum(ref l_options);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.cookingOptions = l_options;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
    }
}
