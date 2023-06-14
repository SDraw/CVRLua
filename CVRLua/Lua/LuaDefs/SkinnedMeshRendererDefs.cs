using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class SkinnedMeshRendererDefs
    {
        const string c_destroyed = "SkinnedMeshRenderer is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_instanceProperties.Add(("bones", (GetBones, null))); // Need table parsing for LuaArgReader
            ms_instanceProperties.Add(("forceMatrixRecalculationPerRender", (GetForceMatrixRecalculationPerRender, SetForceMatrixRecalculationPerRender)));
            ms_instanceProperties.Add(("localBounds", (GetLocalBounds, SetLocalBounds)));
            ms_instanceProperties.Add(("quality", (GetQuality, SetQuality)));
            //ms_instanceProperties.Add(("sharedMesh", (?, ?)));
            ms_instanceProperties.Add(("skinnedMotionVectors", (GetSkinnedMotionVectors, SetSkinnedMotionVectors)));
            ms_instanceProperties.Add(("updateWhenOffscreen", (GetUpdateWhenOffscreen, SetUpdateWhenOffscreen)));

            //ms_instanceMethods.Add((nameof(BakeMesh), BakeMesh));
            ms_instanceMethods.Add((nameof(GetBlendShapeWeight), GetBlendShapeWeight));
            ms_instanceMethods.Add((nameof(SetBlendShapeWeight), SetBlendShapeWeight));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(SkinnedMeshRenderer), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsSkinnedMeshRenderer), IsSkinnedMeshRenderer);
        }

        // Static methods
        static int IsSkinnedMeshRenderer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            l_argReader.ReadNextObject(ref l_render);
            l_argReader.PushBoolean(l_render != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetBones(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushTable(l_render.bones);
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

        static int GetForceMatrixRecalculationPerRender(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.forceMatrixRecalculationPerRender);
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
        static int SetForceMatrixRecalculationPerRender(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.forceMatrixRecalculationPerRender = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLocalBounds(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushObject(new Wrappers.Bounds(l_render.localBounds));
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
        static int SetLocalBounds(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            Wrappers.Bounds l_value = null;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.localBounds = l_value.m_bounds;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetQuality(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushString(l_render.quality.ToString());
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
        static int SetQuality(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            SkinQuality l_value = SkinQuality.Auto;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.quality = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSkinnedMotionVectors(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.skinnedMotionVectors);
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
        static int SetSkinnedMotionVectors(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.skinnedMotionVectors = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUpdateWhenOffscreen(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            l_argReader.ReadObject(ref l_render);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushBoolean(l_render.updateWhenOffscreen);
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
        static int SetUpdateWhenOffscreen(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_render.updateWhenOffscreen = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int GetBlendShapeWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                    l_argReader.PushNumber(l_render.GetBlendShapeWeight(l_index));
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

        static int SetBlendShapeWeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            SkinnedMeshRenderer l_render = null;
            int l_index = 0;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_render);
            l_argReader.ReadInteger(ref l_index);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_render != null)
                {
                    l_render.SetBlendShapeWeight(l_index, l_value);
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
    }
}
