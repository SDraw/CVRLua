using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class CameraDefs
    {
        const string c_destroyed = "Camera is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsCamera), IsCamera));
            //ms_staticMethods.Add((nameof(CalculateProjectionMatrixFromPhysicalProperties), CalculateProjectionMatrixFromPhysicalProperties));
            ms_staticMethods.Add((nameof(FieldOfViewToFocalLength), FieldOfViewToFocalLength));
            ms_staticMethods.Add((nameof(FocalLengthToFieldOfView), FocalLengthToFieldOfView));
            ms_staticMethods.Add((nameof(HorizontalToVerticalFieldOfView), HorizontalToVerticalFieldOfView));
            ms_staticMethods.Add((nameof(VerticalToHorizontalFieldOfView), VerticalToHorizontalFieldOfView));

            //ms_instanceProperties.Add(("activeTexture", (?,?)));
            ms_instanceProperties.Add(("actualRenderingPath", (GetActualRenderingPath, null)));
            ms_instanceProperties.Add(("allowDynamicResolution", (GetAllowDynamicResolution, SetAllowDynamicResolution)));
            ms_instanceProperties.Add(("allowHDR", (GetAllowHDR, SetAllowHDR)));
            ms_instanceProperties.Add(("allowMSAA", (GetAllowMSAA, SetAllowMSAA)));
            ms_instanceProperties.Add(("areVRStereoViewMatricesWithinSingleCullTolerance", (GetAreVRStereoViewMatricesWithinSingleCullTolerance, null)));
            ms_instanceProperties.Add(("aspect", (GetAspect, SetAspect)));
            ms_instanceProperties.Add(("backgroundColor", (GetBackgroundColor, SetBackgroundColor)));
            ms_instanceProperties.Add(("cameraToWorldMatrix", (GetCameraToWorldMatrix, null)));
            ms_instanceProperties.Add(("cameraType", (GetCameraType, SetCameraType)));
            ms_instanceProperties.Add(("clearFlags", (GetClearFlags, SetClearFlags)));
            ms_instanceProperties.Add(("clearStencilAfterLightingPass", (GetClearStencilAfterLightingPass, SetClearStencilAfterLightingPass)));
            ms_instanceProperties.Add(("commandBufferCount", (GetCommandBufferCount, null)));
            ms_instanceProperties.Add(("cullingMask", (GetCullingMask, SetCullingMask)));
            ms_instanceProperties.Add(("cullingMatrix", (GetCullingMatrix, SetCullingMatrix)));
            ms_instanceProperties.Add(("depth", (GetDepth, SetDepth)));
            ms_instanceProperties.Add(("depthTextureMode", (GetDepthTextureMode, SetDepthTextureMode)));
            ms_instanceProperties.Add(("eventMask", (GetEventMask, SetEventMask)));
            ms_instanceProperties.Add(("farClipPlane", (GetFarClipPlane, SetFarClipPlane)));
            ms_instanceProperties.Add(("fieldOfView", (GetFieldOfView, SetFieldOfView)));
            ms_instanceProperties.Add(("focalLength", (GetFocalLength, SetFocalLength)));
            ms_instanceProperties.Add(("forceIntoRenderTexture", (GetForceIntoRenderTexture, SetForceIntoRenderTexture)));
            ms_instanceProperties.Add(("gateFit", (GetGateFit, SetGateFit)));
            ms_instanceProperties.Add(("layerCullDistances", (GetLayerCullDistances, null)));
            ms_instanceProperties.Add(("layerCullSpherical", (GetLayerCullSpherical, SetLayerCullSpherical)));
            ms_instanceProperties.Add(("lensShift", (GetLensShift, SetLensShift)));
            ms_instanceProperties.Add(("nearClipPlane", (GetNearClipPlane, SetNearClipPlane)));
            ms_instanceProperties.Add(("nonJitteredProjectionMatrix", (GetNonJitteredProjectionMatrix, SetNonJitteredProjectionMatrix)));
            ms_instanceProperties.Add(("opaqueSortMode", (GetOpaqueSortMode, SetOpaqueSortMode)));
            ms_instanceProperties.Add(("orthographic", (GetOrthographic, SetOrthographic)));
            ms_instanceProperties.Add(("orthographicSize", (GetOrthographicSize, SetOrthographicSize)));
            ms_instanceProperties.Add(("overrideSceneCullingMask", (GetOverrideSceneCullingMask, SetOverrideSceneCullingMask)));
            ms_instanceProperties.Add(("pixelHeight", (GetPixelHeight, null)));
            //ms_instanceProperties.Add(("pixelRect", (?, ?)));
            ms_instanceProperties.Add(("pixelWidth", (GetPixelWidth, null)));
            ms_instanceProperties.Add(("previousViewProjectionMatrix", (GetPreviousViewProjectionMatrix, null)));
            ms_instanceProperties.Add(("projectionMatrix", (GetProjectionMatrix, SetProjectionMatrix)));
            //ms_instanceProperties.Add(("rect", (?, ?)));
            ms_instanceProperties.Add(("renderingPath", (GetRenderingPath, SetRenderingPath)));
            ms_instanceProperties.Add(("scaledPixelHeight", (GetScaledPixelHeight, null)));
            ms_instanceProperties.Add(("scaledPixelWidth", (GetScaledPixelWidth, null)));
            //ms_instanceProperties.Add(("scene", (?, ?)));
            ms_instanceProperties.Add(("sensorSize", (GetSensorSize, SetSensorSize)));
            ms_instanceProperties.Add(("stereoActiveEye", (GetStereoActiveEye, null)));
            ms_instanceProperties.Add(("stereoConvergence", (GetStereoConvergence, SetStereoConvergence)));
            ms_instanceProperties.Add(("stereoEnabled", (GetStereoEnabled, null)));
            ms_instanceProperties.Add(("stereoSeparation", (GetStereoSeparation, SetStereoSeparation)));
            ms_instanceProperties.Add(("stereoTargetEye", (GetStereoTargetEye, SetStereoTargetEye)));
            //ms_instanceProperties.Add(("targetDisplay", (?, ?))); // Don't allow it yet
            //ms_instanceProperties.Add(("targetTexture", (?, ?)));
            ms_instanceProperties.Add(("transparencySortAxis", (GetTransparencySortAxis, SetTransparencySortAxis)));
            ms_instanceProperties.Add(("transparencySortMode", (GetTransparencySortMode, SetTransparencySortMode)));
            ms_instanceProperties.Add(("useJitteredProjectionMatrixForTransparentRendering", (GetUseJitteredProjectionMatrixForTransparentRendering, SetUseJitteredProjectionMatrixForTransparentRendering))); // Whoa
            ms_instanceProperties.Add(("useOcclusionCulling", (GetUseOcclusionCulling, SetUseOcclusionCulling)));
            ms_instanceProperties.Add(("usePhysicalProperties", (GetUsePhysicalProperties, SetUsePhysicalProperties)));
            ms_instanceProperties.Add(("velocity", (GetVelocity, null)));
            ms_instanceProperties.Add(("worldToCameraMatrix", (GetWorldToCameraMatrix, SetWorldToCameraMatrix)));

            //ms_instanceMethods.Add((nameof(AddCommandBuffer), AddCommandBuffer));
            //ms_instanceMethods.Add((nameof(AddCommandBufferAsync), AddCommandBufferAsync));
            //ms_instanceMethods.Add((nameof(CalculateFrustumCorners), CalculateFrustumCorners));
            ms_instanceMethods.Add((nameof(CalculateObliqueMatrix), CalculateObliqueMatrix));
            ms_instanceMethods.Add((nameof(CopyFrom), CopyFrom));
            ms_instanceMethods.Add((nameof(CopyStereoDeviceProjectionMatrixToNonJittered), CopyStereoDeviceProjectionMatrixToNonJittered));
            //ms_instanceMethods.Add((nameof(GetCommandBuffers), GetCommandBuffers));
            ms_instanceMethods.Add((nameof(GetGateFittedFieldOfView), GetGateFittedFieldOfView));
            ms_instanceMethods.Add((nameof(GetGateFittedLensShift), GetGateFittedLensShift));
            ms_instanceMethods.Add((nameof(GetStereoNonJitteredProjectionMatrix), GetStereoNonJitteredProjectionMatrix));
            ms_instanceMethods.Add((nameof(GetStereoProjectionMatrix), GetStereoProjectionMatrix));
            ms_instanceMethods.Add((nameof(GetStereoViewMatrix), GetStereoViewMatrix));
            ms_instanceMethods.Add((nameof(RemoveAllCommandBuffers), RemoveAllCommandBuffers));
            //ms_instanceMethods.Add((nameof(RemoveCommandBuffer), RemoveCommandBuffer));
            //ms_instanceMethods.Add((nameof(RemoveCommandBuffers), RemoveCommandBuffers));
            ms_instanceMethods.Add((nameof(Render), Render));
            //ms_instanceMethods.Add((nameof(RenderToCubemap), RenderToCubemap));
            //ms_instanceMethods.Add((nameof(RenderWithShader), RenderWithShader));
            ms_instanceMethods.Add((nameof(Reset), Reset));
            ms_instanceMethods.Add((nameof(ResetAspect), ResetAspect));
            ms_instanceMethods.Add((nameof(ResetCullingMatrix), ResetCullingMatrix));
            ms_instanceMethods.Add((nameof(ResetProjectionMatrix), ResetProjectionMatrix));
            ms_instanceMethods.Add((nameof(ResetStereoProjectionMatrices), ResetStereoProjectionMatrices));
            ms_instanceMethods.Add((nameof(ResetStereoViewMatrices), ResetStereoViewMatrices));
            ms_instanceMethods.Add((nameof(ResetTransparencySortSettings), ResetTransparencySortSettings));
            ms_instanceMethods.Add((nameof(ResetWorldToCameraMatrix), ResetWorldToCameraMatrix));
            ms_instanceMethods.Add((nameof(ScreenPointToRay), ScreenPointToRay));
            ms_instanceMethods.Add((nameof(ScreenToViewportPoint), ScreenToViewportPoint));
            ms_instanceMethods.Add((nameof(ScreenToWorldPoint), ScreenToWorldPoint));
            //ms_instanceMethods.Add((nameof(SetReplacementShader), SetReplacementShader));
            ms_instanceMethods.Add((nameof(SetStereoProjectionMatrix), SetStereoProjectionMatrix));
            ms_instanceMethods.Add((nameof(SetStereoViewMatrix), SetStereoViewMatrix));
            //ms_instanceMethods.Add((nameof(SetTargetBuffers), TryGetCullingParameters));
            //ms_instanceMethods.Add((nameof(TryGetCullingParameters), TryGetCullingParameters));
            ms_instanceMethods.Add((nameof(ViewportPointToRay), ViewportPointToRay));
            ms_instanceMethods.Add((nameof(ViewportToScreenPoint), ViewportToScreenPoint));
            ms_instanceMethods.Add((nameof(ViewportToWorldPoint), ViewportToWorldPoint));
            ms_instanceMethods.Add((nameof(WorldToScreenPoint), WorldToScreenPoint));
            ms_instanceMethods.Add((nameof(WorldToViewportPoint), WorldToViewportPoint));

            BehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Camera), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsCamera), IsCamera);
        }


        // Static properties
        static int GetAllCamerasCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushInteger(Camera.allCamerasCount);
            return 1;
        }

        // Static methods
        static int IsCamera(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadNextObject(ref l_camera);
            l_argReader.PushBoolean(l_camera != null);
            return l_argReader.GetReturnValue();
        }

        static int FieldOfViewToFocalLength(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueA = 0f;
            float l_valueB = 0f;
            l_argReader.ReadNumber(ref l_valueA);
            l_argReader.ReadNumber(ref l_valueB);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Camera.FieldOfViewToFocalLength(l_valueA, l_valueB));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int FocalLengthToFieldOfView(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueA = 0f;
            float l_valueB = 0f;
            l_argReader.ReadNumber(ref l_valueA);
            l_argReader.ReadNumber(ref l_valueB);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Camera.FocalLengthToFieldOfView(l_valueA, l_valueB));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int HorizontalToVerticalFieldOfView(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueA = 0f;
            float l_valueB = 0f;
            l_argReader.ReadNumber(ref l_valueA);
            l_argReader.ReadNumber(ref l_valueB);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Camera.HorizontalToVerticalFieldOfView(l_valueA, l_valueB));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int VerticalToHorizontalFieldOfView(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueA = 0f;
            float l_valueB = 0f;
            l_argReader.ReadNumber(ref l_valueA);
            l_argReader.ReadNumber(ref l_valueB);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Camera.VerticalToHorizontalFieldOfView(l_valueA, l_valueB));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Instance methods
        static int GetActualRenderingPath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.actualRenderingPath.ToString());
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

        static int GetAllowDynamicResolution(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.allowDynamicResolution);
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
        static int SetAllowDynamicResolution(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.allowDynamicResolution = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAllowHDR(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.allowHDR);
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
        static int SetAllowHDR(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.allowHDR = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAllowMSAA(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.allowMSAA);
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
        static int SetAllowMSAA(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.allowMSAA = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAreVRStereoViewMatricesWithinSingleCullTolerance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.areVRStereoViewMatricesWithinSingleCullTolerance);
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

        static int GetAspect(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.aspect);
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
        static int SetAspect(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.aspect = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetBackgroundColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Color(l_camera.backgroundColor));
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
        static int SetBackgroundColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Color l_value = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.backgroundColor = l_value.m_color;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCameraToWorldMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.cameraToWorldMatrix));
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

        static int GetCameraType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.cameraType.ToString());
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
        static int SetCameraType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            CameraType l_value = CameraType.Game;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.cameraType = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetClearFlags(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.clearFlags.ToString());
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
        static int SetClearFlags(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            CameraClearFlags l_value = CameraClearFlags.Skybox;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.clearFlags = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetClearStencilAfterLightingPass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.clearStencilAfterLightingPass);
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
        static int SetClearStencilAfterLightingPass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.clearStencilAfterLightingPass = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCommandBufferCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushInteger(l_camera.commandBufferCount);
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

        static int GetCullingMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushInteger(l_camera.cullingMask);
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
            Camera l_camera = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.cullingMask = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCullingMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.cullingMatrix));
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
        static int SetCullingMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Matrix4x4 l_value = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.cullingMatrix = l_value.m_mat;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDepth(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.depth);
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
        static int SetDepth(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.depth = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDepthTextureMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.depthTextureMode.ToString());
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
        static int SetDepthTextureMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            DepthTextureMode l_value = DepthTextureMode.None;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.depthTextureMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetEventMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushInteger(l_camera.eventMask);
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
        static int SetEventMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.eventMask = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetFarClipPlane(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.farClipPlane);
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
        static int SetFarClipPlane(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.farClipPlane = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetFieldOfView(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.fieldOfView);
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
        static int SetFieldOfView(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.fieldOfView = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetFocalLength(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.focalLength);
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
        static int SetFocalLength(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.focalLength = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetForceIntoRenderTexture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.forceIntoRenderTexture);
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
        static int SetForceIntoRenderTexture(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.forceIntoRenderTexture = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetGateFit(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.gateFit.ToString());
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
        static int SetGateFit(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Camera.GateFitMode l_value = Camera.GateFitMode.None;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.gateFit = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLayerCullDistances(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushTable(l_camera.layerCullDistances);
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

        static int GetLayerCullSpherical(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.layerCullSpherical);
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
        static int SetLayerCullSpherical(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.layerCullSpherical = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLensShift(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector2(l_camera.lensShift));
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
        static int SetLensShift(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector2 l_value = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.lensShift = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetNearClipPlane(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.nearClipPlane);
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
        static int SetNearClipPlane(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.nearClipPlane = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetNonJitteredProjectionMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.nonJitteredProjectionMatrix));
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
        static int SetNonJitteredProjectionMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Matrix4x4 l_value = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.nonJitteredProjectionMatrix = l_value.m_mat;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetOpaqueSortMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.opaqueSortMode.ToString());
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
        static int SetOpaqueSortMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            UnityEngine.Rendering.OpaqueSortMode l_value = UnityEngine.Rendering.OpaqueSortMode.Default;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.opaqueSortMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetOrthographic(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.orthographic);
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
        static int SetOrthographic(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.orthographic = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetOrthographicSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.orthographicSize);
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
        static int SetOrthographicSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.orthographicSize = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetOverrideSceneCullingMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushInteger((long)l_camera.overrideSceneCullingMask);
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
        static int SetOverrideSceneCullingMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            long l_value = 0;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.overrideSceneCullingMask = (ulong)l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPixelHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushInteger(l_camera.pixelHeight);
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

        static int GetPixelWidth(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_argReader.PushInteger(l_camera.pixelWidth);
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

        static int GetRenderingPath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.renderingPath.ToString());
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
        static int SetRenderingPath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            RenderingPath l_value = RenderingPath.Forward;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.renderingPath = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPreviousViewProjectionMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.previousViewProjectionMatrix
));
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

        static int GetProjectionMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.projectionMatrix));
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
        static int SetProjectionMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Matrix4x4 l_value = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.projectionMatrix = l_value.m_mat;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetScaledPixelHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_argReader.PushInteger(l_camera.scaledPixelHeight);
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

        static int GetScaledPixelWidth(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_argReader.PushInteger(l_camera.scaledPixelWidth);
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

        static int GetSensorSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector2(l_camera.sensorSize));
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
        static int SetSensorSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector2 l_value = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.sensorSize = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetStereoActiveEye(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.stereoActiveEye.ToString());
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

        static int GetStereoConvergence(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.stereoConvergence);
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
        static int SetStereoConvergence(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.stereoConvergence = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetStereoEnabled(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.stereoEnabled);
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

        static int GetStereoSeparation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.stereoSeparation);
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
        static int SetStereoSeparation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)

                    l_camera.stereoSeparation
 = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetStereoTargetEye(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.stereoTargetEye.ToString());
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
        static int SetStereoTargetEye(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            StereoTargetEyeMask l_value = StereoTargetEyeMask.None;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.stereoTargetEye = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTransparencySortAxis(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_camera.transparencySortAxis));
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
        static int SetTransparencySortAxis(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.transparencySortAxis = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTransparencySortMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushString(l_camera.transparencySortMode.ToString());
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
        static int SetTransparencySortMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            TransparencySortMode l_value = TransparencySortMode.Default;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.transparencySortMode = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUseJitteredProjectionMatrixForTransparentRendering(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.useJitteredProjectionMatrixForTransparentRendering);
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
        static int SetUseJitteredProjectionMatrixForTransparentRendering(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.useJitteredProjectionMatrixForTransparentRendering = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUseOcclusionCulling(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.useOcclusionCulling);
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
        static int SetUseOcclusionCulling(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.useOcclusionCulling = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUsePhysicalProperties(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushBoolean(l_camera.usePhysicalProperties);
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
        static int SetUsePhysicalProperties(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.usePhysicalProperties = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_camera.velocity));
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

        static int GetWorldToCameraMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            l_argReader.ReadObject(ref l_camera);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.worldToCameraMatrix));
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
        static int SetWorldToCameraMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Matrix4x4 l_value = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_camera.worldToCameraMatrix = l_value.m_mat;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int CalculateObliqueMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector4 l_value = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.CalculateObliqueMatrix(l_value.m_vec)));
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

        static int CopyFrom(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            Camera l_cameraB = null;
            l_argReader.ReadObject(ref l_cameraA);
            l_argReader.ReadObject(ref l_cameraB);
            if(!l_argReader.HasErrors())
            {
                if((l_cameraA != null) && (l_cameraB != null))
                {
                    l_cameraA.CopyFrom(l_cameraB);
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

        static int CopyStereoDeviceProjectionMatrixToNonJittered(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Camera.StereoscopicEye l_eye = Camera.StereoscopicEye.Left;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                {
                    l_camera.CopyStereoDeviceProjectionMatrixToNonJittered(l_eye);
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

        static int GetGateFittedFieldOfView(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Camera.StereoscopicEye l_eye = Camera.StereoscopicEye.Left;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushNumber(l_camera.GetGateFittedFieldOfView());
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

        static int GetGateFittedLensShift(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Camera.StereoscopicEye l_eye = Camera.StereoscopicEye.Left;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector2(l_camera.GetGateFittedLensShift()));
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

        static int GetStereoNonJitteredProjectionMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Camera.StereoscopicEye l_eye = Camera.StereoscopicEye.Left;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.GetStereoNonJitteredProjectionMatrix(l_eye)))
;
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

        static int GetStereoProjectionMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Camera.StereoscopicEye l_eye = Camera.StereoscopicEye.Left;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.GetStereoProjectionMatrix(l_eye)))
;
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

        static int GetStereoViewMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Camera.StereoscopicEye l_eye = Camera.StereoscopicEye.Left;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_camera.GetStereoViewMatrix(l_eye)))
