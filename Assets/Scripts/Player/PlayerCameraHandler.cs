using CameraComponents;
using Fusion;
using UnityEngine;

namespace Player
{
    public class PlayerCameraHandler : NetworkBehaviour
    {
        private Camera _camera;

        public override void Spawned()
        {
            if (HasStateAuthority)
            {
                _camera = Camera.main;
                
                _camera.GetComponent<CameraFollow>().CameraAnchorPoint = transform;
            }
        }
    }
}