using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class TextureDefs
    {
        const string c_destroyed = "Texture is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsTexture), IsTexture));

            ms_instanceProperties.Add(("anisoLevel", (GetAnisoLevel, SetAnisoLevel)));
            ms_instanceProperties.Add(("dimension", (GetDimension, SetDimension)));
            ms_instanceProperties.Add(("filterMode", (GetFilterMode, SetFilterMode)));
            ms_instanceProperties.Add(("graphicsFormat", (GetGraphicsFormat, null)));
            ms_instanceProperties.Add(("height", (GetHeight, null)));
            //ms_instanceProperties.Add(("imageContentsHash", (GetImageContentsHash, null))); // Where is it, Unity???
            ms_instanceProperties.Add(("isReadable", (GetIsReadable, null)));
            ms_instanceProperties.Add(("mipMapBias", (GetMipMapBias, SetMipMapBias)));
            ms_instanceProperties.Add(("mipmapCount", (GetMipmapCount, null)));
            ms_instanceProperties.Add(("updateCount", (GetUpdateCount, null)));
            ms_instanceProperties.Add(("width", (GetWidth, null)));
            ms_instanceProperties.Add(("wrapMode", (GetWrapMode, SetWrapMode)));
            ms_instanceProperties.Add(("wrapModeU", (GetWrapModeU, SetWrapModeU)));
            ms_instanceProperties.Add(("wrapModeV", (GetWrapModeV, SetWrapModeV)));
            ms_instanceProperties.Add(("wrapModeW", (GetWrapModeW, SetWrapModeW)));

            ms_instanceMethods.Add((nameof(IncrementUpdateCount), IncrementUpdateCount));

            ObjectDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Texture), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);

        }

        internal static void InheritTo(
            List<(string, LuaInterop.lua_CFunction)> p_metaMethods,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_staticProperties,
            List<(string, LuaInterop.lua_CFunction)> p_staticMethods,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_instanceProperties,
            List<(string, LuaInterop.lua_CFunction)> p_instanceMethods
        )
        {
            if(p_metaMethods != null)
                ms_metaMethods.MergeInto(p_metaMethods);

            if(p_staticProperties != null)
                ms_staticProperties.MergeInto(p_staticProperties);

            if(p_staticMethods != null)
                ms_staticMethods.MergeInto(p_staticMethods);

            if(p_instanceProperties != null)
                ms_instanceProperties.MergeInto(p_instanceProperties);

            if(p_instanceMethods != null)
                ms_instanceMethods.MergeInto(p_instanceMethods);
        }

        // Static methods
        static int IsTexture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadNextObject(ref l_texture);
            l_argReader.PushBoolean(l_texture != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAnisoLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.anisoLevel);
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
        static int SetAnisoLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.anisoLevel = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDimension(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushString(l_texture.dimension.ToString());
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
        static int SetDimension(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            UnityEngine.Rendering.TextureDimension l_value = UnityEngine.Rendering.TextureDimension.Unknown;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.dimension = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetFilterMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushString(l_texture.filterMode.ToString());
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
        static int SetFilterMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            FilterMode l_value = FilterMode.Point;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.filterMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetGraphicsFormat(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushString(l_texture.graphicsFormat.ToString());
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

        static int GetHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.height);
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

        static int GetIsReadable(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushBoolean
(l_texture.isReadable);
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

        static int GetMipMapBias(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushNumber(l_texture.mipMapBias);
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
        static int SetMipMapBias(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.mipMapBias = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMipmapCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.mipmapCount);
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

        static int GetUpdateCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.updateCount);
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

        static int GetWidth(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.width);
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

        static int GetWrapMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushString(l_texture.wrapMode.ToString());
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
        static int SetWrapMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            TextureWrapMode l_value = TextureWrapMode.Repeat;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.wrapMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetWrapModeU(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushString(l_texture.wrapModeU.ToString());
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
        static int SetWrapModeU(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            TextureWrapMode l_value = TextureWrapMode.Repeat;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.wrapModeU
 = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetWrapModeV(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushString(l_texture.wrapModeV.ToString());
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
        static int SetWrapModeV(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            TextureWrapMode l_value = TextureWrapMode.Repeat;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.wrapModeV = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetWrapModeW(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushString(l_texture.wrapModeW.ToString());
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
        static int SetWrapModeW(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            TextureWrapMode l_value = TextureWrapMode.Repeat;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.wrapModeW = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int IncrementUpdateCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                {
                    l_texture.IncrementUpdateCount();
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
