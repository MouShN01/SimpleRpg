using UnityEngine;
using Zenject;

namespace Other
{
    public class Billboard : MonoBehaviour
    {
        private UnityEngine.Camera _camera;

        [Inject]
        private void Construct(UnityEngine.Camera cam)
        {
            _camera = cam;
        }

        void LateUpdate()
        {
            if (_camera != null)
            {
                transform.forward = _camera.transform.forward;
            }
        }
    }
}