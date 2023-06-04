using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class RenderSettingsDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticProperties.Add(("ambientEquatorColor", (GetAmbientEquatorColor, SetAmbientEquatorColor)));
            ms_staticProperties.Add(("ambientGroundColor", (GetAmbientGroundColor, SetAmbientGroundColor)));
            ms_staticProperties.Add(("ambientIntensity", (GetAmbientIntensity, SetAmbientIntensity)));
            ms_staticProperties.Add(("ambientLight", (GetAmbientLight, SetAmbientLight)));
            ms_staticProperties.Add(("ambientMode", (GetAmbientMode, SetAmbientMode)));
            //ms_staticProperties.Add(("ambientProbe", (GetAmbientProbe, SetAmbientProbe)));
            ms_staticProperties.Add(("ambientSkyColor", (GetAmbientSkyColor, SetAmbientSkyColor)));
            //ms_staticProperties.Add(("customReflection", (?, ?)));
            ms_staticProperties.Add(("defaultReflectionMode", (GetDefaultReflectionMode, SetDefaultReflectionMode)));
            ms_staticProperties.Add(("defaultReflectionResolution", (GetDefaultReflectionResolution, SetDefaultReflectionResolution)));
            ms_staticProperties.Add(("flareFadeSpeed", (GetFlareFadeSpeed, SetFlareFadeSpeed)));
            ms_staticProperties.Add(("flareStrength", (GetFlareStrength, SetFlareStrength)));
            ms_staticProperties.Add(("fog", (GetFog, SetFog)));
            ms_staticProperties.Add(("fogColor", (GetFogColor, SetFogColor)));
            ms_staticProperties.Add(("fogDensity", (GetFogDensity, SetFogDensity)));
            ms_staticProperties.Add(("fogEndDistance", (GetFogEndDistance, SetFogEndDistance)));
            ms_staticProperties.Add(("fogMode", (GetFogMode, SetFogMode)));
            ms_staticProperties.Add(("fogStartDistance", (GetFogStartDistance, SetFogStartDistance)));
            ms_staticProperties.Add(("haloStrength", (GetHaloStrength, SetHaloStrength)));
            ms_staticProperties.Add(("reflectionBounces", (GetReflectionBounces, SetReflectionBounces)));
            ms_staticProperties.Add(("reflectionIntensity", (GetReflectionIntensity, SetReflectionIntensity)));
            //ms_staticProperties.Add(("skybox", (GetSkybox, SetSkybox))); // Require Material defs
            ms_staticProperties.Add(("subtractiveShadowColor", (GetSubtractiveShadowColor, SetSubtractiveShadowColor)));
            //ms_staticProperties.Add(("sun", (?, ?))); // Require Light defs

            ObjectDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(RenderSettings), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static properties
        static int GetAmbientEquatorColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(RenderSettings.ambientEquatorColor));
            return 1;
        }
        static int SetAmbientEquatorColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                RenderSettings.ambientEquatorColor = l_color.m_color;

            l_argReader.LogError();
            return 0;
        }

        static int GetAmbientGroundColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(RenderSettings.ambientGroundColor));
            return 1;
        }
        static int SetAmbientGroundColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                RenderSettings.ambientGroundColor = l_color.m_color;

            l_argReader.LogError();
            return 0;
        }

        static int GetAmbientIntensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(RenderSettings.ambientIntensity);
            return 1;
        }
        static int SetAmbientIntensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.ambientIntensity = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetAmbientLight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(RenderSettings.ambientLight));
            return 1;
        }
        static int SetAmbientLight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                RenderSettings.ambientLight = l_color.m_color;

            l_argReader.LogError();
            return 0;
        }

        static int GetAmbientMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushString(RenderSettings.ambientMode.ToString());
            return 1;
        }
        static int SetAmbientMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            UnityEngine.Rendering.AmbientMode l_value = UnityEngine.Rendering.AmbientMode.Skybox;
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.ambientMode = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetAmbientSkyColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(RenderSettings.ambientSkyColor));
            return 1;
        }
        static int SetAmbientSkyColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                RenderSettings.ambientSkyColor = l_color.m_color;

            l_argReader.LogError();
            return 0;
        }

        static int GetDefaultReflectionMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushString(RenderSettings.defaultReflectionMode.ToString());
            return 1;
        }
        static int SetDefaultReflectionMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            UnityEngine.Rendering.DefaultReflectionMode l_value = UnityEngine.Rendering.DefaultReflectionMode.Skybox;
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.defaultReflectionMode = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetDefaultReflectionResolution(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushInteger(RenderSettings.defaultReflectionResolution);
            return 1;
        }
        static int SetDefaultReflectionResolution(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            int l_value = 0;
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.defaultReflectionResolution = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetFlareFadeSpeed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(RenderSettings.flareFadeSpeed);
            return 1;
        }
        static int SetFlareFadeSpeed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.flareFadeSpeed = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetFlareStrength(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(RenderSettings.flareStrength);
            return 1;
        }
        static int SetFlareStrength(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.flareStrength = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetFog(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushBoolean(RenderSettings.fog);
            return 1;
        }
        static int SetFog(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            bool l_value = false;
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.fog = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetFogColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(RenderSettings.fogColor));
            return 1;
        }
        static int SetFogColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                RenderSettings.fogColor = l_color.m_color;

            l_argReader.LogError();
            return 0;
        }

        static int GetFogDensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(RenderSettings.fogDensity);
            return 1;
        }
        static int SetFogDensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.fogDensity = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetFogEndDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(RenderSettings.fogEndDistance);
            return 1;
        }
        static int SetFogEndDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.fogEndDistance = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetFogMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushString(RenderSettings.fogMode.ToString());
            return 1;
        }
        static int SetFogMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            FogMode l_value = FogMode.Linear;
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.fogMode = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetFogStartDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(RenderSettings.fogStartDistance);
            return 1;
        }
        static int SetFogStartDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.fogStartDistance = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetHaloStrength(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(RenderSettings.haloStrength);
            return 1;
        }
        static int SetHaloStrength(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.haloStrength = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetReflectionBounces(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushInteger(RenderSettings.reflectionBounces);
            return 1;
        }
        static int SetReflectionBounces(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            int l_value = 0;
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.reflectionBounces = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetReflectionIntensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(RenderSettings.reflectionIntensity);
            return 1;
        }
        static int SetReflectionIntensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                RenderSettings.reflectionIntensity = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetSubtractiveShadowColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(RenderSettings.subtractiveShadowColor));
            return 1;
        }
        static int SetSubtractiveShadowColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                RenderSettings.subtractiveShadowColor = l_color.m_color;

            l_argReader.LogError();
            return 0;
        }
    }
}
