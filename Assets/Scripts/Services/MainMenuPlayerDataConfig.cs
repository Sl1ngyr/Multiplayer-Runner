using System.Collections.Generic;
using Services.Avatar;
using Services.Garage;
using UnityEngine;

namespace Services
{
    public class MainMenuPlayerDataConfig : MonoBehaviour
    {
        [field: SerializeField] public List<AvatarData> AvatarData { get; private set; }
        [field: SerializeField] public List<GarageData> GarageData { get; private set; }
    }
}