using ABI_RC.Core.Player;
using ABI_RC.Core.Savior;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CVRLua.Players
{
    static class PlayersManager
    {
        static readonly Dictionary<GameObject, Player> ms_players = new Dictionary<GameObject, Player>();
        static readonly Player ms_localPlayer = new Player();

        public static Player GetLocalPlayer() => ms_localPlayer;

        public static List<Player> GetRemotePlayers()
        {
            var l_list = new List<Player>();
            foreach(var l_pair in ms_players)
                l_list.Add(l_pair.Value);
            return l_list;
        }

        public static List<Player> GetAllPlayers()
        {
            var l_list = new List<Player>();
            l_list.Add(ms_localPlayer);
            foreach(var l_pair in ms_players)
                l_list.Add(l_pair.Value);
            return l_list;
        }

        public static Player GetFromId(string p_id)
        {
            if(MetaPort.Instance.ownerId == p_id)
                return ms_localPlayer;

            if(CVRPlayerManager.Instance.GetPlayerPuppetMaster(p_id, out var l_puppet))
            {
                var l_searchPair = ms_players.FirstOrDefault(p => (p.Key == l_puppet.gameObject));
                if(l_searchPair.Key != null)
                    return l_searchPair.Value;
            }
            return null;
        }

        public static Player GetFromGameObject(GameObject p_obj)
        {
            Player l_result = null;
            if(!ms_players.TryGetValue(p_obj, out l_result) && (p_obj == PlayerSetup.Instance.gameObject))
                l_result = ms_localPlayer;
            return l_result;
        }

        // Core call only
        internal static Player AddPlayer(GameObject p_obj)
        {
            Player l_result = null;
            if(!ms_players.TryGetValue(p_obj, out l_result))
            {
                l_result = new Player(p_obj);
                ms_players.Add(p_obj, l_result);
            }
            return l_result;
        }

        // Core call only
        internal static void RemovePlayerByGameObject(GameObject p_obj)
        {
            if(ms_players.ContainsKey(p_obj))
                ms_players.Remove(p_obj);
        }
    }
}
