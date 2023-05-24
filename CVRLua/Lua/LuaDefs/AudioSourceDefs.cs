using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class AudioSourceDefs
    {
        const string c_destroyed = "AudioSource is destroyed";
        const string c_destroyedClip = "Destroyed AudioClip";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(PlayClipAtPoint), PlayClipAtPoint);
            ms_staticMethods.Add(nameof(IsAudioSource), IsAudioSource);

            ms_instanceProperties.Add("bypassEffects", (GetBypassEffects, SetBypassEffects));
            ms_instanceProperties.Add("bypassListenerEffects", (GetBypassListenerEffects, SetBypassListenerEffects));
            ms_instanceProperties.Add("bypassReverbZones", (GetBypassReverbZones, SetBypassReverbZones));
            ms_instanceProperties.Add("clip", (GetClip, SetClip));
            ms_instanceProperties.Add("dopplerLevel", (GetDopplerLevel, SetDopplerLevel));
            ms_instanceProperties.Add("ignoreListenerPause", (GetIgnoreListenerPause, SetIgnoreListenerPause));
            ms_instanceProperties.Add("ignoreListenerVolume", (GetIgnoreListenerVolume, SetIgnoreListenerVolume));
            ms_instanceProperties.Add("isPlaying", (GetPlaying, null));
            ms_instanceProperties.Add("isVirtual", (GetVirtual, null));
            ms_instanceProperties.Add("loop", (GetLoop, SetLoop));
            ms_instanceProperties.Add("maxDistance", (GetMaxDistance, SetMaxDistance));
            ms_instanceProperties.Add("minDistance", (GetMinDistance, SetMinDistance));
            //ms_instanceProperties.Add("outputAudioMixerGroup", (?, ?)); // Requires AudioMixerGroup defs
            ms_instanceProperties.Add("panStereo", (GetPanStereo, SetPanStereo));
            ms_instanceProperties.Add("pitch", (GetPitch, SetPitch));
            ms_instanceProperties.Add("playOnAwake", (GetPlayOnAwake, SetPlayOnAwake));
            ms_instanceProperties.Add("priority", (GetPriority, SetPriority));
            ms_instanceProperties.Add("reverbZoneMix", (GetReverbZoneMix, SetReverbZoneMix));
            ;
            ms_instanceProperties.Add("rolloffMode", (GetRolloffMode, SetRolloffMode));
            ms_instanceProperties.Add("spatialBlend", (GetSpatialBlend, SetSpatialBlend));
            ms_instanceProperties.Add("spatialize", (GetSpatialize, SetSpatialize));
            ms_instanceProperties.Add("spatializePostEffects", (GetSpatializePostEffects, SetSpatializePostEffects));
            ms_instanceProperties.Add("spread", (GetSpread, SetSpread));
            ms_instanceProperties.Add("time", (GetTime, SetTime));
            ms_instanceProperties.Add("timeSamples", (GetTimeSamples, SetTimeSamples));
            ms_instanceProperties.Add("velocityUpdateMode", (GetVelocityUpdateMode, SetVelocityUpdateMode));
            ms_instanceProperties.Add("volume", (GetVolume, SetVolume));

            ms_instanceMethods.Add(nameof(GetAmbisonicDecoderFloat), GetAmbisonicDecoderFloat);
            //ms_instanceMethods.Add(nameof(GetCustomCurve), GetCustomCurve); // Requires Curve defs
            ms_instanceMethods.Add(nameof(GetOutputData), GetOutputData);
            ms_instanceMethods.Add(nameof(GetSpatializerFloat), GetSpatializerFloat);
            ms_instanceMethods.Add(nameof(GetSpectrumData), GetSpectrumData);
            ms_instanceMethods.Add(nameof(Pause), Pause);
            ms_instanceMethods.Add(nameof(Play), Play);
            ms_instanceMethods.Add(nameof(PlayDelayed), PlayDelayed);
            ms_instanceMethods.Add(nameof(PlayOneShot), PlayOneShot);
            ms_instanceMethods.Add(nameof(PlayScheduled), PlayScheduled);
            ms_instanceMethods.Add(nameof(SetAmbisonicDecoderFloat), SetAmbisonicDecoderFloat);
            //ms_instanceMethods.Add(nameof(SetCustomCurve), SetCustomCurve); // Requires Curve defs
            ms_instanceMethods.Add(nameof(SetScheduledEndTime), SetScheduledEndTime);
            ms_instanceMethods.Add(nameof(SetScheduledStartTime), SetScheduledStartTime);
            ms_instanceMethods.Add(nameof(SetSpatializerFloat), SetSpatializerFloat);
            ms_instanceMethods.Add(nameof(Stop), Stop);
            ms_instanceMethods.Add(nameof(UnPause), UnPause);

            BehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_m)
        {
            p_m.RegisterClass(typeof(AudioSource), null, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
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
        static void GetBypassEffects(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).bypassEffects);
        }
        static void SetBypassEffects(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).bypassEffects = l_state;
        }

        static void GetBypassListenerEffects(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).bypassListenerEffects);
        }
        static void SetBypassListenerEffects(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).bypassListenerEffects = l_state;
        }

        static void GetBypassReverbZones(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).bypassReverbZones);
        }
        static void SetBypassReverbZones(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).bypassReverbZones = l_state;
        }

        static void GetClip(object p_obj, LuaArgReader p_reader)
        {
            if((p_obj as AudioSource).clip != null)
                p_reader.PushObject((p_obj as AudioSource).clip);
            else
                p_reader.PushBoolean(false);
        }
        static void SetClip(object p_obj, LuaArgReader p_reader)
        {
            if(p_reader.IsNextNil())
                (p_obj as AudioSource).clip = null;
            else
            {
                AudioClip l_clip = null;
                p_reader.ReadObject(ref l_clip);
                if(!p_reader.HasErrors())
                {
                    if(l_clip != null)
                        (p_obj as AudioSource).clip = l_clip;
                    else
                        p_reader.SetError(c_destroyedClip);
                }
            }
        }

        static void GetDopplerLevel(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).dopplerLevel);
        }
        static void SetDopplerLevel(object p_obj, LuaArgReader p_reader)
        {
            float l_value = 0f;
            p_reader.ReadNumber(ref l_value);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).dopplerLevel = l_value;
        }

        static void GetIgnoreListenerPause(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).ignoreListenerPause);
        }
        static void SetIgnoreListenerPause(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).ignoreListenerPause = l_state;
        }

        static void GetIgnoreListenerVolume(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).ignoreListenerVolume);
        }
        static void SetIgnoreListenerVolume(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).ignoreListenerVolume = l_state;
        }

        static void GetPlaying(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).isPlaying);
        }

        static void GetVirtual(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).isVirtual);
        }

        static void GetLoop(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).loop);
        }
        static void SetLoop(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).loop = l_state;
        }

        static void GetMaxDistance(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).maxDistance);
        }
        static void SetMaxDistance(object p_obj, LuaArgReader p_reader)
        {
            float l_dist = 0f;
            p_reader.ReadNumber(ref l_dist);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).maxDistance = l_dist;
        }

        static void GetMinDistance(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).minDistance);
        }
        static void SetMinDistance(object p_obj, LuaArgReader p_reader)
        {
            float l_dist = 0f;
            p_reader.ReadNumber(ref l_dist);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).minDistance = l_dist;
        }

        static void GetPanStereo(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).panStereo);
        }
        static void SetPanStereo(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).panStereo = l_val;
        }

        static void GetPitch(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).pitch);
        }
        static void SetPitch(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).pitch = l_val;
        }

        static void GetPlayOnAwake(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).playOnAwake);
        }
        static void SetPlayOnAwake(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).playOnAwake = l_state;
        }

        static void GetPriority(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushInteger((p_obj as AudioSource).priority);
        }
        static void SetPriority(object p_obj, LuaArgReader p_reader)
        {
            int l_val = 0;
            p_reader.ReadInteger(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).priority = l_val;
        }

        static void GetReverbZoneMix(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).reverbZoneMix);
        }
        static void SetReverbZoneMix(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).reverbZoneMix = l_val;
        }

        static void GetRolloffMode(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushString((p_obj as AudioSource).rolloffMode.ToString());
        }
        static void SetRolloffMode(object p_obj, LuaArgReader p_reader)
        {
            AudioRolloffMode l_mode = AudioRolloffMode.Logarithmic;
            p_reader.ReadEnum(ref l_mode);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).rolloffMode = l_mode;
        }

        static void GetSpatialBlend(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).spatialBlend);
        }
        static void SetSpatialBlend(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).spatialBlend = l_val;
        }

        static void GetSpatialize(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).spatialize);
        }
        static void SetSpatialize(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).spatialize = l_state;
        }

        static void GetSpatializePostEffects(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioSource).spatializePostEffects);
        }
        static void SetSpatializePostEffects(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).spatializePostEffects = l_state;
        }

        static void GetSpread(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).spread);
        }
        static void SetSpread(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).spread = l_val;
        }

        static void GetTime(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).time);
        }
        static void SetTime(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).time = l_val;
        }

        static void GetTimeSamples(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushInteger((p_obj as AudioSource).timeSamples);
        }
        static void SetTimeSamples(object p_obj, LuaArgReader p_reader)
        {
            int l_val = 0;
            p_reader.ReadInteger(ref l_val);
            (p_obj as AudioSource).timeSamples = l_val;
        }

        static void GetVelocityUpdateMode(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushString((p_obj as AudioSource).velocityUpdateMode.ToString());
        }
        static void SetVelocityUpdateMode(object p_obj, LuaArgReader p_reader)
        {
            AudioVelocityUpdateMode l_mode = AudioVelocityUpdateMode.Auto;
            p_reader.ReadEnum(ref l_mode);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).velocityUpdateMode = l_mode;
        }

        static void GetVolume(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioSource).volume);
        }
        static void SetVolume(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as AudioSource).volume = l_val;
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

        // Static getter
        static int StaticGet(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            string l_key = "";
            l_argReader.Skip(); // Metatable
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(ms_staticMethods.TryGetValue(l_key, out var l_func))
                    l_argReader.PushFunction(l_func);
                else if(ms_staticProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item1 != null))
                    l_pair.Item1.Invoke(l_argReader);
                else
                    l_argReader.PushNil();
            }
            else
                l_argReader.PushNil();

            return l_argReader.GetReturnValue();
        }

        // Instance getter
        static int InstanceGet(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            AudioSource l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    if(ms_instanceMethods.TryGetValue(l_key, out var l_func))
                        l_argReader.PushFunction(l_func); // Lua handles it by itself
                    else if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item1 != null))
                        l_pair.Item1.Invoke(l_obj, l_argReader);
                    else
                        l_argReader.PushNil();
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushNil();
                }
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
            AudioSource l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item2 != null))
                        l_pair.Item2.Invoke(l_obj, l_argReader);
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
