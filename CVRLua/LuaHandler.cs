using System;
using System.Collections.Generic;

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
            OnEnable,
            OnDisable,
            OnDestroy,
            OnGUI,
            OnCollisionEnter,
            OnCollisionExit,
            OnCollisionStay,
            OnTriggerEnter,
            OnTriggerExit,
            OnTriggerStay,
            OnAnimatorIK,
            OnMessage,
            OnInteractableGrab,
            OnInteractableDrop,
            OnInteractableUp,
            OnInteractableDown,
            OnInteractableGazeEnter,
            OnInteractableGazeExit,
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

            Lua.LuaDefs.AudioClipDefs.Init();
            Lua.LuaDefs.AudioSourceDefs.Init();

            Lua.LuaDefs.ColliderDefs.Init();
            Lua.LuaDefs.CharacterControllerDefs.Init();
            Lua.LuaDefs.MeshColliderDefs.Init();
            Lua.LuaDefs.CapsuleColliderDefs.Init();
            Lua.LuaDefs.BoxColliderDefs.Init();
            Lua.LuaDefs.SphereColliderDefs.Init();
            Lua.LuaDefs.WheelColliderDefs.Init();
            Lua.LuaDefs.TerrainColliderDefs.Init();

            Lua.LuaDefs.AnimatorDefs.Init();

            Lua.LuaDefs.ContactPointDefs.Init();
            Lua.LuaDefs.CollisionDefs.Init();

            Lua.LuaDefs.LuaScriptDefs.Init();

            Lua.LuaDefs.QuaternionDefs.Init();
            Lua.LuaDefs.Vector2Defs.Init();
            Lua.LuaDefs.Vector3Defs.Init();
            Lua.LuaDefs.Vector4Defs.Init();

            Lua.LuaDefs.TimeDefs.Init();
            Lua.LuaDefs.DateTimeDefs.Init();
            Lua.LuaDefs.MathfDefs.Init();

            Lua.LuaDefs.CVRPointerDefs.Init();

            Lua.LuaDefs.LocalPlayerDefs.Init();
        }

        internal LuaHandler(string p_name = "")
        {
            m_eventFunctions = new Dictionary<ScriptEvent, int>();

            m_vm = new Lua.LuaVM(p_name);

            Lua.LuaDefs.ObjectDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.ComponentDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.BehaviourDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.MonoBehaviourDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.TransformDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.GameObjectDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.AudioClipDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.AudioSourceDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.ColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CharacterControllerDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.MeshColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CapsuleColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.BoxColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.SphereColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.WheelColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.TerrainColliderDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.AnimatorDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.ContactPointDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CollisionDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.LuaScriptDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.QuaternionDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.Vector2Defs.RegisterInVM(m_vm);
            Lua.LuaDefs.Vector3Defs.RegisterInVM(m_vm);
            Lua.LuaDefs.Vector4Defs.RegisterInVM(m_vm);

            Lua.LuaDefs.TimeDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.DateTimeDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.MathfDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.CVRPointerDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.LocalPlayerDefs.RegisterInVM(m_vm);
        }

        public void Execute(string p_code)
        {
            m_vm.Execute(p_code);
        }

        public void CallEvent(ScriptEvent p_event, params object[] p_args)
        {
            if(m_eventFunctions.ContainsKey(p_event))
                m_vm.CallFunctionByReference(m_eventFunctions[p_event], p_args);
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
