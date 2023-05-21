namespace CVRLua.Wrappers
{
    class Vector2 : WrappedStructure
    {
        public UnityEngine.Vector2 m_vec;

        public Vector2()
        {
            m_vec = UnityEngine.Vector2.zero;
        }

        public Vector2(float p_x, float p_y)
        {
            m_vec = new UnityEngine.Vector2(p_x, p_y);
        }

        public Vector2(UnityEngine.Vector2 p_vec)
        {
            m_vec = p_vec;
        }

        public Vector2(ref UnityEngine.Vector2 p_vec)
        {
            m_vec = p_vec;
        }
    }
}
