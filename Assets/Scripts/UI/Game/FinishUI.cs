using Fusion;
using GameComponents.Configs;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class FinishUI : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI _placeText;
        [SerializeField] private TextMeshProUGUI _raceTimeText;
        [SerializeField] private TextMeshProUGUI _bestResultText;
        
        [SerializeField] private TextMeshProUGUI _nickname;
        [SerializeField] private Image _avatar;

        [SerializeField] private GamePlayerDataConfig _gamePlayerDataConfig;
        
        public void Init(string position, string time)
        {
            _placeText.text += position;
            _raceTimeText.text += time;

            LoadLocalPlayerData(time);
        }
        
        private void LoadLocalPlayerData(string result)
        {
            if (Runner.TryGetPlayerObject(Runner.LocalPlayer, out var networkPlayer))
            {
                string nickname = networkPlayer.GetComponent<DataCollector>().Nickname;
                string score = networkPlayer.GetComponent<DataCollector>().BestScore;
                int avatarID = networkPlayer.GetComponent<DataCollector>().AvatarID;
                
                _bestResultText.text += score;
                
                networkPlayer.GetComponent<DataCollector>().SetNewRecord(result);
                
                SetUserDataToUI(nickname, avatarID);
            }
        }

        private void SetUserDataToUI(string nickname, int avatarID)
        {
            _nickname.text = nickname;
            
            _avatar.sprite = _gamePlayerDataConfig.GetPlayerAvatarByID(avatarID);
        }
    }
}