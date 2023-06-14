using ABI.CCK.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVRLua.Lua.LuaDefs
{
    static class CVRInteractableDefs
    {
        const string c_destroyed = "CVRInteractable is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_instanceProperties.Add(("isAttached", (GetIsAttached, null)));
            ms_instanceProperties.Add(("isHeld", (GetIsHeld, null)));
            ms_instanceProperties.Add(("isLookedAt", (GetIsLookedAt, null)));
            ms_instanceProperties.Add(("isSitting", (GetIsSitting, null)));
            ms_instanceProperties.Add(("tooltip", (GetTooltip, SetTooltip)));

            ms_instanceMethods.Add((nameof(CustomTrigger), CustomTrigger));

            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CVRInteractable), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsCVRInteractable), IsCVRInteractable);
        }

        // Static methods
        static int IsCVRInteractable(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRInteractable l_interactable = null;
            l_argReader.ReadNextObject(ref l_interactable);
            l_argReader.PushBoolean(l_interactable != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetIsAttached(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRInteractable l_interactable = null;
            l_argReader.ReadObject(ref l_interactable);
            if(!l_argReader.HasErrors())
            {
                if(l_interactable != null)
                    l_argReader.PushBoolean(l_interactable.isAttached);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetIsHeld(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRInteractable l_interactable = null;
            l_argReader.ReadObject(ref l_interactable);
            if(!l_argReader.HasErrors())
            {
                if(l_interactable != null)
                    l_argReader.PushBoolean(l_interactable.isHeld);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetIsLookedAt(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRInteractable l_interactable = null;
            l_argReader.ReadObject(ref l_interactable);
            if(!l_argReader.HasErrors())
            {
                if(l_interactable != null)
                    l_argReader.PushBoolean(l_interactable.isLookedAt);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetIsSitting(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRInteractable l_interactable = null;
            l_argReader.ReadObject(ref l_interactable);
            if(!l_argReader.HasErrors())
            {
                if(l_interactable != null)
                    l_argReader.PushBoolean(l_interactable.isSitting);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetTooltip(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRInteractable l_interactable = null;
            l_argReader.ReadObject(ref l_interactable);
            if(!l_argReader.HasErrors())
            {
                if(l_interactable != null)
                    l_argReader.PushString(l_interactable.tooltip);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetTooltip(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRInteractable l_interactable = null;
            string l_tooltip = "";
            l_argReader.ReadObject(ref l_interactable);
            l_argReader.ReadString(ref l_tooltip);
            if(!l_argReader.HasErrors())
            {
                if(l_interactable != null)
                    l_interactable.tooltip = l_tooltip;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int CustomTrigger(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRInteractable l_interactable = null;
            l_argReader.ReadObject(ref l_interactable);
            if(!l_argReader.HasErrors())
            {
                if(l_interactable != null)
                {
                    l_interactable.CustomTrigger();
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
