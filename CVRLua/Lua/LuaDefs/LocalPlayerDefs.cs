using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class LocalPlayerDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add("position", (GetPosition, null));
            ms_instanceProperties.Add("rotation", (GetRotation, null));
            ms_instanceProperties.Add("avatarHeight", (GetAvatarHeight, null));
            ms_instanceProperties.Add("avatarScale", (GetAvatarScale, null));
            ms_instanceProperties.Add("playerHeight", (GetPlayerHeight, null));
            ms_instanceProperties.Add("playerScale", (GetPlayerScale, null));
            ms_instanceProperties.Add("cameraPosition", (GetCameraPosition, null));
            ms_instanceProperties.Add("cameraRotation", (GetCameraRotation, null));
            ms_instanceProperties.Add("inVR", (GetInVR, null));
            ms_instanceProperties.Add("inFBT", (GetFBT, null));
            ms_instanceProperties.Add("isAvatarLoading", (GetAvatarLoading, null));
            ms_instanceProperties.Add("isFlying", (GetFlying, null));
            ms_instanceProperties.Add("isCrouching", (GetCrouching, null));
            ms_instanceProperties.Add("isProning", (GetProning, null));
            ms_instanceProperties.Add("isSitting", (GetSitting, null));
            ms_instanceProperties.Add("isSprinting", (GetSprinting, null));
            ms_instanceProperties.Add("leftHandPosition", (GetLeftHandPosition, null));
            ms_instanceProperties.Add("leftHandRotation", (GetLeftHandRotation, null));
            ms_instanceProperties.Add("rightHandPosition", (GetRightHandPosition, null));
            ms_instanceProperties.Add("rightHandRotation", (GetRightHandRotation, null));
            ms_instanceProperties.Add("leftHandGesture", (GetLeftHandGesture, null));
            ms_instanceProperties.Add("rightHandGesture", (GetRightHandGesture, null));
            ms_instanceProperties.Add("zoom", (GetZoom, null));

            ms_instanceMethods.Add(nameof(Teleport), Teleport);
            ms_instanceMethods.Add(nameof(SetImmobilized), SetImmobilized);
            ms_instanceMethods.Add(nameof(GetBonePosition), GetBonePosition);
            ms_instanceMethods.Add(nameof(GetBoneRotation), GetBoneRotation);
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.LocalPlayer), null, ms_metaMethods, null, null, InstanceGet, InstanceSet);
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

        // Instance properties
        static void GetPosition(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as Wrappers.LocalPlayer).GetPosition()));
        }

        static void GetRotation(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Quaternion((p_obj as Wrappers.LocalPlayer).GetRotation()));
        }

        static void GetAvatarHeight(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.LocalPlayer).GetAvatarHeight());
        }

        static void GetAvatarScale(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.LocalPlayer).GetAvatarScale());
        }

        static void GetPlayerHeight(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.LocalPlayer).GetPlayerHeight());
        }

        static void GetPlayerScale(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.LocalPlayer).GetPlayerScale());
        }

        static void GetCameraPosition(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as Wrappers.LocalPlayer).GetCameraPosition()));
        }

        static void GetCameraRotation(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Quaternion((p_obj as Wrappers.LocalPlayer).GetCameraRotation()));
        }

        static void GetInVR(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Wrappers.LocalPlayer).IsInVR());
        }

        static void GetFBT(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Wrappers.LocalPlayer).IsInFullbody());
        }

        static void GetAvatarLoading(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Wrappers.LocalPlayer).IsAvatarLoading());
        }

        static void GetFlying(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Wrappers.LocalPlayer).IsFlying());
        }

        static void GetCrouching(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Wrappers.LocalPlayer).IsCrouching());
        }

        static void GetProning(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Wrappers.LocalPlayer).IsProning());
        }

        static void GetSitting(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Wrappers.LocalPlayer).IsSitting());
        }

        static void GetSprinting(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Wrappers.LocalPlayer).IsSprinting());
        }

        static void GetLeftHandPosition(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as Wrappers.LocalPlayer).GetLeftHandPosition()));
        }

        static void GetLeftHandRotation(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Quaternion((p_obj as Wrappers.LocalPlayer).GetLeftHandRotation()));
        }

        static void GetRightHandPosition(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as Wrappers.LocalPlayer).GetRightHandPosition()));
        }

        static void GetRightHandRotation(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Quaternion((p_obj as Wrappers.LocalPlayer).GetRightHandRotation()));
        }

        static void GetLeftHandGesture(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.LocalPlayer).GetLeftHandGetsture());
        }

        static void GetRightHandGesture(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.LocalPlayer).GetRightHandGetsture());
        }

        static void GetZoom(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as Wrappers.LocalPlayer).IsZooming());
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

        // Instance getter
        static int InstanceGet(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(ms_instanceMethods.TryGetValue(l_key, out var l_func))
                    l_argReader.PushFunction(l_func); // Lua handles it by itself
                else if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item1 != null))
                    l_pair.Item1.Invoke(l_obj, l_argReader);
                else
                    l_argReader.PushNil();
            }
            else
                l_argReader.PushNil();

            return l_argReader.GetReturnValue();
        }

        // Instance setter
        static int InstanceSet(IntPtr p_state)
        {
            // Our value is on stack top
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.LocalPlayer l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item2 != null))
                    l_pair.Item2.Invoke(l_obj, l_argReader);
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
