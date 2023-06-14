using ABI.CCK.Components;
using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class CVRAttachmentDefs
    {
        const string c_destroyed = "CVRAttachment is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_instanceProperties.Add(("attachmentType", (GetAttachmentType, SetAttachmentType)));
            ms_instanceProperties.Add(("boneType", (GetBoneType, SetBoneType)));
            ms_instanceProperties.Add(("trackerType", (GetTrackerType, SetTrackerType)));
            ms_instanceProperties.Add(("useFixedPositionOffset", (GetUseFixedPositionOffset, SetUseFixedPositionOffset)));
            ms_instanceProperties.Add(("useFixedRotationOffset", (GetUseFixedRotationOffset, SetUseFixedRotationOffset)));
            ms_instanceProperties.Add(("positionOffset", (GetPositionOffset, SetPositionOffset)));
            ms_instanceProperties.Add(("rotationOffset", (GetRotationOffset, SetRotationOffset)));
            ms_instanceProperties.Add(("maxAttachmentDistance", (GetMaxAttachmentDistance, SetMaxAttachmentDistance)));
            ms_instanceProperties.Add(("isAttached", (GetIsAttached, null)));

            ms_instanceMethods.Add((nameof(Attach), Attach));
            ms_instanceMethods.Add((nameof(DeAttach), DeAttach));

            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        static internal void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CVRAttachment), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsCVRAttachment), IsCVRAttachment);
        }

        // Static methods
        static int IsCVRAttachment(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadNextObject(ref l_attach);
            l_argReader.PushBoolean(l_attach != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAttachmentType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_argReader.PushString(l_attach.attachmentType.ToString());
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
        static int SetAttachmentType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            CVRAttachment.AttachmentType l_type = CVRAttachment.AttachmentType.Bone;
            l_argReader.ReadObject(ref l_attach);
            l_argReader.ReadEnum(ref l_type);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_attach.attachmentType = l_type;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetBoneType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_argReader.PushString(l_attach.boneType.ToString());
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
        static int SetBoneType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            CVRAttachment.BoneType l_type = CVRAttachment.BoneType.Hips;
            l_argReader.ReadObject(ref l_attach);
            l_argReader.ReadEnum(ref l_type);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_attach.boneType = l_type;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTrackerType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_argReader.PushString(l_attach.trackerType.ToString());
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
        static int SetTrackerType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            CVRAttachment.TrackerType l_type = CVRAttachment.TrackerType.MainCamera;
            l_argReader.ReadObject(ref l_attach);
            l_argReader.ReadEnum(ref l_type);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_attach.trackerType = l_type;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUseFixedPositionOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_argReader.PushBoolean(l_attach.useFixedPositionOffset);
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
        static int SetUseFixedPositionOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_attach);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_attach.useFixedPositionOffset = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUseFixedRotationOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_argReader.PushBoolean(l_attach.useFixedRotationOffset);
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
        static int SetUseFixedRotationOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_attach);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_attach.useFixedRotationOffset = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPositionOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_attach.positionOffset));
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
        static int SetPositionOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_attach);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_attach.positionOffset = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRotationOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_attach.rotationOffset));
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
        static int SetRotationOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_attach);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_attach.rotationOffset = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMaxAttachmentDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_argReader.PushNumber(l_attach.maxAttachmentDistance);
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
        static int SetMaxAttachmentDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_attach);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_attach.maxAttachmentDistance = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIsAttached(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                    l_argReader.PushBoolean(l_attach.IsAttached());
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
        static int Attach(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                {
                    l_attach.Attach();
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

        static int DeAttach(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRAttachment l_attach = null;
            l_argReader.ReadObject(ref l_attach);
            if(!l_argReader.HasErrors())
            {
                if(l_attach != null)
                {
                    l_attach.DeAttach();
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
