using Services.Garage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services
{
    public class PlayerDataDisplayMainMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nickname;
        [SerializeField] private Vector3 _playerCarSpawnPosition;
        [SerializeField] private Image _avatar;
        
        [SerializeField] private GameObject _parentCarPreview;
        
        private PlayerDataConfig _playerDataConfig;

        private PreviewCar _previewCar;
        private int _avatarID;
        private int _carID;
        
        [Inject]
        private void Construct(PlayerDataConfig playerDataConfig)
        {
            _playerDataConfig = playerDataConfig;
        }
        
        public void InitPlayerDataUI(string nickname, int avatarID, int carID)
        {
            _previewCar = gameObject.AddComponent<PreviewCar>();
            
            _nickname.text = nickname;
            _avatarID = avatarID;
            _carID = carID;
            
            SearchForSelectedAvatar(_avatarID);
            SearchForSelectedCar(_carID);
        }

        public void UpdateDisplayNickname(string nickname)
        {
            _nickname.text = nickname;
        }

        public void UpdateDisplayAvatar(int id)
        {
            _avatar.sprite = _playerDataConfig.AvatarData[id].AvatarSprite;
        }
        
        public void SetNewNicknameUI(string nickname)
        {
            _nickname.text = nickname;
        }
         
        public void SearchForSelectedCar(int carID)
        {
            foreach (var carData in _playerDataConfig.GarageData)
            {
                if (carData.CarID == carID)
                {
                    Quaternion rotation;
                    
                    if (_previewCar.gameObject != null)
                    {
                        rotation = _previewCar.transform.rotation;
                    }
                    else
                    {
                        rotation = carData.GarageCar.transform.rotation;
                    }
                    
                    Destroy(_previewCar.gameObject);
                    _previewCar = Instantiate(carData.GarageCar, _playerCarSpawnPosition, rotation);
                    
                    _previewCar.transform.parent = _parentCarPreview.transform;
                }
            }
        }
        
        public void SearchForSelectedAvatar(int avatarID)
        {
            foreach (var avatarData in _playerDataConfig.AvatarData)
            {
                if (avatarData.ID == avatarID)
                {
                    _avatar.sprite = avatarData.AvatarSprite;
                }
            }
        }
    }
}