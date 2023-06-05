using ABI.CCK.Components;
using ABI_RC.Core.InteractionSystem;
using ABI_RC.Core.Player;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CVRLua
{
    public class Core : MelonLoader.MelonMod
    {
        public const int c_modRelease = 21;

        static public Core Instance { get; private set; } = null;

        readonly List<LuaScript> m_scripts = null;

        internal Core()
        {
            m_scripts = new List<LuaScript>();
        }

        public override void OnInitializeMelon()
        {
            if(Instance == null)
                Instance = this;

            LibrariesHandler.ExtractDependencies();
            LuaHandler.Init();

            // Patches
            // Interactable
            HarmonyInstance.Patch(typeof(CVRInteractable).GetMethod(nameof(CVRInteractable.Grab)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Core).GetMethod(nameof(OnInteractableGrab_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );
            HarmonyInstance.Patch(typeof(CVRInteractable).GetMethod(nameof(CVRInteractable.Drop)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Core).GetMethod(nameof(OnInteractableDrop_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );
            HarmonyInstance.Patch(typeof(CVRInteractable).GetMethod(nameof(CVRInteractable.InteractUp)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Core).GetMethod(nameof(OnInteractableUp_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );
            HarmonyInstance.Patch(typeof(CVRInteractable).GetMethod(nameof(CVRInteractable.InteractDown)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Core).GetMethod(nameof(OnInteractableDown_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );
            HarmonyInstance.Patch(typeof(CVRInteractable).GetMethod(nameof(CVRInteractable.OnGazeEnter)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Core).GetMethod(nameof(OnInteractableGazeEnter_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );
            HarmonyInstance.Patch(typeof(CVRInteractable).GetMethod(nameof(CVRInteractable.OnGazeExit)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Core).GetMethod(nameof(OnInteractableGazeExit_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );

            // PuppetMaster
            HarmonyInstance.Patch(
                typeof(PuppetMaster).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Core).GetMethod(nameof(OnPuppetMasterStart_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );

            SceneManager.sceneLoaded += this.OnSceneWasLoaded;
        }

        public override void OnDeinitializeMelon()
        {
            if(Instance == this)
                Instance = null;
        }

        // Scrips register and removal
        internal void RegisterScript(LuaScript p_script)
        {
            m_scripts.Add(p_script);
        }

        void OnSceneWasLoaded(Scene p_scene, LoadSceneMode p_mode)
        {
            while(true)
            {
                int l_index = m_scripts.FindIndex(p => (p == null));
                if(l_index != -1)
                {
                    m_scripts[l_index].Dispose();
                    m_scripts.RemoveAt(l_index);
                }
                else
                    break;
            }
        }

        // Patches
        static void OnInteractableGrab_Postfix(ref CVRInteractable __instance) => Instance?.OnInteractableGrab(__instance);
        void OnInteractableGrab(CVRInteractable p_interact)
        {
            try
            {
                if(CVR_InteractableManager.enableInteractions)
                {
                    foreach(var l_script in m_scripts)
                        l_script.OnInteractableGrab(p_interact);
                }
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }

        static void OnInteractableDrop_Postfix(ref CVRInteractable __instance) => Instance?.OnInteractableDrop(__instance);
        void OnInteractableDrop(CVRInteractable p_interact)
        {
            try
            {
                if(CVR_InteractableManager.enableInteractions)
                {
                    foreach(var l_script in m_scripts)
                        l_script.OnInteractableDrop(p_interact);
                }
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }

        static void OnInteractableUp_Postfix(ref CVRInteractable __instance) => Instance?.OnInteractableUp(__instance);
        void OnInteractableUp(CVRInteractable p_interact)
        {
            try
            {
                if(CVR_InteractableManager.enableInteractions)
                {
                    foreach(var l_script in m_scripts)
                        l_script.OnInteractableUp(p_interact);
                }
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }

        static void OnInteractableDown_Postfix(ref CVRInteractable __instance) => Instance?.OnInteractableDown(__instance);
        void OnInteractableDown(CVRInteractable p_interact)
        {
            try
            {
                if(CVR_InteractableManager.enableInteractions)
                {
                    foreach(var l_script in m_scripts)
                        l_script.OnInteractableDown(p_interact);
                }
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }

        static void OnInteractableGazeEnter_Postfix(ref CVRInteractable __instance) => Instance?.OnInteractableGazeEnter(__instance);
        void OnInteractableGazeEnter(CVRInteractable p_interact)
        {
            try
            {
                foreach(var l_script in m_scripts)
                    l_script.OnInteractableGazeEnter(p_interact);
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }

        static void OnInteractableGazeExit_Postfix(ref CVRInteractable __instance) => Instance?.OnInteractableGazeExit(__instance);
        void OnInteractableGazeExit(CVRInteractable p_interact)
        {
            try
            {
                foreach(var l_script in m_scripts)
                    l_script.OnInteractableGazeExit(p_interact);
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }

        // Playeres managment
        static void OnPuppetMasterStart_Postfix(ref PuppetMaster __instance) => Instance?.OnPuppetMasterStart(__instance.gameObject);
        void OnPuppetMasterStart(GameObject p_obj)
        {
            try
            {
                p_obj.AddComponent<DestructionDetector>().Detection += this.OnPuppetMasterDestroy;
                Players.Player l_player = Players.PlayersManager.AddPlayer(p_obj);
                foreach(var l_script in m_scripts)
                    l_script.OnPlayerJoin(l_player);
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }

        internal void OnPuppetMasterDestroy(GameObject p_obj)
        {
            try
            {
                Players.Player l_player = Players.PlayersManager.GetFromGameObject(p_obj);
                if(l_player != null)
                {
                    foreach(var l_script in m_scripts)
                        l_script.OnPlayerLeft(l_player);

                    Players.PlayersManager.RemovePlayerByGameObject(p_obj);
                }
            }
            catch(Exception e)
            {
                MelonLoader.MelonLogger.Error(e);
            }
        }
    }
}
