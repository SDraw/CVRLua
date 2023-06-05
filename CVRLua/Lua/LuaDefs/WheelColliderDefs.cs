using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class WheelColliderDefs
    {
        const string c_destroyed = "WheelCollider is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsWheelCollider), IsWheelCollider));

            ms_instanceProperties.Add(("brakeTorque", (GetBrakeTorque, SetBrakeTorque)));
            ms_instanceProperties.Add(("center", (GetCenter, SetCenter)));
            ms_instanceProperties.Add(("forceAppPointDistance", (GetForceAppPointDistance, SetForceAppPointDistance)));
            //ms_instanceProperties.Add(("forwardFriction", (GetForwardFriction, SetForwardFriction)));
            ms_instanceProperties.Add(("isGrounded", (GetIsGrounded, null)));
            ms_instanceProperties.Add(("mass", (GetMass, SetMass)));
            ms_instanceProperties.Add(("motorTorque", (GetMotorTorque, SetMotorTorque)));
            ms_instanceProperties.Add(("radius", (GetRadius, SetRadius)));
            ms_instanceProperties.Add(("rpm", (GetRpm, null)));
            //ms_instanceProperties.Add(("sidewaysFriction", (GetSidewaysFriction, SetSidewaysFriction)));
            ms_instanceProperties.Add(("sprungMass", (GetSprungMass, SetSprungMass)));
            ms_instanceProperties.Add(("steerAngle", (GetSteerAngle, SetSteerAngle)));
            ms_instanceProperties.Add(("suspensionDistance", (GetSuspensionDistance, SetSuspensionDistance)));
            //ms_instanceProperties.Add(("suspensionSpring", (GetSuspensionSpring, SetSuspensionSpring)));
            ms_instanceProperties.Add(("wheelDampingRate", (GetWheelDampingRate, SetWheelDampingRate)));

            ms_instanceMethods.Add((nameof(ConfigureVehicleSubsteps), ConfigureVehicleSubsteps));
            //ms_instanceMethods.Add((nameof(GetGroundHit), GetGroundHit));
            ms_instanceMethods.Add((nameof(GetWorldPose), GetWorldPose));
            ms_instanceMethods.Add((nameof(ResetSprungMasses), ResetSprungMasses));

            ColliderDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(WheelCollider), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsWheelCollider(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadNextObject(ref l_col);
            l_argReader.PushBoolean(l_col != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetBrakeTorque(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.brakeTorque);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetBrakeTorque(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.brakeTorque = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCenter(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_col.center));
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetCenter(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.center = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetForceAppPointDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.forceAppPointDistance);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetForceAppPointDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.forceAppPointDistance = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIsGrounded(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushBoolean(l_col.isGrounded);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetMass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.mass);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetMass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.mass = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMotorTorque(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.motorTorque);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetMotorTorque(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.motorTorque = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRadius(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.radius);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetRadius(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
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

        static int GetRpm(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.rpm);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }

        static int GetSprungMass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.sprungMass);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetSprungMass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.sprungMass = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSteerAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.steerAngle);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetSteerAngle(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.steerAngle = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSuspensionDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.suspensionDistance);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetSuspensionDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.suspensionDistance = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetWheelDampingRate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_argReader.PushNumber(l_col.wheelDampingRate);
                else
                {
                    l_argReader.SetError(c_destroyed);
                    l_argReader.PushBoolean(false);
                }
            }
            else
                l_argReader.PushBoolean(false);

            l_argReader.LogError();
            return 1;
        }
        static int SetWheelDampingRate(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                    l_col.wheelDampingRate = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int ConfigureVehicleSubsteps(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            float l_speed = 0f;
            int l_below = 0;
            int l_above = 0;
            l_argReader.ReadObject(ref l_col);
            l_argReader.ReadNumber(ref l_speed);
            l_argReader.ReadInteger(ref l_below);
            l_argReader.ReadInteger(ref l_above);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                {
                    l_col.ConfigureVehicleSubsteps(l_speed, l_below, l_above);
                    l_argReader.PushBoolean(true);
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
            return 1;
        }

        static int GetWorldPose(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                {
                    l_col.GetWorldPose(out var l_pos, out var l_rot);
                    l_argReader.PushObject(new Wrappers.Vector3(l_pos));
                    l_argReader.PushObject(new Wrappers.Quaternion(l_rot));
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
            return 1;
        }

        static int ResetSprungMasses(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            WheelCollider l_col = null;
            l_argReader.ReadObject(ref l_col);
            if(!l_argReader.HasErrors())
            {
                if(l_col != null)
                {
                    l_col.ResetSprungMasses();
                    l_argReader.PushBoolean(true);
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
            return 1;
        }
    }
}
