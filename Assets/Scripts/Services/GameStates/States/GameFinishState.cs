using System.Collections.Generic;
using System.Linq;
using Fusion;
using Player;
using Services.Const;
using UI.Game;

namespace Services.GameStates.States
{
    public class GameFinishState : GameState
    {
        private TickTimer _tickTimer;
        
        private FinishUI _finishUI;
        
        private string _playerPosition;
        private string _raceTime;
        private bool _isTimerStarted = false;

        private PlayerMovement _remotePlayer;
        
        public GameFinishState(GameStateMachine gameStateMachine, 
            GameStatesManager gameStatesManager, 
            NetworkRunner runner, 
            FinishUI finishUI) : base(gameStateMachine, gameStatesManager, runner)
        {
            _finishUI = finishUI;
        }

        public override void EnterState()
        {
            _finishUI.gameObject.SetActive(true);
            
            if (GameStatesManager.IsRemotePlayerLeft)
            {
                _playerPosition = Constants.GAME_RACE_FIRST_POSITION_TEXT;
            }
            else
            {
                SetFinishedRemotePlayerData();
                
                _playerPosition = GameStatesManager.GameRaceState.PlayerPosition;
            }
            
            _raceTime = GameStatesManager.GameRaceState.PlayerScore;
            
            _finishUI.Init(_playerPosition, _raceTime);
        }

        private void SetFinishedRemotePlayerData()
        {
            if (GameStatesManager.NumberFinishedPlayers != Constants.RUNNER_MAX_PLAYER_IN_SESSION)
            {
                List<PlayerRef> playerRefs = Runner.ActivePlayers.ToList();

                foreach (var player in playerRefs)
                {
                    if (Runner.TryGetPlayerObject(player, out NetworkObject networkPlayer))
                    {
                        if (Runner.LocalPlayer != player)
                        {
                            _remotePlayer = networkPlayer.GetComponent<PlayerMovement>();
                        }
                    }
                }
            }
        }
        
        public override void Update()
        {
            IsRemotePlayerFinishedOrLeft();

            IsAllPlayersFinished();

            IsRemotePlayerLeftBeforeLocalPlayerFinished();
            
            if (_tickTimer.Expired(Runner) && _isTimerStarted)
            {
                Runner.Shutdown();
                GameStatesManager.SceneLoader.TransitionToSceneByIndex(GameStatesManager.SceneBuildIndexAfterPlayersFinished);
            }
        }

        private void IsRemotePlayerLeftBeforeLocalPlayerFinished()
        {
            if (GameStatesManager.IsRemotePlayerLeft && !_isTimerStarted)
            {
                _tickTimer = TickTimer.CreateFromSeconds(Runner, Constants.GAME_TIME_FOR_LEAVE_AFTER_FINISH_PREVIEW);
                _isTimerStarted = true;
            }
        }
        
        private void IsAllPlayersFinished()
        {
            if (GameStatesManager.NumberFinishedPlayers == Constants.RUNNER_MAX_PLAYER_IN_SESSION && !_isTimerStarted)
            {
                _tickTimer = TickTimer.CreateFromSeconds(Runner, Constants.GAME_TIME_FOR_LEAVE_AFTER_FINISH_PREVIEW);
                _isTimerStarted = true;
            }
        }
        
        private void IsRemotePlayerFinishedOrLeft()
        {
            if (GameStatesManager.NumberFinishedPlayers != Constants.RUNNER_MAX_PLAYER_IN_SESSION &&
                !GameStatesManager.IsRemotePlayerLeft)
            {
                if (Runner.ActivePlayers.Count() != Constants.RUNNER_MAX_PLAYER_IN_SESSION)
                {
                    GameStatesManager.IsRemotePlayerLeft = true;
                }
                
                if (_remotePlayer.IsPlayerFinished)
                {
                    GameStatesManager.NumberFinishedPlayers++;
                }
            }
        }
    }
}