;
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

        static int RemoveAllCommandBuffers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.RemoveAllCommandBuffers();
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

        static int Render(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.Render();
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
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.Reset();
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

        static int ResetAspect(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.ResetAspect();
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

        static int ResetCullingMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.ResetCullingMatrix();
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

        static int ResetProjectionMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.ResetProjectionMatrix();
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

        static int ResetStereoProjectionMatrices(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.ResetStereoProjectionMatrices();
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

        static int ResetStereoViewMatrices(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.ResetStereoViewMatrices();
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

        static int ResetTransparencySortSettings(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.ResetTransparencySortSettings();
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

        static int ResetWorldToCameraMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_cameraA = null;
            l_argReader.ReadObject(ref l_cameraA);
            if(!l_argReader.HasErrors())
            {
                if(l_cameraA != null)
                {
                    l_cameraA.ResetWorldToCameraMatrix();
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

        static int ScreenPointToRay(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector3 l_vec = null;
            Camera.MonoOrStereoscopicEye l_eye = Camera.MonoOrStereoscopicEye.Mono;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNextEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Ray(l_camera.ScreenPointToRay(l_vec.m_vec,l_eye)));
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

        static int ScreenToViewportPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_camera.ScreenToViewportPoint(l_vec.m_vec)));
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

        static int ScreenToWorldPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector3 l_vec = null;
            Camera.MonoOrStereoscopicEye l_eye = Camera.MonoOrStereoscopicEye.Mono;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNextEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_camera.ScreenToWorldPoint(l_vec.m_vec, l_eye)));
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

        static int SetStereoProjectionMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Camera.StereoscopicEye l_eye = Camera.StereoscopicEye.Left;
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_eye);
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                {
                    l_camera.SetStereoProjectionMatrix(l_eye, l_mat.m_mat);
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

        static int SetStereoViewMatrix(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Camera.StereoscopicEye l_eye = Camera.StereoscopicEye.Left;
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadEnum(ref l_eye);
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                {
                    l_camera.SetStereoViewMatrix(l_eye, l_mat.m_mat);
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

        static int ViewportPointToRay(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector3 l_vec = null;
            Camera.MonoOrStereoscopicEye l_eye = Camera.MonoOrStereoscopicEye.Mono;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNextEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Ray(l_camera.ViewportPointToRay(l_vec.m_vec, l_eye)));
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

        static int ViewportToScreenPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_camera.ViewportToScreenPoint(l_vec.m_vec)));
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

        static int ViewportToWorldPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector3 l_vec = null;
            Camera.MonoOrStereoscopicEye l_eye = Camera.MonoOrStereoscopicEye.Mono;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNextEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_camera.ViewportToWorldPoint(l_vec.m_vec, l_eye)));
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

        static int WorldToScreenPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector3 l_vec = null;
            Camera.MonoOrStereoscopicEye l_eye = Camera.MonoOrStereoscopicEye.Mono;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNextEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_camera.WorldToScreenPoint(l_vec.m_vec, l_eye)));
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

        static int WorldToViewportPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Camera l_camera = null;
            Wrappers.Vector3 l_vec = null;
            Camera.MonoOrStereoscopicEye l_eye = Camera.MonoOrStereoscopicEye.Mono;
            l_argReader.ReadObject(ref l_camera);
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNextEnum(ref l_eye);
            if(!l_argReader.HasErrors())
            {
                if(l_camera != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_camera.WorldToViewportPoint(l_vec.m_vec, l_eye)));
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
