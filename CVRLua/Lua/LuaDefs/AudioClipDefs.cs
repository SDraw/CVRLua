using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class AudioClipDefs
    {
        const string c_destroyed = "AudioClip is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            //ms_staticMethods(nameof(Create), Create); // Probably will not be implemented
            ms_staticMethods.Add(nameof(IsAudioClip), IsAudioClip);

            ms_instanceProperties.Add("ambisonic", (GetAmbisonic, null));
            ms_instanceProperties.Add("channels", (GetChannels, null));
            ms_instanceProperties.Add("frequency", (GetFrequency, null));
            ms_instanceProperties.Add("length", (GetLength, null));
            ms_instanceProperties.Add("loadInBackground", (GetLoadInBackground, null));
            ms_instanceProperties.Add("loadState", (GetLoadState, null));
            ms_instanceProperties.Add("loadType", (GetLoadType, null));
            ms_instanceProperties.Add("preloadAudioData", (GetPreloadAudioData, null));
            ms_instanceProperties.Add("samples", (GetSamples, null));

            ObjectDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(AudioClip), null, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
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
        static void GetAmbisonic(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioClip).ambisonic);
        }

        static void GetChannels(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushInteger((p_obj as AudioClip).channels);
        }

        static void GetFrequency(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushInteger((p_obj as AudioClip).frequency);
        }

        static void GetLength(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as AudioClip).length);
        }

        static void GetLoadInBackground(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioClip).loadInBackground);
        }

        static void GetLoadState(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushString((p_obj as AudioClip).loadState.ToString());
        }

        static void GetLoadType(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushString((p_obj as AudioClip).loadType.ToString());
        }

        static void GetPreloadAudioData(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as AudioClip).preloadAudioData);
        }

        static void GetSamples(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushInteger((p_obj as AudioClip).samples);
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
            AudioClip l_obj = null;
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
            AudioClip l_obj = null;
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
