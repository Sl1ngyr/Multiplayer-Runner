using Fusion;
using UnityEngine;

namespace GameComponents.Obstacle
{
    public enum TypeObstacle
    {
        PushBack,
        ResetSpeed,
        SlowDown
    }
    
    public class BaseObstacle : NetworkBehaviour
    {
        [field: SerializeField] public TypeObstacle ObstacleType { get; private set; }
    }
}