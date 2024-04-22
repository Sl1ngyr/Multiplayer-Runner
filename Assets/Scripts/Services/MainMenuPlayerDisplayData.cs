using System.Collections;
using Services.Garage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services
{
    public class MainMenuPlayerDisplayData : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nickname;
        [SerializeField] private Vector3 _playerCarSpawnPosition;
        [SerializeField] private Image _avatar;
        
        [SerializeField] private GameObject _parentCarPreview;
        
        private MainMenuPlayerDataConfig _mainMenuPlayerDataConfig;

        private PreviewCar _previewCar;
        private int _avatarID;
        private int _carID;
        private bool _isDataInitialized = false;
        
        [Inject]
        private void Construct(MainMenuPlayerDataConfig mainMenuPlayerDataConfig)
        {
            _mainMenuPlayerDataConfig = mainMenuPlayerDataConfig;
        }

        private void CreatePreviewCar(GarageData carData)
        {
            Quaternion rotation;
            
            if (_isDataInitialized)
            {
                rotation = carData.GarageCar.transform.rotation;
                _isDataInitialized = false;
            }
            else
            {
                rotation = _previewCar.transform.rotation;
            }
                    
            Destroy(_previewCar.gameObject);
            _previewCar = Instantiate(carData.GarageCar, _playerCarSpawnPosition, rotation);
                    
            _previewCar.transform.parent = _parentCarPreview.transform;
        }
        
        private void SearchForSelectedAvatar(int avatarID)
        {
            foreach (var avatarData in _mainMenuPlayerDataConfig.AvatarData)
            {
                if (avatarData.ID == avatarID)
                {
                    _avatar.sprite = avatarData.AvatarSprite;
                }
            }
        }
        
        public void InitPlayerDataUI(string nickname, int avatarID, int carID)
        {
            _previewCar = gameObject.AddComponent<PreviewCar>();
            
            _nickname.text = nickname;
            _avatarID = avatarID;
            _carID = carID;

            _isDataInitialized = true;
            
            SearchForSelectedAvatar(_avatarID);
            SearchForSelectedCar(_carID);
        }

        public void UpdateDisplayNickname(string nickname)
        {
            _nickname.text = nickname;
        }

        public void UpdateDisplayAvatar(int id)
        {
            _avatar.sprite = _mainMenuPlayerDataConfig.AvatarData[id].AvatarSprite;
        }

        public void SearchForSelectedCar(int carID)
        {
            foreach (var carData in _mainMenuPlayerDataConfig.GarageData)
            {
                if (carData.CarID == carID)
                {
                    CreatePreviewCar(carData);
                }
            }
        }
    }
}