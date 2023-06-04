using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class MathfDefs
    {
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticProperties.Add(("Deg2Rad", (GetDeg2Rad, null)));
            ms_staticProperties.Add(("Epsilon", (GetEpsilon, null)));
            ms_staticProperties.Add(("Rad2Deg", (GetRad2Deg, null)));

            ms_staticMethods.Add((nameof(Approximately), Approximately));
            ms_staticMethods.Add((nameof(CeilToInt), CeilToInt));
            ms_staticMethods.Add((nameof(Clamp), Clamp));
            ms_staticMethods.Add((nameof(Clamp01), Clamp01));
            ms_staticMethods.Add((nameof(ClosestPowerOfTwo), ClosestPowerOfTwo));
            ms_staticMethods.Add((nameof(CorrelatedColorTemperatureToRGB), CorrelatedColorTemperatureToRGB));
            ms_staticMethods.Add((nameof(DeltaAngle), DeltaAngle));
            ms_staticMethods.Add((nameof(FloorToInt), FloorToInt));
            ms_staticMethods.Add((nameof(GammaToLinearSpace), GammaToLinearSpace));
            ms_staticMethods.Add((nameof(InverseLerp), InverseLerp));
            ms_staticMethods.Add((nameof(IsPowerOfTwo), IsPowerOfTwo));
            ms_staticMethods.Add((nameof(Lerp), Lerp));
            ms_staticMethods.Add((nameof(LerpAngle), LerpAngle));
            ms_staticMethods.Add((nameof(LerpUnclamped), LerpUnclamped));
            ms_staticMethods.Add((nameof(LinearToGammaSpace), LinearToGammaSpace));
            ms_staticMethods.Add((nameof(MoveTowards), MoveTowards));
            ms_staticMethods.Add((nameof(MoveTowardsAngle), MoveTowardsAngle));
            ms_staticMethods.Add((nameof(NextPowerOfTwo), NextPowerOfTwo));
            ms_staticMethods.Add((nameof(PerlinNoise), PerlinNoise));
            ms_staticMethods.Add((nameof(PingPong), PingPong));
            ms_staticMethods.Add((nameof(Repeat), Repeat));
            ms_staticMethods.Add((nameof(RoundToInt), RoundToInt));
            ms_staticMethods.Add((nameof(Sign), Sign));
            ms_staticMethods.Add((nameof(SmoothDamp), SmoothDamp));
            ms_staticMethods.Add((nameof(SmoothDampAngle), SmoothDampAngle));
            ms_staticMethods.Add((nameof(SmoothStep), SmoothStep));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Mathf), null, ms_staticProperties, ms_staticMethods, null, null, null);
        }

        // Static properties
        static int GetDeg2Rad(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Mathf.Deg2Rad);
            return 1;
        }

        static int GetEpsilon(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Mathf.Epsilon);
            return 1;
        }

        static int GetRad2Deg(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(Mathf.Rad2Deg);
            return 1;
        }

        // Static methods
        static int Approximately(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueA = 0f;
            float l_valueB = 0f;
            l_argReader.ReadNumber(ref l_valueA);
            l_argReader.ReadNumber(ref l_valueB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Mathf.Approximately(l_valueA, l_valueB));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int CeilToInt(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(Mathf.CeilToInt(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Clamp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            float l_min = 0f;
            float l_max = 0f;
            l_argReader.ReadNumber(ref l_value);
            l_argReader.ReadNumber(ref l_min);
            l_argReader.ReadNumber(ref l_max);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.Clamp(l_value, l_min, l_max));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Clamp01(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.Clamp01(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int ClosestPowerOfTwo(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            int l_value = 0;
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(Mathf.ClosestPowerOfTwo(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int CorrelatedColorTemperatureToRGB(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(Mathf.CorrelatedColorTemperatureToRGB(l_value)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int DeltaAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            float l_target = 0f;
            l_argReader.ReadNumber(ref l_value);
            l_argReader.ReadNumber(ref l_target);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.DeltaAngle(l_value, l_target));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int FloorToInt(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(Mathf.FloorToInt(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GammaToLinearSpace(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.GammaToLinearSpace(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int InverseLerp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueA = 0f;
            float l_valueB = 0f;
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_valueA);
            l_argReader.ReadNumber(ref l_valueB);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.InverseLerp(l_valueA, l_valueB, l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int IsPowerOfTwo(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            int l_value = 0;
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(Mathf.IsPowerOfTwo(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Lerp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueA = 0f;
            float l_valueB = 0f;
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_valueA);
            l_argReader.ReadNumber(ref l_valueB);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.Lerp(l_valueA, l_valueB, l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LerpAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueA = 0f;
            float l_valueB = 0f;
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_valueA);
            l_argReader.ReadNumber(ref l_valueB);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.LerpAngle(l_valueA, l_valueB, l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LerpUnclamped(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueA = 0f;
            float l_valueB = 0f;
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_valueA);
            l_argReader.ReadNumber(ref l_valueB);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.LerpUnclamped(l_valueA, l_valueB, l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LinearToGammaSpace(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.LinearToGammaSpace(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int MoveTowards(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_current = 0f;
            float l_target = 0f;
            float l_maxDelta = 0f;
            l_argReader.ReadNumber(ref l_current);
            l_argReader.ReadNumber(ref l_target);
            l_argReader.ReadNumber(ref l_maxDelta);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.MoveTowards(l_current, l_target, l_maxDelta));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int MoveTowardsAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_current = 0f;
            float l_target = 0f;
            float l_maxDelta = 0f;
            l_argReader.ReadNumber(ref l_current);
            l_argReader.ReadNumber(ref l_target);
            l_argReader.ReadNumber(ref l_maxDelta);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.MoveTowardsAngle(l_current, l_target, l_maxDelta));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int NextPowerOfTwo(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            int l_value = 0;
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(Mathf.NextPowerOfTwo(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int PerlinNoise(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_x = 0f;
            float l_y = 0f;
            l_argReader.ReadNumber(ref l_x);
            l_argReader.ReadNumber(ref l_y);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.PerlinNoise(l_x, l_y));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int PingPong(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            float l_length = 0f;
            l_argReader.ReadNumber(ref l_value);
            l_argReader.ReadNumber(ref l_length);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.PingPong(l_value, l_length));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Repeat(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            float l_length = 0f;
            l_argReader.ReadNumber(ref l_value);
            l_argReader.ReadNumber(ref l_length);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.Repeat(l_value, l_length));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int RoundToInt(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(Mathf.RoundToInt(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Sign(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.Sign(l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SmoothDamp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            float l_target = 0f;
            float l_velocity = 0f;
            float l_time = 0f;
            float l_speed = float.PositiveInfinity;
            float l_delta = Time.deltaTime;
            l_argReader.ReadNumber(ref l_value);
            l_argReader.ReadNumber(ref l_target);
            l_argReader.ReadNumber(ref l_velocity);
            l_argReader.ReadNumber(ref l_time);
            l_argReader.ReadNextNumber(ref l_speed);
            l_argReader.ReadNextNumber(ref l_delta);
            if(!l_argReader.HasErrors())
            {
                l_argReader.PushNumber(Mathf.SmoothDamp(l_value, l_target, ref l_velocity, l_time, l_speed, l_delta));
                l_argReader.PushNumber(l_velocity);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SmoothDampAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_value = 0f;
            float l_target = 0f;
            float l_velocity = 0f;
            float l_time = 0f;
            float l_speed = float.PositiveInfinity;
            float l_delta = Time.deltaTime;
            l_argReader.ReadNumber(ref l_value);
            l_argReader.ReadNumber(ref l_target);
            l_argReader.ReadNumber(ref l_velocity);
            l_argReader.ReadNumber(ref l_time);
            l_argReader.ReadNextNumber(ref l_speed);
            l_argReader.ReadNextNumber(ref l_delta);
            if(!l_argReader.HasErrors())
            {
                l_argReader.PushNumber(Mathf.SmoothDampAngle(l_value, l_target, ref l_velocity, l_time, l_speed, l_delta));
                l_argReader.PushNumber(l_velocity);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SmoothStep(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_from = 0f;
            float l_to = 0f;
            float l_alpha = 0f;
            l_argReader.ReadNumber(ref l_from);
            l_argReader.ReadNumber(ref l_to);
            l_argReader.ReadNumber(ref l_alpha);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(Mathf.SmoothStep(l_from, l_to, l_alpha));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
