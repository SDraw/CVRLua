using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class RandomDefs
    {
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticProperties.Add(("insideUnitCircle", (GetInsideUnitCircle, null)));
            ms_staticProperties.Add(("insideUnitSphere", (GetInsideUnitSphere, null)));
            ms_staticProperties.Add(("onUnitSphere", (GetOnUnitSphere, null)));
            ms_staticProperties.Add(("rotation", (GetRotation, null)));
            ms_staticProperties.Add(("rotationUniform", (GetRotationUniform, null)));
            //ms_staticProperties.Add(("state", (?, ?)));
            ms_staticProperties.Add(("value", (GetValue, null)));

            ms_staticMethods.Add((nameof(ColorHSV), ColorHSV));
            ms_staticMethods.Add((nameof(InitState), InitState));
            ms_staticMethods.Add((nameof(Range), Range));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(UnityEngine.Random), null, ms_staticProperties, ms_staticMethods, null, null, null);
        }

        // Static properties
        static int GetInsideUnitCircle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Vector2(UnityEngine.Random.insideUnitCircle));
            return 1;
        }

        static int GetInsideUnitSphere(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Random.insideUnitSphere));
            return 1;
        }

        static int GetOnUnitSphere(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Random.onUnitSphere));
            return 1;
        }

        static int GetRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Random.rotation));
            return 1;
        }

        static int GetRotationUniform(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Random.rotationUniform));
            return 1;
        }

        static int GetValue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushNumber(UnityEngine.Random.value);
            return 1;
        }

        // Static methods
        static int ColorHSV(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_hMin = 0f;
            float l_hMax = 1f;
            float l_sMin = 0f;
            float l_sMax = 1f;
            float l_vMin = 0f;
            float l_vMax = 1f;
            float l_aMin = 1f;
            float l_aMax = 1f;
            l_argReader.ReadNextNumber(ref l_hMin);
            l_argReader.ReadNextNumber(ref l_hMax);
            l_argReader.ReadNextNumber(ref l_sMin);
            l_argReader.ReadNextNumber(ref l_sMax);
            l_argReader.ReadNextNumber(ref l_vMin);
            l_argReader.ReadNextNumber(ref l_vMax);
            l_argReader.ReadNextNumber(ref l_aMin);
            l_argReader.ReadNextNumber(ref l_aMax);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Random.ColorHSV(l_hMin, l_hMax, l_sMin, l_sMax, l_vMin, l_vMax, l_aMin, l_aMax)));
            return l_argReader.GetReturnValue();
        }

        static int InitState(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            int l_seed = 0;
            l_argReader.ReadInteger(ref l_seed);
            if(!l_argReader.HasErrors())
            {
                UnityEngine.Random.InitState(l_seed);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Range(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_min = 0f;
            float l_max = 0f;
            l_argReader.ReadNumber(ref l_min);
            l_argReader.ReadNumber(ref l_max);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(UnityEngine.Random.Range(l_min, l_max));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
