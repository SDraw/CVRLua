using ABI.CCK.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CVRLua
{
    public class LuaScript : MonoBehaviour, System.IDisposable
    {
        public List<TextAsset> Scripts = new List<TextAsset>();
        public List<string> VariableNames = new List<string>();
        public List<string> VariableValues = new List<string>();
        public List<string> VariableObjectNames = new List<string>();
        public List<Object> VariableObjectValues = new List<Object>();

        LuaHandler m_luaHandler = null;

        CVRInteractable m_interactable = null;
        CVRAttachment m_attachment = null;
        Button m_uiButton = null;
        Toggle m_uiToggle = null;
        Slider m_uiSlider = null;

        void Awake()
        {
            if(m_luaHandler == null)
            {
                m_interactable = this.GetComponent<CVRInteractable>();
                if(m_interactable != null)
                {
                    m_interactable.onEnterSeat.AddListener(this.OnEnterSeat);
                    m_interactable.onExitSeat.AddListener(this.OnExitSeat);
                }

                m_attachment = this.GetComponent<CVRAttachment>();
                if(m_attachment != null)
                {
                    m_attachment.onAttach.AddListener(this.OnAttachmentAttach);
                    m_attachment.onDeattach.AddListener(this.OnAttachmentDeattach);
                }

                m_uiButton = this.GetComponent<Button>();
                if(m_uiButton != null)
                    m_uiButton.onClick.AddListener(this.OnButtonClick);

                m_uiToggle = this.GetComponent<Toggle>();
                if(m_uiToggle != null)
                    m_uiToggle.onValueChanged.AddListener(this.OnToggleChange);

                m_uiSlider = this.GetComponent<Slider>();
                if(m_uiSlider != null)
                    m_uiSlider.onValueChanged.AddListener(this.OnSliderChange);

                m_luaHandler = new LuaHandler();
                Core.Instance?.RegisterScript(this);

                m_luaHandler.SetGlobalVariable("this", this.gameObject);
                m_luaHandler.SetGlobalVariable("localPlayer", Players.PlayersManager.GetLocalPlayer());
                m_luaHandler.SetGlobalVariable("modRelease", Core.c_modRelease);

                if((VariableNames.Count > 0) && (VariableValues.Count > 0))
                {
                    for(int i = 0, j = Mathf.Min(VariableNames.Count, VariableValues.Count); i < j; i++)
                        m_luaHandler.Execute(string.Format("{0} = {1}", VariableNames[i], VariableValues[i]));
                }

                if((VariableObjectNames.Count > 0) && (VariableObjectValues.Count > 0))
                {
                    for(int i = 0, j = Mathf.Min(VariableObjectNames.Count, VariableObjectValues.Count); i < j; i++)
                        m_luaHandler.SetGlobalVariable(VariableObjectNames[i], VariableObjectValues[i]);
                }

                foreach(var l_script in Scripts)
                {
                    byte[] l_data = l_script.bytes;
                    m_luaHandler.Execute(string.Format("{0}|{1}", this.name, l_script.name), ref l_data);
                }

                m_luaHandler.ParseEvents();

                Players.PlayersManager.PlayerJoin += this.OnPlayerJoin;
                Players.PlayersManager.PlayerLeft += this.OnPlayerLeft;
            }
        }

        public void Dispose()
        {
            m_luaHandler?.Dispose();
            m_luaHandler = null;
        }

        void Start()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.Start);
            StartCoroutine(CheckEndOfFrame());
        }

        void OnDestroy()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnDestroy);
            Core.Instance?.UnregisterScript(this);

            Players.PlayersManager.PlayerJoin -= this.OnPlayerJoin;
            Players.PlayersManager.PlayerLeft -= this.OnPlayerLeft;
        }

        void Update()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.Update);
        }

        void LateUpdate()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.LateUpdate);
        }

        void FixedUpdate()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.FixedUpdate);
        }

        void OnEnable()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnEnable);
        }

        void OnDisable()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnDisable);
        }

        void OnCollisionEnter(Collision p_col)
        {
            // Add later
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnCollisionEnter, p_col);
        }

        void OnCollisionExit(Collision p_col)
        {
            // Add later
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnCollisionExit, p_col);
        }

        void OnCollisionStay(Collision p_col)
        {
            // Add later
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnCollisionStay, p_col);
        }

        void OnTriggerEnter(Collider p_col)
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnTriggerEnter, p_col, Utils.IsInternal(p_col));
        }

        void OnTriggerExit(Collider p_col)
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnTriggerExit, p_col, Utils.IsInternal(p_col));
        }

        void OnTriggerStay(Collider p_col)
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnTriggerStay, p_col, Utils.IsInternal(p_col));
        }

        void OnAnimatorIK(int p_layer)
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnAnimatorIK, p_layer);
        }

        // GC
        IEnumerator CheckEndOfFrame()
        {
            while(true)
            {
                yield return new WaitForEndOfFrame();
                m_luaHandler?.PerformGC();
            }
        }

        // Custom events
        public void SendScriptMessage(List<object> p_args, List<object> p_result)
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnMessage, p_result, p_args.ToArray());
        }

        // Game events
        internal void OnInteractableGrab(CVRInteractable p_instance)
        {
            if(m_interactable == p_instance)
                m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnInteractableGrab);
        }

        internal void OnInteractableDrop(CVRInteractable p_instance)
        {
            if(m_interactable == p_instance)
                m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnInteractableDrop);
        }

        internal void OnInteractableUp(CVRInteractable p_instance)
        {
            if(m_interactable == p_instance)
                m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnInteractableUp);
        }

        internal void OnInteractableDown(CVRInteractable p_instance)
        {
            if(m_interactable == p_instance)
                m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnInteractableDown);
        }

        internal void OnInteractableGazeEnter(CVRInteractable p_instance)
        {
            if(m_interactable == p_instance)
                m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnInteractableGazeEnter);
        }

        internal void OnInteractableGazeExit(CVRInteractable p_instance)
        {
            if(m_interactable == p_instance)
                m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnInteractableGazeExit);
        }

        void OnEnterSeat()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnEnterSeat);
        }

        void OnExitSeat()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnExitSeat);
        }

        void OnAttachmentAttach()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnAttachmentAttach);
        }

        void OnAttachmentDeattach()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnAttachmentDeattach);
        }

        // Players events
        void OnPlayerJoin(Players.Player p_player)
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnPlayerJoin, p_player);
        }
        void OnPlayerLeft(Players.Player p_player)
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnPlayerLeft, p_player);
        }

        // Unity UI
        void OnButtonClick()
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnButtonClick);
        }

        void OnToggleChange(bool p_state)
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnToggleChange, p_state);
        }

        void OnSliderChange(float p_value)
        {
            m_luaHandler?.CallEvent(LuaHandler.ScriptEvent.OnSliderChange, p_value);
        }
    }
}
