using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class TransformDefs
    {
        const string c_destroyed = "Transform is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsTransform), IsTransform));

            ms_instanceProperties.Add(("childCount", (GetChildCount, null)));
            ms_instanceProperties.Add(("eulerAngles", (GetEulerAngles, SetEulerAngles)));
            ms_instanceProperties.Add(("forward", (GetForward, null)));
            ms_instanceProperties.Add(("hierarchyCapacity", (GetHierarchyCapacity, SetHierarchyCapacity)));
            ms_instanceProperties.Add(("hierarchyCount", (GetHierarchyCount, null)));
            ms_instanceProperties.Add(("localEulerAngles", (GetLocalEulerAngles, SetLocalEulerAngles)));
            ms_instanceProperties.Add(("localPosition", (GetLocalPosition, SetLocalPosition)));
            ms_instanceProperties.Add(("localRotation", (GetLocalRotation, SetLocalRotation)));
            ms_instanceProperties.Add(("localScale", (GetLocalScale, SetLocalScale)));
            //ms_instanceProperties.Add(("localToWorldMatrix", (?, ?)));
            ms_instanceProperties.Add(("lossyScale", (GetLossyScale, null)));
            ms_instanceProperties.Add(("parent", (GetParent, SetParentProp)));
            ms_instanceProperties.Add(("position", (GetPosition, SetPosition)));
            ms_instanceProperties.Add(("right", (GetRight, null)));
            ms_instanceProperties.Add(("root", (GetRoot, null)));
            ms_instanceProperties.Add(("rotation", (GetRotation, SetRotation)));
            ms_instanceProperties.Add(("up", (GetUp, null)));
            //ms_instanceProperties.Add(("worldToLocalMatrix", (?, ?)));

            ms_instanceMethods.Add((nameof(DetachChildren), DetachChildren));
            ms_instanceMethods.Add((nameof(Find), Find));
            ms_instanceMethods.Add((nameof(GetChild), GetChild));
            ms_instanceMethods.Add((nameof(GetSiblingIndex), GetSiblingIndex));
            ms_instanceMethods.Add((nameof(InverseTransformDirection), InverseTransformDirection));
            ms_instanceMethods.Add((nameof(InverseTransformPoint), InverseTransformPoint));
            ms_instanceMethods.Add((nameof(InverseTransformVector), InverseTransformVector));
            ms_instanceMethods.Add((nameof(IsChildOf), IsChildOf));
            ms_instanceMethods.Add((nameof(LookAt), LookAt));
            ms_instanceMethods.Add((nameof(Rotate), Rotate));
            ms_instanceMethods.Add((nameof(RotateAround), RotateAround));
            ms_instanceMethods.Add((nameof(SetAsFirstSibling), SetAsFirstSibling));
            ms_instanceMethods.Add((nameof(SetAsLastSibling), SetAsLastSibling));
            ms_instanceMethods.Add((nameof(SetParent), SetParent));
            ms_instanceMethods.Add((nameof(SetPositionAndRotation), SetPositionAndRotation));
            ms_instanceMethods.Add((nameof(SetSiblingIndex), SetSiblingIndex));
            ms_instanceMethods.Add((nameof(TransformDirection), TransformDirection));
            ms_instanceMethods.Add((nameof(TransformPoint), TransformPoint));
            ms_instanceMethods.Add((nameof(TransformVector), TransformVector));
            ms_instanceMethods.Add((nameof(Translate), Translate));

            ComponentDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Transform), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsTransform(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_trasform = null;
            l_argReader.ReadNextObject(ref l_trasform);
            l_argReader.PushBoolean(l_trasform != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetChildCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushInteger(l_transform.childCount);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }

        static int GetEulerAngles(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.eulerAngles));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }
        static int SetEulerAngles(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_transform.eulerAngles = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetForward(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.forward));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }

        static int GetHierarchyCapacity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushInteger(l_transform.hierarchyCapacity);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }
        static int SetHierarchyCapacity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_transform.hierarchyCapacity = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetHierarchyCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushInteger(l_transform.hierarchyCount);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }

        static int GetLocalEulerAngles(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.localEulerAngles));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }
        static int SetLocalEulerAngles(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_transform.localEulerAngles = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLocalPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.localPosition));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }
        static int SetLocalPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_transform.localPosition = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLocalRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Quaternion(l_transform.localRotation));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }
        static int SetLocalRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Quaternion l_quat = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_quat);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_transform.localRotation = l_quat.m_quat;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLocalScale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.localScale));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }
        static int SetLocalScale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_transform.localScale = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLossyScale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.lossyScale));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }

        static int GetParent(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    if(l_transform.parent != null)
                        l_argReader.PushObject(l_transform.parent);
                    else
                        l_argReader.PushBoolean(false);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }
        static int SetParentProp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Transform l_parent = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadNextObject(ref l_parent);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    if(l_parent != null)
                        l_transform.parent = l_parent;
                    else
                        l_transform.parent = null;
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.position));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }
        static int SetPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_transform.position = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.right));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }

        static int GetRoot(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(l_transform.root);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }

        static int GetRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Quaternion(l_transform.rotation));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }
        static int SetRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Quaternion l_quat = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_quat);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_transform.rotation = l_quat.m_quat;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.up));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return 1;
        }

        // Instance methods
        static int DetachChildren(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    l_transform.DetachChildren();
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Find(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            string l_name = "";
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadString(ref l_name);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    Transform l_child = l_transform.Find(l_name);
                    if(l_child != null)
                        l_argReader.PushObject(l_child);
                    else
                        l_argReader.PushBoolean(false);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GetChild(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    Transform l_child = l_transform.GetChild(l_index);
                    if(l_child != null)
                        l_argReader.PushObject(l_child);
                    else
                        l_argReader.PushBoolean(false);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GetSiblingIndex(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushInteger(l_transform.GetSiblingIndex());
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetSiblingIndex(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    l_transform.SetSiblingIndex(l_index);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int InverseTransformDirection(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.InverseTransformDirection(l_vec.m_vec)));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int InverseTransformPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.InverseTransformPoint(l_vec.m_vec)));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int InverseTransformVector(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.InverseTransformVector(l_vec.m_vec)));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int IsChildOf(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Transform l_parent = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_parent);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    if(l_parent != null)
                        l_argReader.PushBoolean(l_transform.IsChildOf(l_parent));
                    else
                    {
                        l_argReader.SetError(c_destroyed);
                        l_argReader.PushBoolean(false);
                    }
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int LookAt(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Transform l_target = null;
            Wrappers.Vector3 l_up = new Wrappers.Vector3(Vector3.up);
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_target);
            l_argReader.ReadNextObject(ref l_up);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    if(l_target != null)
                    {
                        l_transform.LookAt(l_target, l_up.m_vec);
                        l_argReader.PushBoolean(true);
                    }
                    else
                    {
                        l_argReader.SetError(c_destroyed);
                        l_argReader.PushBoolean(false);
                    }
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int RotateAround(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_point = null;
            Wrappers.Vector3 l_axis = null;
            float l_angle = 0f;
            l_argReader.ReadObject(ref l_point);
            l_argReader.ReadObject(ref l_axis);
            l_argReader.ReadNumber(ref l_angle);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    l_transform.RotateAround(l_point.m_vec, l_axis.m_vec, l_angle);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Rotate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            Space l_space = Space.Self;
            l_argReader.ReadObject(ref l_transform);
            if(l_argReader.IsNextObject())
            {
                l_argReader.ReadObject(ref l_vec);
            }
            else
            {
                l_vec = new Wrappers.Vector3();
                l_argReader.ReadNumber(ref l_vec.m_vec.x);
                l_argReader.ReadNumber(ref l_vec.m_vec.y);
                l_argReader.ReadNumber(ref l_vec.m_vec.z);
            }
            l_argReader.ReadNextEnum(ref l_space);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    l_transform.Rotate(l_vec.m_vec, l_space);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetAsFirstSibling(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    l_transform.SetAsFirstSibling();
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetAsLastSibling(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            l_argReader.ReadObject(ref l_transform);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    l_transform.SetAsFirstSibling();
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetParent(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Transform l_parent = null;
            bool l_worldStay = true;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadNextObject(ref l_parent);
            l_argReader.ReadNextBoolean(ref l_worldStay);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    l_transform.SetParent((l_parent != null) ? l_parent : null, l_worldStay);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetPositionAndRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            Wrappers.Quaternion l_quat = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            l_argReader.ReadObject(ref l_quat);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    l_transform.SetPositionAndRotation(l_vec.m_vec, l_quat.m_quat);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int TransformPoint(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.TransformPoint(l_vec.m_vec)));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int TransformDirection(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.TransformDirection(l_vec.m_vec)));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int TransformVector(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_transform.TransformVector(l_vec.m_vec)));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Translate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Transform l_transform = null;
            Wrappers.Vector3 l_vec = null;
            Space l_space = Space.Self;
            l_argReader.ReadObject(ref l_transform);
            l_argReader.ReadNextEnum(ref l_space);
            if(!l_argReader.HasErrors())
            {
                if(l_transform != null)
                {
                    l_transform.Translate(l_vec.m_vec, l_space);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
