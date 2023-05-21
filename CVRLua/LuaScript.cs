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

        void Awake()
        {
            m_luaHandler = new LuaHandler(this.gameObject, ((ScriptFile != null) ? ScriptFile.name : this.gameObject.name));
            Core.Instance?.RegisterScript(this);

            if((VariableNames.Count > 0) && (VariableValues.Count > 0))
            {
                for(int i = 0, j = Mathf.Min(VariableNames.Count, VariableValues.Count); i < j; i++)
                    m_luaHandler.SetGlobalVariable(VariableNames[i], VariableValues[i]);
            }

            if(ScriptFile != null)
                m_luaHandler.Execute(ScriptFile.text);

            m_luaHandler.ParseEvents();
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.Start);

            StartCoroutine(CheckEndOfFrame());
        }

        void OnDestroy()
        {
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

        void OnGUI()
        {
            //m_luaHandler.CallEvent("OnGUI");
        }

        void OnDisable()
        {
            //m_luaHandler.CallEvent("OnDisable");
        }

        void OnEnable()
        {
            //m_luaHandler.CallEvent("OnEnable");
        }

        public void SendScriptMessage(string p_msg)
        {
            m_luaHandler.CallEvent(LuaHandler.ScriptEvent.OnScriptMessage, p_msg);
        }

        IEnumerator CheckEndOfFrame()
        {
            while(true)
            {
                yield return new WaitForEndOfFrame();
                m_luaHandler.PerformGC();
            }
        }
    }
}
