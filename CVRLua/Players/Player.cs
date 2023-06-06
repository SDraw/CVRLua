using ABI.CCK.Components;
using ABI_RC.Core;
using ABI_RC.Core.Player;
using ABI_RC.Core.Savior;
using ABI_RC.Systems.IK;
using ABI_RC.Systems.IK.SubSystems;
using ABI_RC.Systems.MovementSystem;
using UnityEngine;

namespace CVRLua.Players
{
    class Player
    {
        bool m_local = false;
        bool m_remote = false;
        readonly GameObject m_gameObject = null; // Remote only
        readonly PlayerDescriptor m_descriptor = null; // Remote only
        readonly PuppetMaster m_puppetMaster = null; // Remote only

        public Player()
        {
            m_local = true;
        }
        public Player(GameObject p_obj)
        {
            m_gameObject = p_obj;
            m_descriptor = p_obj.GetComponent<PlayerDescriptor>();
            m_puppetMaster = p_obj.GetComponent<PuppetMaster>();
            m_remote = true;
        }

        public bool IsLocal() => m_local;
        public bool IsRemote() => m_remote;
        public bool IsConnected() => (m_local || (m_remote && (m_puppetMaster != null)));

        public bool GetName(out string p_name)
        {
            if(m_local)
            {
                p_name = MetaPort.Instance.username;
                return true;
            }
            if(m_remote && (m_descriptor != null))
            {
                p_name = m_descriptor.userName;
                return true;
            }
            p_name = "";
            return false;
        }

        public bool GetId(out string p_id)
        {
            if(m_local)
            {
                p_id = MetaPort.Instance.ownerId;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                var l_player = CVRPlayerManager.Instance.NetworkPlayers.Find(p => p.PuppetMaster == m_puppetMaster);
                if(l_player != null)
                {
                    p_id = l_player.Uuid;
                    return true;
                }
            }
            p_id = "";
            return false;
        }

        public bool GetPosition(out Vector3 p_pos)
        {
            if(m_local)
            {
                p_pos = PlayerSetup.Instance.transform.position;
                return true;
            }
            if(m_remote && (m_gameObject != null))
            {
                p_pos = m_gameObject.transform.position;
                return true;
            }
            p_pos = Vector3.zero;
            return false;
        }

        public bool GetRotation(out Quaternion p_rot)
        {
            if(m_local)
            {
                p_rot = PlayerSetup.Instance.transform.rotation;
                return true;
            }
            if(m_remote && (m_gameObject != null))
            {
                p_rot = m_gameObject.transform.rotation;
                return true;
            }
            p_rot = Quaternion.identity;
            return false;
        }

        public bool GetAvatarHeight(out float p_height)
        {
            if(m_local)
            {
                p_height = PlayerSetup.Instance.GetAvatarHeight();
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                p_height = m_puppetMaster.GetAvatarHeight();
                return true;
            }
            p_height = 0f;
            return false;
        }

        public bool GetAvatarScale(out float p_scale)
        {
            if(m_local)
            {
                p_scale = PlayerSetup.Instance.GetAvatarScale().y;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                p_scale = m_puppetMaster.GetAvatarScale().y;
                return true;
            }
            p_scale = 0f;
            return false;
        }

        public bool GetPlayerHeight(out float p_height)
        {
            if(m_local)
            {
                p_height = PlayerSetup.Instance.playerHeight;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                p_height = m_puppetMaster.GetAvatarHeight();
                return true;
            }
            p_height = 0f;
            return false;
        }

        public bool GetPlayerScale(out float p_scale)
        {
            if(m_local)
            {
                p_scale = PlayerSetup.Instance.GetPlayerScale();
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                p_scale = m_puppetMaster.GetAvatarScale().y;
                return true;
            }
            p_scale = 0f;
            return false;
        }

        public bool GetCameraPosition(out Vector3 p_pos)
        {
            if(m_local)
            {
                p_pos = PlayerSetup.Instance.GetActiveCamera().transform.position;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                var l_point = m_puppetMaster.GetHeadPoint();
                if(l_point != null)
                {
                    p_pos = l_point.transform.position;
                    return true;
                }
            }
            p_pos = Vector3.zero;
            return false;
        }

