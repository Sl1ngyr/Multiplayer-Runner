﻿using Services.Firebase;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Services.Garage
{
    public class GarageManager : MonoBehaviour
    {
        [SerializeField] private Button _selectCarButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _backButton;

        private PlayerDataDisplayMainMenu _playerDataDisplayMainMenu;
        private PlayerDataConfig _playerDataConfig;
        private UIMainMenuManager _uiMainMenuManager;
        private UpdateDataManager _updateDataManager;
        
        private int _currentCar;
        private int _maxNumberOfCars;
        

        [Inject]
        private void Construct
            (PlayerDataConfig playerDataConfig, 
            PlayerDataDisplayMainMenu playerDataDisplayMainMenu, 
            UIMainMenuManager uiMainMenuManager,
            UpdateDataManager updateDataManager)
        {
            
            _playerDataConfig = playerDataConfig;
            _playerDataDisplayMainMenu = playerDataDisplayMainMenu;
            _uiMainMenuManager = uiMainMenuManager;
            _updateDataManager = updateDataManager;
        }

        private void Start()
        {
            _maxNumberOfCars = _playerDataConfig.GarageData.Count - 1;
        }

        public void Init()
        {
            _currentCar = 0;
        }

        private void ShowNextCar()
        {
            if(_currentCar + 1 > _maxNumberOfCars) return;
            
            _currentCar++;
            
            _playerDataDisplayMainMenu.SearchForSelectedCar(_currentCar);
        }

        private void ShowPreviousCar()
        {
            if (_currentCar - 1 < 0) return;
            
            _currentCar--;
            
            _playerDataDisplayMainMenu.SearchForSelectedCar(_currentCar);
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