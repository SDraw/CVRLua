using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class RendererDefs
    {
        const string c_destroyed = "Renderer is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsRenderer), IsRenderer));

            ms_instanceProperties.Add(("allowOcclusionWhenDynamic", (GetAllowOcclusionWhenDynamic, SetAllowOcclusionWhenDynamic)));
            ms_instanceProperties.Add(("bounds", (GetBounds, null)));
            ms_instanceProperties.Add(("enabled", (GetEnabled, SetEnabled)));
            ms_instanceProperties.Add(("forceRenderingOff", (GetForceRenderingOff, SetForceRenderingOff)));
            ms_instanceProperties.Add(("isPartOfStaticBatch", (GetIsPartOfStaticBatch, null)));
            ms_instanceProperties.Add(("isVisible", (GetIsVisible, null)));
            ms_instanceProperties.Add(("lightmapIndex", (GetLightmapIndex, SetLightmapIndex)));
            ms_instanceProperties.Add(("lightmapScaleOffset", (GetLightmapScaleOffset, SetLightmapScaleOffset)));
            ms_instanceProperties.Add(("lightProbeProxyVolumeOverride", (GetLightProbeProxyVolumeOverride, SetLightProbeProxyVolumeOverride)));
            ms_instanceProperties.Add(("lightProbeUsage", (GetLightProbeUsage, SetLightProbeUsage)));
            //ms_instanceProperties.Add(("localToWorldMatrix", (?, null)));
            //ms_instanceProperties.Add(("material", (?, ?)));
            //ms_instanceProperties.Add(("materials", (?, ?)));
            ms_instanceProperties.Add(("motionVectorGenerationMode", (GetMotionVectorGenerationMode, SetMotionVectorGenerationMode)));
            ms_instanceProperties.Add(("probeAnchor", (GetProbeAnchor, SetProbeAnchor)));
            ms_instanceProperties.Add(("rayTracingMode", (GetRayTracingMode, SetRayTracingMode)));
            ms_instanceProperties.Add(("realtimeLightmapIndex", (GetRealtimeLightmapIndex, SetRealtimeLightmapIndex)));
            ms_instanceProperties.Add(("realtimeLightmapScaleOffset", (GetRealtimeLightmapScaleOffset, SetRealtimeLightmapScaleOffset)));
            ms_instanceProperties.Add(("receiveShadows", (GetReceiveShadows, SetReceiveShadows)));
            ms_instanceProperties.Add(("reflectionProbeUsage", (GetReflectionProbeUsage, SetReflectionProbeUsage)));
            ms_instanceProperties.Add(("rendererPriority", (GetRendererPriority, SetRendererPriority)));
            ms_instanceProperties.Add(("renderingLayerMask", (GetRenderingLayerMask, SetRenderingLayerMask)));
            ms_instanceProperties.Add(("shadowCastingMode", (GetShadowCastingMode, SetShadowCastingMode)));
            //ms_instanceProperties.Add(("sharedMaterial", (?, ?)));
            //ms_instanceProperties.Add(("sharedMaterials", (?, ?)));
            ms_instanceProperties.Add(("sortingLayerID", (GetSortingLayerID, SetSortingLayerID)));
            ms_instanceProperties.Add(("sortingLayerName", (GetSortingLayerName, SetSortingLayerName)));
            ms_instanceProperties.Add(("sortingOrder", (GetSortingOrder, SetSortingOrder)));
            //ms_instanceProperties.Add(("worldToLocalMatrix", (?, null)));

            //ms_instanceMethods.Add((nameof(GetClosestReflectionProbes), GetClosestReflectionProbes));
            //ms_instanceMethods.Add((nameof(GetMaterials), GetMaterials));
            //ms_instanceMethods.Add((nameof(GetPropertyBlock), GetPropertyBlock));
            //ms_instanceMethods.Add((nameof(GetSharedMaterials), GetSharedMaterials));
            ms_instanceMethods.Add((nameof(HasPropertyBlock), HasPropertyBlock));
            //ms_instanceMethods.Add((nameof(SetPropertyBlock), SetPropertyBlock));

            ComponentDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Renderer), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
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
        static int IsRenderer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadNextObject(ref l_render);
            l_argReader.PushBoolean(l_render != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAllowOcclusionWhenDynamic(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.allowOcclusionWhenDynamic);
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
        static int SetAllowOcclusionWhenDynamic(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.allowOcclusionWhenDynamic = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetBounds(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushObject(new Wrappers.Bounds(l_render.bounds));
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

        static int GetEnabled(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.enabled);
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
        static int SetEnabled(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.enabled = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetForceRenderingOff(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.forceRenderingOff);
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
        static int SetForceRenderingOff(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.forceRenderingOff = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetIsPartOfStaticBatch(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.isPartOfStaticBatch);
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

        static int GetIsVisible(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.isVisible);
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

        static int GetLightmapIndex(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushInteger(l_render.lightmapIndex);
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
        static int SetLightmapIndex(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.lightmapIndex = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetLightmapScaleOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushObject(new Wrappers.Vector4(l_render.lightmapScaleOffset));
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
        static int SetLightmapScaleOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            Wrappers.Vector4 l_value = null;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.lightmapScaleOffset = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetLightProbeProxyVolumeOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                {
                    if(l_render.lightProbeProxyVolumeOverride != null)
                        l_argReader.PushObject(l_render.lightProbeProxyVolumeOverride);
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
        static int SetLightProbeProxyVolumeOverride(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            GameObject l_value = null;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadNextObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                {
                    if(l_value != null)
                        l_render.lightProbeProxyVolumeOverride = l_value;
                    else
                        l_render.lightProbeProxyVolumeOverride = null;
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLightProbeUsage(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushString(l_render.lightProbeUsage.ToString());
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
        static int SetLightProbeUsage(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            UnityEngine.Rendering.LightProbeUsage l_value = UnityEngine.Rendering.LightProbeUsage.Off;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.lightProbeUsage = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMotionVectorGenerationMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushString(l_render.motionVectorGenerationMode.ToString());
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
        static int SetMotionVectorGenerationMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            MotionVectorGenerationMode l_value = MotionVectorGenerationMode.Camera;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.motionVectorGenerationMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetProbeAnchor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                {
                    if(l_render.probeAnchor != null)
                        l_argReader.PushObject(l_render.probeAnchor);
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
        static int SetProbeAnchor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            Transform l_value = null;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadNextObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                {
                    if(l_value != null)
                        l_render.probeAnchor = l_value;
                    else
                        l_render.probeAnchor = null;
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRayTracingMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushString(l_render.rayTracingMode.ToString());
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
        static int SetRayTracingMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            UnityEngine.Experimental.Rendering.RayTracingMode l_value = UnityEngine.Experimental.Rendering.RayTracingMode.Off;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.rayTracingMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRealtimeLightmapIndex(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushInteger(l_render.realtimeLightmapIndex);
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
        static int SetRealtimeLightmapIndex(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.realtimeLightmapIndex = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetRealtimeLightmapScaleOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushObject(new Wrappers.Vector4(l_render.realtimeLightmapScaleOffset));
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
        static int SetRealtimeLightmapScaleOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            Wrappers.Vector4 l_value = null;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.realtimeLightmapScaleOffset = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetReceiveShadows(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.receiveShadows);
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
        static int SetReceiveShadows(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.receiveShadows = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetReflectionProbeUsage(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushString(l_render.reflectionProbeUsage.ToString());
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
        static int SetReflectionProbeUsage(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            UnityEngine.Rendering.ReflectionProbeUsage l_value = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.reflectionProbeUsage = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRendererPriority(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushInteger(l_render.rendererPriority);
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
        static int SetRendererPriority(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.rendererPriority = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetRenderingLayerMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushInteger(l_render.renderingLayerMask);
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
            Renderer l_render = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.renderingLayerMask = (uint)l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetShadowCastingMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushString(l_render.shadowCastingMode.ToString());
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
        static int SetShadowCastingMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            UnityEngine.Rendering.ShadowCastingMode l_value = UnityEngine.Rendering.ShadowCastingMode.Off;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.shadowCastingMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSortingLayerID(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushInteger(l_render.sortingLayerID);
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
        static int SetSortingLayerID(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.sortingLayerID = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetSortingLayerName(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushString(l_render.sortingLayerName);
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
        static int SetSortingLayerName(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            string l_value = "";
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadString(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.sortingLayerName = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        static int GetSortingOrder(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushInteger(l_render.sortingOrder);
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
        static int SetSortingOrder(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.sortingOrder = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }
            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int HasPropertyBlock(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Renderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.HasPropertyBlock());
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
