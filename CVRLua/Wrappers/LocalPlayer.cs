using ABI_RC.Core;
using ABI_RC.Core.Player;
using ABI_RC.Core.Savior;
using ABI_RC.Systems.IK;
using ABI_RC.Systems.MovementSystem;

namespace CVRLua.Wrappers
{
    class LocalPlayer : WrappedStructure
    {
        public string GetName() => MetaPort.Instance.username;

        public UnityEngine.Vector3 GetPosition() => PlayerSetup.Instance.transform.position;
        public UnityEngine.Quaternion GetRotation() => PlayerSetup.Instance.transform.rotation;

        public float GetAvatarHeight() => PlayerSetup.Instance.GetAvatarHeight();
        public float GetAvatarScale() => PlayerSetup.Instance.GetAvatarScale().y;

        public float GetPlayerHeight() => PlayerSetup.Instance.playerHeight;
        public float GetPlayerScale() => PlayerSetup.Instance.GetPlayerScale();

        public UnityEngine.Vector3 GetCameraPosition() => PlayerSetup.Instance.GetActiveCamera().transform.position;
        public UnityEngine.Quaternion GetCameraRotation() => PlayerSetup.Instance.GetActiveCamera().transform.rotation;

        public bool IsInVR() => CheckVR.Instance.hasVrDeviceLoaded;
        public bool IsInFullbody() => CheckVR.Instance.hasFullbodyEnabled;
        public bool HasAvatar() => (PlayerSetup.Instance._avatar != null);
        public bool IsAvatarLoading() => PlayerSetup.Instance.avatarIsLoading;
        public bool IsAvatarHumanoid() => ((PlayerSetup.Instance._animator != null) && PlayerSetup.Instance._animator.isHuman);

        public bool IsFlying() => MovementSystem.Instance.flying;
        public bool IsCrouching() => MovementSystem.Instance.crouching;
        public bool IsProning() => MovementSystem.Instance.crouching;
        public bool IsSitting() => MovementSystem.Instance.sitting;
        public bool IsSprinting() => CVRInputManager.Instance.sprint;
        public bool IsJumping() => CVRInputManager.Instance.jump;
        public bool IsZooming() => CVRInputManager.Instance.zoom;
        public float GetZoomFactor() => CVR_DesktopCameraController.currentZoomProgress;
        public UnityEngine.Vector3 GetMovementVector() => MovementSystem.Instance.movementVector;

        public UnityEngine.Vector3 GetLeftHandPosition() => IKSystem.Instance.leftHandOffset.position;
        public UnityEngine.Quaternion GetLeftHandRotation() => IKSystem.Instance.leftHandOffset.rotation;
        public UnityEngine.Vector3 GetRightHandPosition() => IKSystem.Instance.rightHandOffset.position;
        public UnityEngine.Quaternion GetRightHandRotation() => IKSystem.Instance.rightHandOffset.rotation;
        public float GetLeftHandGetsture() => CVRInputManager.Instance.gestureLeft;
        public float GetRightHandGetsture() => CVRInputManager.Instance.gestureRight;
        
        public void Teleport(UnityEngine.Vector3 p_position) => MovementSystem.Instance.TeleportTo(p_position);
        public void Teleport(UnityEngine.Vector3 p_position, UnityEngine.Quaternion p_rotation) => MovementSystem.Instance.TeleportTo(p_position, p_rotation.eulerAngles);
        public void SetImmobilized(bool p_state) => MovementSystem.Instance.SetImmobilized(p_state);
        public void Respawn() => RootLogic.Instance.Respawn();

        public UnityEngine.Vector3 GetBonePosition(UnityEngine.HumanBodyBones p_bone)
        {
            UnityEngine.Vector3 l_result = UnityEngine.Vector3.zero;
            if((PlayerSetup.Instance._animator != null) && PlayerSetup.Instance._animator.isHuman)
            {
                UnityEngine.Transform l_bone = PlayerSetup.Instance._animator.GetBoneTransform(p_bone);
                if(l_bone != null)
                    l_result = l_bone.position;
            }
            return l_result;
        }

        public UnityEngine.Quaternion GetBoneRotation(UnityEngine.HumanBodyBones p_bone)
        {
            UnityEngine.Quaternion l_result = UnityEngine.Quaternion.identity;
            if((PlayerSetup.Instance._animator != null) && PlayerSetup.Instance._animator.isHuman)
            {
                UnityEngine.Transform l_bone = PlayerSetup.Instance._animator.GetBoneTransform(p_bone);
                if(l_bone != null)
                    l_result = l_bone.rotation;
            }
            return l_result;
        }
    }
}
