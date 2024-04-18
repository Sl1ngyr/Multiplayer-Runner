using Services;
using Services.Firebase;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private UpdateDataManager _updateDataManager;
        [SerializeField] private PlayerDataConfig _playerDataConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<UpdateDataManager>().FromInstance(_updateDataManager);
            Container.Bind<PlayerDataConfig>().FromInstance(_playerDataConfig);
        }
    }
}