        public bool GetCameraRotation(out Quaternion p_rot)
        {
            if(m_local)
            {
                p_rot = PlayerSetup.Instance.GetActiveCamera().transform.rotation;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                var l_point = m_puppetMaster.GetHeadPoint();
                if(l_point != null)
                {
                    p_rot = l_point.transform.rotation;
                    return true;
                }
            }
            p_rot = Quaternion.identity;
            return false;
        }

        public bool IsInVR()
        {
            if(m_local)
                return CheckVR.Instance.hasVrDeviceLoaded;
            if(m_remote && (m_puppetMaster != null))
                return (m_puppetMaster.PlayerAvatarMovementDataInput.DeviceType == PlayerAvatarMovementData.UsingDeviceType.PCVR);
            return false;
        }

        public bool IsInFullbody()
        {
            if(m_local)
                return BodySystem.isCalibratedAsFullBody;
            return false;
        }

        public bool HasAvatar()
        {
            if(m_local)
                return (PlayerSetup.Instance._avatar != null);
            if(m_remote && (m_puppetMaster != null))
            {
                CVRAvatar l_avatar = m_puppetMaster.GetAvatar();
                return (l_avatar != null);
            }
            return false;
        }

        public bool IsAvatarLoading()
        {
            if(m_local)
                return PlayerSetup.Instance.avatarIsLoading;
            return false;
        }

        public bool IsAvatarHumanoid()
        {
            if(m_local)
                return ((PlayerSetup.Instance._animator != null) && PlayerSetup.Instance._animator.isHuman);
            if(m_remote && (m_puppetMaster != null))
            {
                Animator l_animator = m_puppetMaster.GetAnimator();
                return ((l_animator != null) && l_animator.isHuman);
            }
            return false;
        }

        public bool IsFlying()
        {
            if(m_local)
                return MovementSystem.Instance.flying;
            if(m_remote && (m_puppetMaster != null))
                return m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorFlying;
            return false;
        }

        public bool IsCrouching()
        {
            if(m_local)
                return MovementSystem.Instance.crouching;
            if(m_remote && (m_puppetMaster != null))
                return m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorCrouching;
            return false;
        }

        public bool IsProning()
        {
            if(m_local)
                return MovementSystem.Instance.prone;
            if(m_remote && (m_puppetMaster != null))
                return m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorProne;
            return false;
        }

        public bool IsSitting()
        {
            if(m_local)
                return MovementSystem.Instance.sitting;
            if(m_remote && (m_puppetMaster != null))
                return m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorSitting;
            return false;
        }

        public bool IsSprinting()
        {
            if(m_local)
                return CVRInputManager.Instance.sprint;
            return false;
        }

        public bool IsJumping()
        {
            if(m_local)
                return CVRInputManager.Instance.jump;
            return false;
        }

        public bool IsGrounded()
        {
            if(m_local)
                return MovementSystem.Instance.IsGrounded();
            if(m_remote && (m_puppetMaster != null))
                return m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorGrounded;
            return false;
        }

        public bool IsZooming()
        {
            if(m_local)
                return CVRInputManager.Instance.zoom;
            return false;
        }

        public bool GetZoomFactor(out float p_factor)
        {
            if(m_local)
            {
                p_factor = CVR_DesktopCameraController.currentZoomProgress;
                return true;
            }
            p_factor = 0f;
            return false;
        }

        public bool GetMovementVector(out Vector2 p_vec)
        {
            if(m_local)
            {
                p_vec = new Vector2(MovementSystem.Instance.movementVector.x, MovementSystem.Instance.movementVector.z);
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                p_vec = new Vector2(m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorMovementX, m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorMovementY);
                return true;
            }
            p_vec = Vector2.zero;
            return false;
        }

        public bool GetLeftHandPosition(out Vector3 p_pos)
        {
            if(m_local)
            {
                p_pos = IKSystem.Instance.leftHandOffset.position;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                Animator l_animator = m_puppetMaster.GetAnimator();
                if((l_animator != null) && l_animator.isHuman)
                {
                    Transform l_hand = l_animator.GetBoneTransform(HumanBodyBones.LeftHand);
                    if(l_hand != null)
                    {
                        p_pos = l_hand.position;
                        return true;
                    }
                }
            }
            p_pos = Vector3.zero;
            return false;
        }

