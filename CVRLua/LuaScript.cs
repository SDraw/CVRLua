using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua
{
    public class LuaScript : MonoBehaviour
    {
        public TextAsset ScriptFile;

        public List<string> VariableNames = new List<string>();
        public List<GameObject> VariableValues = new List<GameObject>();

        LuaHandler m_luaHandler = null;
        Wrappers.LocalPlayer m_localPlayer = null;

        void Awake()
        {
            m_localPlayer = new Wrappers.LocalPlayer();

            m_luaHandler = new LuaHandler((ScriptFile != null) ? ScriptFile.name : this.gameObject.name);
            Core.Instance?.RegisterScript(this);

            if((VariableNames.Count > 0) && (VariableValues.Count > 0))
            {
                for(int i = 0, j = Mathf.Min(VariableNames.Count, VariableValues.Count); i < j; i++)
                    m_luaHandler.SetGlobalVariable(VariableNames[i], VariableValues[i]);
            }

            m_luaHandler.SetGlobalVariable("this", this.gameObject);
            m_luaHandler.SetGlobalVariable("localPlayer", m_localPlayer);

            if(ScriptFile != null)
                m_luaHandler.Execute(ScriptFile.text);

            m_luaHandler.ParseEvents();
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.Start);

            StartCoroutine(CheckEndOfFrame());
        }

        void OnDestroy()
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnDestroy);
            Core.Instance?.UnregisterScript(this);
        }

        void Update()
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.Update);
        }

        void LateUpdate()
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.LateUpdate);
        }

        void FixedUpdate()
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.FixedUpdate);
        }

        void OnEnable()
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnEnable);
        }

        void OnDisable()
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnDisable);
        }

        void OnGUI()
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnGUI);
        }

        void OnCollisionEnter(Collision p_col)
        {
            // Add later
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnCollisionEnter);
        }

        void OnCollisionExit(Collision p_col)
        {
            // Add later
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnCollisionExit);
        }

        void OnCollisionStay(Collision p_col)
        {
            // Add later
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnCollisionStay);
        }

        void OnTriggerEnter(Collider p_col)
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnTriggerEnter, p_col.name, p_col.GetInstanceID(), Utils.IsInternal(p_col));
        }

        void OnTriggerExit(Collider p_col)
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnTriggerExit, p_col.name, p_col.GetInstanceID(), Utils.IsInternal(p_col));
        }

        void OnTriggerStay(Collider p_col)
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnTriggerStay, p_col.name, p_col.GetInstanceID(), Utils.IsInternal(p_col));
        }

        IEnumerator CheckEndOfFrame()
        {
            while(true)
            {
                yield return new WaitForEndOfFrame();
                m_luaHandler.PerformGC();
            }
        }

        public void SendScriptMessage(params object[] p_args)
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnScriptMessage, p_args);
        }
    }
}
