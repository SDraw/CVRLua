using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class MeshRendererDefs
    {
        const string c_destroyed = "MeshRenderer is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            //ms_instanceProperties.Add(("additionalVertexStreams", (?,?)));
            ms_instanceProperties.Add(("subMeshStartIndex", (GetSubMeshStartIndex, null)));

            RendererDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(MeshRenderer), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsMeshRenderer), IsMeshRenderer);
        }

        // Static methods
        static int IsMeshRenderer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            MeshRenderer l_render = null;
            l_argReader.ReadNextObject(ref l_render);
            l_argReader.PushBoolean(l_render != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetSubMeshStartIndex(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            MeshRenderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushInteger(l_render.subMeshStartIndex);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
    }
}
