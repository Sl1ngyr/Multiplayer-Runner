using UnityEngine;

namespace Services.Garage
{
    [CreateAssetMenu(fileName = "GarageCar", menuName = "ScriptableObjects/GarageData")]
    public class GarageData : ScriptableObject
    {
        [field: SerializeField] public int CarID { get; private set; }
        [field: SerializeField] public PreviewCar GarageCar { get; private set; }
    }
}