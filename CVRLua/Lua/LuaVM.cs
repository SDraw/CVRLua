using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CVRLua.Lua
{
    class LuaVM
    {
        class ReferencedObject
        {
            public object m_object = null;
            public int m_references = 0;

            public ReferencedObject(object p_obj)
            {
                m_object = p_obj;
                m_references = 1;
            }
        }

        const string c_propGet = "__propGet";
        const string c_propSet = "__propSet";
        const string c_methods = "__methods";
        const string c_objectsPool = "___ObjectsPool";

        static readonly Dictionary<IntPtr, LuaVM> ms_VMs = new Dictionary<IntPtr, LuaVM>();

        readonly string m_name;
        readonly IntPtr m_state = IntPtr.Zero;
        readonly Dictionary<long, ReferencedObject> m_objectsMap = null;

        internal LuaVM(string p_name)
        {
            m_name = p_name;
            m_state = LuaInterop.luaL_newstate();
            ms_VMs.Add(m_state, this);

            LuaInterop.luaL_requiref(m_state, "_G", LuaInterop.luaopen_base, 1);
            LuaInterop.luaL_requiref(m_state, "coroutine", LuaInterop.luaopen_coroutine, 1);
            LuaInterop.luaL_requiref(m_state, "table", LuaInterop.luaopen_table, 1);
            LuaInterop.luaL_requiref(m_state, "string", LuaInterop.luaopen_string, 1);
            LuaInterop.luaL_requiref(m_state, "math", LuaInterop.luaopen_math, 1);
            LuaInterop.luaL_requiref(m_state, "utf8", LuaInterop.luaopen_utf8, 1);

            m_objectsMap = new Dictionary<long, ReferencedObject>();

            // Table weak values
            LuaInterop.lua_newtable(m_state);
            LuaInterop.lua_newtable(m_state);
            LuaInterop.lua_pushstring(m_state, "v");
            LuaInterop.lua_setfield(m_state, -2, "__mode");
            LuaInterop.lua_pushcfunction(m_state, NilResultFunction);
            LuaInterop.lua_setfield(m_state, -2, "__metatable");
            LuaInterop.lua_setmetatable(m_state, -2); // Combines two previous tables
            LuaInterop.lua_setfield(m_state, LuaInterop.LUA_REGISTRYINDEX, c_objectsPool);
        }
        ~LuaVM()
        {
            ms_VMs.Remove(m_state);
            LuaInterop.lua_close(m_state);
        }

        // Generic Lua stuff
        public int GetTop() => LuaInterop.lua_gettop(m_state);

        public bool IsBoolean(int p_index) => LuaInterop.lua_isboolean(m_state, p_index);
        public bool ToBoolean(int p_index) => (LuaInterop.lua_toboolean(m_state, p_index) == 1);
        public void PushBoolean(bool p_val) => LuaInterop.lua_pushboolean(m_state, p_val ? 1 : 0);

        public bool IsInteger(int p_index) => (LuaInterop.lua_isinteger(m_state, p_index) == 1);
        public long ToInteger(int p_index) => LuaInterop.lua_tointeger(m_state, p_index);
        public void PushInteger(long p_val) => LuaInterop.lua_pushinteger(m_state, p_val);

        public bool IsNumber(int p_index) => LuaInterop.lua_isnumber(m_state, p_index);
        public double ToNumber(int p_index) => LuaInterop.lua_tonumber(m_state, p_index);
        public void PushNumber(double p_val) => LuaInterop.lua_pushnumber(m_state, p_val);

        public bool IsString(int p_index) => LuaInterop.lua_isstring(m_state, p_index);
        public string ToString(int p_index) => LuaInterop.lua_tostring(m_state, p_index);
        public void PushString(string p_str) => LuaInterop.lua_pushstring(m_state, p_str);

        public bool IsUserdata(int p_index) => LuaInterop.lua_isuserdata(m_state, p_index);
        public IntPtr ToUserdata(int p_index) => LuaInterop.lua_touserdata(m_state, p_index);

        public bool IsNil(int p_index) => LuaInterop.lua_isnil(m_state, p_index);
        public void PushNil() => LuaInterop.lua_pushnil(m_state);

        public bool IsFunction(int p_index) => LuaInterop.lua_isfunction(m_state, p_index);
        public void PushFunction(LuaInterop.lua_CFunction p_func) => LuaInterop.lua_pushcfunction(m_state, p_func);

        // Execution
        internal void Execute(string p_script)
        {
            if((LuaInterop.luaL_loadstring(m_state, p_script) != LuaInterop.LUA_OK) || (LuaInterop.lua_pcall(m_state, 0, 0, 0) != LuaInterop.LUA_OK))
            {
                LuaLogger.Log(LuaInterop.lua_tostring(m_state, -1));
                LuaInterop.lua_pop(m_state, 1);
            }
        }

        internal void Execute(string p_blockName, ref byte[] p_data)
        {
            if((LuaInterop.luaL_loadbuffer(m_state, ref p_data, p_data.Length, p_blockName) != LuaInterop.LUA_OK) || (LuaInterop.lua_pcall(m_state, 0, 0, 0) != LuaInterop.LUA_OK))
            {
                LuaLogger.Log(LuaInterop.lua_tostring(m_state, -1));
                LuaInterop.lua_pop(m_state, 1);
            }
        }

        // Objects push/get
        public void PushObject(object p_obj)
        {
            Type l_type = p_obj.GetType();
            long l_hash = Utils.CombineInts(RuntimeHelpers.GetHashCode(p_obj), l_type.GetHashCode()); // Help ...
            if(m_objectsMap.TryGetValue(l_hash, out var l_refObj))
                l_refObj.m_references++;
            else
                m_objectsMap.Add(l_hash, new ReferencedObject(p_obj));

            LuaInterop.lua_getfield(m_state, LuaInterop.LUA_REGISTRYINDEX, c_objectsPool);
            if(LuaInterop.lua_geti(m_state, -1, l_hash) == LuaInterop.LUA_TNIL)
            {
                LuaInterop.lua_pop(m_state, 1);
                LuaInterop.lua_newuserdata(m_state, IntPtr.Size).SetInt(l_hash);
                LuaInterop.luaL_setmetatable(m_state, l_type.Name);
                LuaInterop.lua_pushvalue(m_state, -1);
                LuaInterop.lua_seti(m_state, -3, l_hash);
            }
            LuaInterop.lua_remove(m_state, -2);
        }

        public bool GetObject<T>(ref T p_obj, int p_index) where T : class
        {
            bool l_result = false;
            if(IsUserdata(p_index))
            {
                long l_hash = ToUserdata(p_index).GetInt();
                if((l_hash != 0) && m_objectsMap.TryGetValue(l_hash, out var l_refObj) && (l_refObj.m_object is T))
                {
                    p_obj = (T)l_refObj.m_object;
                    l_result = true;
                }
            }
            return l_result;
        }

        public bool IsObject(int p_index) => IsUserdata(p_index);

        // Variables
        public void SetGlobalVariable(string p_name, object p_obj)
        {
            PushValue(p_obj);
            LuaInterop.lua_setglobal(m_state, p_name);
        }

        // Functions
        public bool IsGlobalFunctionPresent(string p_name)
        {
            bool l_result = (LuaInterop.lua_getglobal(m_state, p_name) == LuaInterop.LUA_TFUNCTION);
            LuaInterop.lua_pop(m_state, 1);
            return l_result;
        }

        public int GetGlobalFunctionReference(string p_name)
        {
            int l_result = 0;

            if(LuaInterop.lua_getglobal(m_state, p_name) == LuaInterop.LUA_TFUNCTION)
                l_result = LuaInterop.luaL_ref(m_state, LuaInterop.LUA_REGISTRYINDEX);
            else
                LuaInterop.lua_pop(m_state, 1);

            return l_result;
        }

        public void CallFunction(int p_ref, params object[] p_args)
        {
            if(LuaInterop.lua_rawgeti(m_state, LuaInterop.LUA_REGISTRYINDEX, p_ref) == LuaInterop.LUA_TFUNCTION)
            {
                foreach(var l_value in p_args)
                    PushValue(l_value);
                if(LuaInterop.lua_pcall(m_state, p_args.Length, 0, 0) != 0)
                {
                    LuaLogger.Log(LuaInterop.lua_tostring(m_state, -1));
                    LuaInterop.lua_pop(m_state, 1);
                }
            }
            else
                LuaInterop.lua_pop(m_state, 1);
        }

        public void CallFunction(int p_ref, List<object> p_results, params object[] p_args)
        {
            if(LuaInterop.lua_rawgeti(m_state, LuaInterop.LUA_REGISTRYINDEX, p_ref) == LuaInterop.LUA_TFUNCTION)
            {
                int l_top = LuaInterop.lua_gettop(m_state);
                foreach(var l_value in p_args)
                    PushValue(l_value);
                if(LuaInterop.lua_pcall(m_state, p_args.Length, LuaInterop.LUA_MULTRET, 0) != LuaInterop.LUA_OK)
                {
                    LuaLogger.Log(LuaInterop.lua_tostring(m_state, -1));
                    LuaInterop.lua_pop(m_state, 1);
                }
                else
                {
                    for(int i = l_top, j = LuaInterop.lua_gettop(m_state); i <= j; i++)
                        p_results.Add(ReadValue(i));
                    if(p_results.Count > 0)
                        LuaInterop.lua_pop(m_state, p_results.Count);
                }
            }
            else
                LuaInterop.lua_pop(m_state, 1);
        }

        public void RegisterGlobalFunction(string p_name, LuaInterop.lua_CFunction p_func) => LuaInterop.lua_register(m_state, p_name, p_func);

        // VM separation
        public static LuaVM GetVM(IntPtr p_state)
        {
            LuaVM l_result = null;
            ms_VMs.TryGetValue(p_state, out l_result);
            return l_result;
        }

        // GC
        public void PerformGC()
        {
            LuaInterop.lua_gc(m_state, LuaInterop.LUA_GCSTEP, 0);
        }

        static int ObjectsGC(IntPtr p_state)
        {
            LuaVM l_vm = GetVM(p_state);
            if((l_vm != null) && l_vm.IsUserdata(1))
            {
                long l_hash = l_vm.ToUserdata(1).GetInt();
                if((l_hash != 0) && l_vm.m_objectsMap.TryGetValue(l_hash, out var l_orc))
                {
                    l_orc.m_references--;
                    if(l_orc.m_references == 0)
                        l_vm.m_objectsMap.Remove(l_hash);
                }
            }
            return 0;
        }

        // Extended reads/pushes
        public object ReadValue(int p_index)
        {
            object l_result = null;
            if(LuaInterop.lua_isinteger(m_state, p_index) == 1)
                l_result = LuaInterop.lua_tointeger(m_state, p_index);
            else
            {
                switch(LuaInterop.lua_type(m_state, p_index))
                {
                    case LuaInterop.LUA_TBOOLEAN:
                        l_result = (LuaInterop.lua_toboolean(m_state, p_index) == 1);
                        break;
                    case LuaInterop.LUA_TNUMBER:
                        l_result = LuaInterop.lua_tonumber(m_state, p_index);
                        break;
                    case LuaInterop.LUA_TSTRING:
                        l_result = LuaInterop.lua_tostring(m_state, p_index);
                        break;
                    case LuaInterop.LUA_TUSERDATA:
                    {
                        long l_hash = LuaInterop.lua_touserdata(m_state, p_index).GetInt();
                        if(m_objectsMap.TryGetValue(l_hash, out var l_refObj))
                            l_result = l_refObj.m_object;
                    }
                    break;
                }
            }
            return l_result;
        }

        public void PushValue(object p_value)
        {
            // Always pushes something
            if(p_value == null)
            {
                PushNil();
                return;
            }

            switch(Type.GetTypeCode(p_value.GetType()))
            {
                case TypeCode.Boolean:
                    PushBoolean((bool)p_value);
                    break;
                case TypeCode.Int32:
                    PushInteger((int)p_value);
                    break;
                case TypeCode.Int64:
                    PushInteger((long)p_value);
                    break;
                case TypeCode.Single:
                    PushNumber((float)p_value);
                    break;
                case TypeCode.Double:
                    PushNumber((double)p_value);
                    break;
                case TypeCode.String:
                    PushString((string)p_value);
                    break;
                case TypeCode.Object:
                {
                    if(p_value.GetType().IsClass)
                        PushObject(p_value);
                    else
                        PushNil();
                }
                break;

                default:
                    PushNil();
                    break;
            }
        }

        public void PushTable<T>(T[] p_array)
        {
            LuaInterop.lua_newtable(m_state);
            for(int i = 0, j = p_array.Length; i < j; i++)
            {
                LuaInterop.lua_pushinteger(m_state, i + 1);
                PushValue(p_array[i]);
                LuaInterop.lua_settable(m_state, -3);
            }
        }

        public void PushTable<T>(List<T> p_list)
        {
            LuaInterop.lua_newtable(m_state);
            long l_index = 1;
            foreach(object l_obj in p_list)
            {
                LuaInterop.lua_pushinteger(m_state, l_index);
                PushValue(l_obj);
                LuaInterop.lua_settable(m_state, -3);
                l_index++;
            }
        }

        public void PushTable(Dictionary<string, object> p_list)
        {
            LuaInterop.lua_newtable(m_state);
            foreach(var l_pair in p_list)
            {
                PushValue(l_pair.Value);
                LuaInterop.lua_setfield(m_state, -2, l_pair.Key);
            }
        }

        // Debug
        public void GetStateInfo(out string p_name, out int p_line)
        {
            LuaInterop.lua_Debug l_debug = new LuaInterop.lua_Debug();
            LuaInterop.lua_getstack(m_state, 1, ref l_debug);
            LuaInterop.lua_getinfo(m_state, "l", ref l_debug);
            p_line = l_debug.currentline;
            p_name = m_name;
        }

        // Classes
        public void RegisterClass(
            Type p_type,
            LuaInterop.lua_CFunction p_ctor,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_staticProperties,
            List<(string, LuaInterop.lua_CFunction)> p_staticMethods,
            List<(string, LuaInterop.lua_CFunction)> p_metaMethods,
            List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> p_instanceProperties,
            List<(string, LuaInterop.lua_CFunction)> p_instanceMethods
        )
        {
            // Static definition
            LuaInterop.lua_newtable(m_state); // {}
            LuaInterop.lua_newtable(m_state); // {}

            if(p_ctor != null)
            {
                LuaInterop.lua_pushcfunction(m_state, p_ctor);
                LuaInterop.lua_setfield(m_state, -2, "__call");
            }

            LuaInterop.lua_pushcfunction(m_state, NilResultFunction);
            LuaInterop.lua_setfield(m_state, -2, "__metatable");

            LuaInterop.lua_pushcfunction(m_state, ClassStaticGet);
            LuaInterop.lua_setfield(m_state, -2, "__index");

            LuaInterop.lua_pushcfunction(m_state, ClassStaticSet);
            LuaInterop.lua_setfield(m_state, -2, "__newindex");

            LuaInterop.lua_newtable(m_state);
            if(p_staticProperties != null)
            {
                foreach(var l_prop in p_staticProperties)
                {
                    if(l_prop.Item2.Item1 != null) // property get
                    {
                        LuaInterop.lua_pushcfunction(m_state, l_prop.Item2.Item1);
                        LuaInterop.lua_setfield(m_state, -2, l_prop.Item1);
                    }
                }
            }
            LuaInterop.lua_setfield(m_state, -2, c_propGet);

            LuaInterop.lua_newtable(m_state); // {}
            if(p_staticProperties != null)
            {
                foreach(var l_pair in p_staticProperties)
                {
                    if(l_pair.Item2.Item2 != null) // Property set
                    {
                        LuaInterop.lua_pushcfunction(m_state, l_pair.Item2.Item2);
                        LuaInterop.lua_setfield(m_state, -2, l_pair.Item1);
                    }
                }
            }
            LuaInterop.lua_setfield(m_state, -2, c_propSet);

            LuaInterop.lua_newtable(m_state);
            if(p_staticMethods != null)
            {
                foreach(var l_pair in p_staticMethods)
                {
                    LuaInterop.lua_pushcfunction(m_state, l_pair.Item2);
                    LuaInterop.lua_setfield(m_state, -2, l_pair.Item1);
                }
            }
            LuaInterop.lua_setfield(m_state, -2, c_methods);

            LuaInterop.lua_setmetatable(m_state, -2); // Combines two previous tables
            LuaInterop.lua_setglobal(m_state, p_type.Name); // Sets as global

            // Instance definition
            LuaInterop.luaL_newmetatable(m_state, p_type.Name); // Registry metatable

            LuaInterop.lua_pushcfunction(m_state, NilResultFunction);
            LuaInterop.lua_setfield(m_state, -2, "__metatable");

            LuaInterop.lua_pushcfunction(m_state, ClassInstanceGet);
            LuaInterop.lua_setfield(m_state, -2, "__index");

            LuaInterop.lua_pushcfunction(m_state, ClassInstanceSet);
            LuaInterop.lua_setfield(m_state, -2, "__newindex");

            LuaInterop.lua_pushcfunction(m_state, ObjectsGC);
            LuaInterop.lua_setfield(m_state, -2, "__gc"); // Garbage collector for referenced objects as userdata

            if(p_metaMethods != null)
            {
                foreach(var l_pair in p_metaMethods)
                {
                    LuaInterop.lua_pushcfunction(m_state, l_pair.Item2);
                    LuaInterop.lua_setfield(m_state, -2, l_pair.Item1); // Push additional metatable methods if any
                }
            }

            LuaInterop.lua_newtable(m_state);
            if(p_instanceProperties != null)
            {
                foreach(var l_prop in p_instanceProperties)
                {
                    if(l_prop.Item2.Item1 != null)
                    {
                        LuaInterop.lua_pushcfunction(m_state, l_prop.Item2.Item1);
                        LuaInterop.lua_setfield(m_state, -2, l_prop.Item1);
                    }
                }
            }
            LuaInterop.lua_setfield(m_state, -2, c_propGet);

            LuaInterop.lua_newtable(m_state);
            if(p_instanceProperties != null)
            {
                foreach(var l_prop in p_instanceProperties)
                {
                    if(l_prop.Item2.Item2 != null) // Property set
                    {
                        LuaInterop.lua_pushcfunction(m_state, l_prop.Item2.Item2);
                        LuaInterop.lua_setfield(m_state, -2, l_prop.Item1);
                    }
                }
            }
            LuaInterop.lua_setfield(m_state, -2, c_propSet);

            LuaInterop.lua_newtable(m_state);
            if(p_instanceMethods != null)
            {
                foreach(var l_pair in p_instanceMethods)
                {
                    LuaInterop.lua_pushcfunction(m_state, l_pair.Item2);
                    LuaInterop.lua_setfield(m_state, -2, l_pair.Item1);
                }
            }
            LuaInterop.lua_setfield(m_state, -2, c_methods);

            LuaInterop.lua_pop(m_state, 1); // Pop metatable
        }

        static int ClassStaticGet(IntPtr p_state)
        {
            // Current stack - 1-table, 2-key
            if(!LuaInterop.lua_isstring(p_state, 2)) // Not a string as key
            {
                LuaInterop.lua_pushnil(p_state);
                return 1;
            }
            string l_key = LuaInterop.lua_tostring(p_state, 2);

            LuaInterop.luaL_getmetafield(p_state, 1, c_methods); // table on top
            if(LuaInterop.lua_getfield(p_state, -1, l_key) == LuaInterop.LUA_TFUNCTION)
            {
                // function is on top
                LuaInterop.lua_remove(p_state, -2); // remove table and shift stack down
                return 1;
            }

            // Not a method, maybe a prop?
            LuaInterop.lua_pop(p_state, 2); // remove undesired value and table from stack
            LuaInterop.luaL_getmetafield(p_state, 1, c_propGet); // table is on top
            if(LuaInterop.lua_getfield(p_state, -1, l_key) == LuaInterop.LUA_TFUNCTION)
            {
                LuaInterop.lua_call(p_state, 0, 1); // result is on top
                LuaInterop.lua_remove(p_state, -2); // remove table and shift stack down
                return 1;
            }

            // Nothing found, push nil
            LuaInterop.lua_pop(p_state, 2); // remove undesired value and table from stack
            LuaInterop.lua_pushnil(p_state);
            return 1;
        }

        static int ClassStaticSet(IntPtr p_state)
        {
            // Current stack - 1-table, 2-key, 3-value
            if(!LuaInterop.lua_isstring(p_state, 2)) // Not a string as key
                return 0;
            string l_key = LuaInterop.lua_tostring(p_state, 2);

            LuaInterop.luaL_getmetafield(p_state, 1, c_propSet); // table on top
            if(LuaInterop.lua_getfield(p_state, -1, l_key) == LuaInterop.LUA_TFUNCTION)
            {
                // now function is on top
                LuaInterop.lua_pushvalue(p_state, 3); // copy value
                LuaInterop.lua_call(p_state, 1, 0); // call, no result should return
                LuaInterop.lua_pop(p_state, 1); // remove table from stack
                return 0;
            }

            // Nothing found
            LuaInterop.lua_pop(p_state, 2); // remove table and value from stack
            return 0;
        }

        static int ClassInstanceGet(IntPtr p_state)
        {
            // Current stack - 1-userdata, 2-key
            if(!LuaInterop.lua_isstring(p_state, 2)) // Not a string as key
            {
                LuaInterop.lua_pushnil(p_state);
                return 1;
            }
            string l_key = LuaInterop.lua_tostring(p_state, 2);

            LuaInterop.luaL_getmetafield(p_state, 1, c_methods); // table is on top
            if(LuaInterop.lua_getfield(p_state, -1, l_key) == LuaInterop.LUA_TFUNCTION)
            {
                // result function on top
                LuaInterop.lua_remove(p_state, -2); // remove table and shift stack down
                return 1;
            }

            // Not a method, maybe a prop?
            LuaInterop.lua_pop(p_state, 2); // remove undesired value and table from stack
            LuaInterop.luaL_getmetafield(p_state, 1, c_propGet); // table is on top
            if(LuaInterop.lua_getfield(p_state, -1, l_key) == LuaInterop.LUA_TFUNCTION)
            {
                // function is on top
                LuaInterop.lua_pushvalue(p_state, 1); // copy userdata on top
                LuaInterop.lua_call(p_state, 1, 1); // result on top
                LuaInterop.lua_remove(p_state, -2); // remove table and shift stack down
                return 1;
            }

            // Nothing found, push nil
            LuaInterop.lua_pop(p_state, 2); // remove undesired value and table from stack
            LuaInterop.lua_pushnil(p_state);
            return 1;
        }

        static int ClassInstanceSet(IntPtr p_state)
        {
            // Current stack - 1-userdata, 2-key, 3-value
            if(!LuaInterop.lua_isstring(p_state, 2)) // Not a string as key
                return 0;
            string l_key = LuaInterop.lua_tostring(p_state, 2);

            LuaInterop.luaL_getmetafield(p_state, 1, c_propSet); // table on top
            if(LuaInterop.lua_getfield(p_state, -1, l_key) == LuaInterop.LUA_TFUNCTION)
            {
                // function is on top
                LuaInterop.lua_pushvalue(p_state, 1); // copy userdata on top
                LuaInterop.lua_pushvalue(p_state, 3); // copy value on top
                LuaInterop.lua_call(p_state, 2, 0); // call, no result should return
                LuaInterop.lua_pop(p_state, 1); // remove table from stack
                return 0;
            }

            // Nothing found
            LuaInterop.lua_pop(p_state, 2); // remove table and value from stack
            return 0;
        }

        static int NilResultFunction(IntPtr p_state)
        {
            LuaInterop.lua_pushnil(p_state);
            return 1;
        }
    }
}
