using ABI.CCK.Components;
using ABI_RC.Core.InteractionSystem;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CVRLua
{
    public class Core : MelonLoader.MelonMod
    {
        public const int c_modRelease = 35;

        static public Core Instance { get; private set; } = null;
        internal static MelonLoader.MelonLogger.Instance Logger = null;

        readonly List<LuaScript> m_scripts = null;

        internal Core()
        {
            m_scripts = new List<LuaScript>();
        }

        public override void OnInitializeMelon()
        {
            if(Instance == null)
            {
                Instance = this;
                Logger = this.LoggerInstance;
            }

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
            HarmonyInstance.Patch(typeof(CVRInteractable).GetMethod(nameof(CVRInteractable.HoverEnter)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Core).GetMethod(nameof(OnInteractableHoverEnter_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );
            HarmonyInstance.Patch(typeof(CVRInteractable).GetMethod(nameof(CVRInteractable.HoverExit)),
                null,
                new HarmonyLib.HarmonyMethod(typeof(Core).GetMethod(nameof(OnInteractableHoverExit_Postfix), BindingFlags.NonPublic | BindingFlags.Static))
            );

            (typeof(ABI_RC.Core.Util.AssetFiltering.WorldFilter).GetField("_Base", BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null) as HashSet<System.Type>)?.Add(typeof(LuaScript));

            Players.PlayersManager.Init();
        }

        public override void OnDeinitializeMelon()
        {
            if(Instance == this)
            {
                Instance = null;
                Logger = null;
            }
        }

        // Scrips register and removal
        internal void RegisterScript(LuaScript p_script)
        {
            m_scripts.Add(p_script);
        }

        internal void UnregisterScript(LuaScript p_script)
        {
            int l_index = m_scripts.FindIndex(p => ReferenceEquals(p, p_script));
            if(l_index != -1)
            {
                m_scripts[l_index].Dispose();
                m_scripts.RemoveAt(l_index);
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
                Logger?.Error(e);
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
                Logger?.Error(e);
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
                Logger?.Error(e);
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
                Logger?.Error(e);
            }
        }

        static void OnInteractableHoverEnter_Postfix(ref CVRInteractable __instance) => Instance?.OnInteractableHoverEnter(__instance);
        void OnInteractableHoverEnter(CVRInteractable p_interact)
        {
            try
            {
                foreach(var l_script in m_scripts)
                    l_script.OnInteractableHoverEnter(p_interact);
            }
            catch(Exception e)
            {
                Logger?.Error(e);
            }
        }

        static void OnInteractableHoverExit_Postfix(ref CVRInteractable __instance) => Instance?.OnInteractableHoverExit(__instance);
        void OnInteractableHoverExit(CVRInteractable p_interact)
        {
            try
            {
                foreach(var l_script in m_scripts)
                    l_script.OnInteractableHoverExit(p_interact);
            }
            catch(Exception e)
            {
                Logger?.Error(e);
            }
        }
    }
}
