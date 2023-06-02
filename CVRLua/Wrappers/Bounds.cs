using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVRLua.Wrappers
{
    class Bounds
    {
        public UnityEngine.Bounds m_bounds;

        public Bounds(UnityEngine.Bounds p_bounds)
        {
            m_bounds = p_bounds;
        }
    }
}
