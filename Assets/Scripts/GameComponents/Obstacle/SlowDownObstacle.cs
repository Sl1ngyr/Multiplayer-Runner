using UnityEngine;

namespace GameComponents.Obstacle
{
    public class SlowDownObstacle : BaseObstacle
    {
        [field: SerializeField] public float SlowCoefficient { get; private set; }
        [field: SerializeField] public float TimeDelay { get; private set; }
    }
}