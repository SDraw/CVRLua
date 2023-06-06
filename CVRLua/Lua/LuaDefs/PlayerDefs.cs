using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class PlayerDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticProperties.Add(("localPlayer", (GetLocalPlayer, null)));
            ms_staticProperties.Add(("remotePlayers", (GetRemotePlayers, null)));
            ms_staticProperties.Add(("allPlayers", (GetAllPlayers, null)));

            ms_staticMethods.Add((nameof(Find), Find));
            ms_staticMethods.Add((nameof(IsPlayer), IsPlayer));

            ms_metaMethods.Add(("__tostring", ToString));
            ms_metaMethods.Add(("__eq", Equal));

            ms_instanceProperties.Add(("isLocal", (GetIsLocal, null)));
            ms_instanceProperties.Add(("isRemote", (GetIsRemote, null)));
            ms_instanceProperties.Add(("isConnected", (GetIsConnected, null)));
            ms_instanceProperties.Add(("name", (GetName, null)));
            ms_instanceProperties.Add(("uuid", (GetUuid, null)));
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
            ms_instanceProperties.Add(("isGrounded", (GetGrounded, null)));
            ms_instanceProperties.Add(("leftHandPosition", (GetLeftHandPosition, null)));
            ms_instanceProperties.Add(("leftHandRotation", (GetLeftHandRotation, null)));
            ms_instanceProperties.Add(("rightHandPosition", (GetRightHandPosition, null)));
            ms_instanceProperties.Add(("rightHandRotation", (GetRightHandRotation, null)));
            ms_instanceProperties.Add(("leftHandGesture", (GetLeftHandGesture, null)));
            ms_instanceProperties.Add(("rightHandGesture", (GetRightHandGesture, null)));
            ms_instanceProperties.Add(("zoom", (GetZoom, null)));
            ms_instanceProperties.Add(("zoomFactor", (GetZoomFactor, null)));
            ms_instanceProperties.Add(("movementVector", (GetMovementVector, null)));
            ms_instanceProperties.Add(("lookVector", (GetLookVector, null)));
            ms_instanceProperties.Add(("individualFingerTracking", (GetIndividualFingerTracking, null)));
            ms_instanceProperties.Add(("fingerCurls", (GetFingerCurls, null)));

            ms_instanceMethods.Add((nameof(Teleport), Teleport));
            ms_instanceMethods.Add((nameof(SetImmobilized), SetImmobilized));
            ms_instanceMethods.Add((nameof(Respawn), Respawn));
            ms_instanceMethods.Add((nameof(GetBonePosition), GetBonePosition));
            ms_instanceMethods.Add((nameof(GetBoneRotation), GetBoneRotation));
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Players.Player), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static properties
        static int GetLocalPlayer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(Players.PlayersManager.GetLocalPlayer());
            return 1;
        }

        static int GetRemotePlayers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            var l_list = Players.PlayersManager.GetRemotePlayers();
            l_argReader.PushTable(l_list);
            return 1;
        }

        static int GetAllPlayers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            var l_list = Players.PlayersManager.GetAllPlayers();
            l_argReader.PushTable(l_list);
            return 1;
        }

        // Static methods
        static int Find(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            if(l_argReader.IsNextString())
            {
                string l_id = "";
                l_argReader.ReadString(ref l_id);
                if(!l_argReader.HasErrors())
                {
                    Players.Player l_player = Players.PlayersManager.GetFromId(l_id);
                    if(l_player != null)
                        l_argReader.PushObject(l_player);
                    else
                        l_argReader.PushBoolean(false);
                }
                else
                    l_argReader.PushBoolean(false);
            }
            else if(l_argReader.IsNextObject())
            {
                UnityEngine.GameObject l_obj = null;
                l_argReader.ReadObject(ref l_obj);
                if(!l_argReader.HasErrors())
                {
                    if(l_obj != null)
                    {
                        Players.Player l_player = Players.PlayersManager.GetFromGameObject(l_obj);
                        if(l_player != null)
                            l_argReader.PushObject(l_player);
                        else
                            l_argReader.PushBoolean(false);
                    }
                    else
                    {
                        l_argReader.SetError("GameObject is destroyed");
                        l_argReader.PushBoolean(false);
                    }
                }
                else
                    l_argReader.PushBoolean(false);
            }
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int IsPlayer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadNextObject(ref l_player);
            l_argReader.PushBoolean(l_player != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int ToString(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
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
            Players.Player l_playerA = null;
            Players.Player l_playerB = null;
            l_argReader.ReadObject(ref l_playerA);
            l_argReader.ReadObject(ref l_playerB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Players.Player.Compare(l_playerA, l_playerB));
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetIsLocal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsLocal());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetIsRemote(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsRemote());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetIsConnected(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsConnected());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetName(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetName(out string l_name))
                l_argReader.PushString(l_name);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int GetUuid(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetId(out string l_id))
                l_argReader.PushString(l_id);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetPosition(out UnityEngine.Vector3 l_pos))
                l_argReader.PushObject(new Wrappers.Vector3(l_pos));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetRotation(out UnityEngine.Quaternion l_rot))
                l_argReader.PushObject(new Wrappers.Quaternion(l_rot));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetAvatarHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetAvatarHeight(out float l_height))
                l_argReader.PushNumber(l_height);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetAvatarScale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetAvatarScale(out float l_scale))
                l_argReader.PushNumber(l_scale);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetPlayerHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetPlayerHeight(out float l_height))
                l_argReader.PushNumber(l_height);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetPlayerScale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetPlayerScale(out float l_scale))
                l_argReader.PushNumber(l_scale);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetCameraPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetCameraPosition(out var l_pos))
                l_argReader.PushObject(new Wrappers.Vector3(l_pos));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetCameraRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetCameraRotation(out var l_rot))
                l_argReader.PushObject(new Wrappers.Quaternion(l_rot));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetInVR(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsJumping());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetGrounded(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.IsGrounded());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLeftHandPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetLeftHandPosition(out var l_pos))
                l_argReader.PushObject(new Wrappers.Vector3(l_pos));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLeftHandRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetLeftHandRotation(out var l_rot))
                l_argReader.PushObject(new Wrappers.Quaternion(l_rot));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRightHandPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetRightHandPosition(out var l_pos))
                l_argReader.PushObject(new Wrappers.Vector3(l_pos));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRightHandRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetRightHandRotation(out var l_rot))
                l_argReader.PushObject(new Wrappers.Quaternion(l_rot));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLeftHandGesture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetLeftHandGetsture(out float l_gesture))
                l_argReader.PushNumber(l_gesture);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRightHandGesture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetRightHandGetsture(out float l_gesture))
                l_argReader.PushNumber(l_gesture);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetZoom(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
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
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetZoomFactor(out float l_factor))
                l_argReader.PushNumber(l_factor);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetMovementVector(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetMovementVector(out var l_move))
                l_argReader.PushObject(new Wrappers.Vector2(l_move));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLookVector(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetLookVector(out var l_look))
                l_argReader.PushObject(new Wrappers.Vector2(l_look));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetIndividualFingerTracking(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_player.GetIndividualFingerTracking());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetFingerCurls(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors() && l_player.GetFingerCurls(out var l_curls))
                l_argReader.PushTable(l_curls);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance methods
        static int Teleport(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
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
            Players.Player l_player = null;
            UnityEngine.HumanBodyBones l_bone = UnityEngine.HumanBodyBones.LastBone;
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadEnum(ref l_bone);
            if(!l_argReader.HasErrors() && l_player.GetBonePosition(l_bone, out var l_pos))
                l_argReader.PushObject(new Wrappers.Vector3(l_pos));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GetBoneRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Players.Player l_player = null;
            UnityEngine.HumanBodyBones l_bone = UnityEngine.HumanBodyBones.LastBone;
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadEnum(ref l_bone);
            if(!l_argReader.HasErrors() && l_player.GetBoneRotation(l_bone, out var l_rot))
                l_argReader.PushObject(new Wrappers.Quaternion(l_rot));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
