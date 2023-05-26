using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class AudioClipDefs
    {
        const string c_destroyed = "AudioClip is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            //ms_staticMethods(nameof(Create), Create); // Probably will not be implemented
            ms_staticMethods.Add((nameof(IsAudioClip), IsAudioClip));

            ms_instanceProperties.Add(("ambisonic", (GetAmbisonic, null)));
            ms_instanceProperties.Add(("channels", (GetChannels, null)));
            ms_instanceProperties.Add(("frequency", (GetFrequency, null)));
            ms_instanceProperties.Add(("length", (GetLength, null)));
            ms_instanceProperties.Add(("loadInBackground", (GetLoadInBackground, null)));
            ms_instanceProperties.Add(("loadState", (GetLoadState, null)));
            ms_instanceProperties.Add(("loadType", (GetLoadType, null)));
            ms_instanceProperties.Add(("preloadAudioData", (GetPreloadAudioData, null)));
            ms_instanceProperties.Add(("samples", (GetSamples, null)));

            ObjectDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(AudioClip), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsAudioClip(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadNextObject(ref l_clip);
            l_argReader.PushBoolean(l_clip != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAmbisonic(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                    l_argReader.PushBoolean(l_clip.ambisonic);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetChannels(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                    l_argReader.PushInteger(l_clip.channels);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetFrequency(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                    l_argReader.PushInteger(l_clip.frequency);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLength(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                    l_argReader.PushNumber(l_clip.length);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLoadInBackground(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                    l_argReader.PushBoolean(l_clip.loadInBackground);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLoadState(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                    l_argReader.PushString(l_clip.loadState.ToString());
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLoadType(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                    l_argReader.PushString(l_clip.loadType.ToString());
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetPreloadAudioData(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                    l_argReader.PushBoolean(l_clip.preloadAudioData);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetSamples(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                    l_argReader.PushInteger(l_clip.samples);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
    }
}
