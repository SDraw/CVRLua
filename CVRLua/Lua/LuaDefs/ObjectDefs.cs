using System;
using System.Collections.Generic;

namespace CVRLua.Lua.LuaDefs
{
    static class ObjectDefs
    {
        const string c_destroyed = "Object is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(Destroy), Destroy));
            ms_staticMethods.Add((nameof(Instantiate), Instantiate));

            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("name", (GetName, SetName)));

            ms_instanceMethods.Add((nameof(GetInstanceID), GetInstanceID));
            ms_instanceMethods.Add((nameof(ToString), ToString));
            ms_instanceMethods.Add(("Equals", Equal));
        }

        internal static void InheritTo(
            List<(string, LuaInterop.lua_CFunction)> p_metaMethods,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_staticProperties,
            List<(string, LuaInterop.lua_CFunction)> p_staticMethods,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_instanceProperties,
            List<(string, LuaInterop.lua_CFunction)> p_instanceMethods
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
            p_vm.RegisterClass(typeof(UnityEngine.Object), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsObject), IsObject);
        }

        // Static methods
        static int Destroy(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            UnityEngine.Object l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    if(l_obj.IsSafeToManipulate())
                    {
                        UnityEngine.Object.Destroy(l_obj);
                        l_argReader.PushBoolean(true);
                    }
                    else
                    {
                        l_argReader.SetError("Attempt to destroy unsafe Object");
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
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                {
                    if(l_obj.IsSafeToManipulate())
                    {
                        UnityEngine.Object l_copy = UnityEngine.Object.Instantiate(l_obj);
                        l_argReader.PushObject(l_copy);
                    }
                    else
                    {
                        l_argReader.SetError("Attempt to instantiate unsafe Object");
                        l_argReader.PushBoolean(false);
                    }
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
                l_argReader.PushBoolean(l_objA == l_objB);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        // Instane properties
        static int GetName(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            UnityEngine.Object l_obj = null;
            l_argReader.ReadObject(ref l_obj);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_argReader.PushString(l_obj.name);
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
        static int SetName(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            UnityEngine.Object l_obj = null;
            string l_name = "";
            l_argReader.ReadObject(ref l_obj);
            l_argReader.ReadString(ref l_name);
            if(!l_argReader.HasErrors())
            {
                if(l_obj != null)
                    l_obj.name = l_name;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
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
    }
}
