using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class CollisionDefs
    {
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_metaMethods.Add(("__eq", Equal));
            ms_metaMethods.Add(("__tostring", ToString));

            ms_instanceProperties.Add(("collider", (GetCollider, null)));
            ms_instanceProperties.Add(("contactCount", (GetContactCount, null)));
            ms_instanceProperties.Add(("impulse", (GetImpulse, null)));
            ms_instanceProperties.Add(("relativeVelocity", (GetRelativeVelocity, null)));

            ms_instanceMethods.Add((nameof(GetContact), GetContact));
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Collision), Create, null, null, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
            p_vm.RegisterFunction(nameof(IsCollision), IsCollision);
        }

        // Ctor
        static int Create(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            l_argReader.PushObject(new Collision());
            return l_argReader.GetReturnValue();
        }

        // Static methods
        static int IsCollision(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_colA = null;
            l_argReader.ReadNextObject(ref l_colA);
            l_argReader.PushBoolean(l_colA != null);
            return l_argReader.GetReturnValue();
        }

        // Metamethods
        static int Equal(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_colA = null;
            Collision l_colB = null;
            l_argReader.ReadObject(ref l_colA);
            l_argReader.ReadObject(ref l_colB);
            if(!l_argReader.HasErrors())
                l_argReader.PushBoolean(l_colA == l_colB);
            else
                l_argReader.PushBoolean(false);

            return l_argReader.GetReturnValue();
        }

        static int ToString(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
                l_argReader.PushString(l_col.ToString());
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetCollider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(l_col.collider);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetContactCount(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
                l_argReader.PushInteger(l_col.contactCount);
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetImpulse(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_col.impulse));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetRelativeVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
                l_argReader.PushObject(new Wrappers.Vector3(l_col.relativeVelocity));
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        // Instance methods
        static int GetContact(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Collision l_col = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_index < l_col.contactCount)
                    l_argReader.PushObject(new Wrappers.ContactPoint(l_col.GetContact(l_index)));
                else
                    l_argReader.PushBoolean(false);
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return l_argReader.GetReturnValue();
        }
    }
}
