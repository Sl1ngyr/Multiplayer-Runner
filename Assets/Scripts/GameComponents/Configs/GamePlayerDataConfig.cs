using System.Collections.Generic;
using UnityEngine;

namespace GameComponents.Configs
{
    public class GamePlayerDataConfig : MonoBehaviour
    {
        [field: SerializeField] public List<PlayerCarData> PlayersCars { get; private set; }

        public GameObject GetPlayerCarByID(int ID)
        {
            GameObject playerCar = null;
            
            foreach (var car in PlayersCars)
            {
                if(car.CarID == ID) playerCar = car.PlayerCar;
            }

            if(playerCar == null) Debug.LogError("There is no car with this ID");
            
            return playerCar;
        }
    }
}