using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVRLua.Wrappers
{
    class Color
    {
        public UnityEngine.Color m_color;

        public Color(UnityEngine.Color p_color)
        {
            m_color = p_color;
        }
    }
}
