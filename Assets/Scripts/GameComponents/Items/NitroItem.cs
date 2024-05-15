using Fusion;
using Player;
using UnityEngine;

namespace GameComponents.Items
{
    public class NitroItem : NetworkBehaviour
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