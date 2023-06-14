using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace CVRLua.Lua.LuaDefs
{
    static class UiSliderDefs
    {
        const string c_destroyed = "Slider is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_instanceProperties.Add(("normalizedValue", (GetNormalizedValue, SetNormalizedValue)));
            ms_instanceProperties.Add(("value", (GetValue, SetValue)));
            ms_instanceProperties.Add(("wholeNumbers", (GetWholeNumbers, SetWholeNumbers)));
            ms_instanceProperties.Add(("maxValue", (GetMaxValue, SetMaxValue)));
            ms_instanceProperties.Add(("minValue", (GetMinValue, SetMinValue)));
            ms_instanceProperties.Add(("direction", (GetDirectionProp, SetDirectionProp)));
            //ms_instanceProperties.Add(("handleRect", (GetDirection, SetDirection)));
            //ms_instanceProperties.Add(("fillRect", (GetDirection, SetDirection)));

            ms_instanceMethods.Add((nameof(SetDirection), SetDirection));
            ms_instanceMethods.Add((nameof(SetValueWithoutNotify), SetValueWithoutNotify));

            UiSelectableDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Slider), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsSlider), IsSlider);
        }

        // Static methods
        static int IsSlider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            l_argReader.ReadNextObject(ref l_slider);
            l_argReader.PushBoolean(l_slider != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetNormalizedValue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            l_argReader.ReadObject(ref l_slider);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_argReader.PushNumber(l_slider.normalizedValue);
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
        static int SetNormalizedValue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_slider);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_slider.normalizedValue = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetValue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            l_argReader.ReadObject(ref l_slider);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_argReader.PushNumber(l_slider.value);
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
        static int SetValue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_slider);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_slider.value = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetWholeNumbers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            l_argReader.ReadObject(ref l_slider);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_argReader.PushBoolean(l_slider.wholeNumbers);
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
        static int SetWholeNumbers(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_slider);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_slider.wholeNumbers = l_value;

                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMaxValue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            l_argReader.ReadObject(ref l_slider);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_argReader.PushNumber(l_slider.maxValue);
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
        static int SetMaxValue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_slider);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_slider.maxValue = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMinValue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            l_argReader.ReadObject(ref l_slider);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_argReader.PushNumber(l_slider.minValue);
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
        static int SetMinValue(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_slider);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_slider.minValue = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDirectionProp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            l_argReader.ReadObject(ref l_slider);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_argReader.PushString(l_slider.direction.ToString());
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
        static int SetDirectionProp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            Slider.Direction l_value = Slider.Direction.LeftToRight;
            l_argReader.ReadObject(ref l_slider);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                    l_slider.direction = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int SetDirection(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            Slider.Direction l_value = Slider.Direction.LeftToRight;
            bool l_layout = false;
            l_argReader.ReadObject(ref l_slider);
            l_argReader.ReadEnum(ref l_value);
            l_argReader.ReadBoolean(ref l_layout);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                {
                    l_slider.SetDirection(l_value, l_layout);
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

        static int SetValueWithoutNotify(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Slider l_slider = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_slider);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_slider != null)
                {
                    l_slider.SetValueWithoutNotify(l_value);
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
