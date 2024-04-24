using System.Collections.Generic;
using Services.Avatar;
using Services.Const;
using Services.Firebase;
using TMPro;
using UI.AuthMenu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.MainMenu
{
    public class PlayerSettings : MonoBehaviour
    {
        [Header("Update nickname")]
        [SerializeField] private TMP_InputField _nicknameField;
        [SerializeField] private Button _confirmUpdateNickname;
        [SerializeField] private int _minLettersToChangeNickname = 3;
        
        [Space]
        [Header("Update avatar")]
        [SerializeField] private List<AvatarButton> _avatarButtons;
        
        [Space]
        [SerializeField] private Button _backToMainMenuButton;

        [Space] 
        [SerializeField] private PopUpMessageHandler _popUpMessageHandler;
        
        private UpdateDataManager _updateDataManager;
        private UIMainMenuManager _uiMainMenuManager;
        private MainMenuPlayerDisplayData _mainMenuPlayerDisplayData;
        
        [Inject]
        private void Construct
            (UpdateDataManager updateDataManager, 
            UIMainMenuManager uiMainMenuManager, 
            MainMenuPlayerDisplayData mainMenuPlayerDisplayData)
        {
            _updateDataManager = updateDataManager;
            _uiMainMenuManager = uiMainMenuManager;
            _mainMenuPlayerDisplayData = mainMenuPlayerDisplayData;
        }

        private void ConfirmUpdateAvatar()
        {
            var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            var buttonAvatarID = button.GetComponent<AvatarButton>().AvatarID;

            if (buttonAvatarID == _updateDataManager.UserAvatarID)
            {
                _popUpMessageHandler.SetUpMessageToPopUp(Constants.PLAYER_SETTINGS_AVATAR_ERROR_MESSAGE_SAME_AVATAR);
                return;
            }
            
            _popUpMessageHandler.SetUpMessageToPopUp(Constants.PLAYER_SETTINGS_AVATAR_SUCCESSFULLY_CHANGED);
            _mainMenuPlayerDisplayData.UpdateDisplayAvatar(buttonAvatarID);
            _updateDataManager.InitUpdateAvatarID(buttonAvatarID);
        }
        
        private void ConfirmUpdateNickname()
        {
            if (_nicknameField.text.Length < _minLettersToChangeNickname)
            {
                _popUpMessageHandler.SetUpMessageToPopUp(Constants.PLAYER_SETTINGS_NICKNAME_ERROR_MESSAGE_LESS_CHARACTERS);
                return;
            }

            if (_nicknameField.text == _updateDataManager.UserNickname)
            {
                _popUpMessageHandler.SetUpMessageToPopUp(Constants.PLAYER_SETTINGS_NICKNAME_ERROR_MESSAGE_SAME_NICKNAME);
                return;
            }
            
            _popUpMessageHandler.SetUpMessageToPopUp(Constants.PLAYER_SETTINGS_NICKNAME_SUCCESSFULLY_CHANGED);
            _updateDataManager.InitUpdateNickname(_nicknameField.text);
            _mainMenuPlayerDisplayData.UpdateDisplayNickname(_nicknameField.text);
        }
        
        private void BackToMainMenu()
        {
            _uiMainMenuManager.ManagementStatusStartCanvases(true);
            gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            _confirmUpdateNickname.onClick.AddListener(ConfirmUpdateNickname);
            _backToMainMenuButton.onClick.AddListener(BackToMainMenu);
                
            foreach (var button in _avatarButtons)
            {
                button.onClick.AddListener(ConfirmUpdateAvatar);
            }
        }

        private void OnDisable()
        {
            _confirmUpdateNickname.onClick.RemoveListener(ConfirmUpdateNickname);
            _backToMainMenuButton.onClick.RemoveListener(BackToMainMenu);
            
            foreach (var button in _avatarButtons)
            {
                button.onClick.RemoveListener(ConfirmUpdateAvatar);
            }
        }
        
    }
    
}