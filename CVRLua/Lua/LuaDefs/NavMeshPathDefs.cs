using System;
using System.Collections.Generic;
using UnityEngine.AI;

namespace CVRLua.Lua.LuaDefs
{
    static class NavMeshPathDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsNavMeshPath), IsNavMeshPath));

            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("corners", (GetCorners, null)));
            ms_instanceProperties.Add(("status", (GetStatus, null)));

            ms_instanceMethods.Add((nameof(ClearCorners), ClearCorners));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(NavMeshPath), Create, null, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Constructor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new NavMeshPath());
            return l_argReader.GetReturnValue();
        }

        // Static methods
        static int IsNavMeshPath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshPath l_path = null;
            l_argReader.ReadNextObject(ref l_path);
            l_argReader.PushBoolean(l_path != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Equal(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            NavMeshPath l_pathA = null;
            NavMeshPath l_pathB = null;
            l_argReader.ReadObject(ref l_pathA);
            l_argReader.ReadObject(ref l_pathB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_pathA == l_pathB);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int ToString(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            NavMeshPath l_path = null;
            l_argReader.ReadObject(ref l_path);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_path.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance properties
        static int GetCorners(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            NavMeshPath l_path = null;
            l_argReader.ReadObject(ref l_path);
            if(!l_argReader.HasErrors())
            {
                UnityEngine.Vector3[] l_corners = l_path.corners;
                List<Wrappers.Vector3> l_wrapped = new List<Wrappers.Vector3>();
                foreach(var l_vec in l_corners)
                    l_wrapped.Add(new Wrappers.Vector3(l_vec));
                l_argReader.PushTable(l_wrapped);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetStatus(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            NavMeshPath l_path = null;
            l_argReader.ReadObject(ref l_path);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_path.status.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance methods
        static int ClearCorners(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            NavMeshPath l_path = null;
            l_argReader.ReadObject(ref l_path);
            if(!l_argReader.HasErrors())
            {
                l_path.ClearCorners();
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
    }
}
