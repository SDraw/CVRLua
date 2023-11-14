using ABI_RC.Core.Player;
using ABI_RC.Core.Savior;
using System.Collections.Generic;
using UnityEngine;
using ABI_RC.Systems.GameEventSystem;
using System;
using ABI_RC.Systems.MovementSystem;

namespace CVRLua.Players
{
    static class PlayersManager
    {
        static readonly Dictionary<PlayerDescriptor, Player> ms_remotePlayers = new Dictionary<PlayerDescriptor, Player>();
        static readonly Player ms_localPlayer = new Player();

        static internal Action<Player> PlayerJoin;
        static internal Action<Player> PlayerLeft;

        internal static void Init()
        {
            CVRGameEventSystem.Player.OnJoin.AddListener(OnPlayerJoin);
            CVRGameEventSystem.Player.OnLeave.AddListener(OnPlayerLeave);
        }

        static void OnPlayerJoin(PlayerDescriptor p_descriptor)
        {
            try
            {
                if(!ms_remotePlayers.ContainsKey(p_descriptor))
                {
                    Player l_remotePlayer = new Player(p_descriptor);
                    ms_remotePlayers.Add(p_descriptor, l_remotePlayer);
                    PlayerJoin?.Invoke(l_remotePlayer);
                }
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Warning(e);
            }
        }

        static void OnPlayerLeave(PlayerDescriptor p_descriptor)
        {
            try
            {
                if(ms_remotePlayers.TryGetValue(p_descriptor, out Player l_remotePlayer))
                {
                    PlayerLeft?.Invoke(l_remotePlayer);
                    ms_remotePlayers.Remove(p_descriptor);
                }
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Warning(e);
            }
        }

        public static Player GetLocalPlayer() => ms_localPlayer;

        public static List<Player> GetRemotePlayers()
        {
            var l_list = new List<Player>();
            foreach(var l_pair in ms_remotePlayers)
                l_list.Add(l_pair.Value);
            return l_list;
        }

        public static List<Player> GetAllPlayers()
        {
            var l_list = new List<Player>();
            l_list.Add(ms_localPlayer);
            foreach(var l_pair in ms_remotePlayers)
                l_list.Add(l_pair.Value);
            return l_list;
        }

        public static Player GetFromId(string p_id)
        {
            if(MetaPort.Instance.ownerId == p_id)
                return ms_localPlayer;

            foreach(var l_remotePlayer in ms_remotePlayers)
            {
                if(l_remotePlayer.Key.ownerId == p_id)
                    return l_remotePlayer.Value;
            }
            return null;
        }

        public static Player GetFromGameObject(GameObject p_obj)
        {
            Player l_result = null;
            Transform l_root = p_obj.transform.root;
            if(l_root == PlayerSetup.Instance.transform || l_root == MovementSystem.Instance.proxyCollider.transform || l_root == MovementSystem.Instance.forceCollider.transform)
                l_result = ms_localPlayer;
            else
            {
                PlayerDescriptor l_descriptor = l_root.GetComponent<PlayerDescriptor>();
                if(l_descriptor != null)
                    ms_remotePlayers.TryGetValue(l_descriptor, out l_result);
            }
            return l_result;
        }
    }
}
