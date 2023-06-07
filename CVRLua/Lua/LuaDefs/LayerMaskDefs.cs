using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class LayerMaskDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(GetMask), GetMask));
            ms_staticMethods.Add((nameof(LayerToName), LayerToName));
            ms_staticMethods.Add((nameof(NameToLayer), NameToLayer));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(LayerMask), null, null, ms_staticMethods, null, null, null);
        }

        // Static methods
        static int GetMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            List<object> l_args = new List<object>();
            l_argReader.ReadArguments(l_args);
            if(!l_argReader.HasErrors())
            {
                List<string> l_layers = new List<string>();
                foreach(var l_arg in l_args)
                {
                    if((l_arg != null) && (l_arg is string))
                        l_layers.Add((string)l_arg);
                }
                l_argReader.PushInteger(LayerMask.GetMask(l_layers.ToArray()));
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LayerToName(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            int l_layer = 0;
            l_argReader.ReadInteger(ref l_layer);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(LayerMask.LayerToName(l_layer));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int NameToLayer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            string l_layer = "";
            l_argReader.ReadString(ref l_layer);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(LayerMask.NameToLayer(l_layer));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
