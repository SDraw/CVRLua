using System;
using System.Collections.Generic;
using TMPro;

namespace CVRLua.Lua.LuaDefs
{
    static class TextMeshProDefs
    {
        const string c_destroyed = "TextMeshPro is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_instanceProperties.Add(("autoSizeTextContainer", (GetAutoSizeTextContainer, SetAutoSizeTextContainer)));
            ms_instanceProperties.Add(("maskType", (GetMaskType, SetMaskType)));
            //ms_instanceProperties.Add(("mesh", (?, ?)));
            //ms_instanceProperties.Add(("meshFilter", (?, ?)));
            //ms_instanceProperties.Add(("renderer", (?, ?)));
            ms_instanceProperties.Add(("sortingLayerID", (GetSortingLayerID, SetSortingLayerID)));
            ms_instanceProperties.Add(("sortingOrder", (GetSortingOrder, SetSortingOrder)));
            ms_instanceProperties.Add(("text", (GetText, SetText))); // Should be implemented in one of parent classes, but too lazy

            ms_instanceMethods.Add((nameof(CalculateLayoutInputHorizontal), CalculateLayoutInputHorizontal));
            ms_instanceMethods.Add((nameof(CalculateLayoutInputVertical), CalculateLayoutInputVertical));
            ms_instanceMethods.Add((nameof(ClearMesh), ClearMesh));
            ms_instanceMethods.Add((nameof(ComputeMarginSize), ComputeMarginSize));
            ms_instanceMethods.Add((nameof(ForceMeshUpdate), ForceMeshUpdate));
            //ms_instanceMethods.Add((nameof(GetTextInfo), GetTextInfo));
            ms_instanceMethods.Add((nameof(Rebuild), Rebuild));
            ms_instanceMethods.Add((nameof(SetAllDirty), SetAllDirty));
            ms_instanceMethods.Add((nameof(SetLayoutDirty), SetLayoutDirty));
            ms_instanceMethods.Add((nameof(SetMask), SetMask));
            ms_instanceMethods.Add((nameof(SetMaterialDirty), SetMaterialDirty));
            ms_instanceMethods.Add((nameof(SetVerticesDirty), SetVerticesDirty));
            ms_instanceMethods.Add((nameof(UpdateFontAsset), UpdateFontAsset));
            //ms_instanceMethods.Add((nameof(UpdateGeometry), UpdateGeometry));
            ms_instanceMethods.Add((nameof(UpdateMeshPadding), UpdateMeshPadding));
            ms_instanceMethods.Add((nameof(UpdateVertexData), UpdateVertexData));

            MonoBehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(TextMeshPro), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsTextMeshPro), IsTextMeshPro);
        }

        // Static methods
        static int IsTextMeshPro(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_obj = null;
            l_argReader.ReadNextObject(ref l_obj);
            l_argReader.PushBoolean(l_obj != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAutoSizeTextContainer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_argReader.PushBoolean(l_text.autoSizeTextContainer);
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
        static int SetAutoSizeTextContainer(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_text.autoSizeTextContainer = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMaskType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_argReader.PushString(l_text.maskType.ToString());
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
        static int SetMaskType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            MaskingTypes l_type = MaskingTypes.MaskOff;
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadEnum(ref l_type);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_text.maskType = l_type;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSortingLayerID(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_argReader.PushInteger(l_text.sortingLayerID);
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
        static int SetSortingLayerID(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_text.sortingLayerID = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSortingOrder(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_argReader.PushInteger(l_text.sortingOrder);
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
        static int SetSortingOrder(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_text.sortingOrder = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetText(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_argReader.PushString(l_text.text);
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
        static int SetText(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            string l_value = "";
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadString(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                    l_text.text = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int CalculateLayoutInputHorizontal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.CalculateLayoutInputHorizontal();
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

        static int CalculateLayoutInputVertical(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.CalculateLayoutInputVertical();
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

        static int ClearMesh(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadNextBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.ClearMesh(l_state);
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

        static int ComputeMarginSize(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.ComputeMarginSize();
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

        static int ForceMeshUpdate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            bool l_stateA = false;
            bool l_stateB = false;
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadNextBoolean(ref l_stateA);
            l_argReader.ReadNextBoolean(ref l_stateB);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.ForceMeshUpdate(l_stateA, l_stateB);
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

        static int Rebuild(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            UnityEngine.UI.CanvasUpdate l_mode = UnityEngine.UI.CanvasUpdate.Prelayout;
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.Rebuild(l_mode);
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

        static int SetAllDirty(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.SetAllDirty();
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

        static int SetLayoutDirty(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.SetLayoutDirty();
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

        static int SetMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            MaskingTypes l_type = MaskingTypes.MaskOff;
            Wrappers.Vector4 l_vec = null;
            float l_softX = 0f;
            float l_softY = 0f;
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadEnum(ref l_type);
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadNextNumber(ref l_softX);
            l_argReader.ReadNextNumber(ref l_softY);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.SetMask(l_type, l_vec.m_vec, l_softX, l_softY);
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

        static int SetMaterialDirty(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.SetMaterialDirty();
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

        static int SetVerticesDirty(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.SetVerticesDirty();
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

        static int UpdateFontAsset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.UpdateFontAsset();
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

        static int UpdateMeshPadding(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            l_argReader.ReadObject(ref l_text);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.UpdateMeshPadding();
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

        static int UpdateVertexData(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            TextMeshPro l_text = null;
            TMP_VertexDataUpdateFlags l_flags = TMP_VertexDataUpdateFlags.All;
            l_argReader.ReadObject(ref l_text);
            l_argReader.ReadNextEnum(ref l_flags);
            if(!l_argReader.HasErrors())
            {
                if(l_text != null)
                {
                    l_text.UpdateVertexData(l_flags);
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
