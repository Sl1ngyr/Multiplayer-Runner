using Services.Garage;
using Services.Leaderboard;
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
        [SerializeField] private LeaderboardManager _leaderboardManager;
        
        [Space]
        [Header("Main menu buttons")]
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _openGarage;
        [SerializeField] private Button _openLeaderboard;
        [SerializeField] private Button _openPlayerSettings;

        [Space]
        [SerializeField] private int _gameSceneBuildIndex = 2;
        
        private SceneLoader _sceneLoader;
        private MainMenuPlayerDisplayData _mainMenuPlayerDisplayData;
        
        [Inject]
        private void Construct(SceneLoader sceneLoader, MainMenuPlayerDisplayData mainMenuPlayerDisplayData)
        {
            _sceneLoader = sceneLoader;
            _mainMenuPlayerDisplayData = mainMenuPlayerDisplayData;
        }
        
        public void ManagementStatusStartCanvases(bool status)
        {
            _mainMenu.gameObject.SetActive(status);
            _playerDisplayData.gameObject.SetActive(status);
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
            
            _mainMenuPlayerDisplayData.SearchForSelectedCar(0);
            
            _garageManager.gameObject.SetActive(true);
        }

        private void OpenLeaderboard()
        {
            ManagementStatusStartCanvases(false);
            
            _leaderboardManager.gameObject.SetActive(true);
            _leaderboardManager.Init();
            
            
        }
        
        private void OnEnable()
        {
            _startGame.onClick.AddListener(StartGame);
            _openGarage.onClick.AddListener(OpenGarage);
            _openPlayerSettings.onClick.AddListener(OpenPlayerSettings);
            _openLeaderboard.onClick.AddListener(OpenLeaderboard);
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveListener(StartGame);
            _openGarage.onClick.RemoveListener(OpenGarage);
            _openPlayerSettings.onClick.RemoveListener(OpenPlayerSettings);
            _openLeaderboard.onClick.RemoveListener(OpenLeaderboard);
        }
        
    }
}