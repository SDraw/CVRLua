using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class Vector3Defs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticProperties.Add("back", (GetBack, null));
            ms_staticProperties.Add("down", (GetDown, null));
            ms_staticProperties.Add("forward", (GetForward, null));
            ms_staticProperties.Add("left", (GetLeft, null));
            ms_staticProperties.Add("one", (GetOne, null));
            ms_staticProperties.Add("right", (GetRight, null));
            ms_staticProperties.Add("up", (GetUp, null));
            ms_staticProperties.Add("zero", (GetZero, null));

            ms_staticMethods.Add(nameof(Angle), Angle);
            ms_staticMethods.Add(nameof(ClampMagnitude), ClampMagnitude);
            ms_staticMethods.Add(nameof(Cross), Cross);
            ms_staticMethods.Add(nameof(Distance), Distance);
            ms_staticMethods.Add(nameof(Dot), Dot);
            ms_staticMethods.Add(nameof(Lerp), Lerp);
            ms_staticMethods.Add(nameof(LerpUnclamped), LerpUnclamped);
            ms_staticMethods.Add(nameof(Max), Max);
            ms_staticMethods.Add(nameof(Min), Min);
            ms_staticMethods.Add(nameof(MoveTowards), MoveTowards);
            ms_staticMethods.Add(nameof(Project), Project);
            ms_staticMethods.Add(nameof(ProjectOnPlane), Project);
            ms_staticMethods.Add(nameof(Reflect), Reflect);
            ms_staticMethods.Add(nameof(Scale), Scale);
            ms_staticMethods.Add(nameof(SignedAngle), SignedAngle);
            ms_staticMethods.Add(nameof(Slerp), Slerp);
            ms_staticMethods.Add(nameof(SlerpUnclamped), SlerpUnclamped);
            ms_staticMethods.Add(nameof(SmoothDamp), SmoothDamp);
            ms_staticMethods.Add(nameof(IsValid), IsValid);

            ms_metaMethods.Add(("__add", Add));
            ms_metaMethods.Add(("__sub", Subtract));
            ms_metaMethods.Add(("__mul", Multiply));
            ms_metaMethods.Add(("__div", Divide));
            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__len", GetMagnitude));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add("x", (GetX, SetX));
            ms_instanceProperties.Add("y", (GetY, SetY));
            ms_instanceProperties.Add("z", (GetZ, SetZ));
            ms_instanceProperties.Add("magnitude", (Magnitude, null));
            ms_instanceProperties.Add("normalized", (Normalized, null));
            ms_instanceProperties.Add("sqrMagnitude", (SquareMagnitude, null));

            ms_instanceMethods.Add("Equals", Equal);
            ms_instanceMethods.Add(nameof(Normalize), Normalize);
            ms_instanceMethods.Add(nameof(Set), Set);
            ms_instanceMethods.Add(nameof(ToString), ToString);
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.Vector3), Constructor, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
        }

        static int Constructor(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            float l_val1 = 0f;
            float l_val2 = 0f;
            float l_val3 = 0f;
            l_argReader.Skip(); // Metatable first
            l_argReader.ReadNextNumber(ref l_val1);
            l_argReader.ReadNextNumber(ref l_val2);
            l_argReader.ReadNextNumber(ref l_val3);
            l_argReader.PushObject(new Wrappers.Vector3(l_val1, l_val2, l_val3));
            return l_argReader.GetReturnValue();
        }

        // Static properties
        static void GetBack(LuaArgReader p_reader) => p_reader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.back));
        static void GetDown(LuaArgReader p_reader) => p_reader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.down));
        static void GetForward(LuaArgReader p_reader) => p_reader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.forward));
        static void GetLeft(LuaArgReader p_reader) => p_reader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.left));
        static void GetOne(LuaArgReader p_reader) => p_reader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.one));
        static void GetRight(LuaArgReader p_reader) => p_reader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.right));
        static void GetUp(LuaArgReader p_reader) => p_reader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.up));
        static void GetZero(LuaArgReader p_reader) => p_reader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.zero));

        // Static methods
        static int Angle(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(UnityEngine.Vector3.Angle(l_vecA.m_vec, l_vecB.m_vec));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int ClampMagnitude(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            float l_length = 0f;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadNumber(ref l_length);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.ClampMagnitude(l_vecA.m_vec, l_length)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Cross(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.Cross(l_vecA.m_vec, l_vecB.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Distance(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(UnityEngine.Vector3.Distance(l_vecA.m_vec, l_vecB.m_vec));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Dot(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(UnityEngine.Vector3.Dot(l_vecA.m_vec, l_vecB.m_vec));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Lerp(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            float l_alpha = 0f;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            l_argReader.ReadNextNumber(ref l_alpha);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.Lerp(l_vecA.m_vec, l_vecB.m_vec, l_alpha)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LerpUnclamped(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            float l_alpha = 0f;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            l_argReader.ReadNextNumber(ref l_alpha);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.LerpUnclamped(l_vecA.m_vec, l_vecB.m_vec, l_alpha)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Max(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.Max(l_vecA.m_vec, l_vecB.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Min(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.Min(l_vecA.m_vec, l_vecB.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Project(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.Project(l_vecA.m_vec, l_vecB.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int ProjectOnPlane(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.ProjectOnPlane(l_vecA.m_vec, l_vecB.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Reflect(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.Reflect(l_vecA.m_vec, l_vecB.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Scale(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.Scale(l_vecA.m_vec, l_vecB.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SignedAngle(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            Wrappers.Vector3 l_vecC = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            l_argReader.ReadObject(ref l_vecC);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(UnityEngine.Vector3.SignedAngle(l_vecA.m_vec, l_vecB.m_vec, l_vecC.m_vec));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Slerp(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            float l_alpha = 0f;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            l_argReader.ReadNumber(ref l_alpha);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.Slerp(l_vecA.m_vec, l_vecB.m_vec, l_alpha)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SlerpUnclamped(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            float l_alpha = 0f;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            l_argReader.ReadNumber(ref l_alpha);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.SlerpUnclamped(l_vecA.m_vec, l_vecB.m_vec, l_alpha)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SmoothDamp(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            Wrappers.Vector3 l_vecC = null;
            float l_time = 0f;
            float l_speed = UnityEngine.Mathf.Infinity;
            float l_delta = UnityEngine.Time.deltaTime;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            l_argReader.ReadObject(ref l_vecC);
            l_argReader.ReadNumber(ref l_time);
            l_argReader.ReadNextNumber(ref l_speed);
            l_argReader.ReadNextNumber(ref l_speed);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.SmoothDamp(l_vecA.m_vec, l_vecB.m_vec, ref l_vecC.m_vec, l_time, l_speed, l_delta)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int IsValid(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_obj = null;
            l_argReader.ReadNextObject(ref l_obj);
            l_argReader.PushBoolean(l_obj != null);
            return l_argReader.GetReturnValue();
        }

        static int MoveTowards(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            double l_distance = .0;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            l_argReader.ReadNextNumber(ref l_distance);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(UnityEngine.Vector3.MoveTowards(l_vecA.m_vec, l_vecB.m_vec, (float)l_distance)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Add(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_vecA.m_vec + l_vecB.m_vec));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Subtract(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_vecA.m_vec - l_vecB.m_vec));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Multiply(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            float l_val = 0f;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadNumber(ref l_val);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_vecA.m_vec * l_val));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Divide(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            float l_val = 0f;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadNumber(ref l_val);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_vecA.m_vec / l_val));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Equal(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_vecA.m_vec == l_vecB.m_vec);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int GetMagnitude(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            l_argReader.ReadObject(ref l_vecA);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_vecA.m_vec.magnitude);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int ToString(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            l_argReader.ReadObject(ref l_vecA);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_vecA.m_vec.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static void GetX(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.Vector3).m_vec.x);
        }
        static void SetX(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as Wrappers.Vector3).m_vec.x = l_val;
        }

        static void GetY(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.Vector3).m_vec.y);
        }
        static void SetY(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as Wrappers.Vector3).m_vec.y = l_val;
        }

        static void GetZ(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.Vector3).m_vec.z);
        }
        static void SetZ(object p_obj, LuaArgReader p_reader)
        {
            float l_val = 0f;
            p_reader.ReadNumber(ref l_val);
            if(!p_reader.HasErrors())
                (p_obj as Wrappers.Vector3).m_vec.z = l_val;
        }

        static void Magnitude(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.Vector3).m_vec.magnitude);
        }

        static void Normalized(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as Wrappers.Vector3).m_vec.normalized));
        }

        static void SquareMagnitude(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Wrappers.Vector3).m_vec.sqrMagnitude);
        }

        // Instance methods
        static int Set(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vec = null;
            float l_valX = 0f;
            float l_valY = 0f;
            float l_valZ = 0f;
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNumber(ref l_valX);
            l_argReader.ReadNumber(ref l_valY);
            l_argReader.ReadNumber(ref l_valZ);
            if(!l_argReader.HasErrors())
            {
                l_vec.m_vec.Set(l_valX, l_valY, l_valZ);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Normalize(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                l_vec.m_vec.Normalize();
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Static getter
        static int StaticGet(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            string l_key = "";
            l_argReader.Skip(); // Metatable
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(ms_staticMethods.TryGetValue(l_key, out var l_func))
                    l_argReader.PushFunction(l_func);
                else if(ms_staticProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item1 != null))
                    l_pair.Item1.Invoke(l_argReader);
                else
                    l_argReader.PushNil();
            }
            else
                l_argReader.PushNil();

            return l_argReader.GetReturnValue();
        }

        // Instance getter
        static int InstanceGet(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(ms_instanceMethods.TryGetValue(l_key, out var l_func))
                    l_argReader.PushFunction(l_func); // Lua handles it by itself
                else if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item1 != null))
                    l_pair.Item1.Invoke(l_obj, l_argReader);
                else
                    l_argReader.PushNil();
            }
            else
                l_argReader.PushNil();

            return l_argReader.GetReturnValue();
        }

        // Instance setter
        static int InstanceSet(IntPtr p_state)
        {
            // Our value is on stack top
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item2 != null))
                    l_pair.Item2.Invoke(l_obj, l_argReader);
            }

            return l_argReader.GetReturnValue();
        }
    }
}
