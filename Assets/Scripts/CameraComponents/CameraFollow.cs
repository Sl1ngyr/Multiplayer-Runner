using UnityEngine;

namespace CameraComponents
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _cameraOffest;
        
        private Transform _cameraAnchorPoint;
        
        public Transform CameraAnchorPoint
        {
            get => _cameraAnchorPoint;
            set => _cameraAnchorPoint = value;
        }
        
        private void LateUpdate()
        {
            if(_cameraAnchorPoint == null) return;

            transform.position = _cameraAnchorPoint.position + _cameraOffest;
        }
    }
}