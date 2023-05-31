namespace CVRLua.Wrappers
{
    class RaycastHit
    {
        public UnityEngine.RaycastHit m_hit;

        public RaycastHit(UnityEngine.RaycastHit p_hit)
        {
            m_hit = p_hit;
        }
    }
}
