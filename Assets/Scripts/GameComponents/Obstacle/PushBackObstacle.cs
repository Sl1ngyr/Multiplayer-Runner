using Player;
using UnityEngine;

namespace GameComponents.Obstacle
{
    public class PushBackObstacle : BaseObstacle
    {
        [field: SerializeField] public Vector3 PushBackPositon { get; private set; }
        
        private void OnTriggerEnter(Collider coll)
        {
            if (coll.TryGetComponent(out PlayerMovement player))
            {
                Runner.Despawn(Object);
            }
        }
    }
}