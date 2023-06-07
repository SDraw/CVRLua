using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class CVRMirrorDefs
    {
        const string c_destroyed = "CVRMirror is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsCVRMirror), IsCVRMirror));

            ms_instanceProperties.Add(("disablePixelLights", (GetDisablePixelLights, SetDisablePixelLights)));
            ms_instanceProperties.Add(("textureSize", (GetTextureSize, null)));
            ms_instanceProperties.Add(("clipPlaneOffset", (GetClipPlaneOffset, SetClipPlaneOffset)));
            ms_instanceProperties.Add(("reflectLayers", (GetReflectLayers, SetReflectLayers)));

            ms_instanceMethods.Add((nameof(GetReflectionPosition), GetReflectionPosition));

            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CVRMirror), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsCVRMirror(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRMirror l_mirror = null;
            l_argReader.ReadNextObject(ref l_mirror);
            l_argReader.PushBoolean(l_mirror != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetDisablePixelLights(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRMirror l_mirror = null;
            l_argReader.ReadObject(ref l_mirror);
            if(!l_argReader.HasErrors())
            {
                if(l_mirror != null)
                    l_argReader.PushBoolean(l_mirror.m_DisablePixelLights);
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
        static int SetDisablePixelLights(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRMirror l_mirror = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_mirror);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_mirror != null)
                    l_mirror.m_DisablePixelLights = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTextureSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRMirror l_mirror = null;
            l_argReader.ReadObject(ref l_mirror);
            if(!l_argReader.HasErrors())
            {
                if(l_mirror != null)
                    l_argReader.PushInteger(l_mirror.m_TextureSize);

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

        static int GetClipPlaneOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRMirror l_mirror = null;
            l_argReader.ReadObject(ref l_mirror);
            if(!l_argReader.HasErrors())
            {
                if(l_mirror != null)
                    l_argReader.PushNumber(l_mirror.m_ClipPlaneOffset);
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
        static int SetClipPlaneOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRMirror l_mirror = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_mirror);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_mirror != null)
                    l_mirror.m_ClipPlaneOffset = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetReflectLayers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRMirror l_mirror = null;
            l_argReader.ReadObject(ref l_mirror);
            if(!l_argReader.HasErrors())
            {
                if(l_mirror != null)
                    l_argReader.PushInteger(l_mirror.m_ReflectLayers);
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
        static int SetReflectLayers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRMirror l_mirror = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_mirror);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_mirror != null)
                    l_mirror.m_ReflectLayers = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int GetReflectionPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRMirror l_mirror = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_mirror);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_mirror != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_mirror.GetMirrorReflectionPosition(l_vec.m_vec)));
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
