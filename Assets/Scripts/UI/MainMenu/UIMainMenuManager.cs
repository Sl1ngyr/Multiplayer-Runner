using Services;
using Services.Garage;
using Services.Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.MainMenu
{
    public class UIMainMenuManager : MonoBehaviour
    {
        [Header("UI canvases")]
        [SerializeField] private Canvas _mainMenu;
        [SerializeField] private Canvas _playerDisplayData;
        [SerializeField] private Canvas _playerSettings;
        [SerializeField] private GarageManager _garageManager;
        
        [Space]
        [Header("Main menu buttons")]
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _openGarage;
        [SerializeField] private Button _openLeaderboard;
        [SerializeField] private Button _openPlayerSettings;

        [Space]
        [SerializeField] private int _gameSceneBuildIndex = 2;
        
        private SceneLoader _sceneLoader;
        private PlayerDataDisplayMainMenu _playerDataDisplayMainMenu;
        
        [Inject]
        private void Construct(SceneLoader sceneLoader, PlayerDataDisplayMainMenu playerDataDisplayMainMenu)
        {
            _sceneLoader = sceneLoader;
            _playerDataDisplayMainMenu = playerDataDisplayMainMenu;
        }
        
        private void StartGame()
        {
            _sceneLoader.TransitionToSceneByIndex(_gameSceneBuildIndex);
        }

        private void OpenPlayerSettings()
        {
            ManagementStatusStartCanvases(false);
            _playerSettings.gameObject.SetActive(true);
        }
        
        private void OpenGarage()
        {
            ManagementStatusStartCanvases(false);
            
            _garageManager.Init();
            
            _playerDataDisplayMainMenu.SearchForSelectedCar(0);
            
            _garageManager.gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            _startGame.onClick.AddListener(StartGame);
            _openGarage.onClick.AddListener(OpenGarage);
            _openPlayerSettings.onClick.AddListener(OpenPlayerSettings);
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveListener(StartGame);
            _openGarage.onClick.RemoveListener(OpenGarage);
            _openPlayerSettings.onClick.RemoveListener(OpenPlayerSettings);
        }
        
        public void ManagementStatusStartCanvases(bool status)
        {
            _mainMenu.gameObject.SetActive(status);
            _playerDisplayData.gameObject.SetActive(status);
        }
    }
}