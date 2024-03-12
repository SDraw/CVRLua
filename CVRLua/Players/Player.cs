using ABI.CCK.Components;
using ABI_RC.Core;
using ABI_RC.Core.InteractionSystem;
using ABI_RC.Core.Player;
using ABI_RC.Core.Savior;
using ABI_RC.Systems.IK;
using ABI_RC.Systems.IK.SubSystems;
using ABI_RC.Systems.InputManagement;
using MovementSystem = ABI_RC.Systems.Movement.BetterBetterCharacterController;
using UnityEngine;

namespace CVRLua.Players
{
    class Player
    {
        enum PlayerType
        {
            Local = 0,
            Remote
        };

        PlayerType m_type;
        readonly GameObject m_gameObject = null; // Remote only
        readonly PlayerDescriptor m_descriptor = null; // Remote only
        readonly PuppetMaster m_puppetMaster = null; // Remote only

        public Player()
        {
            m_type = PlayerType.Local;
        }
        public Player(PlayerDescriptor p_descriptor)
        {
            m_type = PlayerType.Remote;
            m_descriptor = p_descriptor;
            m_gameObject = p_descriptor.gameObject;
            m_puppetMaster = m_gameObject.GetComponent<PuppetMaster>();
        }

        public bool IsLocal() => (m_type == PlayerType.Local);
        public bool IsRemote() => (m_type == PlayerType.Remote);
        public bool IsConnected() => ((m_type == PlayerType.Local) || (m_descriptor != null));

        public string GetName()
        {
            string l_result = "";
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = CVR_MenuManager.Instance.coreData.core.username;
                    break;
                case PlayerType.Remote:
                    l_result = m_descriptor.userName;
                    break;
            }
            return l_result;
        }

