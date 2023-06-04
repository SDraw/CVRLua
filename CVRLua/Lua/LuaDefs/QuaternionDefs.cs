using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class QuaternionDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticProperties.Add(("identity", (GetIdentity, null)));

            ms_staticMethods.Add((nameof(Angle), Angle));
            ms_staticMethods.Add((nameof(AngleAxis), AngleAxis));
            ms_staticMethods.Add((nameof(Dot), Dot));
            ms_staticMethods.Add((nameof(Euler), Euler));
            ms_staticMethods.Add((nameof(FromToRotation), FromToRotation));
            ms_staticMethods.Add((nameof(Inverse), Inverse));
            ms_staticMethods.Add((nameof(Lerp), Lerp));
            ms_staticMethods.Add((nameof(LerpUnclamped), LerpUnclamped));
            ms_staticMethods.Add((nameof(LookRotation), LookRotation));
            ms_staticMethods.Add((nameof(RotateTowards), RotateTowards));
            ms_staticMethods.Add((nameof(Slerp), Slerp));
            ms_staticMethods.Add((nameof(SlerpUnclamped), SlerpUnclamped));
            ms_staticMethods.Add((nameof(IsQuaternion), IsQuaternion));

            ms_metaMethods.Add(("__mul", Multiply));
            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("x", (GetX, SetX)));
            ms_instanceProperties.Add(("y", (GetY, SetY)));
            ms_instanceProperties.Add(("z", (GetZ, SetZ)));
            ms_instanceProperties.Add(("w", (GetW, SetW)));
            ms_instanceProperties.Add(("eulerAngles", (GetEuler, SetEuler)));
            ms_instanceProperties.Add(("normalized", (GetNormalized, null)));

            ms_instanceMethods.Add((nameof(Normalize), Normalize));
            ms_instanceMethods.Add((nameof(Set), Set));
            ms_instanceMethods.Add((nameof(SetFromToRotation), SetFromToRotation));
            ms_instanceMethods.Add((nameof(SetLookRotation), SetLookRotation));
            ms_instanceMethods.Add((nameof(ToAngleAxis), ToAngleAxis));
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.Quaternion), Create, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        static int Create(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            float l_valX = 0f;
            float l_valY = 0f;
            float l_valZ = 0f;
            float l_valW = 0f;
            l_argReader.Skip(); // Metatable first
            l_argReader.ReadNextNumber(ref l_valX);
            l_argReader.ReadNextNumber(ref l_valY);
            l_argReader.ReadNextNumber(ref l_valZ);
            l_argReader.ReadNextNumber(ref l_valW);
            l_argReader.PushObject(new Wrappers.Quaternion(l_valX, l_valY, l_valZ, l_valW));
            return l_argReader.GetReturnValue();
        }

        // Static properties
        static int GetIdentity(IntPtr p_state)
        {
            var l_reader = new LuaArgReader(p_state);
            l_reader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.identity));
            return 1;
        }

        // Static methods
        static int Angle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            Wrappers.Quaternion l_quatB = null;
            l_argReader.ReadObject(ref l_quatA);
            l_argReader.ReadObject(ref l_quatB);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(UnityEngine.Quaternion.Angle(l_quatA.m_quat, l_quatB.m_quat));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int AngleAxis(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_angle = 0f;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadNumber(ref l_angle);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.AngleAxis(l_angle, l_vec.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Euler(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vec = new Wrappers.Vector3();
            if(l_argReader.IsNextObject())
                l_argReader.ReadObject(ref l_vec);
            else
            {
                l_argReader.ReadNumber(ref l_vec.m_vec.x);
                l_argReader.ReadNumber(ref l_vec.m_vec.y);
                l_argReader.ReadNumber(ref l_vec.m_vec.z);
            }
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.Euler(l_vec.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int FromToRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.FromToRotation(l_vecA.m_vec, l_vecB.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Inverse(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quat = null;
            l_argReader.ReadObject(ref l_quat);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.Inverse(l_quat.m_quat)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Lerp(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            Wrappers.Quaternion l_quatB = null;
            float l_alpha = 0f;
            l_argReader.ReadObject(ref l_quatA);
            l_argReader.ReadObject(ref l_quatB);
            l_argReader.ReadNumber(ref l_alpha);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.Lerp(l_quatA.m_quat, l_quatB.m_quat, l_alpha)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LerpUnclamped(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            Wrappers.Quaternion l_quatB = null;
            float l_alpha = 0f;
            l_argReader.ReadObject(ref l_quatA);
            l_argReader.ReadObject(ref l_quatB);
            l_argReader.ReadNumber(ref l_alpha);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.LerpUnclamped(l_quatA.m_quat, l_quatB.m_quat, l_alpha)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LookRotation(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_dir = null;
            Wrappers.Vector3 l_up = new Wrappers.Vector3(UnityEngine.Vector3.up);
            l_argReader.ReadObject(ref l_dir);
            l_argReader.ReadNextObject(ref l_up);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.LookRotation(l_dir.m_vec, l_up.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Dot(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            Wrappers.Quaternion l_quatB = null;
            l_argReader.ReadObject(ref l_quatA);
            l_argReader.ReadObject(ref l_quatB);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(UnityEngine.Quaternion.Dot(l_quatA.m_quat, l_quatB.m_quat));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Normalize(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quat = null;
            l_argReader.ReadObject(ref l_quat);
            if(!l_argReader.HasErrors())
            {
                l_quat.m_quat.Normalize();
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int RotateTowards(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            Wrappers.Quaternion l_quatB = null;
            float l_delta = 0f;
            l_argReader.ReadObject(ref l_quatA);
            l_argReader.ReadObject(ref l_quatB);
            l_argReader.ReadNumber(ref l_delta);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.RotateTowards(l_quatA.m_quat, l_quatB.m_quat, l_delta)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Slerp(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            Wrappers.Quaternion l_quatB = null;
            float l_delta = 0f;
            l_argReader.ReadObject(ref l_quatA);
            l_argReader.ReadObject(ref l_quatB);
            l_argReader.ReadNumber(ref l_delta);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.Slerp(l_quatA.m_quat, l_quatB.m_quat, l_delta)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SlerpUnclamped(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            Wrappers.Quaternion l_quatB = null;
            float l_delta = 0f;
            l_argReader.ReadObject(ref l_quatA);
            l_argReader.ReadObject(ref l_quatB);
            l_argReader.ReadNumber(ref l_delta);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(UnityEngine.Quaternion.SlerpUnclamped(l_quatA.m_quat, l_quatB.m_quat, l_delta)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int IsQuaternion(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_obj = null;
            l_argReader.ReadNextObject(ref l_obj);
            l_argReader.PushBoolean(l_obj != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Multiply(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            Wrappers.Quaternion l_quatB = null;
            l_argReader.ReadObject(ref l_quatA);
            l_argReader.ReadObject(ref l_quatB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(l_quatA.m_quat * l_quatB.m_quat));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Equal(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            Wrappers.Quaternion l_quatB = null;
            l_argReader.ReadObject(ref l_quatA);
            l_argReader.ReadObject(ref l_quatB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_quatA.m_quat == l_quatB.m_quat);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int ToString(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quatA = null;
            l_argReader.ReadObject(ref l_quatA);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_quatA.m_quat.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Instance methods
        static int Set(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quat = null;
            float l_valX = 0f;
            float l_valY = 0f;
            float l_valZ = 0f;
            float l_valW = 0f;
            l_argReader.ReadObject(ref l_quat);
            l_argReader.ReadNumber(ref l_valX);
            l_argReader.ReadNumber(ref l_valY);
            l_argReader.ReadNumber(ref l_valZ);
            l_argReader.ReadNumber(ref l_valW);
            if(!l_argReader.HasErrors())
            {
                l_quat.m_quat.Set(l_valX, l_valY, l_valZ, l_valW);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetFromToRotation(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quat = null;
            Wrappers.Vector3 l_from = null;
            Wrappers.Vector3 l_to = null;
            l_argReader.ReadObject(ref l_quat);
            l_argReader.ReadObject(ref l_from);
            l_argReader.ReadObject(ref l_to);
            if(!l_argReader.HasErrors())
            {
                l_quat.m_quat.SetFromToRotation(l_from.m_vec, l_to.m_vec);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetLookRotation(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quat = null;
            Wrappers.Vector3 l_view = null;
            Wrappers.Vector3 l_up = new Wrappers.Vector3(0f, 1f, 0f);
            l_argReader.ReadObject(ref l_quat);
            l_argReader.ReadObject(ref l_view);
            l_argReader.ReadNextObject(ref l_up);
            if(!l_argReader.HasErrors())
            {
                l_quat.m_quat.SetLookRotation(l_view.m_vec, l_up.m_vec);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int ToAngleAxis(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quat = null;
            l_argReader.ReadObject(ref l_quat);
            if(!l_argReader.HasErrors())
            {
                Wrappers.Vector3 l_vec = new Wrappers.Vector3();
                l_quat.m_quat.ToAngleAxis(out float l_angle, out l_vec.m_vec);
                l_argReader.PushNumber(l_angle);
                l_argReader.PushObject(l_vec);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetX(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_vec.m_quat.x);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetX(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_vec.m_quat.x = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetY(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_vec.m_quat.y);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetY(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_vec.m_quat.y = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetZ(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_vec.m_quat.z);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetZ(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_vec.m_quat.z = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetW(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_vec.m_quat.w);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetW(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
                l_vec.m_quat.w = l_value;

            l_argReader.LogError();
            return 0;
        }

        static int GetEuler(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_vec.m_quat.eulerAngles));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetEuler(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_quat = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_quat);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_quat.m_quat.eulerAngles = l_vec.m_vec;

            l_argReader.LogError();
            return 0;
        }

        static int GetNormalized(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_vec = null;
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(l_vec.m_quat.normalized));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
    }
}
