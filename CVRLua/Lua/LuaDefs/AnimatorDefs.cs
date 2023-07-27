using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class AnimatorDefs
    {
        const string c_destroyed = "Animator is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(StringToHash), StringToHash));

            ms_instanceProperties.Add(("angularVelocity", (GetAngularVelocity, null)));
            ms_instanceProperties.Add(("applyRootMotion", (GetApplyRootMotion, SetApplyRootMotion)));
            //ms_instanceProperties.Add(("avatar", (?, ?))); // Requires Avatar defs
            ms_instanceProperties.Add(("bodyPosition", (GetBodyPosition, null)));
            ms_instanceProperties.Add(("bodyRotation", (GetBodyRotation, null)));
            ms_instanceProperties.Add(("cullingMode", (GetCullingMode, SetCullingMode)));
            ms_instanceProperties.Add(("deltaPosition", (GetDeltaPosition, null)));
            ms_instanceProperties.Add(("deltaRotation", (GetDeltaRotation, null)));
            ms_instanceProperties.Add(("feetPivotActive", (GetFeetPivotActive, SetFeetPivotActive)));
            ms_instanceProperties.Add(("fireEvents", (GetFireEvents, SetFireEvents)));
            ms_instanceProperties.Add(("gravityWeight", (GetGravityWeight, null)));
            ms_instanceProperties.Add(("hasBoundPlayables", (GetHasBoundPlayables, null)));
            ms_instanceProperties.Add(("hasRootMotion", (GetHasRootMotion, null)));
            ms_instanceProperties.Add(("hasTransformHierarchy", (GetHasTransformHierarchy, null)));
            ms_instanceProperties.Add(("humanScale", (GetHumanScale, null)));
            ms_instanceProperties.Add(("isHuman", (GetIsHuman, null)));
            ms_instanceProperties.Add(("isInitialized", (GetIsInitialized, null)));
            ms_instanceProperties.Add(("isMatchingTarget", (GetIsMatchingTarget, null)));
            ms_instanceProperties.Add(("isOptimizable", (GetIsOptimizable, null)));
            ms_instanceProperties.Add(("keepAnimatorControllerStateOnDisable", (GetKeepAnimatorControllerStateOnDisable, SetKeepAnimatorControllerStateOnDisable)));
            ms_instanceProperties.Add(("layerCount", (GetLayerCount, null)));
            ms_instanceProperties.Add(("layersAffectMassCenter", (GetLayersAffectMassCenter, SetLayersAffectMassCenter)));
            ms_instanceProperties.Add(("leftFeetBottomHeight", (GetLeftFeetBottomHeight, null)));
            ms_instanceProperties.Add(("parameterCount", (GetParameterCount, null)));
            //ms_instanceProperties.Add(("parameters", (?, ?)));
            ms_instanceProperties.Add(("pivotPosition", (GetPivotPosition, null)));
            ms_instanceProperties.Add(("pivotWeight", (GetPivotWeight, null)));
            //ms_instanceProperties.Add(("playableGraph", (?, ?)));
            ms_instanceProperties.Add(("playbackTime", (GetPlaybackTime, SetPlaybackTime)));
            ms_instanceProperties.Add(("recorderMode", (GetRecorderMode, null)));
            ms_instanceProperties.Add(("recorderStartTime", (GetRecorderStartTime, SetRecorderStartTime)));
            ms_instanceProperties.Add(("recorderStopTime", (GetRecorderStopTime, SetRecorderStopTime)));
            ms_instanceProperties.Add(("rightFeetBottomHeight", (GetRightFeetBottomHeight, null)));
            ms_instanceProperties.Add(("rootPosition", (GetRootPosition, null)));
            ms_instanceProperties.Add(("rootRotation", (GetRootRotation, null)));
            //ms_instanceProperties.Add(("runtimeAnimatorController", (?, ?)));
            ms_instanceProperties.Add(("speed", (GetSpeed, SetSpeed)));
            ms_instanceProperties.Add(("stabilizeFeet", (GetStabilizeFeet, SetStabilizeFeet)));
            ms_instanceProperties.Add(("targetPosition", (GetTargetPosition, null)));
            ms_instanceProperties.Add(("targetRotation", (GetTargetRotation, null)));
            ms_instanceProperties.Add(("updateMode", (GetUpdateMode, SetUpdateMode)));
            ms_instanceProperties.Add(("velocity", (GetVelocity, null)));

            ms_instanceMethods.Add((nameof(ApplyBuiltinRootMotion), ApplyBuiltinRootMotion));
            ms_instanceMethods.Add((nameof(CrossFade), CrossFade));
            ms_instanceMethods.Add((nameof(CrossFadeInFixedTime), CrossFadeInFixedTime));
            //ms_instanceMethods.Add((nameof(GetAnimatorTransitionInfo), GetAnimatorTransitionInfo));
            //ms_instanceMethods.Add((nameof(GetBehaviour), GetBehaviour));
            //ms_instanceMethods.Add((nameof(GetBehaviours), GetBehaviours));
            ms_instanceMethods.Add((nameof(GetBoneTransform), GetBoneTransform));
            ms_instanceMethods.Add((nameof(GetBool), GetBool));
            //ms_instanceMethods.Add((nameof(GetCurrentAnimatorClipInfo), GetCurrentAnimatorClipInfo));
            ms_instanceMethods.Add((nameof(GetCurrentAnimatorClipInfoCount), GetCurrentAnimatorClipInfoCount));
            //ms_instanceMethods.Add((nameof(GetCurrentAnimatorStateInfo), GetCurrentAnimatorStateInfo));
            ms_instanceMethods.Add((nameof(GetFloat), GetFloat));
            ms_instanceMethods.Add((nameof(GetIKHintPosition), GetIKHintPosition));
            ms_instanceMethods.Add((nameof(GetIKHintPositionWeight), GetIKHintPositionWeight));
            ms_instanceMethods.Add((nameof(GetIKPosition), GetIKPosition));
            ms_instanceMethods.Add((nameof(GetIKPositionWeight), GetIKPositionWeight));
            ms_instanceMethods.Add((nameof(GetIKRotation), GetIKRotation));
            ms_instanceMethods.Add((nameof(GetIKRotationWeight), GetIKRotationWeight));
            ms_instanceMethods.Add((nameof(GetInteger), GetInteger));
            ms_instanceMethods.Add((nameof(GetLayerIndex), GetLayerIndex));
            ms_instanceMethods.Add((nameof(GetLayerName), GetLayerName));
            ms_instanceMethods.Add((nameof(GetLayerWeight), GetLayerWeight));
            //ms_instanceMethods.Add((nameof(GetNextAnimatorClipInfo), GetNextAnimatorClipInfo));
            ms_instanceMethods.Add((nameof(GetNextAnimatorClipInfoCount), GetNextAnimatorClipInfoCount));
            //ms_instanceMethods.Add((nameof(GetNextAnimatorStateInfo), GetNextAnimatorStateInfo));
            //ms_instanceMethods.Add((nameof(GetParameter), GetParameter));
            ms_instanceMethods.Add((nameof(HasState), HasState));
            ms_instanceMethods.Add((nameof(InterruptMatchTarget), InterruptMatchTarget));
            ms_instanceMethods.Add((nameof(IsInTransition), IsInTransition));
            ms_instanceMethods.Add((nameof(IsParameterControlledByCurve), IsParameterControlledByCurve));
            //ms_instanceMethods.Add((nameof(MatchTarget), MatchTarget));
            ms_instanceMethods.Add((nameof(Play), Play));
            ms_instanceMethods.Add((nameof(PlayInFixedTime), PlayInFixedTime));
            ms_instanceMethods.Add((nameof(Rebind), Rebind));
            ms_instanceMethods.Add((nameof(ResetTrigger), ResetTrigger));
            ms_instanceMethods.Add((nameof(SetBoneLocalRotation), SetBoneLocalRotation));
            ms_instanceMethods.Add((nameof(SetBool), SetBool));
            ms_instanceMethods.Add((nameof(SetFloat), SetFloat));
            ms_instanceMethods.Add((nameof(SetIKHintPosition), SetIKHintPosition));
            ms_instanceMethods.Add((nameof(SetIKHintPositionWeight), SetIKHintPositionWeight));
            ms_instanceMethods.Add((nameof(SetIKPosition), SetIKPosition));
            ms_instanceMethods.Add((nameof(SetIKPositionWeight), SetIKPositionWeight));
            ms_instanceMethods.Add((nameof(SetIKRotation), SetIKRotation));
            ms_instanceMethods.Add((nameof(SetIKRotationWeight), SetIKRotationWeight));
            ms_instanceMethods.Add((nameof(SetInteger), SetInteger));
            ms_instanceMethods.Add((nameof(SetLayerWeight), SetLayerWeight));
            ms_instanceMethods.Add((nameof(SetLookAtPosition), SetLookAtPosition));
            ms_instanceMethods.Add((nameof(SetLookAtWeight), SetLookAtWeight));
            ms_instanceMethods.Add((nameof(SetTarget), SetTarget));
            ms_instanceMethods.Add((nameof(SetTrigger), SetTrigger));
            ms_instanceMethods.Add((nameof(StartPlayback), StartPlayback));
            ms_instanceMethods.Add((nameof(StartRecording), StartRecording));
            ms_instanceMethods.Add((nameof(StopPlayback), StopPlayback));
            ms_instanceMethods.Add((nameof(StopRecording), StopRecording));
            ms_instanceMethods.Add((nameof(Update), Update));
            ms_instanceMethods.Add((nameof(WriteDefaultValues), WriteDefaultValues));

            BehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Animator), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsAnimator), IsAnimator);
        }

        // Static methods
        static int StringToHash(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            string l_str = "";
            l_argReader.ReadString(ref l_str);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(Animator.StringToHash(l_str));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int IsAnimator(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadNextObject(ref l_animator);
            l_argReader.PushBoolean(l_animator != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAngularVelocity(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_animator.angularVelocity));
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

        static int GetApplyRootMotion(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.applyRootMotion);
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
        static int SetApplyRootMotion(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.applyRootMotion = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetBodyPosition(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_animator.bodyPosition));
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

        static int GetBodyRotation(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Quaternion(l_animator.bodyRotation));
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

        static int GetCullingMode(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushString(l_animator.cullingMode.ToString());
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
        static int SetCullingMode(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AnimatorCullingMode l_mode = AnimatorCullingMode.AlwaysAnimate;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.cullingMode = l_mode;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDeltaPosition(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_animator.deltaPosition));
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

        static int GetDeltaRotation(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Quaternion(l_animator.deltaRotation));
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

        static int GetFeetPivotActive(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.feetPivotActive);
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
        static int SetFeetPivotActive(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.feetPivotActive = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetFireEvents(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.fireEvents);
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
        static int SetFireEvents(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.fireEvents = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetGravityWeight(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.gravityWeight);
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

        static int GetHasBoundPlayables(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.hasBoundPlayables);
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

        static int GetHasRootMotion(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.hasRootMotion);
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

        static int GetHasTransformHierarchy(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.hasTransformHierarchy);
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

        static int GetHumanScale(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.humanScale);
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

        static int GetIsHuman(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.isHuman);
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

        static int GetIsInitialized(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.isInitialized);
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

        static int GetIsMatchingTarget(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.isMatchingTarget);
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

        static int GetIsOptimizable(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.isOptimizable);
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

        static int GetKeepAnimatorControllerStateOnDisable(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.keepAnimatorStateOnDisable);
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
        static int SetKeepAnimatorControllerStateOnDisable(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.keepAnimatorStateOnDisable = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLayerCount(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushInteger(l_animator.layerCount);
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

        static int GetLayersAffectMassCenter(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.layersAffectMassCenter);
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
        static int SetLayersAffectMassCenter(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.layersAffectMassCenter = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLeftFeetBottomHeight(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.leftFeetBottomHeight);
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

        static int GetParameterCount(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushInteger(l_animator.parameterCount);
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

        static int GetPivotPosition(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_animator.pivotPosition));
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

        static int GetPivotWeight(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.pivotWeight);
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

        static int GetPlaybackTime(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.playbackTime);
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
        static int SetPlaybackTime(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.playbackTime = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRecorderMode(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushString(l_animator.recorderMode.ToString());
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

        static int GetRecorderStartTime(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.recorderStartTime);
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
        static int SetRecorderStartTime(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.recorderStartTime = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRecorderStopTime(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.recorderStopTime);
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
        static int SetRecorderStopTime(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.recorderStopTime = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRightFeetBottomHeight(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.rightFeetBottomHeight);
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

        static int GetRootPosition(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_animator.rootPosition));
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

        static int GetRootRotation(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Quaternion(l_animator.rootRotation));
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

        static int GetSpeed(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.speed);
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
        static int SetSpeed(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.speed = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetStabilizeFeet(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.stabilizeFeet);
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
        static int SetStabilizeFeet(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.stabilizeFeet = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTargetPosition(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_animator.targetPosition));
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

        static int GetTargetRotation(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Quaternion(l_animator.targetRotation));
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

        static int GetUpdateMode(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushString(l_animator.updateMode.ToString());
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
        static int SetUpdateMode(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AnimatorUpdateMode l_mode = AnimatorUpdateMode.Normal;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_animator.updateMode = l_mode;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetVelocity(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_animator.velocity));
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

        // Instance methods
        static int ApplyBuiltinRootMotion(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.ApplyBuiltinRootMotion();
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

        static int CrossFade(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_state = "";
            float l_duration = 0f;
            int l_layer = -1;
            float l_offset = float.NegativeInfinity;
            float l_transition = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_state);
            l_argReader.ReadNumber(ref l_duration);
            l_argReader.ReadNextInteger(ref l_layer);
            l_argReader.ReadNextNumber(ref l_offset);
            l_argReader.ReadNextNumber(ref l_transition);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.CrossFade(l_state, l_duration, l_layer, l_offset, l_transition);
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

        static int CrossFadeInFixedTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_state = "";
            float l_duration = 0f;
            int l_layer = -1;
            float l_offset = 0f;
            float l_transition = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_state);
            l_argReader.ReadNumber(ref l_duration);
            l_argReader.ReadNextInteger(ref l_layer);
            l_argReader.ReadNextNumber(ref l_offset);
            l_argReader.ReadNextNumber(ref l_transition);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.CrossFadeInFixedTime(l_state, l_duration, l_layer, l_offset, l_transition);
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

        static int GetBoneTransform(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            HumanBodyBones l_bone = HumanBodyBones.LastBone;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_bone);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    Transform l_transform = l_animator.GetBoneTransform(l_bone);
                    if(l_transform != null)
                        l_argReader.PushObject(l_transform);
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

        static int GetBool(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_param = "";
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_param);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.GetBool(l_param));
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

        static int GetCurrentAnimatorClipInfoCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            int l_layer = 0;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadInteger(ref l_layer);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushInteger(l_animator.GetCurrentAnimatorClipInfoCount(l_layer));
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

        static int GetFloat(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_param = "";
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_param);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.GetFloat(l_param));
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

        static int GetIKHintPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKHint l_hint = AvatarIKHint.LeftKnee;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_hint);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_animator.GetIKHintPosition(l_hint)));
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

        static int GetIKHintPositionWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKHint l_hint = AvatarIKHint.LeftKnee;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_hint);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.GetIKHintPositionWeight(l_hint));
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

        static int GetIKPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKGoal l_goal = AvatarIKGoal.LeftFoot;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_goal);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_animator.GetIKPosition(l_goal)));
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

        static int GetIKPositionWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKGoal l_goal = AvatarIKGoal.LeftFoot;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_goal);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.GetIKPositionWeight(l_goal));
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

        static int GetIKRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKGoal l_goal = AvatarIKGoal.LeftFoot;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_goal);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushObject(new Wrappers.Quaternion(l_animator.GetIKRotation(l_goal)));
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

        static int GetIKRotationWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKGoal l_goal = AvatarIKGoal.LeftFoot;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_goal);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.GetIKRotationWeight(l_goal));
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

        static int GetInteger(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_param = "";
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_param);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushInteger(l_animator.GetInteger(l_param));
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

        static int GetLayerIndex(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_layer = "";
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_layer);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushInteger(l_animator.GetLayerIndex(l_layer));
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

        static int GetLayerName(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushString(l_animator.GetLayerName(l_index));
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

        static int GetLayerWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushNumber(l_animator.GetLayerWeight(l_index));
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

        static int GetNextAnimatorClipInfoCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushInteger(l_animator.GetNextAnimatorClipInfoCount(l_index));
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

        static int HasState(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            int l_index = 0;
            int l_state = 0;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadInteger(ref l_index);
            l_argReader.ReadInteger(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.HasState(l_index, l_state));
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

        static int InterruptMatchTarget(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.InterruptMatchTarget();
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

        static int IsInTransition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.IsInTransition(l_index));
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

        static int IsParameterControlledByCurve(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_param = "";
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_param);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                    l_argReader.PushBoolean(l_animator.IsParameterControlledByCurve(l_param));
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
            Animator l_animator = null;
            string l_state = "";
            int l_layer = -1;
            float l_time = float.NegativeInfinity;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_state);
            l_argReader.ReadNextInteger(ref l_layer);
            l_argReader.ReadNextNumber(ref l_time);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.Play(l_state, l_layer, l_time);
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

        static int PlayInFixedTime(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_state = "";
            int l_layer = -1;
            float l_time = float.NegativeInfinity;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_state);
            l_argReader.ReadNextInteger(ref l_layer);
            l_argReader.ReadNextNumber(ref l_time);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.PlayInFixedTime(l_state, l_layer, l_time);
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

        static int Rebind(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.Rebind();
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

        static int ResetTrigger(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_name = "";
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_name);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.ResetTrigger(l_name);
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

        static int SetBoneLocalRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            HumanBodyBones l_bone = HumanBodyBones.LastBone;
            Wrappers.Quaternion l_rot = null;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_bone);
            l_argReader.ReadObject(ref l_rot);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetBoneLocalRotation(l_bone, l_rot.m_quat);
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

        static int SetBool(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_param = "";
            bool l_state = false;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_param);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetBool(l_param, l_state);
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

        static int SetFloat(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_param = "";
            float l_val = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_param);
            l_argReader.ReadNumber(ref l_val);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetFloat(l_param, l_val);
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

        static int SetIKHintPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKHint l_hint = AvatarIKHint.LeftKnee;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_hint);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetIKHintPosition(l_hint, l_pos.m_vec);
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

        static int SetIKHintPositionWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKHint l_hint = AvatarIKHint.LeftKnee;
            float l_val = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_hint);
            l_argReader.ReadNumber(ref l_val);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetIKHintPositionWeight(l_hint, l_val);
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

        static int SetIKPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKGoal l_goal = AvatarIKGoal.LeftFoot;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_goal);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetIKPosition(l_goal, l_pos.m_vec);
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

        static int SetIKPositionWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKGoal l_goal = AvatarIKGoal.LeftFoot;
            float l_val = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_goal);
            l_argReader.ReadNumber(ref l_val);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetIKPositionWeight(l_goal, l_val);
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

        static int SetIKRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKGoal l_goal = AvatarIKGoal.LeftFoot;
            Wrappers.Quaternion l_rot = null;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_goal);
            l_argReader.ReadObject(ref l_rot);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetIKRotation(l_goal, l_rot.m_quat);
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

        static int SetIKRotationWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarIKGoal l_goal = AvatarIKGoal.LeftFoot;
            float l_val = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_goal);
            l_argReader.ReadNumber(ref l_val);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetIKRotationWeight(l_goal, l_val);
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

        static int SetInteger(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_param = "";
            int l_val = 0;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_param);
            l_argReader.ReadInteger(ref l_val);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetInteger(l_param, l_val);
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

        static int SetLayerWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            int l_index = 0;
            float l_val = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadInteger(ref l_index);
            l_argReader.ReadNumber(ref l_val);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetLayerWeight(l_index, l_val);
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

        static int SetLookAtPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetLookAtPosition(l_pos.m_vec);
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

        static int SetLookAtWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            float l_weight = 0f;
            float l_bodyWeight = 0f;
            float l_headWeight = 1f;
            float l_eyesWeight = 0f;
            float l_clampWeight = 0.5f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadNumber(ref l_weight);
            l_argReader.ReadNextNumber(ref l_bodyWeight);
            l_argReader.ReadNextNumber(ref l_headWeight);
            l_argReader.ReadNextNumber(ref l_eyesWeight);
            l_argReader.ReadNextNumber(ref l_clampWeight);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetLookAtWeight(l_weight, l_bodyWeight, l_headWeight, l_eyesWeight, l_clampWeight);
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

        static int SetTarget(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            AvatarTarget l_target = AvatarTarget.Root;
            float l_time = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadEnum(ref l_target);
            l_argReader.ReadNumber(ref l_time);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetTarget(l_target, l_time);
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

        static int SetTrigger(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            string l_param = "";
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadString(ref l_param);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.SetTrigger(l_param);
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

        static int StartPlayback(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.StartPlayback();
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

        static int StartRecording(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            int l_frames = 0;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadInteger(ref l_frames);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.StartRecording(l_frames);
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

        static int StopPlayback(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.StopPlayback();
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

        static int StopRecording(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.StopRecording();
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

        static int Update(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            float l_delta = 0f;
            l_argReader.ReadObject(ref l_animator);
            l_argReader.ReadNumber(ref l_delta);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.Update(l_delta);
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

        static int WriteDefaultValues(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Animator l_animator = null;
            l_argReader.ReadObject(ref l_animator);
            if(!l_argReader.HasErrors())
            {
                if(l_animator != null)
                {
                    l_animator.WriteDefaultValues();
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
