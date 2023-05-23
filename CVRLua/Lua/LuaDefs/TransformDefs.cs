using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class TransformDefs
    {
        const string c_destroyed = "Transform is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(IsTransform), IsTransform);

            ms_instanceProperties.Add("childCount", (GetChildCount, null));
            ms_instanceProperties.Add("eulerAngles", (GetEulerAngles, SetEulerAngles));
            ms_instanceProperties.Add("forward", (GetForward, null));
            ms_instanceProperties.Add("hierarchyCapacity", (GetHierarchyCapacity, SetHierarchyCapacity));
            ms_instanceProperties.Add("hierarchyCount", (GetHierarchyCount, null));
            ms_instanceProperties.Add("localEulerAngles", (GetLocalEulerAngles, SetLocalEulerAngles));
            ms_instanceProperties.Add("localPosition", (GetLocalPosition, SetLocalPosition));
            ms_instanceProperties.Add("localRotation", (GetLocalRotation, SetLocalRotation));
            ms_instanceProperties.Add("localScale", (GetLocalScale, SetLocalScale));
            //ms_instanceProperties.Add("localToWorldMatrix", (?, ?));
            ms_instanceProperties.Add("lossyScale", (GetLossyScale, null));
            ms_instanceProperties.Add("parent", (GetParent, SetParent));
            ms_instanceProperties.Add("position", (GetPosition, SetPosition));
            ms_instanceProperties.Add("right", (GetRight, null));
            ms_instanceProperties.Add("root", (GetRoot, null));
            ms_instanceProperties.Add("rotation", (GetRotation, SetRotation));
            ms_instanceProperties.Add("up", (GetUp, null));
            //ms_instanceProperties.Add("worldToLocalMatrix", (?, ?));

            ms_instanceMethods.Add(nameof(DetachChildren), DetachChildren);
            ms_instanceMethods.Add(nameof(Find), Find);
            ms_instanceMethods.Add(nameof(GetChild), GetChild);
            ms_instanceMethods.Add(nameof(GetSiblingIndex), GetSiblingIndex);
            ms_instanceMethods.Add(nameof(InverseTransformDirection), InverseTransformDirection);
            ms_instanceMethods.Add(nameof(InverseTransformPoint), InverseTransformPoint);
            ms_instanceMethods.Add(nameof(InverseTransformVector), InverseTransformVector);
            ms_instanceMethods.Add(nameof(IsChildOf), IsChildOf);
            ms_instanceMethods.Add(nameof(LookAt), LookAt);
            ms_instanceMethods.Add(nameof(Rotate), Rotate);
            ms_instanceMethods.Add(nameof(RotateAround), RotateAround);
            ms_instanceMethods.Add(nameof(SetAsFirstSibling), SetAsFirstSibling);
            ms_instanceMethods.Add(nameof(SetAsLastSibling), SetAsLastSibling);
            ms_instanceMethods.Add(nameof(SetParent), SetParent);
            ms_instanceMethods.Add(nameof(SetPositionAndRotation), SetPositionAndRotation);
            ms_instanceMethods.Add(nameof(SetSiblingIndex), SetSiblingIndex);
            ms_instanceMethods.Add(nameof(TransformDirection), TransformDirection);
            ms_instanceMethods.Add(nameof(TransformPoint), TransformPoint);
            ms_instanceMethods.Add(nameof(TransformVector), TransformVector);
            ms_instanceMethods.Add(nameof(Translate), Translate);

            ComponentDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Transform), null, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
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
        static void GetChildCount(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as Transform).childCount);
        }

        static void GetEulerAngles(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = new Wrappers.Vector3((p_obj as Transform).eulerAngles);
            p_reader.PushObject(l_vec);
        }
        static void SetEulerAngles(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = null;
            p_reader.ReadObject(ref l_vec);
            if(!p_reader.HasErrors())
                (p_obj as Transform).eulerAngles = l_vec.m_vec;
        }

        static void GetForward(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = new Wrappers.Vector3((p_obj as Transform).forward);
            p_reader.PushObject(l_vec);
        }

        static void GetHierarchyCapacity(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushInteger((p_obj as Transform).hierarchyCapacity);
        }
        static void SetHierarchyCapacity(object p_obj, LuaArgReader p_reader)
        {
            int l_capacity = 0;
            p_reader.ReadInteger(ref l_capacity);
            if(!p_reader.HasErrors())
                (p_obj as Transform).hierarchyCapacity = l_capacity;
        }

        static void GetHierarchyCount(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushInteger((p_obj as Transform).hierarchyCount);
        }

        static void GetLocalEulerAngles(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = new Wrappers.Vector3((p_obj as Transform).localEulerAngles);
            p_reader.PushObject(l_vec);
        }
        static void SetLocalEulerAngles(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = null;
            p_reader.ReadObject(ref l_vec);
            if(!p_reader.HasErrors())
                (p_obj as Transform).localEulerAngles = l_vec.m_vec;
        }

        static void GetLocalPosition(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = new Wrappers.Vector3((p_obj as Transform).localPosition);
            p_reader.PushObject(l_vec);
        }
        static void SetLocalPosition(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = null;
            p_reader.ReadObject(ref l_vec);
            if(!p_reader.HasErrors())
                (p_obj as Transform).localPosition = l_vec.m_vec;
        }

        static void GetLocalRotation(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Quaternion l_quat = new Wrappers.Quaternion((p_obj as Transform).localRotation);
            p_reader.PushObject(l_quat);
        }
        static void SetLocalRotation(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Quaternion l_quat = null;
            p_reader.ReadObject(ref l_quat);
            if(!p_reader.HasErrors())
                (p_obj as Transform).localRotation = l_quat.m_quat;
        }

        static void GetLocalScale(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = new Wrappers.Vector3((p_obj as Transform).localScale);
            p_reader.PushObject(l_vec);
        }
        static void SetLocalScale(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = null;
            p_reader.ReadObject(ref l_vec);
            if(!p_reader.HasErrors())
                (p_obj as Transform).localScale = l_vec.m_vec;
        }

        static void GetLossyScale(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = new Wrappers.Vector3((p_obj as Transform).lossyScale);
            p_reader.PushObject(l_vec);
        }

        static void GetParent(object p_obj, LuaArgReader p_reader)
        {
            if((p_obj as Transform).parent != null)
                p_reader.PushObject((p_obj as Transform).parent);
            else
                p_reader.PushBoolean(false);
        }

        static void SetParent(object p_obj, LuaArgReader p_reader)
        {
            if(p_reader.IsNextNil())
                (p_obj as Transform).parent = null;
            else
            {
                Transform l_parent = null;
                p_reader.ReadObject(ref l_parent);
                if(!p_reader.HasErrors())
                {
                    if(l_parent != null)
                        (p_obj as Transform).parent = l_parent;
                    else
                        p_reader.SetError(c_destroyed);
                }
            }
        }

        static void GetPosition(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = new Wrappers.Vector3((p_obj as Transform).position);
            p_reader.PushObject(l_vec);
        }
        static void SetPosition(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = null;
            p_reader.ReadObject(ref l_vec);
            if(!p_reader.HasErrors())
                (p_obj as Transform).position = l_vec.m_vec;
        }

        static void GetRight(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = new Wrappers.Vector3((p_obj as Transform).right);
            p_reader.PushObject(l_vec);
        }

        static void GetRoot(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject((p_obj as Transform).root);
        }

        static void GetRotation(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Quaternion l_quat = new Wrappers.Quaternion((p_obj as Transform).rotation);
            p_reader.PushObject(l_quat);
        }
        static void SetRotation(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Quaternion l_quat = null;
            p_reader.ReadObject(ref l_quat);
            if(!p_reader.HasErrors())
                (p_obj as Transform).rotation = l_quat.m_quat;
        }

        static void GetUp(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = new Wrappers.Vector3((p_obj as Transform).up);
            p_reader.PushObject(l_vec);
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
            Transform l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    if(ms_instanceMethods.TryGetValue(l_key, out var l_func))
                        l_argReader.PushFunction(l_func); // Lua handles it by itself
                    else if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item1 != null))
                        l_pair.Item1.Invoke(l_obj, l_argReader);
                    else
                        l_argReader.PushNil();
                }
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushNil();
                }
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
            Transform l_obj = null;
            string l_key = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_key);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    if(ms_instanceProperties.TryGetValue(l_key, out var l_pair) && (l_pair.Item2 != null))
                        l_pair.Item2.Invoke(l_obj, l_argReader);
                }
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
