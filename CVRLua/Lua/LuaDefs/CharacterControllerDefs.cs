using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class CharacterControllerDefs
    {
        const string c_destroyed = "CharacterController is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly Dictionary<string, (StaticParseDelegate, StaticParseDelegate)> ms_staticProperties = new Dictionary<string, (StaticParseDelegate, StaticParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_staticMethods = new Dictionary<string, LuaInterop.lua_CFunction>();
        static readonly Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)> ms_instanceProperties = new Dictionary<string, (InstanceParseDelegate, InstanceParseDelegate)>();
        static readonly Dictionary<string, LuaInterop.lua_CFunction> ms_instanceMethods = new Dictionary<string, LuaInterop.lua_CFunction>();

        internal static void Init()
        {
            ms_staticMethods.Add(nameof(IsCharacterController), IsCharacterController);

            ms_instanceProperties.Add("center", (GetCenter, SetCenter));
            ms_instanceProperties.Add("collisionFlags", (GetCollisionFlags, null));
            ms_instanceProperties.Add("detectCollisions", (GetDetectCollisions, SetDetectCollisions));
            ms_instanceProperties.Add("enableOverlapRecovery", (GetEnableOverlapRecovery, SetEnableOverlapRecovery));
            ms_instanceProperties.Add("height", (GetHeight, SetHeight));
            ms_instanceProperties.Add("isGrounded", (GetIsGrounded, null));
            ms_instanceProperties.Add("minMoveDistance", (GetMinMoveDistance, SetMinMoveDistance));
            ms_instanceProperties.Add("radius", (GetRadius, SetRadius));
            ms_instanceProperties.Add("skinWidth", (GetSkinWidth, SetSkinWidth));
            ms_instanceProperties.Add("slopeLimit", (GetSlopeLimit, SetSlopeLimit));
            ms_instanceProperties.Add("stepOffset", (GetStepOffset, SetStepOffset));
            ms_instanceProperties.Add("velocity", (GetVelocity, null));

            ms_instanceMethods.Add(nameof(Move), Move);
            ms_instanceMethods.Add(nameof(SimpleMove), SimpleMove);

            ColliderDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CharacterController), null, ms_metaMethods, StaticGet, null, InstanceGet, InstanceSet);
        }

        // Static methods
        static int IsCharacterController(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadNextObject(ref l_col);
            l_argReader.PushBoolean(l_col != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static void GetCenter(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as CharacterController).center));
        }
        static void SetCenter(object p_obj, LuaArgReader p_reader)
        {
            Wrappers.Vector3 l_vec = null;
            p_reader.ReadObject(ref l_vec);
            if(!p_reader.HasErrors())
            {
                (p_obj as CharacterController).center = l_vec.m_vec;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetCollisionFlags(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject((p_obj as CharacterController).collisionFlags.ToString());
        }

        static void GetDetectCollisions(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as CharacterController).detectCollisions);
        }
        static void SetDetectCollisions(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
            {
                (p_obj as CharacterController).detectCollisions = l_state;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetEnableOverlapRecovery(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as CharacterController).enableOverlapRecovery);
        }
        static void SetEnableOverlapRecovery(object p_obj, LuaArgReader p_reader)
        {
            bool l_state = false;
            p_reader.ReadBoolean(ref l_state);
            if(!p_reader.HasErrors())
            {
                (p_obj as CharacterController).enableOverlapRecovery = l_state;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetHeight(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as CharacterController).height);
        }
        static void SetHeight(object p_obj, LuaArgReader p_reader)
        {
            float l_height = 0f;
            p_reader.ReadNumber(ref l_height);
            if(!p_reader.HasErrors())
            {
                (p_obj as CharacterController).height = l_height;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetIsGrounded(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushBoolean((p_obj as CharacterController).isGrounded);
        }

        static void GetMinMoveDistance(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as CharacterController).minMoveDistance);
        }
        static void SetMinMoveDistance(object p_obj, LuaArgReader p_reader)
        {
            float l_dist = 0f;
            p_reader.ReadNumber(ref l_dist);
            if(!p_reader.HasErrors())
            {
                (p_obj as CharacterController).minMoveDistance = l_dist;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetRadius(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as CharacterController).radius);
        }
        static void SetRadius(object p_obj, LuaArgReader p_reader)
        {
            float l_radius = 0f;
            p_reader.ReadNumber(ref l_radius);
            if(!p_reader.HasErrors())
            {
                (p_obj as CharacterController).radius = l_radius;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetSkinWidth(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as CharacterController).skinWidth);
        }
        static void SetSkinWidth(object p_obj, LuaArgReader p_reader)
        {
            float l_width = 0f;
            p_reader.ReadNumber(ref l_width);
            if(!p_reader.HasErrors())
            {
                (p_obj as CharacterController).skinWidth = l_width;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetStepOffset(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as CharacterController).stepOffset);
        }
        static void SetStepOffset(object p_obj, LuaArgReader p_reader)
        {
            float l_offset = 0f;
            p_reader.ReadNumber(ref l_offset);
            if(!p_reader.HasErrors())
            {
                (p_obj as CharacterController).stepOffset = l_offset;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetSlopeLimit(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushNumber((p_obj as CharacterController).slopeLimit);
        }
        static void SetSlopeLimit(object p_obj, LuaArgReader p_reader)
        {
            float l_limit = 0f;
            p_reader.ReadNumber(ref l_limit);
            if(!p_reader.HasErrors())
            {
                (p_obj as CharacterController).slopeLimit = l_limit;
                p_reader.PushBoolean(true);
            }
            else
                p_reader.PushBoolean(false);
        }

        static void GetVelocity(object p_obj, LuaArgReader p_reader)
        {
            p_reader.PushObject(new Wrappers.Vector3((p_obj as CharacterController).velocity));
        }

        // Instance methods
        static int Move(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                {
                    var l_flags = l_col.Move(l_vec.m_vec);
                    l_argReader.PushString(l_flags.ToString());
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

        static int SimpleMove(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushBoolean(l_col.SimpleMove(l_vec.m_vec));
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
            CharacterController l_obj = null;
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
            CharacterController l_obj = null;
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
