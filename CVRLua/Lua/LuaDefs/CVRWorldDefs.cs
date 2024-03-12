using ABI.CCK.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CVRLua.Lua.LuaDefs
{
    static class CVRWorldDefs
    {
        const string c_destroyed = "CVRWorld is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        private static MethodInfo minf_SetupWorldRules;

        internal static void Init()
        {
            ms_instanceProperties.Add(("baseMovementSpeed", (GetBaseMovementSpeed, SetBaseMovementSpeed)));
            ms_instanceProperties.Add(("sprintMultiplier", (GetSprintMultiplier, SetSprintMultiplier)));
            ms_instanceProperties.Add(("strafeMultiplier", (GetStrafeMultiplier, SetStrafeMultiplier)));
            ms_instanceProperties.Add(("crouchMultiplier", (GetCrouchMultiplier, SetCrouchMultiplier)));
            ms_instanceProperties.Add(("proneMultiplier", (GetProneMultiplier, SetProneMultiplier)));
            ms_instanceProperties.Add(("flyMultiplier", (GetFlyMultiplier, SetFlyMultiplier)));
            ms_instanceProperties.Add(("inAirMovementMultiplier", (GetInAirMovementMultiplier, SetInAirMovementMultiplier)));
            ms_instanceProperties.Add(("playerGravity", (GetPlayerGravity, SetPlayerGravity)));
            ms_instanceProperties.Add(("jumpHeight", (GetJumpHeight, SetJumpHeight)));
            ms_instanceProperties.Add(("objectGravity", (GetObjectGravity, SetObjectGravity)));
            ms_instanceProperties.Add(("respawnHeight", (GetRespawnHeight, SetRespawnHeight)));
            ms_instanceProperties.Add(("allowSpawnables", (GetAllowSpawnables, SetAllowSpawnables)));
            ms_instanceProperties.Add(("allowPortals", (GetAllowPortals, SetAllowPortals)));
            ms_instanceProperties.Add(("allowFlying", (GetAllowFlying, SetAllowFlying)));
            ms_instanceProperties.Add(("allowZoom", (GetAllowZoom, SetAllowZoom)));
            ms_instanceProperties.Add(("showNameplates", (GetShowNameplates, SetShowNameplates)));


            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, null);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CVRWorld), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, null);
            p_vm.RegisterFunction(nameof(IsCVRWorld), IsCVRWorld);
        }

        // Static methods
        static int IsCVRWorld(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadNextObject(ref l_world);
            l_argReader.PushBoolean(l_world != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetBaseMovementSpeed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.baseMovementSpeed);
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
        static int SetBaseMovementSpeed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_baseMovementSpeed = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_baseMovementSpeed);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetBaseMovementSpeed(l_baseMovementSpeed);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetSprintMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.sprintMultiplier);
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
        static int SetSprintMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_sprintMultiplier = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_sprintMultiplier);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetSprintMultiplier(l_sprintMultiplier);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetStrafeMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.strafeMultiplier);
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
        static int SetStrafeMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_strafeMultiplier = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_strafeMultiplier);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetStrafeMultiplier(l_strafeMultiplier);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetCrouchMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.crouchMultiplier);
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
        static int SetCrouchMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_crouchMultiplier = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_crouchMultiplier);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetCrouchMultiplier(l_crouchMultiplier);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetProneMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.proneMultiplier);
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
        static int SetProneMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_proneMultiplier = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_proneMultiplier);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetProneMultiplier(l_proneMultiplier);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetFlyMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.flyMultiplier);
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
        static int SetFlyMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_flyMultiplier = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_flyMultiplier);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetFlyMultiplier(l_flyMultiplier);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetInAirMovementMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.inAirMovementMultiplier);
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
        static int SetInAirMovementMultiplier(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_inAirMovementMultiplier = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_inAirMovementMultiplier);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetInAirMovementMultiplier(l_inAirMovementMultiplier);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetPlayerGravity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.gravity);
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
        static int SetPlayerGravity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_gravity = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_gravity);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetGravity(l_gravity);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetJumpHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.jumpHeight);
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
        static int SetJumpHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_height = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_height);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetJumpHeight(l_height);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetObjectGravity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushNumber(l_world.objectGravity);
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
        static int SetObjectGravity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            float l_gravity = 0f;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadNumber(ref l_gravity);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_world.SetObjectGravity(l_gravity);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetRespawnHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushInteger(l_world.respawnHeightY);
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
        static int SetRespawnHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            int l_height = -25;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadInteger(ref l_height);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                {
                    l_world.respawnHeightY = l_height;
                    l_world.ApplyMovementSettings();
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetAllowSpawnables(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushBoolean(l_world.allowSpawnables);
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
        static int SetAllowSpawnables(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            bool l_allowSpawnables = false;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadBoolean(ref l_allowSpawnables);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                {
                    l_world.allowSpawnables = l_allowSpawnables;
                    if (minf_SetupWorldRules == null)
                        minf_SetupWorldRules = typeof(CVRWorld).GetMethod("SetupWorldRules", BindingFlags.NonPublic | BindingFlags.Instance);
                    minf_SetupWorldRules.Invoke(l_world, null);
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetAllowPortals(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushBoolean(l_world.allowPortals);
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
        static int SetAllowPortals(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            bool l_allowPortals = false;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadBoolean(ref l_allowPortals);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                {
                    l_world.allowPortals = l_allowPortals;
                    if (minf_SetupWorldRules == null)
                        minf_SetupWorldRules = typeof(CVRWorld).GetMethod("SetupWorldRules", BindingFlags.NonPublic | BindingFlags.Instance);
                    minf_SetupWorldRules.Invoke(l_world, null);
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetAllowFlying(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushBoolean(l_world.allowFlying);
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
        static int SetAllowFlying(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            bool l_allowFlying = false;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadBoolean(ref l_allowFlying);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                {
                    l_world.allowFlying = l_allowFlying;
                    if (minf_SetupWorldRules == null)
                        minf_SetupWorldRules = typeof(CVRWorld).GetMethod("SetupWorldRules", BindingFlags.NonPublic | BindingFlags.Instance);
                    minf_SetupWorldRules.Invoke(l_world, null);
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetAllowZoom(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushBoolean(l_world.enableZoom);
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
        static int SetAllowZoom(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            bool l_allowZoom = false;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadBoolean(ref l_allowZoom);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                {
                    l_world.enableZoom = l_allowZoom;
                    if (minf_SetupWorldRules == null)
                        minf_SetupWorldRules = typeof(CVRWorld).GetMethod("SetupWorldRules", BindingFlags.NonPublic | BindingFlags.Instance);
                    minf_SetupWorldRules.Invoke(l_world, null);
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
        static int GetShowNameplates(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            l_argReader.ReadObject(ref l_world);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                    l_argReader.PushBoolean(l_world.showNamePlates);
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
        static int SetShowNameplates(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRWorld l_world = null;
            bool l_showNameplates = false;
            l_argReader.ReadObject(ref l_world);
            l_argReader.ReadBoolean(ref l_showNameplates);
            if (!l_argReader.HasErrors())
            {
                if (l_world != null)
                {
                    l_world.showNamePlates = l_showNameplates;
                    if (minf_SetupWorldRules == null)
                        minf_SetupWorldRules = typeof(CVRWorld).GetMethod("SetupWorldRules", BindingFlags.NonPublic | BindingFlags.Instance);
                    minf_SetupWorldRules.Invoke(l_world, null);
                    ABI_RC.Core.Player.CVRPlayerManager.Instance.ReloadAllNameplates();
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }
    }
}
