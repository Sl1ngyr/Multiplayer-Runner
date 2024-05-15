using Player;
using UnityEngine;

namespace GameComponents.Obstacle
{
    public class ResetSpeedObstacle : BaseObstacle
    {
        private void OnTriggerEnter(Collider coll)
        {
            if (coll.TryGetComponent(out PlayerMovement player))
            {
                Runner.Despawn(Object);
            }
        }
    }
}