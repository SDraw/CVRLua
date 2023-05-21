using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVRLua.Lua.LuaDefs
{
    static class LuaScriptDefs
    {
        static internal void Init(LuaVM p_vm)
        {
            p_vm.AddClassStart(nameof(LuaScript), null);
            p_vm.AddClassMethod(nameof(SendScriptMessage), SendScriptMessage);
            p_vm.AddClassFinish();
        }

        static int SendScriptMessage(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            string l_msg = "";
            LuaScript l_script = null;
            l_argReader.ReadObject(ref l_script);
            l_argReader.ReadString(ref l_msg);
            if(!l_argReader.HasErrors())
            {
                if(l_script != null)
                {
                    l_script.SendScriptMessage(l_msg);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError("LuaScript is destroyed");
                }
            }
            else
                l_argReader.PushBoolean(false);
            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
