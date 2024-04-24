using Fusion;
using Services.Const;
using UI.Game;
using UnityEngine;

namespace Services.GameStates.States
{
    public class GamePreparationForStartState : GameState
    {
        private PreparationForStart _preparationForStartUI;
        
        private float _countdownTimer;
        
        public GamePreparationForStartState(GameStateMachine gameStateMachine, 
            GameStatesManager gameStatesManager, 
            NetworkRunner runner, 
            PreparationForStart preparationForStart) : base(gameStateMachine, gameStatesManager, runner)
        {
            _preparationForStartUI = preparationForStart;
        }
        
        public override void EnterState()
        {
            _preparationForStartUI.gameObject.SetActive(true);
            
            _preparationForStartUI.Init();
            
            _countdownTimer = Constants.GAME_TIME_FOR_PREPARATION;
        }

        public override void ExitState()
        {
            _preparationForStartUI.gameObject.SetActive(false);
        }

        public override void Update()
        {
            if (_countdownTimer <= 0)
            {
                _preparationForStartUI.SetTimerText(0);
                
                GameStatesManager.GameStateMachine.ChangeState(GameStatesManager.GameRaceState);
            }
            else
            {
                _preparationForStartUI.SetTimerText(Mathf.FloorToInt(_countdownTimer));
            }
            
            _countdownTimer -= Time.deltaTime;
        }
    }
}