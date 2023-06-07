using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class LightDefs
    {
        const string c_destroyed = "Light is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsLight), IsLight));

            //ms_instanceProperties.Add(("bakingOutput", (?,?)));
            ms_instanceProperties.Add(("bounceIntensity", (GetBounceIntensity, SetBounceIntensity)));
            ms_instanceProperties.Add(("boundingSphereOverride", (GetBoundingSphereOverride, SetBoundingSphereOverride)));
            ms_instanceProperties.Add(("color", (GetColor, SetColor)));
            ms_instanceProperties.Add(("colorTemperature", (GetColorTemperature, SetColorTemperature)));
            ms_instanceProperties.Add(("commandBufferCount", (GetCommandBufferCount, null)));
            // ms_instanceProperties.Add(("cookie", (?, ?)));
            ms_instanceProperties.Add(("cookieSize", (GetCookieSize, SetCookieSize)));
            ms_instanceProperties.Add(("cullingMask", (GetCullingMask, SetCullingMask)));
            //ms_instanceProperties.Add(("flare", (?, ?)));
            ms_instanceProperties.Add(("innerSpotAngle", (GetInnerSpotAngle, SetInnerSpotAngle)));
            ms_instanceProperties.Add(("intensity", (GetIntensity, SetIntensity)));
            ms_instanceProperties.Add(("layerShadowCullDistances", (GetLayerShadowCullDistances, null)));
            ms_instanceProperties.Add(("lightShadowCasterMode", (GetLightShadowCasterMode, SetLightShadowCasterMode)));
            ms_instanceProperties.Add(("range", (GetRange, SetRange)));
            ms_instanceProperties.Add(("renderingLayerMask", (GetRenderingLayerMask, SetRenderingLayerMask)));
            ms_instanceProperties.Add(("renderMode", (GetRenderMode, SetRenderMode)));
            //ms_instanceProperties.Add(("shadowAngle", (GetShadowAngle, SetShadowAngle))); // Where is it, Unity???
            ms_instanceProperties.Add(("shadowBias", (GetShadowBias, SetShadowBias)));
            ms_instanceProperties.Add(("shadowCustomResolution", (GetShadowCustomResolution, SetShadowCustomResolution)));
            ms_instanceProperties.Add(("shadowMatrixOverride", (GetShadowMatrixOverride, SetShadowMatrixOverride)));
            ms_instanceProperties.Add(("shadowNearPlane", (GetShadowNearPlane, SetShadowNearPlane)));
            ms_instanceProperties.Add(("shadowNormalBias", (GetShadowNormalBias, SetShadowNormalBias)));
            //ms_instanceProperties.Add(("shadowRadius", (GetShadowRadius, SetShadowRadius))); // Where is it, Unity???
            ms_instanceProperties.Add(("shadowResolution", (GetShadowResolution, SetShadowResolution)));
            ms_instanceProperties.Add(("shadows", (GetShadows, SetShadows)));
            ms_instanceProperties.Add(("shadowStrength", (GetShadowStrength, SetShadowStrength)));
            ms_instanceProperties.Add(("shape", (GetShape, SetShape)));
            ms_instanceProperties.Add(("spotAngle", (GetSpotAngle, SetSpotAngle)));
            ms_instanceProperties.Add(("type", (GetLightType, SetLightType)));
            ms_instanceProperties.Add(("useBoundingSphereOverride", (GetUseBoundingSphereOverride, SetUseBoundingSphereOverride)));
            ms_instanceProperties.Add(("useColorTemperature", (GetUseColorTemperature, SetUseColorTemperature)));
            ms_instanceProperties.Add(("useShadowMatrixOverride", (GetUseShadowMatrixOverride, SetUseShadowMatrixOverride)));

            //ms_instanceMethods.Add((nameof(AddCommandBuffer), AddCommandBuffer));
            //ms_instanceMethods.Add((nameof(AddCommandBufferAsync), AddCommandBufferAsync));
            //ms_instanceMethods.Add((nameof(GetCommandBuffers), GetCommandBuffers));
            ms_instanceMethods.Add((nameof(RemoveAllCommandBuffers), RemoveAllCommandBuffers));
            //ms_instanceMethods.Add((nameof(RemoveCommandBuffer), RemoveCommandBuffer));
            //ms_instanceMethods.Add((nameof(RemoveCommandBuffers), RemoveCommandBuffers));
            ms_instanceMethods.Add((nameof(Reset), Reset));

            BehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Light), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsLight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadNextObject(ref l_light);
            l_argReader.PushBoolean(l_light != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetBounceIntensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.intensity);
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
        static int SetBounceIntensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.intensity = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetBoundingSphereOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushObject(new Wrappers.Vector4(l_light.boundingSphereOverride));
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
        static int SetBoundingSphereOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            Wrappers.Vector4 l_value = null;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.boundingSphereOverride = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushObject(new Wrappers.Color(l_light.color));
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
        static int SetColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            Wrappers.Color l_value = null;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.color = l_value.m_color;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetColorTemperature(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.colorTemperature);
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
        static int SetColorTemperature(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.colorTemperature = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCommandBufferCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushInteger(l_light.commandBufferCount);
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

        static int GetCookieSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.cookieSize);
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
        static int SetCookieSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.cookieSize = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCullingMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushInteger(l_light.cullingMask);
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
        static int SetCullingMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.cullingMask = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetInnerSpotAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.innerSpotAngle);
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
        static int SetInnerSpotAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.innerSpotAngle = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIntensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.intensity);
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
        static int SetIntensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.intensity = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLayerShadowCullDistances(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushTable(l_light.layerShadowCullDistances);
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

        static int GetLightShadowCasterMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushString(l_light.lightShadowCasterMode.ToString());
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
        static int SetLightShadowCasterMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            LightShadowCasterMode l_value = LightShadowCasterMode.Default;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.lightShadowCasterMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRange(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.range);
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
        static int SetRange(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.range = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRenderingLayerMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushInteger(l_light.renderingLayerMask);
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
        static int SetRenderingLayerMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.renderingLayerMask = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRenderMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushString(l_light.renderMode.ToString());
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
        static int SetRenderMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            LightRenderMode l_value = LightRenderMode.Auto;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.renderMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetShadowBias(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.shadowBias);
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
        static int SetShadowBias(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.shadowBias = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetShadowCustomResolution(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushInteger(l_light.shadowCustomResolution);
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
        static int SetShadowCustomResolution(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.shadowCustomResolution = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetShadowMatrixOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_light.shadowMatrixOverride));
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
        static int SetShadowMatrixOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            Wrappers.Matrix4x4 l_value = null;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.shadowMatrixOverride = l_value.m_mat;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetShadowNearPlane(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.shadowNearPlane);
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
        static int SetShadowNearPlane(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.shadowNearPlane = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetShadowNormalBias(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.shadowNormalBias);
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
        static int SetShadowNormalBias(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.shadowNormalBias = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetShadowResolution(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushString(l_light.shadowResolution.ToString());
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
        static int SetShadowResolution(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            UnityEngine.Rendering.LightShadowResolution l_value = UnityEngine.Rendering.LightShadowResolution.FromQualitySettings;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.shadowResolution = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetShadows(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushString(l_light.shadows.ToString());
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
        static int SetShadows(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            LightShadows l_value = LightShadows.None;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.shadows = l_value;

                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetShadowStrength(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.shadowStrength);
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
        static int SetShadowStrength(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.shadowStrength = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetShape(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushString(l_light.shape.ToString());
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
        static int SetShape(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            LightShape l_value = LightShape.Cone;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.shape = l_value;

                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSpotAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushNumber(l_light.spotAngle);
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
        static int SetSpotAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.spotAngle = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLightType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushString(l_light.type.ToString());
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
        static int SetLightType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            LightType l_value = LightType.Spot;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.type = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUseBoundingSphereOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushBoolean(l_light.useBoundingSphereOverride);
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
        static int SetUseBoundingSphereOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.useBoundingSphereOverride = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUseColorTemperature(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushBoolean(l_light.useColorTemperature);
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
        static int SetUseColorTemperature(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.useColorTemperature = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUseShadowMatrixOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_argReader.PushBoolean(l_light.useShadowMatrixOverride);
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
        static int SetUseShadowMatrixOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_light);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                    l_light.useShadowMatrixOverride = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int RemoveAllCommandBuffers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                {
                    l_light.RemoveAllCommandBuffers();
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

        static int Reset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Light l_light = null;
            l_argReader.ReadObject(ref l_light);
            if(!l_argReader.HasErrors())
            {
                if(l_light != null)
                {
                    l_light.Reset();
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
