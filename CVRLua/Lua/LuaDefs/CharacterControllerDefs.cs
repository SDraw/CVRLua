using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class CharacterControllerDefs
    {
        const string c_destroyed = "CharacterController is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsCharacterController), IsCharacterController));

            ms_instanceProperties.Add(("center", (GetCenter, SetCenter)));
            ms_instanceProperties.Add(("collisionFlags", (GetCollisionFlags, null)));
            ms_instanceProperties.Add(("detectCollisions", (GetDetectCollisions, SetDetectCollisions)));
            ms_instanceProperties.Add(("enableOverlapRecovery", (GetEnableOverlapRecovery, SetEnableOverlapRecovery)));
            ms_instanceProperties.Add(("height", (GetHeight, SetHeight)));
            ms_instanceProperties.Add(("isGrounded", (GetIsGrounded, null)));
            ms_instanceProperties.Add(("minMoveDistance", (GetMinMoveDistance, SetMinMoveDistance)));
            ms_instanceProperties.Add(("radius", (GetRadius, SetRadius)));
            ms_instanceProperties.Add(("skinWidth", (GetSkinWidth, SetSkinWidth)));
            ms_instanceProperties.Add(("slopeLimit", (GetSlopeLimit, SetSlopeLimit)));
            ms_instanceProperties.Add(("stepOffset", (GetStepOffset, SetStepOffset)));
            ms_instanceProperties.Add(("velocity", (GetVelocity, null)));

            ms_instanceMethods.Add((nameof(Move), Move));
            ms_instanceMethods.Add((nameof(SimpleMove), SimpleMove));

            ColliderDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(CharacterController), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
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
        static int GetCenter(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_col.center));
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
        static int SetCenter(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            Wrappers.Vector3 l_center = null;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadObject(ref l_center);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.center = l_center.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCollisionFlags(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushString(l_col.collisionFlags.ToString());
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

        static int GetDetectCollisions(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushBoolean(l_col.detectCollisions);
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
        static int SetDetectCollisions(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.detectCollisions = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetEnableOverlapRecovery(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushBoolean(l_col.enableOverlapRecovery);
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
        static int SetEnableOverlapRecovery(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.enableOverlapRecovery = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetHeight(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.height);
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
        static int SetHeight(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.height = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIsGrounded(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushBoolean(l_col.isGrounded);
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

        static int GetMinMoveDistance(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.minMoveDistance);
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
        static int SetMinMoveDistance(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.minMoveDistance = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRadius(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.radius);
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
        static int SetRadius(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.radius = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSkinWidth(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.skinWidth);
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
        static int SetSkinWidth(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.skinWidth = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetStepOffset(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.stepOffset);
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
        static int SetStepOffset(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.stepOffset = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSlopeLimit(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.slopeLimit);
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
        static int SetSlopeLimit(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.slopeLimit = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetVelocity(IntPtr p_state)
        {
            LuaArgReader l_argReader = new LuaArgReader(p_state);
            CharacterController l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_col.velocity));
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
    }
}
