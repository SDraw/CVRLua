using ABI.CCK.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class CVRPickupObjectDefs
    {
        const string c_destroyed = "CVRPickupObject is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_instanceProperties.Add(("gripType", (GetGripType, SetGripType)));
            ms_instanceProperties.Add(("gripOrigin", (GetGripOrigin, SetGripOrigin)));
            ms_instanceProperties.Add(("disallowTheft", (GetDisallowTheft, SetDisallowTheft)));
            ms_instanceProperties.Add(("maximumGrabDistance", (GetMaximumGrabDistance, SetMaximumGrabDistance)));
            ms_instanceProperties.Add(("autoHold", (GetAutoHold, SetAutoHold)));
            ms_instanceProperties.Add(("ikReference", (GetIkReference, SetIkReference)));
            ms_instanceProperties.Add(("isGrabbedByMe", (GetIsGrabbedByMe, null)));
            ms_instanceProperties.Add(("isTeleGrabbed", (GetIsTeleGrabbed, null)));
            ms_instanceProperties.Add(("grabbedBy", (GetGrabbedBy, null)));

            ms_instanceMethods.Add((nameof(Drop), Drop));
            ms_instanceMethods.Add((nameof(Respawn), Respawn));

            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CVRPickupObject), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsCVRPickupObject), IsCVRPickupObject);
        }

        // Static methods
        static int IsCVRPickupObject(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadNextObject(ref l_pickup);
            l_argReader.PushBoolean(l_pickup != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetGripType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_argReader.PushString(l_pickup.gripType.ToString());
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
        static int SetGripType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            CVRPickupObject.GripType l_type = CVRPickupObject.GripType.Free;
            l_argReader.ReadObject(ref l_pickup);
            l_argReader.ReadEnum(ref l_type);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_pickup.gripType = l_type;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetGripOrigin(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                {
                    if(l_pickup.gripOrigin != null)
                        l_argReader.PushObject(l_pickup.gripOrigin);
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
        static int SetGripOrigin(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            Transform l_origin = null;
            l_argReader.ReadObject(ref l_pickup);
            l_argReader.ReadNextObject(ref l_origin);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                {
                    if(l_origin != null)
                        l_pickup.gripOrigin = l_origin;
                    else
                        l_pickup.gripOrigin = null;
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDisallowTheft(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_argReader.PushBoolean(l_pickup.disallowTheft);
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
        static int SetDisallowTheft(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_pickup);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_pickup.disallowTheft = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMaximumGrabDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_argReader.PushNumber(l_pickup.maximumGrabDistance);
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
        static int SetMaximumGrabDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            float l_distance = 0f;
            l_argReader.ReadObject(ref l_pickup);
            l_argReader.ReadNumber(ref l_distance);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_pickup.maximumGrabDistance = l_distance;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAutoHold(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_argReader.PushBoolean(l_pickup.autoHold);
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
        static int SetAutoHold(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_pickup);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_pickup.autoHold = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIkReference(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                {
                    if(l_pickup.ikReference != null)
                        l_argReader.PushObject(l_pickup.ikReference);
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
        static int SetIkReference(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            Transform l_ref = null;
            l_argReader.ReadObject(ref l_pickup);
            l_argReader.ReadNextObject(ref l_ref);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                {
                    if(l_ref != null)
                        l_pickup.ikReference = l_ref;
                    else
                        l_pickup.ikReference = null;
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIsGrabbedByMe(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_argReader.PushBoolean(l_pickup.IsGrabbedByMe());
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

        static int GetIsTeleGrabbed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_argReader.PushBoolean(l_pickup.isTeleGrabbed);
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

        static int GetGrabbedBy(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                    l_argReader.PushString(l_pickup.grabbedBy);

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

        // Instance methods
        static int Drop(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                {
                    l_pickup.Drop();
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

        static int Respawn(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRPickupObject l_pickup = null;
            l_argReader.ReadObject(ref l_pickup);
            if(!l_argReader.HasErrors())
            {
                if(l_pickup != null)
                {
                    l_pickup.ResetLocation();
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
