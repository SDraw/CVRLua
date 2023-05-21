using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua
{
    class LuaHandler
    {
        public enum ScriptEvent
        {
            Start = 0,
            Update,
            LateUpdate,
            FixedUpdate,
            OnScriptMessage
        }

        readonly Lua.LuaVM m_vm = null;
        readonly Dictionary<ScriptEvent, int> m_eventFunctions = null; // Event <-> Reference

        internal static void Init()
        {
            Lua.LuaDefs.ObjectDefs.Init();
            Lua.LuaDefs.ComponentDefs.Init();
            Lua.LuaDefs.BehaviourDefs.Init();
            Lua.LuaDefs.MonoBehaviourDefs.Init();

            Lua.LuaDefs.TransformDefs.Init();
            Lua.LuaDefs.GameObjectDefs.Init();

            Lua.LuaDefs.QuaternionDefs.Init();
            Lua.LuaDefs.Vector2Defs.Init();
            Lua.LuaDefs.Vector3Defs.Init();
            Lua.LuaDefs.Vector4Defs.Init();

            Lua.LuaDefs.LocalPlayerDefs.Init();
        }

        internal LuaHandler(GameObject p_attachedTo, string p_name = "")
        {
            m_eventFunctions = new Dictionary<ScriptEvent, int>();

            m_vm = new Lua.LuaVM(p_name);

            Lua.LuaDefs.ObjectDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.ComponentDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.BehaviourDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.MonoBehaviourDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.TransformDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.GameObjectDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.QuaternionDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.Vector2Defs.RegisterInVM(m_vm);
            Lua.LuaDefs.Vector3Defs.RegisterInVM(m_vm);
            Lua.LuaDefs.Vector4Defs.RegisterInVM(m_vm);
            //Lua.LuaDefs.LuaScriptDefs.Init(m_vm);

            Lua.LuaDefs.LocalPlayerDefs.RegisterInVM(m_vm);

            // Fill default global variables
            SetGlobalVariable("this", p_attachedTo);
            SetGlobalVariable("localPlayer", new Wrappers.LocalPlayer());
        }

        public void Execute(string p_code)
        {
            m_vm.Execute(p_code);
        }

        public void CallEvent(ScriptEvent p_event)
        {
            if(m_eventFunctions.ContainsKey(p_event))
                m_vm.CallFunctionByReference(m_eventFunctions[p_event]);
        }

        public void CallEvent(ScriptEvent p_event, string p_str)
        {
            if(m_eventFunctions.ContainsKey(p_event))
                m_vm.CallFunctionByReference(m_eventFunctions[p_event], p_str);
        }

        public void SetGlobalVariable<T>(string p_name, T p_val) => m_vm.SetGlobalVariable(p_name, p_val);

        internal void ParseEvents()
        {
            foreach(ScriptEvent l_enum in Enum.GetValues(typeof(ScriptEvent)))
            {
                if(m_vm.IsGlobalFunctionPresent(l_enum.ToString()))
                    m_eventFunctions.Add(l_enum, m_vm.GetGlobalFunctionReference(l_enum.ToString()));
            }
        }

        internal void PerformGC()
        {
            m_vm.PerformGC();
        }
    }
}
