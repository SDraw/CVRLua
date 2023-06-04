using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class ColorDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        internal static void Init()
        {
            ms_staticProperties.Add(("black", (GetBlack, null)));
            ms_staticProperties.Add(("blue", (GetBlue, null)));
            ms_staticProperties.Add(("clear", (GetClear, null)));
            ms_staticProperties.Add(("gray", (GetGray, null)));
            ms_staticProperties.Add(("green", (GetGreen, null)));
            ms_staticProperties.Add(("grey", (GetGray, null)));
            ms_staticProperties.Add(("magenta", (GetMagenta, null)));
            ms_staticProperties.Add(("red", (GetRed, null)));
            ms_staticProperties.Add(("white", (GetWhite, null)));
            ms_staticProperties.Add(("yellow", (GetYellow, null)));

            ms_staticMethods.Add((nameof(IsColor), IsColor));
            ms_staticMethods.Add((nameof(HSVToRGB), HSVToRGB));
            ms_staticMethods.Add((nameof(Lerp), Lerp));
            ms_staticMethods.Add((nameof(LerpUnclamped), LerpUnclamped));
            ms_staticMethods.Add((nameof(RGBToHSV), RGBToHSV));

            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));
            ms_metaMethods.Add(("__add", Add));
            ms_metaMethods.Add(("__sub", Subtract));
            ms_metaMethods.Add(("__mul", Multiply));
            ms_metaMethods.Add(("__div", Divide));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("r", (GetR, SetR)));
            ms_instanceProperties.Add(("g", (GetG, SetG)));
            ms_instanceProperties.Add(("b", (GetB, SetB)));
            ms_instanceProperties.Add(("a", (GetA, SetA)));
            ms_instanceProperties.Add(("gamma", (GetGamma, null)));
            ms_instanceProperties.Add(("grayscale", (GetGrayscale, null)));
            ms_instanceProperties.Add(("linear", (GetLinear, null)));
            ms_instanceProperties.Add(("maxColorComponent", (GetMaxColorComponent, null)));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.Color), Create, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, null);
        }

        // Constructor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_colorR = 0f;
            float l_colorG = 0f;
            float l_colorB = 0f;
            float l_colorA = 1f;
            l_argReader.Skip(); // Metatable first
            l_argReader.ReadNextNumber(ref l_colorR);
            l_argReader.ReadNextNumber(ref l_colorG);
            l_argReader.ReadNextNumber(ref l_colorB);
            l_argReader.ReadNextNumber(ref l_colorA);
            l_argReader.PushObject(new Wrappers.Color(new UnityEngine.Color(l_colorR, l_colorG, l_colorB, l_colorA)));
            return l_argReader.GetReturnValue();
        }

        // Static properties
        static int GetBlack(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.black));
            return 1;
        }

        static int GetBlue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.blue));
            return 1;
        }

        static int GetClear(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.clear));
            return 1;
        }

        static int GetGray(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.gray));
            return 1;
        }

        static int GetGreen(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.green));
            return 1;
        }

        static int GetMagenta(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.magenta));
            return 1;
        }

        static int GetRed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.red));
            return 1;
        }

        static int GetWhite(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.white));
            return 1;
        }

        static int GetYellow(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.yellow));
            return 1;
        }

        // Static methods
        static int IsColor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_obj = null;
            l_argReader.ReadNextObject(ref l_obj);
            l_argReader.PushBoolean(l_obj != null);
            return l_argReader.GetReturnValue();
        }

        static int HSVToRGB(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_valueH = 0f;
            float l_valueS = 0f;
            float l_valueV = 0f;
            bool l_hdr = true;
            l_argReader.ReadNumber(ref l_valueH);
            l_argReader.ReadNumber(ref l_valueS);
            l_argReader.ReadNumber(ref l_valueV);
            l_argReader.ReadNextBoolean(ref l_hdr);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.HSVToRGB(l_valueH, l_valueS, l_valueV, l_hdr)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Lerp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_colorA = null;
            Wrappers.Color l_colorB = null;
            float l_alpha = 0f;
            l_argReader.ReadObject(ref l_colorA);
            l_argReader.ReadObject(ref l_colorB);
            l_argReader.ReadNumber(ref l_alpha);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.Lerp(l_colorA.m_color, l_colorB.m_color, l_alpha)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LerpUnclamped(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_colorA = null;
            Wrappers.Color l_colorB = null;
            float l_alpha = 0f;
            l_argReader.ReadObject(ref l_colorA);
            l_argReader.ReadObject(ref l_colorB);
            l_argReader.ReadNumber(ref l_alpha);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(UnityEngine.Color.LerpUnclamped(l_colorA.m_color, l_colorB.m_color, l_alpha)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int RGBToHSV(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
            {
                UnityEngine.Color.RGBToHSV(l_color.m_color, out float l_valueH, out float l_valueS, out float l_valueV);
                l_argReader.PushNumber(l_valueH);
                l_argReader.PushNumber(l_valueS);
                l_argReader.PushNumber(l_valueV);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Equal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_colorA = null;
            Wrappers.Color l_colorB = null;
            l_argReader.ReadObject(ref l_colorA);
            l_argReader.ReadObject(ref l_colorB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_colorA.m_color == l_colorB.m_color);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int ToString(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_color.m_color.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int Add(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_colorA = null;
            Wrappers.Color l_colorB = null;
            l_argReader.ReadObject(ref l_colorA);
            l_argReader.ReadObject(ref l_colorB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(l_colorA.m_color + l_colorB.m_color));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int Subtract(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_colorA = null;
            Wrappers.Color l_colorB = null;
            l_argReader.ReadObject(ref l_colorA);
            l_argReader.ReadObject(ref l_colorB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(l_colorA.m_color - l_colorB.m_color));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int Multiply(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_colorA = null;
            Wrappers.Color l_colorB = null;
            l_argReader.ReadObject(ref l_colorA);
            l_argReader.ReadObject(ref l_colorB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(l_colorA.m_color * l_colorB.m_color));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int Divide(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_color);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(l_color.m_color / l_value));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance properties
        static int GetR(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_color.m_color.r);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetR(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_color);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_color.m_color.r = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetG(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_color.m_color.g);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetG(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_color);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_color.m_color.g = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetB(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_color.m_color.b);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetB(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_color);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_color.m_color.b = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetA(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_color.m_color.a);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetA(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_color);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_color.m_color.a = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetGamma(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(l_color.m_color.gamma));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetGrayscale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_color.m_color.grayscale);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLinear(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Color(l_color.m_color.linear));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetMaxColorComponent(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Color l_color = null;
            l_argReader.ReadObject(ref l_color);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_color.m_color.maxColorComponent);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
    }
}
