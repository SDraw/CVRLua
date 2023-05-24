namespace CVRLua.Wrappers
{
    class Vector4
    {
        public UnityEngine.Vector4 m_vec;

        public Vector4()
        {
            m_vec = UnityEngine.Vector4.zero;
        }

        public Vector4(float p_x, float p_y, float p_z, float p_w)
        {
            m_vec = new UnityEngine.Vector4(p_x, p_y, p_z, p_w);
        }

        public Vector4(UnityEngine.Vector4 p_vec)
        {
            m_vec = p_vec;
        }

        public Vector4(ref UnityEngine.Vector4 p_vec)
        {
            m_vec = p_vec;
        }
    }
}
