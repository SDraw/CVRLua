using System;
using System.Collections.Generic;
using UnityEngine.AI;

namespace CVRLua.Lua.LuaDefs
{
    static class NavMeshAgentDefs
    {
        const string c_destroyed = "NavMeshAgent is destroyed";

        static readonly List<(string, LuaInterop.lua_CFunction)> ms_metaMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_staticProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_staticMethods = new List<(string, LuaInterop.lua_CFunction)>();
        static readonly List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))> ms_instanceProperties = new List<(string, (LuaInterop.lua_CFunction, LuaInterop.lua_CFunction))>();
        static readonly List<(string, LuaInterop.lua_CFunction)> ms_instanceMethods = new List<(string, LuaInterop.lua_CFunction)>();

        internal static void Init()
        {
            ms_staticMethods.Add((nameof(IsNavMeshAgent), IsNavMeshAgent));

            ms_instanceProperties.Add(("acceleration", (GetAcceleration, SetAcceleration)));
            ms_instanceProperties.Add(("agentTypeID", (GetAgentTypeID, SetAgentTypeID)));
            ms_instanceProperties.Add(("angularSpeed", (GetAngularSpeed, SetAngularSpeed)));
            ms_instanceProperties.Add(("areaMask", (GetAreaMask, SetAreaMask)));
            ms_instanceProperties.Add(("autoBraking", (GetAutoBraking, SetAutoBraking)));
            ms_instanceProperties.Add(("autoRepath", (GetAutoRepath, SetAutoRepath)));
            ms_instanceProperties.Add(("autoTraverseOffMeshLink", (GetAutoTraverseOffMeshLink, SetAutoTraverseOffMeshLink)));
            ms_instanceProperties.Add(("avoidancePriority", (GetAvoidancePriority, SetAvoidancePriority)));
            ms_instanceProperties.Add(("baseOffset", (GetBaseOffset, SetBaseOffset)));
            ms_instanceProperties.Add(("currentOffMeshLinkData", (GetCurrentOffMeshLinkData, null)));
            ms_instanceProperties.Add(("desiredVelocity", (GetDesiredVelocity, null)));
            ms_instanceProperties.Add(("destination", (GetDestination, SetDestinationProp)));
            ms_instanceProperties.Add(("hasPath", (GetHasPath, null)));
            ms_instanceProperties.Add(("height", (GetHeight, SetHeight)));
            ms_instanceProperties.Add(("isOnNavMesh", (GetIsOnNavMesh, null)));
            ms_instanceProperties.Add(("isOnOffMeshLink", (GetIsOnOffMeshLink, null)));
            ms_instanceProperties.Add(("isPathStale", (GetIsPathStale, null)));
            ms_instanceProperties.Add(("isStopped", (GetIsStopped, SetIsStopped)));
            //ms_instanceProperties.Add(("navMeshOwner", (?, ?))); // It's Unity.Object, but what exactly?
            ms_instanceProperties.Add(("nextOffMeshLinkData", (GetNextOffMeshLinkData, null)));
            ms_instanceProperties.Add(("nextPosition", (GetNextPosition, SetNextPosition)));
            ms_instanceProperties.Add(("obstacleAvoidanceType", (GetObstacleAvoidanceType, SetObstacleAvoidanceType)));
            ms_instanceProperties.Add(("path", (GetPath, SetPathProp)));
            ms_instanceProperties.Add(("pathPending", (GetPathPending, null)));
            ms_instanceProperties.Add(("pathStatus", (GetPathStatus, null)));
            ms_instanceProperties.Add(("radius", (GetRadius, SetRadius)));
            ms_instanceProperties.Add(("remainingDistance", (GetRemainingDistance, null)));
            ms_instanceProperties.Add(("speed", (GetSpeed, SetSpeed)));
            ms_instanceProperties.Add(("steeringTarget", (GetSteeringTarget, null)));
            ms_instanceProperties.Add(("stoppingDistance", (GetStoppingDistance, SetStoppingDistance)));
            ms_instanceProperties.Add(("updatePosition", (GetUpdatePosition, SetUpdatePosition)));
            ms_instanceProperties.Add(("updateRotation", (GetUpdateRotation, SetUpdateRotation)));
            ms_instanceProperties.Add(("updateUpAxis", (GetUpdateUpAxis, SetUpdateUpAxis)));
            ms_instanceProperties.Add(("velocity", (GetVelocity, SetVelocity)));

            ms_instanceMethods.Add((nameof(ActivateCurrentOffMeshLink), ActivateCurrentOffMeshLink));
            ms_instanceMethods.Add((nameof(CalculatePath), CalculatePath));
            ms_instanceMethods.Add((nameof(CompleteOffMeshLink), CompleteOffMeshLink));
            ms_instanceMethods.Add((nameof(FindClosestEdge), FindClosestEdge));
            ms_instanceMethods.Add((nameof(GetAreaCost), GetAreaCost));
            ms_instanceMethods.Add((nameof(Move), Move));
            ms_instanceMethods.Add((nameof(Raycast), Raycast));
            ms_instanceMethods.Add((nameof(ResetPath), ResetPath));
            ms_instanceMethods.Add((nameof(SamplePathPosition), SamplePathPosition));
            ms_instanceMethods.Add((nameof(SetAreaCost), SetAreaCost));
            ms_instanceMethods.Add((nameof(SetDestination), SetDestination));
            ms_instanceMethods.Add((nameof(SetPath), SetPath));
            ms_instanceMethods.Add((nameof(Warp), Warp));

            BehaviourDefs.InheritTo(ms_metaMethods, ms_staticProperties, ms_staticMethods, ms_instanceProperties, ms_instanceMethods);
        }

        internal static void RegisterInVM(LuaVM p_vm)
        {
            p_vm.RegisterClass(typeof(NavMeshAgent), null, ms_staticProperties, ms_staticMethods, ms_metaMethods, ms_instanceProperties, ms_instanceMethods);
        }

        // Static methods
        static int IsNavMeshAgent(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadNextObject(ref l_argReader);
            l_argReader.PushBoolean(l_agent != null);
            return l_argReader.GetReturnValue();
        }

        // Instance properties
        static int GetAcceleration(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushNumber(l_agent.acceleration);
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
        static int SetAcceleration(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.acceleration = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAgentTypeID(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushInteger(l_agent.agentTypeID);
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
        static int SetAgentTypeID(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.agentTypeID = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAngularSpeed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushNumber(l_agent.angularSpeed);
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
        static int SetAngularSpeed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.angularSpeed = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAreaMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushInteger(l_agent.areaMask);
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
        static int SetAreaMask(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.areaMask = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAutoBraking(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.autoBraking);
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
        static int SetAutoBraking(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.autoBraking = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAutoRepath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.autoRepath);
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
        static int SetAutoRepath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.autoRepath = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAutoTraverseOffMeshLink(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.autoTraverseOffMeshLink);
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
        static int SetAutoTraverseOffMeshLink(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.autoTraverseOffMeshLink = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetAvoidancePriority(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushInteger(l_agent.avoidancePriority);
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
        static int SetAvoidancePriority(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            int l_value = 0;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadInteger(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.avoidancePriority = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetBaseOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushNumber(l_agent.baseOffset);
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
        static int SetBaseOffset(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.baseOffset = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetCurrentOffMeshLinkData(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushObject(new Wrappers.OffMeshLinkData(l_agent.currentOffMeshLinkData));
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

        static int GetDesiredVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_agent.desiredVelocity));
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

        static int GetDestination(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_agent.destination));
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
        static int SetDestinationProp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.destination = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetHasPath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.hasPath);
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

        static int GetHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushNumber(l_agent.height);
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
        static int SetHeight(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.height = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetIsOnNavMesh(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.isOnNavMesh);
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

        static int GetIsOnOffMeshLink(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.isOnOffMeshLink);
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

        static int GetIsPathStale(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.isPathStale);
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

        static int GetIsStopped(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.isStopped);
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
        static int SetIsStopped(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.isStopped = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetNextOffMeshLinkData(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushObject(new Wrappers.OffMeshLinkData(l_agent.nextOffMeshLinkData));
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

        static int GetNextPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_agent.nextPosition));
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
        static int SetNextPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.nextPosition = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetObstacleAvoidanceType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushString(l_agent.obstacleAvoidanceType.ToString());
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
        static int SetObstacleAvoidanceType(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            ObstacleAvoidanceType l_value = ObstacleAvoidanceType.NoObstacleAvoidance;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadEnum(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.obstacleAvoidanceType = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                {
                    if(l_agent.path != null)
                        l_argReader.PushObject(l_agent.path);
                    else
                        l_argReader.PushBoolean(false);
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
        static int SetPathProp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            NavMeshPath l_path = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadNextObject(ref l_path);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.path = l_path;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetPathPending(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.pathPending);
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

        static int GetPathStatus(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushString(l_agent.pathStatus.ToString());
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

        static int GetRadius(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushNumber(l_agent.radius);
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
            NavMeshAgent l_agent = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.radius = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetRemainingDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushNumber(l_agent.remainingDistance);
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

        static int GetSpeed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushNumber(l_agent.speed);
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
        static int SetSpeed(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.speed = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetSteeringTarget(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_agent.steeringTarget));
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

        static int GetStoppingDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushNumber(l_agent.stoppingDistance);
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
        static int SetStoppingDistance(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.stoppingDistance = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUpdatePosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.updatePosition);
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
        static int SetUpdatePosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.updatePosition = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUpdateRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.updateRotation);
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
        static int SetUpdateRotation(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.updateRotation = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetUpdateUpAxis(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.updateUpAxis);
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
        static int SetUpdateUpAxis(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            bool l_value = false;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadBoolean(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.updateUpAxis = l_value;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        static int GetVelocity(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushObject(new Wrappers.Vector3(l_agent.velocity));
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
            NavMeshAgent l_agent = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_agent.velocity = l_value.m_vec;
                else
                    l_argReader.SetError(c_destroyed);
            }

            l_argReader.LogError();
            return 0;
        }

        // Instance methods
        static int ActivateCurrentOffMeshLink(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            bool l_state = false;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadBoolean(ref l_state);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                {
                    l_agent.ActivateCurrentOffMeshLink(l_state);
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

        static int CalculatePath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            Wrappers.Vector3 l_pos = null;
            NavMeshPath l_path = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadObject(ref l_pos);
            l_argReader.ReadObject(ref l_path);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.CalculatePath(l_pos.m_vec, l_path));
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

        static int CompleteOffMeshLink(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                {
                    l_agent.CompleteOffMeshLink();
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

        static int FindClosestEdge(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                {
                    if(l_agent.FindClosestEdge(out var l_hit))
                        l_argReader.PushObject(new Wrappers.NavMeshHit(l_hit));
                    else
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

        static int GetAreaCost(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            int l_index = 0;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadInteger(ref l_index);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushNumber(l_agent.GetAreaCost(l_index));
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

        static int Move(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            Wrappers.Vector3 l_offset = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadObject(ref l_offset);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                {
                    l_agent.Move(l_offset.m_vec);
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

        static int Raycast(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                {
                    if(l_agent.Raycast(l_pos.m_vec, out var l_hit))
                        l_argReader.PushObject(new Wrappers.NavMeshHit(l_hit));
                    else
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

        static int ResetPath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            l_argReader.ReadObject(ref l_agent);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                {
                    l_agent.ResetPath();
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

        static int SamplePathPosition(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            int l_mask = 0;
            float l_distance = 0f;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadInteger(ref l_mask);
            l_argReader.ReadNumber(ref l_distance);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                {
                    if(l_agent.SamplePathPosition(l_mask, l_distance, out var l_hit))
                        l_argReader.PushObject(new Wrappers.NavMeshHit(l_hit));
                    else
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

        static int SetAreaCost(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            int l_index = 0;
            float l_value = 0f;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadInteger(ref l_index);
            l_argReader.ReadNumber(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                {
                    l_agent.SetAreaCost(l_index, l_value);
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

        static int SetDestination(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            Wrappers.Vector3 l_value = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadObject(ref l_value);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.SetDestination(l_value.m_vec));
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

        static int SetPath(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            NavMeshPath l_path = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadObject(ref l_path);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.SetPath(l_path));
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

        static int Warp(IntPtr p_state)
        {
            var l_argReader = new LuaArgReader(p_state);
            NavMeshAgent l_agent = null;
            Wrappers.Vector3 l_pos = null;
            l_argReader.ReadObject(ref l_agent);
            l_argReader.ReadObject(ref l_pos);
            if(!l_argReader.HasErrors())
            {
                if(l_agent != null)
                    l_argReader.PushBoolean(l_agent.Warp(l_pos.m_vec));
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