        public bool GetLeftHandRotation(out Quaternion p_rot)
        {
            if(m_local)
            {
                p_rot = IKSystem.Instance.leftHandOffset.rotation;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                Animator l_animator = m_puppetMaster.GetAnimator();
                if((l_animator != null) && l_animator.isHuman)
                {
                    Transform l_hand = l_animator.GetBoneTransform(HumanBodyBones.LeftHand);
                    if(l_hand != null)
                    {
                        p_rot = l_hand.rotation;
                        return true;
                    }
                }
            }
            p_rot = Quaternion.identity;
            return false;
        }

        public bool GetRightHandPosition(out Vector3 p_pos)
        {
            if(m_local)
            {
                p_pos = IKSystem.Instance.rightHandOffset.position;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                Animator l_animator = m_puppetMaster.GetAnimator();
                if((l_animator != null) && l_animator.isHuman)
                {
                    Transform l_hand = l_animator.GetBoneTransform(HumanBodyBones.RightHand);
                    if(l_hand != null)
                    {
                        p_pos = l_hand.position;
                        return true;
                    }
                }
            }
            p_pos = Vector3.zero;
            return false;
        }

        public bool GetRightHandRotation(out Quaternion p_rot)
        {
            if(m_local)
            {
                p_rot = IKSystem.Instance.rightHandOffset.rotation;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                Animator l_animator = m_puppetMaster.GetAnimator();
                if((l_animator != null) && l_animator.isHuman)
                {
                    Transform l_hand = l_animator.GetBoneTransform(HumanBodyBones.RightHand);
                    if(l_hand != null)
                    {
                        p_rot = l_hand.rotation;
                        return true;
                    }
                }
            }
            p_rot = Quaternion.identity;
            return false;
        }

        public bool GetLeftHandGetsture(out float p_gesture)
        {
            if(m_local)
            {
                p_gesture = CVRInputManager.Instance.gestureLeft;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                p_gesture = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorGestureLeft;
                return true;
            }
            p_gesture = 0f;
            return false;
        }

        public bool GetRightHandGetsture(out float p_gesture)
        {
            if(m_local)
            {
                p_gesture = CVRInputManager.Instance.gestureRight;
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                p_gesture = m_puppetMaster.PlayerAvatarMovementDataInput.AnimatorGestureRight;
                return true;
            }
            p_gesture = 0f;
            return false;
        }

        public void Teleport(Vector3 p_position)
        {
            if(m_local)
                MovementSystem.Instance.TeleportTo(p_position);
        }

        public void Teleport(Vector3 p_position, Quaternion p_rotation)
        {
            if(m_local)
                MovementSystem.Instance.TeleportTo(p_position, p_rotation.eulerAngles);
        }

        public void SetImmobilized(bool p_state)
        {
            if(m_local)
                MovementSystem.Instance.SetImmobilized(p_state);
        }

        public void Respawn()
        {
            if(m_local)
                RootLogic.Instance.Respawn();
        }

        public bool GetBonePosition(HumanBodyBones p_bone, out Vector3 p_vec)
        {
            if(m_local)
            {
                Vector3 l_result = Vector3.zero;
                if((PlayerSetup.Instance._animator != null) && PlayerSetup.Instance._animator.isHuman)
                {
                    Transform l_bone = PlayerSetup.Instance._animator.GetBoneTransform(p_bone);
                    if(l_bone != null)
                    {
                        p_vec = l_bone.position;
                        return true;
                    }
                }
            }
            if(m_remote && (m_puppetMaster != null))
            {
                Animator l_animator = m_puppetMaster.GetAnimator();
                if((l_animator != null) && l_animator.isHuman)
                {
                    Transform l_bone = l_animator.GetBoneTransform(p_bone);
                    if(l_bone != null)
                    {
                        p_vec = l_bone.position;
                        return true;
                    }
                }
            }
            p_vec = Vector3.zero;
            return false;
        }

