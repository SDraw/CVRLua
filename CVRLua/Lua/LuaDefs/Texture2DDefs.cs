using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class Texture2DDefs
    {
        const string c_destroyed = "Texture2D is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticProperties.Add(("blackTexture", (GetBlackTexture, null)));
            ms_staticProperties.Add(("grayTexture", (GetGrayTexture, null)));
            ms_staticProperties.Add(("linearGrayTexture", (GetLinearGrayTexture, null)));
            ms_staticProperties.Add(("normalTexture", (GetNormalTexture, null)));
            ms_staticProperties.Add(("redTexture", (GetRedTexture, null)));
            ms_staticProperties.Add(("whiteTexture", (GetWhiteTexture, null)));

            ms_instanceProperties.Add(("calculatedMipmapLevel", (GetCalculatedMipmapLevel, null)));
            ms_instanceProperties.Add(("desiredMipmapLevel", (GetDesiredMipmapLevel, null)));
            ms_instanceProperties.Add(("format", (GetFormat, null)));
            ms_instanceProperties.Add(("loadedMipmapLevel", (GetLoadedMipmapLevel, null)));
            ms_instanceProperties.Add(("loadingMipmapLevel", (GetLoadingMipmapLevel, null)));
            ms_instanceProperties.Add(("minimumMipmapLevel", (GetMinimumMipmapLevel, SetMinimumMipmapLevel)));
            ms_instanceProperties.Add(("requestedMipmapLevel", (GetRequestedMipmapLevel, SetRequestedMipmapLevel)));
            ms_instanceProperties.Add(("streamingMipmaps", (GetStreamingMipmaps, null)));
            ms_instanceProperties.Add(("streamingMipmapsPriority", (GetStreamingMipmapsPriority, null)));

            ms_instanceMethods.Add((nameof(Apply), Apply));
            ms_instanceMethods.Add((nameof(ClearMinimumMipmapLevel), ClearMinimumMipmapLevel));
            ms_instanceMethods.Add((nameof(ClearRequestedMipmapLevel), ClearRequestedMipmapLevel));
            ms_instanceMethods.Add((nameof(Compress), Compress));
            ms_instanceMethods.Add((nameof(GetPixel), GetPixel));
            ms_instanceMethods.Add((nameof(GetPixelBilinear), GetPixelBilinear));
            //ms_instanceMethods.Add((nameof(GetPixels), GetPixels));
            //ms_instanceMethods.Add((nameof(GetPixels32), GetPixels32));
            //ms_instanceMethods.Add((nameof(GetRawTextureData), GetRawTextureData));
            ms_instanceMethods.Add((nameof(IsRequestedMipmapLevelLoaded), IsRequestedMipmapLevelLoaded));
            //ms_instanceMethods.Add((nameof(LoadRawTextureData), LoadRawTextureData));
            //ms_instanceMethods.Add((nameof(PackTextures), PackTextures));
            //ms_instanceMethods.Add((nameof(ReadPixels), ReadPixels));
            ms_instanceMethods.Add((nameof(Reinitialize), Reinitialize));
            ms_instanceMethods.Add((nameof(SetPixel), SetPixel));
            //ms_instanceMethods.Add((nameof(SetPixelData), SetPixelData));
            //ms_instanceMethods.Add((nameof(SetPixels), SetPixels));
            //ms_instanceMethods.Add((nameof(SetPixels32), SetPixels32));
            //ms_instanceMethods.Add((nameof(UpdateExternalTexture), UpdateExternalTexture));

            TextureDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Texture2D), Create, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsTexture2D), IsTexture2D);
        }

        // Constructor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            int l_width = 0;
            int l_height = 0;
            TextureFormat l_format = TextureFormat.RGBA32;
            bool l_mipChain = true;
            bool l_linear = false;
            l_argReader.Skip(); // Metatable
            l_argReader.ReadInteger(ref l_width);
            l_argReader.ReadInteger(ref l_height);
            l_argReader.ReadNextEnum(ref l_format);
            l_argReader.ReadNextBoolean(ref l_mipChain);
            l_argReader.ReadNextBoolean(ref l_linear);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Texture2D(l_width, l_height, l_format, l_mipChain, l_linear));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Static properties
        static int GetBlackTexture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(Texture2D.blackTexture);
            return 1;
        }

        static int GetGrayTexture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(Texture2D.grayTexture);
            return 1;
        }

        static int GetLinearGrayTexture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(Texture2D.linearGrayTexture);
            return 1;
        }

        static int GetNormalTexture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(Texture2D.normalTexture);
            return 1;
        }

        static int GetRedTexture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(Texture2D.redTexture);
            return 1;
        }

        static int GetWhiteTexture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(Texture2D.whiteTexture);
            return 1;
        }

        // Static methods
        static int IsTexture2D(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadNextObject(ref l_texture);
            l_argReader.PushBoolean(l_texture != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetCalculatedMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.calculatedMipmapLevel);
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

        static int GetDesiredMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.desiredMipmapLevel);
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

        static int GetFormat(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushString(l_texture.format.ToString());
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

        static int GetLoadedMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.loadedMipmapLevel);
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

        static int GetLoadingMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.loadingMipmapLevel);

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

        static int GetMinimumMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.minimumMipmapLevel);

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
        static int SetMinimumMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.minimumMipmapLevel = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 1;
        }

        static int GetRequestedMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.requestedMipmapLevel);

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
        static int SetRequestedMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_texture.requestedMipmapLevel = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 1;
        }

        static int GetStreamingMipmaps(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushBoolean(l_texture.streamingMipmaps);

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

        static int GetStreamingMipmapsPriority(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushInteger(l_texture.streamingMipmapsPriority);

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
        static int Apply(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            bool l_mipMaps = true;
            bool l_unreadable = false;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadNextBoolean(ref l_mipMaps);
            l_argReader.ReadNextBoolean(ref l_unreadable);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                {
                    l_texture.Apply(l_mipMaps, l_unreadable);
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

        static int ClearMinimumMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                {
                    l_texture.ClearMinimumMipmapLevel();
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

        static int ClearRequestedMipmapLevel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                {
                    l_texture.ClearRequestedMipmapLevel();
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

        static int Compress(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            bool l_high = false;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadBoolean(ref l_high);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                {
                    l_texture.Compress(l_high);

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

        static int GetPixel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            int l_x = 0;
            int l_y = 0;
            int l_mip = 0;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadInteger(ref l_x);
            l_argReader.ReadInteger(ref l_y);
            l_argReader.ReadNextInteger(ref l_mip);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushObject(new Wrappers.Color(l_texture.GetPixel(l_x, l_y, l_mip)));
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

        static int GetPixelBilinear(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            float l_x = 0;
            float l_y = 0;
            int l_mip = 0;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadNumber(ref l_x);
            l_argReader.ReadNumber(ref l_y);
            l_argReader.ReadNextInteger(ref l_mip);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushObject(new Wrappers.Color(l_texture.GetPixelBilinear(l_x, l_y, l_mip)));
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

        static int IsRequestedMipmapLevelLoaded(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            l_argReader.ReadObject(ref l_texture);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                    l_argReader.PushBoolean(l_texture.IsRequestedMipmapLevelLoaded());
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

        static int Reinitialize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            bool l_extended = false;
            Texture2D l_texture = null;
            int l_width = 0;
            int l_height = 0;
            TextureFormat l_format = TextureFormat.RGBA32;
            bool l_mipChain = false;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadInteger(ref l_width);
            l_argReader.ReadInteger(ref l_height);
            if(l_argReader.IsNextString())
            {
                l_argReader.ReadEnum(ref l_format);
                l_argReader.ReadBoolean(ref l_mipChain);
                l_extended = true;
            }
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                {
                    if(l_extended)
                        l_texture.Reinitialize(l_width, l_height, l_format, l_mipChain);
                    else
                        l_texture.Reinitialize(l_width, l_height);
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

        static int SetPixel(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Texture2D l_texture = null;
            int l_x = 0;
            int l_y = 0;
            Wrappers.Color l_color = null;
            int l_mip = 0;
            l_argReader.ReadObject(ref l_texture);
            l_argReader.ReadInteger(ref l_x);
            l_argReader.ReadInteger(ref l_y);
            l_argReader.ReadObject(ref l_color);
            l_argReader.ReadNextInteger(ref l_mip);
            if(!l_argReader.HasErrors())
            {
                if(l_texture != null)
                {
                    l_texture.SetPixel(l_x, l_y, l_color.m_color, l_mip);
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
