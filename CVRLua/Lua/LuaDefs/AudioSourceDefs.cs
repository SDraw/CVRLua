using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class AudioSourceDefs
    {
        const string c_destroyed = "AudioSource is destroyed";
        const string c_destroyedClip = "AudioClip is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(PlayClipAtPoint), PlayClipAtPoint));
            ms_staticMethods.Add((nameof(IsAudioSource), IsAudioSource));

            ms_instanceProperties.Add(("bypassEffects", (GetBypassEffects, SetBypassEffects)));
            ms_instanceProperties.Add(("bypassListenerEffects", (GetBypassListenerEffects, SetBypassListenerEffects)));
            ms_instanceProperties.Add(("bypassReverbZones", (GetBypassReverbZones, SetBypassReverbZones)));
            ms_instanceProperties.Add(("clip", (GetClip, SetClip)));
            ms_instanceProperties.Add(("dopplerLevel", (GetDopplerLevel, SetDopplerLevel)));
            ms_instanceProperties.Add(("ignoreListenerPause", (GetIgnoreListenerPause, SetIgnoreListenerPause)));
            ms_instanceProperties.Add(("ignoreListenerVolume", (GetIgnoreListenerVolume, SetIgnoreListenerVolume)));
            ms_instanceProperties.Add(("isPlaying", (GetPlaying, null)));
            ms_instanceProperties.Add(("isVirtual", (GetVirtual, null)));
            ms_instanceProperties.Add(("loop", (GetLoop, SetLoop)));
            ms_instanceProperties.Add(("maxDistance", (GetMaxDistance, SetMaxDistance)));
            ms_instanceProperties.Add(("minDistance", (GetMinDistance, SetMinDistance)));
            //ms_instanceProperties.Add(("outputAudioMixerGroup", (?, ?))); // Requires AudioMixerGroup defs
            ms_instanceProperties.Add(("panStereo", (GetPanStereo, SetPanStereo)));
            ms_instanceProperties.Add(("pitch", (GetPitch, SetPitch)));
            ms_instanceProperties.Add(("playOnAwake", (GetPlayOnAwake, SetPlayOnAwake)));
            ms_instanceProperties.Add(("priority", (GetPriority, SetPriority)));
            ms_instanceProperties.Add(("reverbZoneMix", (GetReverbZoneMix, SetReverbZoneMix)));
            ms_instanceProperties.Add(("rolloffMode", (GetRolloffMode, SetRolloffMode)));
            ms_instanceProperties.Add(("spatialBlend", (GetSpatialBlend, SetSpatialBlend)));
            ms_instanceProperties.Add(("spatialize", (GetSpatialize, SetSpatialize)));
            ms_instanceProperties.Add(("spatializePostEffects", (GetSpatializePostEffects, SetSpatializePostEffects)));
            ms_instanceProperties.Add(("spread", (GetSpread, SetSpread)));
            ms_instanceProperties.Add(("time", (GetTime, SetTime)));
            ms_instanceProperties.Add(("timeSamples", (GetTimeSamples, SetTimeSamples)));
            ms_instanceProperties.Add(("velocityUpdateMode", (GetVelocityUpdateMode, SetVelocityUpdateMode)));
            ms_instanceProperties.Add(("volume", (GetVolume, SetVolume)));

            ms_instanceMethods.Add((nameof(GetAmbisonicDecoderFloat), GetAmbisonicDecoderFloat));
            //ms_instanceMethods.Add((nameof(GetCustomCurve), GetCustomCurve)); // Requires Curve defs
            ms_instanceMethods.Add((nameof(GetOutputData), GetOutputData));
            ms_instanceMethods.Add((nameof(GetSpatializerFloat), GetSpatializerFloat));
            ms_instanceMethods.Add((nameof(GetSpectrumData), GetSpectrumData));
            ms_instanceMethods.Add((nameof(Pause), Pause));
            ms_instanceMethods.Add((nameof(Play), Play));
            ms_instanceMethods.Add((nameof(PlayDelayed), PlayDelayed));
            ms_instanceMethods.Add((nameof(PlayOneShot), PlayOneShot));
            ms_instanceMethods.Add((nameof(PlayScheduled), PlayScheduled));
            ms_instanceMethods.Add((nameof(SetAmbisonicDecoderFloat), SetAmbisonicDecoderFloat));
            //ms_instanceMethods.Add((nameof(SetCustomCurve), SetCustomCurve)); // Requires Curve defs
            ms_instanceMethods.Add((nameof(SetScheduledEndTime), SetScheduledEndTime));
            ms_instanceMethods.Add((nameof(SetScheduledStartTime), SetScheduledStartTime));
            ms_instanceMethods.Add((nameof(SetSpatializerFloat), SetSpatializerFloat));
            ms_instanceMethods.Add((nameof(Stop), Stop));
            ms_instanceMethods.Add((nameof(UnPause), UnPause));

            BehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_m)
        {
            p_m.RegisterClass(typeof(AudioSource), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int PlayClipAtPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioClip l_clip = null;
            Wrappers.Vector3 l_vec = null;
            float l_volume = 1f;
            l_argReader.ReadObject(ref l_clip);
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNextNumber(ref l_volume);
            if(!l_argReader.HasErrors())
            {
                if(l_clip != null)
                {
                    AudioSource.PlayClipAtPoint(l_clip, l_vec.m_vec, l_volume);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyedClip);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int IsAudioSource(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadNextObject(ref l_source);
            l_argReader.PushBoolean(l_source != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetBypassEffects(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.bypassEffects);
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
        static int SetBypassEffects(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.bypassEffects = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetBypassListenerEffects(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.bypassListenerEffects);
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
        static int SetBypassListenerEffects(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.bypassListenerEffects = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetBypassReverbZones(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.bypassReverbZones);
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
        static int SetBypassReverbZones(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.bypassReverbZones = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetClip(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    if(l_source.clip != null)
                        l_argReader.PushObject(l_source.clip);
                    else
                        l_argReader.PushBoolean(false);
                }
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
        static int SetClip(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            AudioClip l_clip = null;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNextObject(ref l_clip);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    if(l_clip != null)
                        l_source.clip = l_clip;
                    else
                        l_source.clip = null;
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDopplerLevel(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.dopplerLevel);
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
        static int SetDopplerLevel(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.dopplerLevel = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIgnoreListenerPause(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.ignoreListenerPause);
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
        static int SetIgnoreListenerPause(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.ignoreListenerPause = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIgnoreListenerVolume(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.ignoreListenerVolume);
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
        static int SetIgnoreListenerVolume(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.ignoreListenerVolume = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPlaying(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.isPlaying);
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

        static int GetVirtual(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.isVirtual);
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

        static int GetLoop(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.loop);
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
        static int SetLoop(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.loop = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMaxDistance(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.maxDistance);
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
        static int SetMaxDistance(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.maxDistance = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMinDistance(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.minDistance);
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
        static int SetMinDistance(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.minDistance = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPanStereo(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.panStereo);
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
        static int SetPanStereo(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.panStereo = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPitch(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.pitch);
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
        static int SetPitch(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.pitch = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPlayOnAwake(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.playOnAwake);
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
        static int SetPlayOnAwake(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.playOnAwake = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPriority(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushInteger(l_source.priority);
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
        static int SetPriority(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            int l_priority = 0;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadInteger(ref l_priority);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.priority = l_priority;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetReverbZoneMix(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.reverbZoneMix);
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
        static int SetReverbZoneMix(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.reverbZoneMix = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRolloffMode(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushString(l_source.rolloffMode.ToString());
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
        static int SetRolloffMode(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            AudioRolloffMode l_mode = AudioRolloffMode.Logarithmic;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.rolloffMode = l_mode;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSpatialBlend(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.spatialBlend);
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
        static int SetSpatialBlend(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.spatialBlend = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSpatialize(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.spatialize);
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
        static int SetSpatialize(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.spatialize = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSpatializePostEffects(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.spatializePostEffects);
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
        static int SetSpatializePostEffects(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.spatializePostEffects = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSpread(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.spread);
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
        static int SetSpread(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.spread = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTime(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.time);
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
        static int SetTime(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.time = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTimeSamples(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushInteger(l_source.timeSamples);
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
        static int SetTimeSamples(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.timeSamples = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetVelocityUpdateMode(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushString(l_source.velocityUpdateMode.ToString());
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
        static int SetVelocityUpdateMode(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            AudioVelocityUpdateMode l_mode = AudioVelocityUpdateMode.Auto;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.velocityUpdateMode = l_mode;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetVolume(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushNumber(l_source.volume);
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
        static int SetVolume(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_source.volume = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int GetAmbisonicDecoderFloat(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    if(l_source.GetAmbisonicDecoderFloat(l_index, out float l_result))
                        l_argReader.PushNumber(l_result);
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
            return l_argReader.GetReturnValue();
        }

        static int GetOutputData(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            int l_samples = 0;
            int l_channel = 0;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadInteger(ref l_samples);
            l_argReader.ReadInteger(ref l_channel);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    if(Mathf.IsPowerOfTwo(l_samples))
                    {
                        float[] l_data = new float[l_samples];
                        l_source.GetOutputData(l_data, l_channel);
                        l_argReader.PushTable(l_data);
                    }
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
            return l_argReader.GetReturnValue();
        }

        static int GetSpatializerFloat(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    if(l_source.GetSpatializerFloat(l_index, out float l_result))
                        l_argReader.PushNumber(l_result);
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
            return l_argReader.GetReturnValue();
        }

        static int GetSpectrumData(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            int l_samples = 0;
            int l_channel = 0;
            FFTWindow l_window = FFTWindow.Rectangular;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadInteger(ref l_samples);
            l_argReader.ReadInteger(ref l_channel);
            l_argReader.ReadEnum(ref l_window);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    if(Mathf.IsPowerOfTwo(l_samples))
                    {
                        float[] l_data = new float[l_samples];
                        l_source.GetSpectrumData(l_data, l_channel, l_window);
                        l_argReader.PushTable(l_data);
                    }
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
            return l_argReader.GetReturnValue();
        }

        static int Pause(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    l_source.Pause();
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
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    l_source.Play();
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

        static int PlayDelayed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            float l_delay = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_delay);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    l_source.PlayDelayed(l_delay);
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

        static int PlayOneShot(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            AudioClip l_clip = null;
            float l_volume = 1f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadObject(ref l_clip);
            l_argReader.ReadNextNumber(ref l_volume);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    if(l_clip != null)
                    {
                        l_source.PlayOneShot(l_clip, l_volume);
                        l_argReader.PushBoolean(true);
                    }
                    else
                    {
                        l_argReader.SetError("Destroyed AudioClip");
                        l_argReader.PushBoolean(false);
                    }
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

        static int PlayScheduled(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            double l_time = .0;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_time);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    l_source.PlayScheduled(l_time);
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

        static int SetAmbisonicDecoderFloat(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            int l_index = 0;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadInteger(ref l_index);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.SetAmbisonicDecoderFloat(l_index, l_value));
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

        static int SetScheduledEndTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            double l_time = .0;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_time);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    l_source.SetScheduledEndTime(l_time);
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

        static int SetScheduledStartTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            double l_time = .0;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadNumber(ref l_time);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    l_source.SetScheduledStartTime(l_time);
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

        static int SetSpatializerFloat(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            int l_index = 0;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_source);
            l_argReader.ReadInteger(ref l_index);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                    l_argReader.PushBoolean(l_source.SetSpatializerFloat(l_index, l_value));
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

        static int Stop(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    l_source.Stop();
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

        static int UnPause(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_source = null;
            l_argReader.ReadObject(ref l_source);
            if(!l_argReader.HasErrors())
            {
                if(l_source != null)
                {
                    l_source.UnPause();
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
