using Services.Const;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class PreparationForStart : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        
        public void Init()
        {
            SetTimerText(Constants.GAME_TIME_FOR_PREPARATION);
        }

        public void SetTimerText(int time)
        {
            _timerText.text = time.ToString();
        }
    }
}