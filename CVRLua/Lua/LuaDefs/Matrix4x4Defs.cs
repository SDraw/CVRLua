using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class Matrix4x4Defs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticProperties.Add(("identity", (GetIdentity, null)));
            ms_staticProperties.Add(("zero", (GetZero, null)));

            ms_staticMethods.Add((nameof(Frustum), Frustum));
            ms_staticMethods.Add((nameof(Inverse3DAffine), Inverse3DAffine));
            ms_staticMethods.Add((nameof(LookAt), LookAt));
            ms_staticMethods.Add((nameof(Ortho), Ortho));
            ms_staticMethods.Add((nameof(Perspective), Perspective));
            ms_staticMethods.Add((nameof(Rotate), Rotate));
            ms_staticMethods.Add((nameof(Scale), Scale));
            ms_staticMethods.Add((nameof(Translate), Translate));
            ms_staticMethods.Add((nameof(TRS), Translate));

            ms_metaMethods.Add(("__mul", Multiply));
            ms_metaMethods.Add(("__tostring", ToString));

            //ms_instanceProperties.Add(("decomposeProjection", (GetDecomposeProjection, null)));
            ms_instanceProperties.Add(("determinant", (GetDeterminant, null)));
            ms_instanceProperties.Add(("inverse", (GetInverse, null)));
            ms_instanceProperties.Add(("isIdentity", (GetIsIdentity, null)));
            ms_instanceProperties.Add(("lossyScale", (GetLossyScale, null)));
            ms_instanceProperties.Add(("rotation", (GetRotation, null)));
            ms_instanceProperties.Add(("transpose", (GetTranspose, null)));

            ms_instanceMethods.Add((nameof(GetColumn), GetColumn));
            ms_instanceMethods.Add((nameof(GetRow), GetRow));
            ms_instanceMethods.Add((nameof(MultiplyPoint), MultiplyPoint));
            ms_instanceMethods.Add((nameof(MultiplyPoint3x4), MultiplyPoint3x4));
            ms_instanceMethods.Add((nameof(MultiplyVector), MultiplyVector));
            ms_instanceMethods.Add((nameof(SetColumn), SetColumn));
            ms_instanceMethods.Add((nameof(SetRow), SetRow));
            ms_instanceMethods.Add((nameof(SetTRS), SetTRS));
            //ms_instanceMethods.Add((nameof(TransformPlane), TransformPlane));
            ms_instanceMethods.Add((nameof(ValidTRS), ValidTRS));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Wrappers.Matrix4x4), Create, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Constructor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector4 l_columnA = null;
            Wrappers.Vector4 l_columnB = null;
            Wrappers.Vector4 l_columnC = null;
            Wrappers.Vector4 l_columnD = null;
            l_argReader.Skip();
            l_argReader.ReadNextObject(ref l_columnA);
            l_argReader.ReadNextObject(ref l_columnB);
            l_argReader.ReadNextObject(ref l_columnC);
            l_argReader.ReadNextObject(ref l_columnD);
            l_argReader.PushObject(new Wrappers.Matrix4x4(new UnityEngine.Matrix4x4(
                (l_columnA != null) ? l_columnA.m_vec : UnityEngine.Vector4.zero,
                (l_columnB != null) ? l_columnB.m_vec : UnityEngine.Vector4.zero,
                (l_columnC != null) ? l_columnC.m_vec : UnityEngine.Vector4.zero,
                (l_columnD != null) ? l_columnD.m_vec : UnityEngine.Vector4.zero))
            );
            return l_argReader.GetReturnValue();
        }

        // Static properties
        static int GetIdentity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.identity));
            return 1;
        }

        static int GetZero(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.zero));
            return 1;
        }

        // Static methods
        static int Frustum(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_left = 0f;
            float l_right = 0f;
            float l_bottom = 0f;
            float l_top = 0f;
            float l_near = 0f;
            float l_far = 0f;
            l_argReader.ReadNumber(ref l_left);
            l_argReader.ReadNumber(ref l_right);
            l_argReader.ReadNumber(ref l_bottom);
            l_argReader.ReadNumber(ref l_top);
            l_argReader.ReadNumber(ref l_near);
            l_argReader.ReadNumber(ref l_far);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.Frustum(l_left, l_right, l_bottom, l_top, l_near, l_far)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Inverse3DAffine(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
            {
                UnityEngine.Matrix4x4 l_inv = UnityEngine.Matrix4x4.zero;
                if(UnityEngine.Matrix4x4.Inverse3DAffine(l_mat.m_mat, ref l_inv))
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_inv));
                else
                    l_argReader.PushBoolean(false);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LookAt(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vecA = null;
            Wrappers.Vector3 l_vecB = null;
            Wrappers.Vector3 l_vecC = null;
            l_argReader.ReadObject(ref l_vecA);
            l_argReader.ReadObject(ref l_vecB);
            l_argReader.ReadObject(ref l_vecC);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.LookAt(l_vecA.m_vec, l_vecB.m_vec, l_vecC.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Ortho(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_left = 0f;
            float l_right = 0f;
            float l_bottom = 0f;
            float l_top = 0f;
            float l_near = 0f;
            float l_far = 0f;
            l_argReader.ReadNumber(ref l_left);
            l_argReader.ReadNumber(ref l_right);
            l_argReader.ReadNumber(ref l_bottom);
            l_argReader.ReadNumber(ref l_top);
            l_argReader.ReadNumber(ref l_near);
            l_argReader.ReadNumber(ref l_far);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.Ortho(l_left, l_right, l_bottom, l_top, l_near, l_far)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Perspective(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            float l_fov = 0f;
            float l_aspect = 0f;
            float l_near = 0f;
            float l_far = 0f;
            l_argReader.ReadNumber(ref l_fov);
            l_argReader.ReadNumber(ref l_aspect);
            l_argReader.ReadNumber(ref l_near);
            l_argReader.ReadNumber(ref l_far);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.Perspective(l_fov, l_aspect, l_near, l_far)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Rotate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Quaternion l_rot = null;
            l_argReader.ReadObject(ref l_rot);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.Rotate(l_rot.m_quat)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Scale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.Scale(l_vec.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Translate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.Translate(l_vec.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int TRS(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Vector3 l_pos = null;
            Wrappers.Quaternion l_rot = null;
            Wrappers.Vector3 l_scl = null;
            l_argReader.ReadObject(ref l_pos);
            l_argReader.ReadObject(ref l_rot);
            l_argReader.ReadObject(ref l_scl);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(UnityEngine.Matrix4x4.TRS(l_pos.m_vec, l_rot.m_quat, l_scl.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Multiply(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            object l_val = null;
            l_argReader.ReadObject(ref l_mat);
            l_argReader.ReadObject(ref l_val);
            if(!l_argReader.HasErrors())
            {
                if(l_val is Wrappers.Matrix4x4)
                    l_argReader.PushObject(new Wrappers.Matrix4x4(l_mat.m_mat * (l_val as Wrappers.Matrix4x4).m_mat));
                else if(l_val is Wrappers.Vector4)
                    l_argReader.PushObject(new Wrappers.Vector4(l_mat.m_mat * (l_val as Wrappers.Vector4).m_vec));
                else
                {
                    l_argReader.SetError("Expected Matrix4x4 or Vector4");
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int ToString(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_mat.m_mat.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance properties
        static int GetDeterminant(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
                l_argReader.PushNumber(l_mat.m_mat.determinant);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetInverse(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(l_mat.m_mat.inverse));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetIsIdentity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_mat.m_mat.isIdentity);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetLossyScale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_mat.m_mat.lossyScale));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Quaternion(l_mat.m_mat.rotation));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetTranspose(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Matrix4x4(l_mat.m_mat.transpose));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance methods
        static int GetColumn(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_mat);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector4(l_mat.m_mat.GetColumn(UnityEngine.Mathf.Clamp(l_index, 0, 3))));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GetRow(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_mat);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector4(l_mat.m_mat.GetRow(UnityEngine.Mathf.Clamp(l_index, 0, 3))));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int MultiplyPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_mat);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_mat.m_mat.MultiplyPoint(l_vec.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int MultiplyPoint3x4(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_mat);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_mat.m_mat.MultiplyPoint3x4(l_vec.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int MultiplyVector(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_mat);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_mat.m_mat.MultiplyVector(l_vec.m_vec)));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetColumn(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            int l_index = 0;
            Wrappers.Vector4 l_vec = null;
            l_argReader.ReadObject(ref l_mat);
            l_argReader.ReadInteger(ref l_index);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                l_mat.m_mat.SetColumn(UnityEngine.Mathf.Clamp(l_index, 0, 3), l_vec.m_vec);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetRow(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            int l_index = 0;
            Wrappers.Vector4 l_vec = null;
            l_argReader.ReadObject(ref l_mat);
            l_argReader.ReadInteger(ref l_index);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                l_mat.m_mat.SetRow(UnityEngine.Mathf.Clamp(l_index, 0, 3), l_vec.m_vec);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetTRS(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            Wrappers.Vector3 l_pos = null;
            Wrappers.Quaternion l_rot = null;
            Wrappers.Vector3 l_scl = null;
            l_argReader.ReadObject(ref l_mat);
            l_argReader.ReadObject(ref l_pos);
            l_argReader.ReadObject(ref l_rot);
            l_argReader.ReadObject(ref l_scl);
            if(!l_argReader.HasErrors())
            {
                l_mat.m_mat.SetTRS(l_pos.m_vec, l_rot.m_quat, l_scl.m_vec);
                l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int ValidTRS(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Wrappers.Matrix4x4 l_mat = null;
            l_argReader.ReadObject(ref l_mat);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_mat.m_mat.ValidTRS());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
