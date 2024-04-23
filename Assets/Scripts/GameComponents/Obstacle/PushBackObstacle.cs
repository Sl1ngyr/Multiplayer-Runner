using UnityEngine;

namespace GameComponents.Obstacle
{
    public class PushBackObstacle : BaseObstacle
    {
        [field: SerializeField] public Vector3 PushBackPositon { get; private set; }
    }
}