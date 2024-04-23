using System.Collections.Generic;
using System.Linq;
using Fusion;
using Player;
using Services.Const;
using UI.Game;

namespace Services.GameStates.States
{
    public class GameRaceState : GameState
    {
        private RaceCalculation _raceCalculationUI;
        private SpeedHandlerUI _speedHandlerUI;
        private PlayerMovement _localPlayer;
        private PlayerMovement _remotePlayer;
        
        public string PlayerPosition { get; private set; }
        public string PlayerScore { get; private set; }
        
        public GameRaceState(GameStateMachine gameStateMachine, 
            GameStatesManager gameStatesManager, 
            NetworkRunner runner, 
            RaceCalculation raceCalculationUI, 
            SpeedHandlerUI speedHandlerUI) : base(gameStateMachine, gameStatesManager, runner)
        {
            _raceCalculationUI = raceCalculationUI;
            _speedHandlerUI = speedHandlerUI;
        }

        public override void EnterState()
        {
            _raceCalculationUI.gameObject.SetActive(true);
            _speedHandlerUI.gameObject.SetActive(true);
            
            _raceCalculationUI.Init();
            _speedHandlerUI.Init();

            List<PlayerRef> playerRefs = Runner.ActivePlayers.ToList();

            foreach (var player in playerRefs)
            {
                if (Runner.TryGetPlayerObject(player, out var networkPlayer))
                {
                    if (Runner.LocalPlayer == player)
                    {
                        _localPlayer = networkPlayer.GetComponent<PlayerMovement>();
                    }
                    else
                    {
                        _remotePlayer = networkPlayer.GetComponent<PlayerMovement>();
                    }
                }
            }
        }

        public override void ExitState()
        {
            PlayerPosition = _raceCalculationUI.PositionText.text;
            PlayerScore = _raceCalculationUI.CurrentTime.ToString();
            
            _raceCalculationUI.gameObject.SetActive(false);
            _speedHandlerUI.gameObject.SetActive(false);
        }

        public override void Update()
        {
            _speedHandlerUI.SetSpeed(_localPlayer.CurrentSpeed);
            _speedHandlerUI.SetNitro(_localPlayer.IsNitroChargeReady);
            
            if (Runner.ActivePlayers.Count() != Constants.RUNNER_MAX_PLAYER_IN_SESSION)
            {
                GameStatesManager.IsRemotePlayerLeft = true;
                GameStatesManager.GameStateMachine.ChangeState(GameStatesManager.GameFinishState);
            }

            if (_localPlayer.IsPlayerFinished)
            {
                if (_remotePlayer.IsPlayerFinished)
                {
                    GameStatesManager.NumberFinishedPlayers++;
                }

                GameStatesManager.NumberFinishedPlayers++;
                
                GameStatesManager.GameStateMachine.ChangeState(GameStatesManager.GameFinishState);
            }
        }
    }
}