using Fusion;
using GameComponents.Configs;
using Services.Const;
using UnityEngine;

namespace Services.Network
{
    public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
    {
        [SerializeField] private GamePlayerDataConfig _playerDataConfig;

        public void PlayerJoined(PlayerRef player)
        {
            if (Runner.LocalPlayer == player)
            {
                int carID = PlayerPrefs.GetInt(Constants.PLAYER_PREFS_CAR_ID);

                if (Runner.LocalPlayer.PlayerId == 1)
                {
                    NetworkObject networkPlayer = Runner.Spawn(_playerDataConfig.GetPlayerCarByID(carID), 
                        new Vector3(-Constants.RUNNER_OFFSET_TO_SPAWN_PLAYER, 0,0), 
                        Quaternion.identity);
                    
                    Runner.SetPlayerObject(player, networkPlayer);
                }
                else
                {
                    NetworkObject networkPlayer = Runner.Spawn(_playerDataConfig.GetPlayerCarByID(carID), 
                        new Vector3(Constants.RUNNER_OFFSET_TO_SPAWN_PLAYER, 0,0), 
                        Quaternion.identity);
                    
                    Runner.SetPlayerObject(player, networkPlayer);
                }
                
            }
        }
        
    }
}