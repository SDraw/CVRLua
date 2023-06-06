using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVRLua.Wrappers
{
    class Matrix4x4
    {
        public UnityEngine.Matrix4x4 m_mat;

        public Matrix4x4(UnityEngine.Matrix4x4 p_mat)
        {
            m_mat = p_mat;
        }
    }
}
