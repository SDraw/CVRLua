using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class ObjectDefs
    {
        const string c_destroyed = "Object is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(Destroy), Destroy);
            ms_staticMethods.Add(nameof(Instantiate), Instantiate);
            ms_staticMethods.Add(nameof(IsObject), IsObject);

            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add("name", (GetName, SetName));

            ms_instanceMethods.Add(nameof(GetInstanceID), GetInstanceID);
            ms_instanceMethods.Add(nameof(ToString), ToString);
            ms_instanceMethods.Add("Equals", Equal);
        }

        internal static void Inherit(
            List<(string, LuaInterop.lua_CFunction)> p_metaMethods,
            Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> p_staticProperties,
            Dictionary<string, LuaInterop.lua_CFunction> p_staticMethods,
            Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> p_instanceProperties,
            Dictionary<string, LuaInterop.lua_CFunction> p_instanceMethods
        )
        {
            if(p_metaMethods != null)
                ms_metaMethods.MergeInto(p_metaMethods);

            if(p_staticProperties != null)
                ms_staticProperties.MergeInto(p_staticProperties);

            if(p_staticMethods != null)
                ms_staticMethods.MergeInto(p_staticMethods);

            if(p_instanceProperties != null)
                ms_instanceProperties.MergeInto(p_instanceProperties);

            if(p_instanceMethods != null)
                ms_instanceMethods.MergeInto(p_instanceMethods);
        }

        public static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(UnityEngine.Object), null, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
        }

        // Static methods
        static int Destroy(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            UnityEngine.Object l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    if(l_obj.IsSafeToDestroy())
                    {
                        UnityEngine.Object.Destroy(l_obj);
                        l_argReader.PushBoolean(true);
                    }
                    else
                    {
                        l_argReader.SetError("Attempt to destroy non-safe Object");
                        l_argReader.PushBoolean(false);
                    }
                }
                else
                    l_argReader.PushBoolean(true);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        static int Instantiate(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            UnityEngine.Object l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    UnityEngine.Object l_copy = UnityEngine.Object.Instantiate(l_obj);
                    l_argReader.PushObject(l_copy);
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

        static int IsObject(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            UnityEngine.Object l_obj = null;
            l_argReader.ReadNextObject(ref l_obj);
            l_argReader.PushBoolean(l_obj != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int ToString(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            UnityEngine.Object l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushString(l_obj.ToString());
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

        static int Equal(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            UnityEngine.Object l_objA = null;
            UnityEngine.Object l_objB = null;
            l_argReader.ReadObject(ref l_objA);
            l_argReader.ReadObject(ref l_objB);
            if(!l_argReader.HasErrors())
            {
                if((l_objA != null) && (l_objB != null))
                    l_argReader.PushBoolean(l_objA.GetInstanceID() == l_objB.GetInstanceID());
                else
                    l_argReader.PushBoolean(false);
            }
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        // Instane properties
        static void GetName(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushString((p_obj as UnityEngine.Object).name);
        }

        static void SetName(object p_obj, LuaArgReader p_reader)
        {
            string l_name = "";
            p_reader.ReadString(ref l_name);
            if(!p_reader.HasErrors())
            {
                (p_obj as UnityEngine.Object).name = l_name;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        // Instance methods
        static int GetInstanceID(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            UnityEngine.Object l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushInteger(l_obj.GetInstanceID());
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
            UnityEngine.Object l_obj = null;
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
            UnityEngine.Object l_obj = null;
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
