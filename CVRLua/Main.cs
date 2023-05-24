using ABI.CCK.Components;
using ABI_RC.Core.InteractionSystem;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CVRLua
{
    public class Core : MelonLoader.MelonMod
    {
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

            LuaHandler.Init();
        }

        public override void OnDeinitializeMelon()
        {
            if(Instance == this)
                Instance = null;
        }

        internal void RegisterScript(LuaScript p_script)
        {
            m_scripts.Add(p_script);
        }

        internal void UnregisterScript(LuaScript p_script)
        {
            m_scripts.Remove(p_script);
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
    }
}
