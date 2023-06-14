using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class OffMeshLinkDataDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();

        internal static void Init()
        {
            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("activated", (GetActivated, null)));
            ms_instanceProperties.Add(("endPos", (GetEndPos, null)));
            ms_instanceProperties.Add(("linkType", (GetLinkType, null)));
            ms_instanceProperties.Add(("offMeshLink", (GetOffMeshLink, null)));
            ms_instanceProperties.Add(("startPos", (GetStartPos, null)));
            ms_instanceProperties.Add(("valid", (GetValid, null)));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.OffMeshLinkData), Create, null, null, ms_metaMethods, ms_instanceProperties, null);
            p_vm.RegisterFunction(nameof(IsOffMeshLinkData), IsOffMeshLinkData);
        }

        // Constructor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.OffMeshLinkData(new UnityEngine.AI.OffMeshLinkData()));
            return l_argReader.GetReturnValue();
        }

        // Static methods
        static int IsOffMeshLinkData(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.OffMeshLinkData l_data = null;
            l_argReader.ReadNextObject(ref l_data);
            l_argReader.PushBoolean(l_data != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Equal(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.OffMeshLinkData l_dataA = null;
            Wrappers.OffMeshLinkData l_dataB = null;
            l_argReader.ReadObject(ref l_dataA);
            l_argReader.ReadObject(ref l_dataB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_dataA == l_dataB);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int ToString(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.OffMeshLinkData l_data = null;
            l_argReader.ReadObject(ref l_data);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_data.m_data.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance properties
        static int GetActivated(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.OffMeshLinkData l_data = null;
            l_argReader.ReadObject(ref l_data);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_data.m_data.activated);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetEndPos(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.OffMeshLinkData l_data = null;
            l_argReader.ReadObject(ref l_data);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_data.m_data.endPos));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLinkType(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.OffMeshLinkData l_data = null;
            l_argReader.ReadObject(ref l_data);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_data.m_data.linkType.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetOffMeshLink(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.OffMeshLinkData l_data = null;
            l_argReader.ReadObject(ref l_data);
            if(!l_argReader.HasErrors())
            {
                if(l_data.m_data.offMeshLink != null)
                    l_argReader.PushObject(l_data.m_data.offMeshLink);
                else
                    l_argReader.PushBoolean(false);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetStartPos(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.OffMeshLinkData l_data = null;
            l_argReader.ReadObject(ref l_data);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_data.m_data.startPos));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetValid(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.OffMeshLinkData l_data = null;
            l_argReader.ReadObject(ref l_data);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_data.m_data.valid);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
    }
}
