using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //ms_staticMethods.Add((nameof(ColorHSV), ColorHSV));
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
                l_argReader.PushNumber(UnityEngine.Random.Range(l_min,l_max));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
