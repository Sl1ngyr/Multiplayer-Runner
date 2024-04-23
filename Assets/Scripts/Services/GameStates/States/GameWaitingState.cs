using System.Linq;
using Fusion;
using Services.Const;
using UnityEngine;

namespace Services.GameStates.States
{
    public class GameWaitingState : GameState
    {
        private Canvas _waitingCanvas;
        
        public GameWaitingState(GameStateMachine gameStateMachine, 
            GameStatesManager gameStatesManager,  
            NetworkRunner runner, 
            Canvas waitingCanvas) : base(gameStateMachine, gameStatesManager, runner)
        {
            _waitingCanvas = waitingCanvas;
        }

        public override void EnterState()
        {
            _waitingCanvas.gameObject.SetActive(true);
        }

        public override void ExitState()
        {
            _waitingCanvas.gameObject.SetActive(false);
        }

        public override void Update()
        {

            if (Runner.ActivePlayers.Count() == Constants.RUNNER_MAX_PLAYER_IN_SESSION)
            {
                GameStatesManager.GameStateMachine.ChangeState(GameStatesManager.GamePreviewState);
            }
        }
        
    }
}