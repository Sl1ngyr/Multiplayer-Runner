using Services.Firebase;
using UI.MainMenu;
using UnityEngine;

namespace Services
{
    public class EntryPointMainMenu : MonoBehaviour
    {
        [SerializeField] private UpdateDataManager _updateDataManager;
        [SerializeField] private PlayerDataUI _playerDataUI;

        private void Awake()
        {
            _updateDataManager.InitDatabase();
            _playerDataUI.InitPlayerDataUI(_updateDataManager.UserNickname, _updateDataManager.UserAvatarID);
        }
    }
}