using Services;
using Services.Firebase;
using UI.MainMenu;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private UpdateDataManager _updateDataManager;
        [SerializeField] private PlayerDataConfig _playerDataConfig;
        [SerializeField] private PlayerDataDisplayMainMenu _playerDataDisplayMainMenu;
        [SerializeField] private UIMainMenuManager _uiMainMenuManager;
        
        public override void InstallBindings()
        {
            Container.Bind<UpdateDataManager>().FromInstance(_updateDataManager);
            Container.Bind<PlayerDataConfig>().FromInstance(_playerDataConfig);
            Container.Bind<PlayerDataDisplayMainMenu>().FromInstance(_playerDataDisplayMainMenu);
            Container.Bind<UIMainMenuManager>().FromInstance(_uiMainMenuManager);
        }
    }
}