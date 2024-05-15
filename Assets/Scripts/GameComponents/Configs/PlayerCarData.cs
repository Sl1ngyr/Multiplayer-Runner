using UnityEngine;

namespace GameComponents.Configs
{
    [CreateAssetMenu(fileName = "PlayerCar", menuName = "ScriptableObjects/PlayerCarData")]
    public class PlayerCarData : ScriptableObject
    {
        [field: SerializeField] public int CarID { get; private set; }
        [field: SerializeField] public GameObject PlayerCar { get; private set; }
    }
}