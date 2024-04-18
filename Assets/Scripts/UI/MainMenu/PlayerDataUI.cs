using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace UI.MainMenu
{
    public class PlayerDataUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nickname;
        [SerializeField] private Image _avatar;

        public void InitPlayerDataUI(string nickname, int avatarID)
        {
            _nickname.text = nickname;
            
        }

        public void SetNewNicknameUI(string nickname)
        {
            _nickname.text = nickname;
        }
        
        public void SetNewAvatarUI(int avatarID)
        {

        }
    }
}