using System;
using System.Collections.Generic;

namespace CVRLua
{
    class LuaHandler : IDisposable
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
            OnPlayerJoin,
            OnPlayerLeft,
            OnAttachmentAttach,
            OnAttachmentDeattach,
            OnButtonClick,
            OnToggleChange,
            OnSliderChange
        }

        Lua.LuaVM m_vm = null;
        readonly Dictionary<ScriptEvent, int> m_eventFunctions = null; // Event <-> Function reference

        internal static void Init()
        {
            // Unity defs
            Lua.LuaDefs.ObjectDefs.Init();
            Lua.LuaDefs.ComponentDefs.Init();
            Lua.LuaDefs.BehaviourDefs.Init();
            Lua.LuaDefs.MonoBehaviourDefs.Init();

            Lua.LuaDefs.AnimatorDefs.Init();
            Lua.LuaDefs.AudioClipDefs.Init();
            Lua.LuaDefs.AudioSourceDefs.Init();
            Lua.LuaDefs.BoundsDefs.Init();
            Lua.LuaDefs.CameraDefs.Init();
            Lua.LuaDefs.CollisionDefs.Init();
            Lua.LuaDefs.ColorDefs.Init();
            Lua.LuaDefs.ContactPointDefs.Init();
            Lua.LuaDefs.GameObjectDefs.Init();
            Lua.LuaDefs.LayerMaskDefs.Init();
            Lua.LuaDefs.LightDefs.Init();
            Lua.LuaDefs.MathfDefs.Init();
            Lua.LuaDefs.Matrix4x4Defs.Init();
            Lua.LuaDefs.NavMeshAgentDefs.Init();
            Lua.LuaDefs.NavMeshHitDefs.Init();
            Lua.LuaDefs.NavMeshPathDefs.Init();
            Lua.LuaDefs.OffMeshLinkDefs.Init();
            Lua.LuaDefs.OffMeshLinkDataDefs.Init();
            Lua.LuaDefs.PhysicsDefs.Init();
            Lua.LuaDefs.QuaternionDefs.Init();
            Lua.LuaDefs.RandomDefs.Init();
            Lua.LuaDefs.RayDefs.Init();
            Lua.LuaDefs.RaycastHitDefs.Init();
            Lua.LuaDefs.RenderSettingsDefs.Init();
            Lua.LuaDefs.RigidbodyDefs.Init();
            Lua.LuaDefs.TextMeshProDefs.Init();
            Lua.LuaDefs.TimeDefs.Init();
            Lua.LuaDefs.TransformDefs.Init();
            Lua.LuaDefs.Vector2Defs.Init();
            Lua.LuaDefs.Vector3Defs.Init();
            Lua.LuaDefs.Vector4Defs.Init();

            Lua.LuaDefs.ColliderDefs.Init();
            Lua.LuaDefs.CharacterControllerDefs.Init();
            Lua.LuaDefs.MeshColliderDefs.Init();
            Lua.LuaDefs.CapsuleColliderDefs.Init();
            Lua.LuaDefs.BoxColliderDefs.Init();
            Lua.LuaDefs.SphereColliderDefs.Init();
            Lua.LuaDefs.WheelColliderDefs.Init();
            Lua.LuaDefs.TerrainColliderDefs.Init();

            Lua.LuaDefs.RendererDefs.Init();
            Lua.LuaDefs.MeshRendererDefs.Init();
            Lua.LuaDefs.SkinnedMeshRendererDefs.Init();

            Lua.LuaDefs.UiSelectableDefs.Init();
            Lua.LuaDefs.UiButtonDefs.Init();
            Lua.LuaDefs.UiToggleDefs.Init();
            Lua.LuaDefs.UiSliderDefs.Init();

            // CVR defs
            Lua.LuaDefs.CVRAttachmentDefs.Init();
            Lua.LuaDefs.CVRMirrorDefs.Init();
            Lua.LuaDefs.CVRPointerDefs.Init();
            Lua.LuaDefs.CVRVideoPlayerDefs.Init();
            Lua.LuaDefs.CVRInteractableDefs.Init();
            Lua.LuaDefs.CVRPickupObjectDefs.Init();

            // Own defs
            Lua.LuaDefs.DateTimeDefs.Init();
            Lua.LuaDefs.PlayerDefs.Init();
            Lua.LuaDefs.LuaScriptDefs.Init();
        }

        internal LuaHandler()
        {
            m_eventFunctions = new Dictionary<ScriptEvent, int>();

            m_vm = new Lua.LuaVM();

            // Unity defs
            Lua.LuaDefs.ObjectDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.ComponentDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.BehaviourDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.MonoBehaviourDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.AnimatorDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.AudioClipDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.AudioSourceDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.BoundsDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CameraDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CollisionDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.ColorDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.ContactPointDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.GameObjectDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.LayerMaskDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.LightDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.MathfDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.Matrix4x4Defs.RegisterInVM(m_vm);
            Lua.LuaDefs.NavMeshAgentDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.NavMeshHitDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.NavMeshPathDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.OffMeshLinkDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.OffMeshLinkDataDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.PhysicsDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.QuaternionDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.RandomDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.RayDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.RaycastHitDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.RenderSettingsDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.RigidbodyDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.TextMeshProDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.TimeDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.TransformDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.Vector2Defs.RegisterInVM(m_vm);
            Lua.LuaDefs.Vector3Defs.RegisterInVM(m_vm);
            Lua.LuaDefs.Vector4Defs.RegisterInVM(m_vm);

            Lua.LuaDefs.ColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CharacterControllerDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.MeshColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CapsuleColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.BoxColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.SphereColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.WheelColliderDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.TerrainColliderDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.RendererDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.MeshRendererDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.SkinnedMeshRendererDefs.RegisterInVM(m_vm);

            Lua.LuaDefs.UiSelectableDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.UiButtonDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.UiToggleDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.UiSliderDefs.RegisterInVM(m_vm);

            // CVR defs
            Lua.LuaDefs.CVRAttachmentDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CVRMirrorDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CVRPointerDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CVRVideoPlayerDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CVRInteractableDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.CVRPickupObjectDefs.RegisterInVM(m_vm);

            // Own defs
            Lua.LuaDefs.DateTimeDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.PlayerDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.LuaScriptDefs.RegisterInVM(m_vm);
            Lua.LuaDefs.UtilityDefs.RegisterInVM(m_vm);
        }

        public void Dispose()
        {
            m_eventFunctions.Clear();
            m_vm?.Dispose();
            m_vm = null;
        }

        public void Execute(string p_code)
        {
            m_vm.Execute(p_code);
        }
        public void Execute(string p_blockName, ref byte[] p_data)
        {
            m_vm.Execute(p_blockName, ref p_data);
        }

        public void CallEvent(ScriptEvent p_event, params object[] p_args)
        {
            if(m_eventFunctions.TryGetValue(p_event, out int l_ref))
                m_vm.CallFunction(l_ref, p_args);
        }

        public void CallEvent(ScriptEvent p_event, List<object> p_result, params object[] p_args)
        {
            if(m_eventFunctions.TryGetValue(p_event, out int l_ref))
                m_vm.CallFunction(l_ref, p_result, p_args);
        }

        public void SetGlobalVariable(string p_name, object p_val) => m_vm.SetGlobalVariable(p_name, p_val);

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
