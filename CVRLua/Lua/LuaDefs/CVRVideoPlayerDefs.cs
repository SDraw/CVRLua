using ABI.CCK.Components;
using ABI_RC.VideoPlayer.Scripts;
using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class CVRVideoPlayerDefs
    {
        const string c_destroyed = "CVRVideoPlayer is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsCVRVideoPlayer), IsCVRVideoPlayer));

            ms_instanceProperties.Add(("audioMode", (GetAudioMode, SetAudioMode)));
            ms_instanceProperties.Add(("autoplay", (GetAutoplay, null)));
            ms_instanceProperties.Add(("customAudioSource", (GetCustomAudioSource, null)));
            ms_instanceProperties.Add(("interactiveUI", (GetInteractiveUI, null)));
            ms_instanceProperties.Add(("networkSync", (GetNetworkSync, SetNetworkSync)));
            ms_instanceProperties.Add(("url", (GetUrl, SetUrl)));
            ms_instanceProperties.Add(("volume", (GetVolume, SetVolume)));
            ms_instanceProperties.Add(("playerId", (GetPlayerId, null)));

            ms_instanceMethods.Add((nameof(Pause), Pause));
            ms_instanceMethods.Add((nameof(Play), Play));
            ms_instanceMethods.Add((nameof(Previous), Previous));
            ms_instanceMethods.Add((nameof(Next), Next));

            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CVRVideoPlayer), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsCVRVideoPlayer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadNextObject(ref l_player);
            l_argReader.PushBoolean(l_player != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAudioMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_argReader.PushString(l_player.audioPlaybackMode.ToString());
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
        static int SetAudioMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            VideoPlayerUtils.AudioMode l_mode = VideoPlayerUtils.AudioMode.Direct;
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_player.audioPlaybackMode = l_mode;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAutoplay(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_argReader.PushBoolean(l_player.autoplay);
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

        static int GetCustomAudioSource(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                {
                    if(l_player.customAudioSource != null)
                        l_argReader.PushObject(l_player.customAudioSource);
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

        static int GetInteractiveUI(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_argReader.PushBoolean(l_player.interactiveUI);
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

        static int GetNetworkSync(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_argReader.PushBoolean(l_player.syncEnabled);
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
        static int SetNetworkSync(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_player.SetNetworkSync(l_state);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUrl(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_argReader.PushString(l_player.lastNetworkVideoUrl);
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
        static int SetUrl(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            string l_url = "";
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadString(ref l_url);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_player.SetUrl(l_url);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetVolume(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_argReader.PushNumber(l_player.playbackVolume);
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
        static int SetVolume(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            float l_volume = 1f;
            l_argReader.ReadObject(ref l_player);
            l_argReader.ReadNumber(ref l_volume);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_player.SetVolume(l_volume);
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPlayerId(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                    l_argReader.PushString(l_player.playerId);
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
        static int Pause(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                {
                    l_player.Pause();
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

        static int Play(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                {
                    l_player.Play();
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

        static int Previous(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                {
                    l_player.Previous();
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

        static int Next(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CVRVideoPlayer l_player = null;
            l_argReader.ReadObject(ref l_player);
            if(!l_argReader.HasErrors())
            {
                if(l_player != null)
                {
                    l_player.Next();
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
