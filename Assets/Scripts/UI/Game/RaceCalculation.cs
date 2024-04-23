using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Player;
using Services.Const;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class RaceCalculation : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        
        private bool _isRaceStarted = false;
        
        private Transform _localPlayer;
        private Transform _otherPlayer;
        
        [field: SerializeField] public TextMeshProUGUI PositionText { get; private set; }
        public float CurrentTime { get; private set; }
        
        public void Init()
        {
            CurrentTime = 0;
            
            SetPlayersData();
            _localPlayer.GetComponent<PlayerMovement>().RaceStarted();
            _isRaceStarted = true;
        }

        private void Update()
        {
            if(!_isRaceStarted) return;
            
            CurrentTime += Time.deltaTime;
            
            CalculateTime(CurrentTime);
            
            SetPosition(CalculationPosition());
        }

        private void SetPlayersData()
        {
            List<PlayerRef> playersRef = Runner.ActivePlayers.ToList();

            foreach (var player in playersRef)
            {
                if (Runner.TryGetPlayerObject(player, out NetworkObject networkPlayer))
                {
                    if (Runner.LocalPlayer == player)
                    {
                        _localPlayer = networkPlayer.transform;
                    }
                    else
                    {
                        _otherPlayer = networkPlayer.transform;
                    }
                }
            }
        }
        
        private void SetPosition(int position)
        {
            if (position == Constants.GAME_RACE_CALCULATION_FIRST_POSITION)
            {
                PositionText.text = Constants.GAME_RACE_FIRST_POSITION_TEXT;
            }
            else
            {
                PositionText.text = Constants.GAME_RACE_SECOND_POSITION_TEXT;
            }
            
        }

        private int CalculationPosition()
        {
            if (_localPlayer.position.z > _otherPlayer.position.z)
            {
                return Constants.GAME_RACE_CALCULATION_FIRST_POSITION;
            }

            return Constants.GAME_RACE_CALCULATION_SECOND_POSITION;
        }
        
        private void CalculateTime(float time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            
            _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
        }
    }
}