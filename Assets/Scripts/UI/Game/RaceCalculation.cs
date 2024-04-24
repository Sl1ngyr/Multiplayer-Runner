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
        private bool _isRaceStarted = false;
        private float _currentTime;
        
        private Transform _localPlayer;
        private Transform _otherPlayer;
        
        [field: SerializeField] public TextMeshProUGUI PositionText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TimerText { get; private set; }
        
        public void Init()
        {
            _currentTime = 0;
            
            SetPlayersData();
            _localPlayer.GetComponent<PlayerMovement>().RaceStarted();
            _isRaceStarted = true;
        }

        private void Update()
        {
            if(!_isRaceStarted) return;
            
            _currentTime += Time.deltaTime;
            
            CalculateTime(_currentTime);
            
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
            
            TimerText.text = timeSpan.ToString(@"mm\:ss\:ff");
        }
    }
}