using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class GameObjectDefs
    {
        const string c_destroyed = "GameObject is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(CreatePrimitive), CreatePrimitive);
            ms_staticMethods.Add(nameof(IsGameObject), IsGameObject);

            ms_instanceProperties.Add("activeInHierarchy", (IsActiveInHierarchy, null));
            ms_instanceProperties.Add("activeSelf", (IsActiveSelf, null));
            ms_instanceProperties.Add("isStatic", (GetStatic, SetStatic));
            ms_instanceProperties.Add("layer", (GetLayer, SetLayer));
            ms_instanceProperties.Add("tag", (GetTag, SetTag));
            ms_instanceProperties.Add("transform", (GetTransform, null));

            ms_instanceMethods.Add(nameof(CompareTag), CompareTag);
            ms_instanceMethods.Add(nameof(GetComponent), GetComponent);
            ms_instanceMethods.Add(nameof(SetActive), SetActive);

            ObjectDefs.Inherit(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(GameObject), Constructor, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
        }

        static int Constructor(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            string l_name = "";
            l_argReader.Skip(); // Metatable first
            l_argReader.ReadNextString(ref l_name);
            GameObject l_obj = new GameObject(l_name);
            l_argReader.PushObject(l_obj);
            return l_argReader.GetReturnValue();
        }

        // Static methods
        static int CreatePrimitive(IntPtr p_state)
        {
            var l_luaArgReader = new LuaArgReader(p_state);
            PrimitiveType l_type = PrimitiveType.Sphere;
            l_luaArgReader.ReadEnum(ref l_type);
            if(!l_luaArgReader.HasErrors())
            {
                GameObject l_obj = GameObject.CreatePrimitive(l_type);
                l_luaArgReader.PushObject(l_obj);
            }
            else
                l_luaArgReader.PushBoolean(false);

            l_luaArgReader.LogError();
            return l_luaArgReader.GetReturnValue();
        }

        static int IsGameObject(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            l_argReader.ReadNextObject(ref l_obj);
            l_argReader.PushBoolean(l_obj != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static void IsActiveInHierarchy(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as GameObject).activeInHierarchy);
        }

        static void IsActiveSelf(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as GameObject).activeSelf);
        }

        static void GetStatic(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as GameObject).isStatic);
        }

        static void SetStatic(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
            {
                (p_obj as GameObject).isStatic = l_state;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetLayer(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushInteger((p_obj as GameObject).layer);
        }

        static void SetLayer(object p_obj, LuaArgReader p_reader)
        {
            int l_layer = 0;
            p_reader.ReadInteger(ref l_layer);
            if(!p_reader.HasErrors())
            {
                (p_obj as GameObject).layer = l_layer;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetTag(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushString((p_obj as GameObject).tag);
        }

        static void SetTag(object p_obj, LuaArgReader p_reader)
        {
            string l_tag = "";
            p_reader.ReadString(ref l_tag);
            if(!p_reader.HasErrors())
            {
                (p_obj as GameObject).tag = l_tag;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetTransform(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject((p_obj as GameObject).transform);
        }

        // Instance methods
        static int CompareTag(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            string l_tag = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_tag);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushBoolean(l_obj.CompareTag(l_tag));
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int SetActive(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    l_obj.SetActive(l_state);
                    l_argReader.PushBoolean(true);
                }
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int GetComponent(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            string l_typeName = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_typeName);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    var l_component = l_obj.GetComponent(l_typeName);
                    if(l_component != null)
                        l_argReader.PushObject(l_component, l_typeName);
                    else
                        l_argReader.PushBoolean(false);
                }
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
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
            GameObject l_obj = null;
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
            GameObject l_obj = null;
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
