using System;

namespace CVRLua.Lua.LuaDefs
{
    static class UtilityDefs
    {
        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterFunction(nameof(Log), Log);
        }

        static int Log(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            string l_text = "";
            l_argReader.ReadString(ref l_text);
            if(!l_argReader.HasErrors())
                Core.Logger?.Msg(l_text);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
