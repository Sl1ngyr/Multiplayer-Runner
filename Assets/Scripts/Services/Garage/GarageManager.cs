using Services.Firebase;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services.Garage
{
    public class GarageManager : MonoBehaviour
    {
        [SerializeField] private Button _selectCarButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _backButton;

        private MainMenuPlayerDisplayData _mainMenuPlayerDisplayData;
        private MainMenuPlayerDataConfig _mainMenuPlayerDataConfig;
        private UIMainMenuManager _uiMainMenuManager;
        private UpdateDataManager _updateDataManager;
        
        private int _currentCar;
        private int _maxNumberOfCars;
        
        [Inject]
        private void Construct
            (MainMenuPlayerDataConfig mainMenuPlayerDataConfig, 
            MainMenuPlayerDisplayData mainMenuPlayerDisplayData, 
            UIMainMenuManager uiMainMenuManager,
            UpdateDataManager updateDataManager)
        {
            
            _mainMenuPlayerDataConfig = mainMenuPlayerDataConfig;
            _mainMenuPlayerDisplayData = mainMenuPlayerDisplayData;
            _uiMainMenuManager = uiMainMenuManager;
            _updateDataManager = updateDataManager;
        }

        private void Start()
        {
            _maxNumberOfCars = _mainMenuPlayerDataConfig.GarageData.Count - 1;
        }

        public void Init()
        {
            _currentCar = 0;
        }

        private void ShowNextCar()
        {
            if(_currentCar + 1 > _maxNumberOfCars) return;
            
            _currentCar++;
            
            _mainMenuPlayerDisplayData.SearchForSelectedCar(_currentCar);
        }

        private void ShowPreviousCar()
        {
            if (_currentCar - 1 < 0) return;
            
            _currentCar--;
            
            _mainMenuPlayerDisplayData.SearchForSelectedCar(_currentCar);
        }

        private void SelectCar()
        {
            _updateDataManager.InitUpdateCarID(_currentCar);
            
            _uiMainMenuManager.ManagementStatusStartCanvases(true);
            
            gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            _nextButton.onClick.AddListener(ShowNextCar);
            _backButton.onClick.AddListener(ShowPreviousCar);
            _selectCarButton.onClick.AddListener(SelectCar);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(ShowNextCar);
            _backButton.onClick.RemoveListener(ShowPreviousCar);
            _selectCarButton.onClick.RemoveListener(SelectCar);
        }
    }
}