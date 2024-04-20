using Services;
using Services.Firebase;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private UpdateDataManager _updateDataManager;
        [SerializeField] private PlayerDataConfig _playerDataConfig;
        [FormerlySerializedAs("_playerDataDisplayMainMenu")] [SerializeField] private MainMenuPlayerDisplayData mainMenuPlayerDisplayData;
        [SerializeField] private UIMainMenuManager _uiMainMenuManager;
        
        public override void InstallBindings()
        {
            Container.Bind<UpdateDataManager>().FromInstance(_updateDataManager);
            Container.Bind<PlayerDataConfig>().FromInstance(_playerDataConfig);
            Container.Bind<MainMenuPlayerDisplayData>().FromInstance(mainMenuPlayerDisplayData);
            Container.Bind<UIMainMenuManager>().FromInstance(_uiMainMenuManager);
        }
    }
}