using ABI.CCK.Components;
using ABI_RC.Core.Player;
using ABI_RC.Core.Player.AvatarTracking.Remote;
using ABI_RC.Systems.MovementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CVRLua
{
    static class Utils
    {
        static readonly FieldInfo ms_puppetLastScale = typeof(PuppetMaster).GetField("lastAvatarScale", BindingFlags.NonPublic | BindingFlags.Instance);
        static readonly FieldInfo ms_puppetViewPoint = typeof(PuppetMaster).GetField("_viewPoint", BindingFlags.NonPublic | BindingFlags.Instance);
        static readonly FieldInfo ms_puppetAvatar = typeof(PuppetMaster).GetField("_avatar", BindingFlags.NonPublic | BindingFlags.Instance);
        static readonly FieldInfo ms_puppetAnimator = typeof(PuppetMaster).GetField("_animator", BindingFlags.NonPublic | BindingFlags.Instance);
        static readonly FieldInfo ms_movementGrounded = typeof(MovementSystem).GetField("_isGrounded", BindingFlags.NonPublic | BindingFlags.Instance);

        public static long GetInt(this IntPtr p_source)
        {
            long l_result = 0;
            try
            {
                l_result = Marshal.ReadInt64(p_source);
            }
            catch(AccessViolationException) { }
            return l_result;
        }

        public static void SetInt(this IntPtr p_source, long p_value)
        {
            try
            {
                Marshal.WriteInt64(p_source, p_value);
            }
            catch(AccessViolationException) { }
        }

        public static void MergeInto<T, V>(this Dictionary<T, V> p_source, Dictionary<T, V> p_target)
        {
            p_source.ToList().ForEach(p => p_target.Add(p.Key, p.Value));
        }

        public static void MergeInto<T>(this List<T> p_source, List<T> p_target)
        {
            p_source.ForEach(p => p_target.Add(p));
        }

        public static bool IsSafeToDestroy(this UnityEngine.Object p_object)
        {
            GameObject l_rootObject = null;
            if(p_object is Component)
                l_rootObject = (p_object as Component).transform.root.gameObject;
            if(p_object is GameObject)
                l_rootObject = (p_object as GameObject).transform.root.gameObject;

            if(l_rootObject == null)
                return true;

            if(l_rootObject.scene.name == "DontDestroyOnLoad")
                return false;

            if(l_rootObject.name.StartsWith("p+"))
                return false;

            if(l_rootObject.GetComponent<PuppetMaster>() != null)
                return false;

            return true;
        }

        public static bool IsInternal(Component p_col)
        {
            return (p_col.gameObject.scene.name == "DontDestroyOnLoad");
        }

        public static long CombineInts(int left, int right) => ((((long)left) << 32) | (uint)right);

        // PuppetMaster extenions
        public static Vector3 GetAvatarScale(this PuppetMaster p_instance) => (Vector3)ms_puppetLastScale.GetValue(p_instance);
        public static RemoteHeadPoint GetHeadPoint(this PuppetMaster p_instance) => (RemoteHeadPoint)ms_puppetViewPoint.GetValue(p_instance);
        public static CVRAvatar GetAvatar(this PuppetMaster p_instance) => (CVRAvatar)ms_puppetAvatar.GetValue(p_instance);
        public static Animator GetAnimator(this PuppetMaster p_instance) => (Animator)ms_puppetAnimator.GetValue(p_instance);

        // MovementSystem
        public static bool IsGrounded(this MovementSystem p_instance) => (bool)ms_movementGrounded.GetValue(p_instance);
    }
}
