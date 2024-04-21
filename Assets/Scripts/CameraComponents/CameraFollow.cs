using UnityEngine;

namespace CameraComponents
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _cameraAnchorPoint;
        
        [SerializeField] private Vector3 _cameraOffest;
        
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