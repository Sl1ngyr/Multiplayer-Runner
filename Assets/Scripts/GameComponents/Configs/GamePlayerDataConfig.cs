using System.Collections.Generic;
using Services.Avatar;
using UnityEngine;

namespace GameComponents.Configs
{
    public class GamePlayerDataConfig : MonoBehaviour
    {
        [field: SerializeField] public List<PlayerCarData> PlayersCars { get; private set; }
        [field: SerializeField] public List<AvatarData> AvatarData { get; private set; }
        
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
        
        public Sprite GetPlayerAvatarByID(int ID)
        {
            Sprite playerAvatar = null;
            
            foreach (var avatar in AvatarData)
            {
                if (avatar.ID == ID) playerAvatar = avatar.AvatarSprite;
            }

            if(playerAvatar == null) Debug.LogError("No sprite with this identifier exists");
            
            return playerAvatar;
        }
    }
}