        public string GetId()
        {
            string l_result = "";
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = MetaPort.Instance.ownerId;
                    break;
                case PlayerType.Remote:
                    l_result = m_descriptor.ownerId;
                    break;
            }
            return l_result;
        }

        public Vector3 GetPosition()
        {
            Vector3 l_result = Vector3.zero;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = PlayerSetup.Instance.transform.position;
                    break;

                case PlayerType.Remote:
                {
                    if(m_gameObject != null)
                        l_result = m_gameObject.transform.position;
                }
                break;
            }
            return l_result;
        }

        public Quaternion GetRotation()
        {
            Quaternion l_result = Quaternion.identity;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = PlayerSetup.Instance.transform.rotation;
                    break;

                case PlayerType.Remote:
                {
                    if(m_gameObject != null)
                        l_result = m_gameObject.transform.rotation;
                }
                break;
            }
            return l_result;
        }

        public float GetAvatarHeight()
        {
            float l_result = 0f;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = PlayerSetup.Instance.GetAvatarHeight();
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.GetAvatarHeight();
                    break;
            }
            return l_result;
        }

        public float GetAvatarScale()
        {
            float l_result = 0f;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = PlayerSetup.Instance.GetAvatarScale().y;
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.GetAvatarScale().y;
                    break;
            }
            return l_result;
        }

        public float GetPlayerHeight()
        {
            float l_result = 0f;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = PlayerSetup.Instance.playerHeight;
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.GetAvatarHeight();
                    break;
            }
            return l_result;
        }

        public float GetPlayerScale()
        {
            float l_result = 0f;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = PlayerSetup.Instance.GetPlayerScale();
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.GetAvatarScale().y;
                    break;
            }
            return l_result;
        }

        public Vector3 GetViewPosition()
        {
            Vector3 l_result = Vector3.zero;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = PlayerSetup.Instance.GetActiveCamera().transform.position;
                    break;
                case PlayerType.Remote:
                {
                    var l_point = m_puppetMaster.GetHeadPoint();
                    if(l_point != null)
                        l_result = l_point.transform.position;
                }
                break;
            }
            return l_result;
        }

        public Quaternion GetViewRotation()
        {
            Quaternion l_result = Quaternion.identity;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = PlayerSetup.Instance.GetActiveCamera().transform.rotation;
                    break;
                case PlayerType.Remote:
                {
                    var l_point = m_puppetMaster.GetHeadPoint();
                    if(l_point != null)
                        l_result = l_point.transform.rotation;
                }
                break;
            }
            return l_result;
        }

        public bool IsInVR()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = CheckVR.Instance.hasVrDeviceLoaded;
                    break;
                case PlayerType.Remote:
                    l_result = (m_puppetMaster.PlayerAvatarMovementDataInput.DeviceType != PlayerAvatarMovementData.UsingDeviceType.PCStanalone);
                    break;
            }
            return l_result;
        }

        public bool IsInFullbody() => ((m_type == PlayerType.Local) && BodySystem.isCalibratedAsFullBody);

        public bool HasAvatar()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = (PlayerSetup.Instance._avatar != null);
                    break;
                case PlayerType.Remote:
                    l_result = (m_puppetMaster.GetAvatar() != null);
                    break;
            }
            return l_result;
        }

        public bool IsAvatarLoading() => ((m_type == PlayerType.Local) && PlayerSetup.Instance.avatarIsLoading);

        public bool IsAvatarHumanoid()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = ((PlayerSetup.Instance._animator != null) && PlayerSetup.Instance._animator.isHuman);
                    break;
                case PlayerType.Remote:
                {
                    Animator l_animator = m_puppetMaster.GetAnimator();
                    l_result = ((l_animator != null) && l_animator.isHuman);
                }
                break;
            }
            return l_result;
        }

        public bool IsFlying()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = MovementSystem.Instance.IsFlying();
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorFlying;
                    break;
            }
            return l_result;
        }

        public bool IsCrouching()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = MovementSystem.Instance.crouching;
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorCrouching;
                    break;
            }
            return l_result;
        }

        public bool IsProning()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = MovementSystem.Instance.prone;
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorProne;
                    break;
            }
            return l_result;
        }

        public bool IsSitting()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = MovementSystem.Instance.IsSitting();
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorSitting;
                    break;
            }
            return l_result;
        }

        public bool IsSprinting()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = CVRInputManager.Instance.sprint;
                    break;
                case PlayerType.Remote:
                {
                    Vector2 l_move = new Vector2(m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorMovementX, m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorMovementY);
                    l_result = (l_move.magnitude >= 0.5f);
                }
                break;
            }
            return l_result;
        }

        public bool IsJumping() => ((m_type == PlayerType.Local) && CVRInputManager.Instance.jump);

        public bool IsGrounded()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = MovementSystem.Instance.IsGrounded();
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorGrounded;
                    break;
            }
            return false;
        }

        public bool IsZooming() => ((m_type == PlayerType.Local) && CVRInputManager.Instance.zoom);

        public float GetZoomFactor() => ((m_type == PlayerType.Local) ? CVR_DesktopCameraController.currentZoomProgress : 0f);

        public Vector2 GetMovementVector()
        {
            Vector2 l_result = Vector2.zero;
            switch(m_type)
            {
                case PlayerType.Local:
                {
                    l_result.x = MovementSystem.Instance.AppliedMovementVector.x;
                    l_result.y = MovementSystem.Instance.AppliedMovementVector.y;
                }
                break;
                case PlayerType.Remote:
                {
                    l_result.x = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorMovementX;
                    l_result.y = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorMovementY;
                }
                break;
            }
            return l_result;
        }

        public Vector3 GetLeftHandPosition()
        {
            Vector3 l_result = Vector3.zero;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = IKSystem.Instance.leftHandTarget.position;
                    break;
                case PlayerType.Remote:
                {
                    Animator l_animator = m_puppetMaster.GetAnimator();
                    if((l_animator != null) && l_animator.isHuman)
                    {
                        Transform l_hand = l_animator.GetBoneTransform(HumanBodyBones.LeftHand);
                        if(l_hand != null)
                            l_result = l_hand.position;
                    }
                }
                break;
            }
            return l_result;
        }

        public Quaternion GetLeftHandRotation()
        {
            Quaternion l_result = Quaternion.identity;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = IKSystem.Instance.leftHandRotations.rotation;
                    break;
                case PlayerType.Remote:
                {
                    Animator l_animator = m_puppetMaster.GetAnimator();
                    if((l_animator != null) && l_animator.isHuman)
                    {
                        Transform l_hand = l_animator.GetBoneTransform(HumanBodyBones.LeftHand);
                        if(l_hand != null)
                            l_result = l_hand.rotation;
                    }
                }
                break;
            }
            return l_result;
        }

        public Vector3 GetRightHandPosition()
        {
            Vector3 l_result = Vector3.zero;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = IKSystem.Instance.rightHandTarget.position;
                    break;
                case PlayerType.Remote:
                {
                    Animator l_animator = m_puppetMaster.GetAnimator();
                    if((l_animator != null) && l_animator.isHuman)
                    {
                        Transform l_hand = l_animator.GetBoneTransform(HumanBodyBones.RightHand);
                        if(l_hand != null)
                            l_result = l_hand.position;
                    }
                }
                break;
            }
            return l_result;
        }

        public Quaternion GetRightHandRotation()
        {
            Quaternion l_result = Quaternion.identity;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = IKSystem.Instance.rightHandRotations.rotation;
                    break;
                case PlayerType.Remote:
                {
                    Animator l_animator = m_puppetMaster.GetAnimator();
                    if((l_animator != null) && l_animator.isHuman)
                    {
                        Transform l_hand = l_animator.GetBoneTransform(HumanBodyBones.RightHand);
                        if(l_hand != null)
                            l_result = l_hand.rotation;
                    }
                }
                break;
            }
            return l_result;
        }

        public float GetLeftHandGetsture()
        {
            float l_result = 0f;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = CVRInputManager.Instance.gestureLeft;
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorGestureLeft;
                    break;
            }
            return l_result;
        }

        public float GetRightHandGetsture()
        {
            float l_result = 0f;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = CVRInputManager.Instance.gestureRight;
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorGestureRight;
                    break;
            }
            return l_result;
        }

        public void Teleport(Vector3 p_position)
        {
            if(m_type == PlayerType.Local)
                MovementSystem.Instance.TeleportPosition(p_position);
        }

        public void Teleport(Vector3 p_position, Quaternion p_rotation)
        {
            if(m_type == PlayerType.Local)
                MovementSystem.Instance.TeleportPlayerTo(p_position, true, false, p_rotation);
        }

        public void SetImmobilized(bool p_state)
        {
            if(m_type == PlayerType.Local)
                MovementSystem.Instance.SetImmobilized(p_state);
        }

        public void Respawn()
        {
            if(m_type == PlayerType.Local)
                RootLogic.Instance.Respawn();
        }

        public Vector3 GetBonePosition(HumanBodyBones p_bone)
        {
            Vector3 l_result = Vector3.zero;
            switch(m_type)
            {
                case PlayerType.Local:
                {
                    if((PlayerSetup.Instance._animator != null) && PlayerSetup.Instance._animator.isHuman)
                    {
                        Transform l_bone = PlayerSetup.Instance._animator.GetBoneTransform(p_bone);
                        if(l_bone != null)
                            l_result = l_bone.position;
                    }
                }
                break;
                case PlayerType.Remote:
                {
                    Animator l_animator = m_puppetMaster.GetAnimator();
                    if((l_animator != null) && l_animator.isHuman)
                    {
                        Transform l_bone = l_animator.GetBoneTransform(p_bone);
                        if(l_bone != null)
                            l_result = l_bone.position;
                    }
                }
                break;
            }
            return l_result;
        }

        public Quaternion GetBoneRotation(HumanBodyBones p_bone)
        {
            Quaternion l_result = Quaternion.identity;
            switch(m_type)
            {
                case PlayerType.Local:
                {
                    if((PlayerSetup.Instance._animator != null) && PlayerSetup.Instance._animator.isHuman)
                    {
                        Transform l_bone = PlayerSetup.Instance._animator.GetBoneTransform(p_bone);
                        if(l_bone != null)
                            l_result = l_bone.rotation;
                    }
                }
                break;
                case PlayerType.Remote:
                {
                    Animator l_animator = m_puppetMaster.GetAnimator();
                    if((l_animator != null) && l_animator.isHuman)
                    {
                        Transform l_bone = l_animator.GetBoneTransform(p_bone);
                        if(l_bone != null)
                            l_result = l_bone.rotation;
                    }
                }
                break;
            }
            return l_result;
        }

        public Vector2 GetLookVector() => ((m_type == PlayerType.Local) ? CVRInputManager.Instance.lookVector : Vector2.zero);

        public bool GetIndividualFingerTracking()
        {
            bool l_result = false;
            switch(m_type)
            {
                case PlayerType.Local:
                    l_result = CVRInputManager.Instance.individualFingerTracking;
                    break;
                case PlayerType.Remote:
                    l_result = m_puppetMaster.PlayerAvatarMovementDataInput.UseIndividualFingers;
                    break;
            }
            return l_result;
        }

        public float[] GetFingerCurls()
        {
            float[] l_result = null;
            switch(m_type)
            {
                case PlayerType.Local:
                {
                    l_result = new float[10]
                    {
                        CVRInputManager.Instance.fingerFullCurlNormalizedLeftThumb,
                        CVRInputManager.Instance.fingerFullCurlNormalizedLeftIndex,
                        CVRInputManager.Instance.fingerFullCurlNormalizedLeftMiddle,
                        CVRInputManager.Instance.fingerFullCurlNormalizedLeftThumb,
                        CVRInputManager.Instance.fingerFullCurlNormalizedLeftPinky,
                        CVRInputManager.Instance.fingerFullCurlNormalizedRightThumb,
                        CVRInputManager.Instance.fingerFullCurlNormalizedRightIndex,
                        CVRInputManager.Instance.fingerFullCurlNormalizedRightMiddle,
                        CVRInputManager.Instance.fingerFullCurlNormalizedRightThumb,
                        CVRInputManager.Instance.fingerFullCurlNormalizedRightPinky
                    };
                }
                break;
                case PlayerType.Remote:
                {
                    l_result = new float[10]
                    {
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftThumbSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftIndexSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftMiddleSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftRingSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftPinkySpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightThumbSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightIndexSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightMiddleSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightRingSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightPinkySpread
                    };
                }
                break;
            }
            return l_result;
        }

        public float[] GetFingerSpreads()
        {
            float[] l_result = null;
            switch(m_type)
            {
                case PlayerType.Local:
                {
                    l_result = new float[10]
                    {
                        CVRInputManager.Instance.fingerSpreadLeftThumb,
                        CVRInputManager.Instance.fingerSpreadLeftIndex,
                        CVRInputManager.Instance.fingerSpreadLeftMiddle,
                        CVRInputManager.Instance.fingerSpreadLeftThumb,
                        CVRInputManager.Instance.fingerSpreadLeftPinky,
                        CVRInputManager.Instance.fingerSpreadRightThumb,
                        CVRInputManager.Instance.fingerSpreadRightIndex,
                        CVRInputManager.Instance.fingerSpreadRightMiddle,
                        CVRInputManager.Instance.fingerSpreadRightThumb,
                        CVRInputManager.Instance.fingerSpreadRightPinky
                    };
                }
                break;
                case PlayerType.Remote:
                {
                    l_result = new float[10]
                    {
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftThumbSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftIndexSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftMiddleSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftRingSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.LeftPinkySpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightThumbSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightIndexSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightMiddleSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightRingSpread,
                        m_puppetMaster.PlayerAvatarMovementDataInput.RightPinkySpread
                    };
                }
                break;
            }
            return l_result;
        }

        public static bool Compare(Player p_playerA, Player p_playerB)
        {
            if(p_playerA.IsLocal() && p_playerB.IsLocal())
                return true;
            if(p_playerA.IsLocal() && p_playerB.IsRemote())
                return false;
            if(p_playerA.IsRemote() && p_playerB.IsLocal())
                return false;
            if(p_playerA.IsRemote() && p_playerB.IsRemote())
                return (p_playerA.m_gameObject == p_playerB.m_gameObject);

            return false;
        }

        public override string ToString() => string.Format("Player({0})", GetName());
    }
}
