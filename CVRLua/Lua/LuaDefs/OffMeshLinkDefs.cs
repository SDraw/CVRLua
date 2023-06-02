using System;
using System.Collections.Generic;
using UnityEngine.AI;

namespace CVRLua.Lua.LuaDefs
{
    static class OffMeshLinkDefs
    {
        const string c_destroyed = "OffMeshLink is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsOffMeshLink), IsOffMeshLink));

            ms_instanceProperties.Add(("activated", (GetActivated, SetActivated)));
            ms_instanceProperties.Add(("area", (GetArea, SetArea)));
            ms_instanceProperties.Add(("autoUpdatePositions", (GetAutoUpdatePositions, SetAutoUpdatePositions)));
            ms_instanceProperties.Add(("biDirectional", (GetBiDirectional, SetBiDirectional)));
            ms_instanceProperties.Add(("costOverride", (GetCostOverride, SetCostOverride)));
            ms_instanceProperties.Add(("endTransform", (GetEndTransform, SetEndTransform)));
            ms_instanceProperties.Add(("occupied", (GetOccupied, null)));
            ms_instanceProperties.Add(("startTransform", (GetStartTransform, SetStartTransform)));

            BehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(OffMeshLink), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsOffMeshLink(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            l_argReader.ReadNextObject(ref l_link);
            l_argReader.PushBoolean(l_link != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetActivated(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            l_argReader.ReadObject(ref l_link);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_argReader.PushBoolean(l_link.activated);
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
        static int SetActivated(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_link);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_link.activated = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetArea(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            l_argReader.ReadObject(ref l_link);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_argReader.PushInteger(l_link.area);
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
        static int SetArea(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_link);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_link.area = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAutoUpdatePositions(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            l_argReader.ReadObject(ref l_link);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_argReader.PushBoolean(l_link.autoUpdatePositions);
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
        static int SetAutoUpdatePositions(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_link);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_link.autoUpdatePositions = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetBiDirectional(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            l_argReader.ReadObject(ref l_link);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_argReader.PushBoolean(l_link.biDirectional);
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
        static int SetBiDirectional(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_link);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_link.biDirectional = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCostOverride(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            l_argReader.ReadObject(ref l_link);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_argReader.PushNumber(l_link.costOverride);
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
        static int SetCostOverride(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_link);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_link.costOverride = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetEndTransform(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            l_argReader.ReadObject(ref l_link);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                {
                    if(l_link.endTransform != null)
                        l_argReader.PushObject(l_link.endTransform);
                    else
                        l_argReader.PushBoolean(false);
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
            return 1;
        }
        static int SetEndTransform(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            UnityEngine.Transform l_transform = null;
            l_argReader.ReadObject(ref l_link);
            l_argReader.ReadNextObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_link.endTransform = l_transform;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetOccupied(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            l_argReader.ReadObject(ref l_link);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_argReader.PushBoolean(l_link.occupied);
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

        static int GetStartTransform(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            l_argReader.ReadObject(ref l_link);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                {
                    if(l_link.startTransform != null)
                        l_argReader.PushObject(l_link.startTransform);
                    else
                        l_argReader.PushBoolean(false);
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
            return 1;
        }
        static int SetStartTransform(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            OffMeshLink l_link = null;
            UnityEngine.Transform l_transform = null;
            l_argReader.ReadObject(ref l_link);
            l_argReader.ReadNextObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_link != null)
                    l_link.startTransform = l_transform;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
    }
}
