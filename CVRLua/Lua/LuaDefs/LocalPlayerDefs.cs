using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class LocalPlayerDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsLocalPlayer), IsLocalPlayer));

            ms_metaMethods.Add(("__tostring", ToString));
            ms_metaMethods.Add(("__eq", Equal));

            ms_instanceProperties.Add(("name", (GetName, null)));
            ms_instanceProperties.Add(("position", (GetPosition, null)));
            ms_instanceProperties.Add(("rotation", (GetRotation, null)));
            ms_instanceProperties.Add(("avatarHeight", (GetAvatarHeight, null)));
            ms_instanceProperties.Add(("avatarScale", (GetAvatarScale, null)));
            ms_instanceProperties.Add(("playerHeight", (GetPlayerHeight, null)));
            ms_instanceProperties.Add(("playerScale", (GetPlayerScale, null)));
            ms_instanceProperties.Add(("cameraPosition", (GetCameraPosition, null)));
            ms_instanceProperties.Add(("cameraRotation", (GetCameraRotation, null)));
            ms_instanceProperties.Add(("inVR", (GetInVR, null)));
            ms_instanceProperties.Add(("inFBT", (GetFBT, null)));
            ms_instanceProperties.Add(("hasAvatar", (GetAvatar, null)));
            ms_instanceProperties.Add(("isAvatarLoading", (GetAvatarLoading, null)));
            ms_instanceProperties.Add(("isAvatarHumanoid", (GetAvatarHumanoid, null)));
            ms_instanceProperties.Add(("isFlying", (GetFlying, null)));
            ms_instanceProperties.Add(("isCrouching", (GetCrouching, null)));
            ms_instanceProperties.Add(("isProning", (GetProning, null)));
            ms_instanceProperties.Add(("isSitting", (GetSitting, null)));
            ms_instanceProperties.Add(("isSprinting", (GetSprinting, null)));
            ms_instanceProperties.Add(("isJumping", (GetJumping, null)));
            ms_instanceProperties.Add(("leftHandPosition", (GetLeftHandPosition, null)));
            ms_instanceProperties.Add(("leftHandRotation", (GetLeftHandRotation, null)));
            ms_instanceProperties.Add(("rightHandPosition", (GetRightHandPosition, null)));
            ms_instanceProperties.Add(("rightHandRotation", (GetRightHandRotation, null)));
            ms_instanceProperties.Add(("leftHandGesture", (GetLeftHandGesture, null)));
            ms_instanceProperties.Add(("rightHandGesture", (GetRightHandGesture, null)));
            ms_instanceProperties.Add(("zoom", (GetZoom, null)));
            ms_instanceProperties.Add(("zoomFactor", (GetZoomFactor, null)));
            ms_instanceProperties.Add(("movementVector", (GetMovementVector, null)));

            ms_instanceMethods.Add((nameof(Teleport), Teleport));
            ms_instanceMethods.Add((nameof(SetImmobilized), SetImmobilized));
            ms_instanceMethods.Add((nameof(Respawn), Respawn));
            ms_instanceMethods.Add((nameof(GetBonePosition), GetBonePosition));
            ms_instanceMethods.Add((nameof(GetBoneRotation), GetBoneRotation));
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.LocalPlayer), Constructor, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Constructor
        static int Constructor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = new Wrappers.LocalPlayer();
            l_argReader.PushObject(l_player);
            return l_argReader.GetReturnValue();
        }

        // Static methods
        static int IsLocalPlayer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadNextObject(ref l_player);
            l_argReader.PushBoolean(l_player != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int ToString(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(l_argReader.HasErrors())
                l_argReader.PushString(l_player.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Equal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_playerA = null;
            Wrappers.LocalPlayer l_playerB = null;
            l_argReader.ReadObject(ref l_playerA);
            l_argReader.ReadObject(ref l_playerB);
            l_argReader.PushBoolean(!l_argReader.HasErrors());
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetName(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_player.GetName());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_player.GetPosition()));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(l_player.GetRotation()));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetAvatarHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_player.GetAvatarHeight());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetAvatarScale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_player.GetAvatarScale());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetPlayerHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_player.GetPlayerHeight());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetPlayerScale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_player.GetPlayerScale());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetCameraPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_player.GetCameraPosition()));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetCameraRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(l_player.GetCameraRotation()));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetInVR(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsInVR());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetFBT(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsInFullbody());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetAvatar(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.HasAvatar());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetAvatarLoading(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsAvatarLoading());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetAvatarHumanoid(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsAvatarHumanoid());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetFlying(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsFlying());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetCrouching(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsCrouching());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetProning(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsProning());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetSitting(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsSitting());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetSprinting(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsSprinting());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetJumping(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsJumping());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLeftHandPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_player.GetLeftHandPosition()));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLeftHandRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(l_player.GetLeftHandRotation()));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRightHandPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_player.GetRightHandPosition()));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRightHandRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(l_player.GetRightHandRotation()));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLeftHandGesture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_player.GetLeftHandGetsture());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRightHandGesture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_player.GetRightHandGetsture());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetZoom(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsZooming());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetZoomFactor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_player.GetZoomFactor());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetMovementVector(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector2(l_player.GetMovementVector()));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance methods
        static int Teleport(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            Wrappers.Vector3 l_pos = null;
            Wrappers.Quaternion l_rot = null;
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadObject(ref l_pos);
            l_argReader.ReadNextObject(ref l_rot);
            if(!l_argReader.HasErrors())
            {
                if(l_rot != null)
                    l_player.Teleport(l_pos.m_vec, l_rot.m_quat);
                else
                    l_player.Teleport(l_pos.m_vec);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetImmobilized(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                l_player.SetImmobilized(l_state);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Respawn(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                l_player.Respawn();
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GetBonePosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            UnityEngine.HumanBodyBones l_bone = UnityEngine.HumanBodyBones.LastBone;
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadEnum(ref l_bone);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_player.GetBonePosition(l_bone)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GetBoneRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_player = null;
            UnityEngine.HumanBodyBones l_bone = UnityEngine.HumanBodyBones.LastBone;
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadEnum(ref l_bone);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(l_player.GetBoneRotation(l_bone)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
