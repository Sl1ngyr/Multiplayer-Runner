using Fusion;
using UnityEngine;
using System;
using GameComponents.Obstacle;

namespace Player
{
    public class PlayerCollisionDetector : NetworkBehaviour
    {
        public Action<float,float> OnSlowObstacleDetected;
        
        public Action<Vector3> OnPushBackObstacleDetected;
        
        public Action OnResetSpeedObstacleDetected;

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.TryGetComponent(out BaseObstacle obstacle))
            {
                DetectObstacle(obstacle.ObstacleType, obstacle);
            }
        }

        private void DetectObstacle(TypeObstacle typeObstacle, BaseObstacle obstacle)
        {
            switch (typeObstacle)
            {
                case TypeObstacle.PushBack:
                    OnResetSpeedObstacleDetected?.Invoke();
                    PushBackObstacle pushBackObstacle = obstacle.GetComponent<PushBackObstacle>();
                    OnPushBackObstacleDetected?.Invoke(pushBackObstacle.PushBackPositon);
                    break;
                
                case TypeObstacle.SlowDown:
                    SlowDownObstacle slowDownObstacle = obstacle.GetComponent<SlowDownObstacle>();
                    OnSlowObstacleDetected?.Invoke(slowDownObstacle.TimeDelay,slowDownObstacle.SlowCoefficient);
                    break;
                
                case TypeObstacle.ResetSpeed:
                    OnResetSpeedObstacleDetected?.Invoke();
                    break;
                
                default: Debug.LogError("Error");
                    break;
            }
        }
    }
}