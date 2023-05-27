using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class GameObjectDefs
    {
        const string c_destroyed = "GameObject is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(CreatePrimitive), CreatePrimitive));
            ms_staticMethods.Add((nameof(IsGameObject), IsGameObject));

            ms_instanceProperties.Add(("activeInHierarchy", (GetIsActiveInHierarchy, null)));
            ms_instanceProperties.Add(("activeSelf", (GetIsActiveSelf, null)));
            ms_instanceProperties.Add(("isStatic", (GetStatic, SetStatic)));
            ms_instanceProperties.Add(("layer", (GetLayer, SetLayer)));
            ms_instanceProperties.Add(("tag", (GetTag, SetTag)));
            ms_instanceProperties.Add(("transform", (GetTransform, null)));

            ms_instanceMethods.Add((nameof(CompareTag), CompareTag));
            ms_instanceMethods.Add((nameof(GetComponent), GetComponent));
            ms_instanceMethods.Add((nameof(SetActive), SetActive));

            ObjectDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(GameObject), Constructor, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
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
        static int GetIsActiveInHierarchy(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushBoolean(l_obj.activeInHierarchy);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetIsActiveSelf(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushBoolean(l_obj.activeSelf);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetStatic(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushBoolean(l_obj.isStatic);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetStatic(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_obj.isStatic = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetLayer(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushInteger(l_obj.layer);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetLayer(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            int l_layer = 0;
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadInteger(ref l_layer);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_obj.layer = l_layer;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTag(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushString(l_obj.tag);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetTag(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            string l_tag = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_tag);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_obj.tag = l_tag;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetTransform(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            GameObject l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushObject(l_obj.transform);
                else
                {
                    l_argReader.PushBoolean(false);
                    l_argReader.SetError(c_destroyed);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
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
                        l_argReader.PushObject(l_component);
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
    }
}
