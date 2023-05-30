using System;
using System.Collections.Generic;
using UnityEngine;

namespace CVRLua.Lua.LuaDefs
{
    static class RigidbodyDefs
    {
        const string c_destroyed = "Rigidbody is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsRigidbody), IsRigidbody));

            ms_instanceProperties.Add(("angularDrag", (GetAngularDrag, SetAngularDrag)));
            ms_instanceProperties.Add(("angularVelocity", (GetAngularVelocity, SetAngularVelocity)));
            ms_instanceProperties.Add(("centerOfMass", (GetCenterOfMass, SetCenterOfMass)));
            ms_instanceProperties.Add(("collisionDetectionMode", (GetCollisionDetectionMode, SetCollisionDetectionMode)));
            ms_instanceProperties.Add(("constraints", (GetConstraints, SetConstraints)));
            ms_instanceProperties.Add(("detectCollisions", (GetDetectCollisions, SetDetectCollisions)));
            ms_instanceProperties.Add(("drag", (GetDrag, SetDrag)));
            ms_instanceProperties.Add(("freezeRotation", (GetFreezeRotation, SetFreezeRotation)));
            ms_instanceProperties.Add(("inertiaTensor", (GetInertiaTensor, SetInertiaTensor)));
            ms_instanceProperties.Add(("inertiaTensorRotation", (GetInertiaTensorRotation, SetInertiaTensorRotation)));
            ms_instanceProperties.Add(("interpolation", (GetInterpolation, SetInterpolation)));
            ms_instanceProperties.Add(("isKinematic", (GetIsKinematic, SetIsKinematic)));
            ms_instanceProperties.Add(("mass", (GetMass, SetMass)));
            ms_instanceProperties.Add(("maxAngularVelocity", (GetMaxAngularVelocity, SetMaxAngularVelocity)));
            ms_instanceProperties.Add(("maxDepenetrationVelocity", (GetMaxDepenetrationVelocity, SetMaxDepenetrationVelocity)));
            ms_instanceProperties.Add(("position", (GetPosition, SetPosition)));
            ms_instanceProperties.Add(("rotation", (GetRotation, SetRotation)));
            ms_instanceProperties.Add(("sleepThreshold", (GetSleepThreshold, SetSleepThreshold)));
            ms_instanceProperties.Add(("solverIterations", (GetSolverIterations, SetSolverIterations)));
            ms_instanceProperties.Add(("solverVelocityIterations", (GetSolverVelocityIterations, SetSolverVelocityIterations)));
            ms_instanceProperties.Add(("useGravity", (GetUseGravity, SetUseGravity)));
            ms_instanceProperties.Add(("velocity", (GetVelocity, SetVelocity)));
            ms_instanceProperties.Add(("worldCenterOfMass", (GetWorldCenterOfMass, null)));

            ms_instanceMethods.Add((nameof(AddExplosionForce), AddExplosionForce));
            ms_instanceMethods.Add((nameof(AddForce), AddForce));
            ms_instanceMethods.Add((nameof(AddForceAtPosition), AddForceAtPosition));
            ms_instanceMethods.Add((nameof(AddRelativeForce), AddRelativeForce));
            ms_instanceMethods.Add((nameof(AddRelativeTorque), AddRelativeTorque));
            ms_instanceMethods.Add((nameof(AddTorque), AddTorque));
            ms_instanceMethods.Add((nameof(ClosestPointOnBounds), ClosestPointOnBounds));
            ms_instanceMethods.Add((nameof(GetPointVelocity), GetPointVelocity));
            ms_instanceMethods.Add((nameof(GetRelativePointVelocity), GetRelativePointVelocity));
            ms_instanceMethods.Add((nameof(IsSleeping), IsSleeping));
            ms_instanceMethods.Add((nameof(MovePosition), MovePosition));
            ms_instanceMethods.Add((nameof(MoveRotation), MoveRotation));
            ms_instanceMethods.Add((nameof(ResetCenterOfMass), ResetCenterOfMass));
            ms_instanceMethods.Add((nameof(ResetInertiaTensor), ResetInertiaTensor));
            ms_instanceMethods.Add((nameof(SetDensity), SetDensity));
            ms_instanceMethods.Add((nameof(Sleep), Sleep));
            //ms_instanceMethods.Add((nameof(SweepTest), SweepTest));
            //ms_instanceMethods.Add((nameof(SweepTestAll), SweepTestAll));
            ms_instanceMethods.Add((nameof(WakeUp), WakeUp));

            ComponentDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(Rigidbody), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        static int IsRigidbody(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadNextObject(ref l_body);
            l_argReader.PushBoolean(l_body != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAngularDrag(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushNumber(l_body.angularDrag);
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
        static int SetAngularDrag(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.angularDrag = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAngularVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_body.angularVelocity));
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
        static int SetAngularVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.angularVelocity = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCenterOfMass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_body.centerOfMass));
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
        static int SetCenterOfMass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.centerOfMass = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCollisionDetectionMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushString(l_body.collisionDetectionMode.ToString());
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
        static int SetCollisionDetectionMode(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            CollisionDetectionMode l_mode = CollisionDetectionMode.Discrete;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.collisionDetectionMode = l_mode;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetConstraints(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushString(l_body.constraints.ToString());
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
        static int SetConstraints(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            RigidbodyConstraints l_constrtaints = RigidbodyConstraints.None;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadEnum(ref l_constrtaints);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.constraints = l_constrtaints;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDetectCollisions(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushBoolean(l_body.detectCollisions);
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
        static int SetDetectCollisions(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.detectCollisions = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetDrag(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushNumber(l_body.drag);
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
        static int SetDrag(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.drag = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetFreezeRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushBoolean(l_body.freezeRotation);
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
        static int SetFreezeRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.freezeRotation = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetInertiaTensor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_body.inertiaTensor));
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
        static int SetInertiaTensor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.inertiaTensor = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetInertiaTensorRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Quaternion(l_body.inertiaTensorRotation));
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
        static int SetInertiaTensorRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Quaternion l_quat = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_quat);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.inertiaTensorRotation = l_quat.m_quat;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetInterpolation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushString(l_body.interpolation.ToString());
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
        static int SetInterpolation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            RigidbodyInterpolation l_mode = RigidbodyInterpolation.None;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.interpolation = l_mode;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIsKinematic(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushBoolean(l_body.isKinematic);
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
        static int SetIsKinematic(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.isKinematic = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushNumber(l_body.mass);
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
            Rigidbody l_body = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.mass = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMaxAngularVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushNumber(l_body.maxAngularVelocity);
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
        static int SetMaxAngularVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.maxAngularVelocity = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetMaxDepenetrationVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushNumber(l_body.maxDepenetrationVelocity);
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
        static int SetMaxDepenetrationVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.maxDepenetrationVelocity = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_body.position));
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
        static int SetPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.position = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Quaternion(l_body.rotation));
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
        static int SetRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Quaternion l_quat = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_quat);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.rotation = l_quat.m_quat;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSleepThreshold(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushNumber(l_body.sleepThreshold);
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
        static int SetSleepThreshold(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.sleepThreshold = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSolverIterations(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushInteger(l_body.solverIterations);
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
        static int SetSolverIterations(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.solverIterations = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSolverVelocityIterations(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushInteger(l_body.solverVelocityIterations);
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
        static int SetSolverVelocityIterations(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.solverVelocityIterations = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUseGravity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushBoolean(l_body.useGravity);
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
        static int SetUseGravity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.useGravity = l_state;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_body.velocity));
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
        static int SetVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_vec = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_vec);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_body.velocity = l_vec.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetWorldCenterOfMass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_body.worldCenterOfMass));
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

        // Instance methods
        static int AddExplosionForce(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            float l_force = 0f;
            Wrappers.Vector3 l_pos = null;
            float l_radius = 0f;
            float l_upwards = 0f;
            ForceMode l_mode = ForceMode.Force;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadNumber(ref l_force);
            l_argReader.ReadObject(ref l_pos);
            l_argReader.ReadNumber(ref l_radius);
            l_argReader.ReadNextNumber(ref l_radius);
            l_argReader.ReadNextEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.AddExplosionForce(l_force, l_pos.m_vec, l_radius, l_upwards, l_mode);
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
            return l_argReader.GetReturnValue();
        }

        static int AddForce(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_force = null;
            ForceMode l_mode = ForceMode.Force;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_force);
            l_argReader.ReadNextEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.AddForce(l_force.m_vec, l_mode);
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
            return l_argReader.GetReturnValue();
        }

        static int AddForceAtPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_force = null;
            Wrappers.Vector3 l_pos = null;
            ForceMode l_mode = ForceMode.Force;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_force);
            l_argReader.ReadObject(ref l_pos);
            l_argReader.ReadNextEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.AddForceAtPosition(l_force.m_vec, l_pos.m_vec, l_mode);
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
            return l_argReader.GetReturnValue();
        }

        static int AddRelativeForce(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_force = null;
            ForceMode l_mode = ForceMode.Force;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_force);
            l_argReader.ReadNextEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.AddRelativeForce(l_force.m_vec, l_mode);
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
            return l_argReader.GetReturnValue();
        }

        static int AddRelativeTorque(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_torque = null;
            ForceMode l_mode = ForceMode.Force;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_torque);
            l_argReader.ReadNextEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.AddRelativeTorque(l_torque.m_vec, l_mode);
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
            return l_argReader.GetReturnValue();
        }

        static int AddTorque(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_torque = null;
            ForceMode l_mode = ForceMode.Force;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_torque);
            l_argReader.ReadNextEnum(ref l_mode);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.AddTorque(l_torque.m_vec, l_mode);
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
            return l_argReader.GetReturnValue();
        }

        static int ClosestPointOnBounds(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_body.ClosestPointOnBounds(l_pos.m_vec)));
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

        static int GetPointVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_body.GetPointVelocity(l_pos.m_vec)));
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

        static int GetRelativePointVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_body.GetRelativePointVelocity(l_pos.m_vec)));
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

        static int IsSleeping(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                    l_argReader.PushBoolean(l_body.IsSleeping());
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

        static int MovePosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.MovePosition(l_pos.m_vec);
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
            return l_argReader.GetReturnValue();
        }

        static int MoveRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            Wrappers.Quaternion l_quat = null;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadObject(ref l_quat);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.MoveRotation(l_quat.m_quat);
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
            return l_argReader.GetReturnValue();
        }

        static int ResetCenterOfMass(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.ResetCenterOfMass();
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
            return l_argReader.GetReturnValue();
        }

        static int ResetInertiaTensor(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.ResetInertiaTensor();
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
            return l_argReader.GetReturnValue();
        }

        static int SetDensity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_body);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.SetDensity(l_value);
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
            return l_argReader.GetReturnValue();
        }

        static int Sleep(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.Sleep();
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
            return l_argReader.GetReturnValue();
        }

        static int WakeUp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            Rigidbody l_body = null;
            l_argReader.ReadObject(ref l_body);
            if(!l_argReader.HasErrors())
            {
                if(l_body != null)
                {
                    l_body.WakeUp();
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
            return l_argReader.GetReturnValue();
        }
    }
}
