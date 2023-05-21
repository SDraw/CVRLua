namespace CVRLua.Wrappers
{
    class Quaternion : WrappedStructure
    {
        public UnityEngine.Quaternion m_quat;

        public Quaternion()
        {
            m_quat = UnityEngine.Quaternion.identity;
        }

        public Quaternion(float x, float y, float z, float w)
        {
            m_quat = new UnityEngine.Quaternion(x, y, z, w);
        }

        public Quaternion(UnityEngine.Quaternion p_quat)
        {
            m_quat = p_quat;
        }

        public Quaternion(ref UnityEngine.Quaternion p_quat)
        {
            m_quat = p_quat;
        }

        public Quaternion(UnityEngine.Vector3 p_vec)
        {
            m_quat = UnityEngine.Quaternion.Euler(p_vec);
        }

        public Quaternion(ref UnityEngine.Vector3 p_vec)
        {
            m_quat = UnityEngine.Quaternion.Euler(p_vec);
        }

        public Quaternion(UnityEngine.Vector4 p_vec)
        {
            m_quat = new UnityEngine.Quaternion(p_vec.x, p_vec.y, p_vec.z, p_vec.w);
        }

        public Quaternion(ref UnityEngine.Vector4 p_vec)
        {
            m_quat = new UnityEngine.Quaternion(p_vec.x, p_vec.y, p_vec.z, p_vec.w);
        }
    }
}
