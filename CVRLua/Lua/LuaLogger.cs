namespace CVRLua.Lua
{
    static class LuaLogger
    {
        public static void Log(object p_obj) => MelonLoader.MelonLogger.Warning(p_obj);
        public static void Log(string p_txt, params object[] p_args) => MelonLoader.MelonLogger.Warning(p_txt, p_args);
        public static void Log(string p_msg) => MelonLoader.MelonLogger.Warning(p_msg);
    }
}
