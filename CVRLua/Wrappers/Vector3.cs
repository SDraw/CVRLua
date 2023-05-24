namespace CVRLua.Wrappers
{
    class Vector3
    {
        public UnityEngine.Vector3 m_vec;

        public Vector3()
        {
            m_vec = UnityEngine.Vector3.zero;
        }

        public Vector3(float p_x, float p_y, float p_z)
        {
            m_vec = new UnityEngine.Vector3(p_x, p_y, p_z);
        }

        public Vector3(UnityEngine.Vector3 p_vec)
        {
            m_vec = p_vec;
        }

        public Vector3(ref UnityEngine.Vector3 p_vec)
        {
            m_vec = p_vec;
        }
    }
}
