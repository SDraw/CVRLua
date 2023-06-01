using UnityEngine;

namespace CVRLua
{
    class DestructionDetector : MonoBehaviour
    {
        public System.Action<GameObject> Detection;

        void OnDestroy()
        {
            Detection?.Invoke(this.gameObject);
        }
    }
}