        public bool GetBoneRotation(HumanBodyBones p_bone, out Quaternion p_rot)
        {
            if(m_local)
            {
                Vector3 l_result = Vector3.zero;
                if((PlayerSetup.Instance._animator != null) && PlayerSetup.Instance._animator.isHuman)
                {
                    Transform l_bone = PlayerSetup.Instance._animator.GetBoneTransform(p_bone);
                    if(l_bone != null)
                    {
                        p_rot = l_bone.rotation;
                        return true;
                    }
                }
            }
            if(m_remote && (m_puppetMaster != null))
            {
                Animator l_animator = m_puppetMaster.GetAnimator();
                if((l_animator != null) && l_animator.isHuman)
                {
                    Transform l_bone = l_animator.GetBoneTransform(p_bone);
                    if(l_bone != null)
                    {
                        p_rot = l_bone.rotation;
                        return true;
                    }
                }
            }
            p_rot = Quaternion.identity;
            return false;
        }

        public bool GetLookVector(out Vector2 p_vec)
        {
            if(m_local)
            {
                p_vec = CVRInputManager.Instance.lookVector;
                return true;
            }
            p_vec = Vector2.zero;
            return false;
        }

        public bool GetIndividualFingerTracking()
        {
            if(m_local)
                return CVRInputManager.Instance.individualFingerTracking;
            if(m_remote && (m_puppetMaster != null))
                return m_puppetMaster.PlayerAvatarMovementDataInput.IndexUseIndividualFingers;
            return false;
        }

        public bool GetFingerCurls(out float[] p_curls)
        {
            if(m_local)
            {
                p_curls = new float[10]
                {
                    CVRInputManager.Instance.fingerCurlLeftThumb,
                    CVRInputManager.Instance.fingerCurlLeftIndex,
                    CVRInputManager.Instance.fingerCurlLeftMiddle,
                    CVRInputManager.Instance.fingerCurlLeftThumb,
                    CVRInputManager.Instance.fingerCurlLeftPinky,
                    CVRInputManager.Instance.fingerCurlRightThumb,
                    CVRInputManager.Instance.fingerCurlRightIndex,
                    CVRInputManager.Instance.fingerCurlRightMiddle,
                    CVRInputManager.Instance.fingerCurlRightThumb,
                    CVRInputManager.Instance.fingerCurlRightPinky
                };
                return true;
            }
            if(m_remote && (m_puppetMaster != null))
            {
                p_curls = new float[10]
                {
                    m_puppetMaster.PlayerAvatarMovementDataInput.LeftThumbCurl,
                    m_puppetMaster.PlayerAvatarMovementDataInput.LeftIndexCurl,
                    m_puppetMaster.PlayerAvatarMovementDataInput.LeftMiddleCurl,
                    m_puppetMaster.PlayerAvatarMovementDataInput.LeftRingCurl,
                    m_puppetMaster.PlayerAvatarMovementDataInput.LeftPinkyCurl,
                    m_puppetMaster.PlayerAvatarMovementDataInput.RightThumbCurl,
                    m_puppetMaster.PlayerAvatarMovementDataInput.RightIndexCurl,
                    m_puppetMaster.PlayerAvatarMovementDataInput.RightMiddleCurl,
                    m_puppetMaster.PlayerAvatarMovementDataInput.RightRingCurl,
                    m_puppetMaster.PlayerAvatarMovementDataInput.RightPinkyCurl
                };
                return true;
            }
            p_curls = null;
            return false;
        }

        public static bool Compare(Player p_playerA, Player p_playerB)
        {
            if(p_playerA.m_local && p_playerB.m_local)
                return true;
            if(p_playerA.m_local && p_playerB.m_remote)
                return false;
            if(p_playerA.m_remote && p_playerB.m_local)
                return false;
            if(p_playerA.m_remote && p_playerB.m_remote)
                return (p_playerA.m_gameObject == p_playerB.m_gameObject);

            return false;
        }

        public override string ToString()
        {
            string l_result = "Player(";
            if(GetName(out string l_name))
                l_result += l_name;
            l_result += ')';
            return l_result;
        }
    }
}
