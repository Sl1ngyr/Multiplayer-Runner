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
        [FormerlySerializedAs("_playerDataConfig")] [SerializeField] private MainMenuPlayerDataConfig _mainMenuPlayerDataConfig;
        [SerializeField] private MainMenuPlayerDisplayData mainMenuPlayerDisplayData;
        [SerializeField] private UIMainMenuManager _uiMainMenuManager;
        
        public override void InstallBindings()
        {
            Container.Bind<UpdateDataManager>().FromInstance(_updateDataManager);
            Container.Bind<MainMenuPlayerDataConfig>().FromInstance(_mainMenuPlayerDataConfig);
            Container.Bind<MainMenuPlayerDisplayData>().FromInstance(mainMenuPlayerDisplayData);
            Container.Bind<UIMainMenuManager>().FromInstance(_uiMainMenuManager);
        }
    }
}