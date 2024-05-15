using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Leaderboard
{
    public class LeaderboardRow : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _position;
        [SerializeField] private TextMeshProUGUI _nickname;
        [SerializeField] private TextMeshProUGUI _score;

        [SerializeField] private Color _colorLocalPlayerInTop;
        
        public void Init(int position, string nickname, Sprite avatar, string score, bool isLocalPlayerInTop)
        {
            if (isLocalPlayerInTop)
            {
                _background.color = _colorLocalPlayerInTop;
            }
            
            _position.text = position.ToString();
            _nickname.text = nickname;
            _avatar.sprite = avatar;
            _score.text = score;
        }
    }
}