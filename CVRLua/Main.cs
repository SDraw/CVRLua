using System.Collections.Generic;

namespace CVRLua
{
    public class Core : MelonLoader.MelonMod
    {
        static public Core Instance { get; private set; } = null;

        readonly List<LuaScript> m_scripts = null;

        internal Core()
        {
            m_scripts = new List<LuaScript>();
        }

        public override void OnInitializeMelon()
        {
            if(Instance == null)
                Instance = this;

            LuaHandler.Init();
        }

        public override void OnDeinitializeMelon()
        {
            if(Instance == this)
                Instance = null;
        }

        internal void RegisterScript(LuaScript p_script)
        {
            m_scripts.Add(p_script);
        }

        internal void UnregisterScript(LuaScript p_script)
        {
            m_scripts.Remove(p_script);
        }
    }
}
