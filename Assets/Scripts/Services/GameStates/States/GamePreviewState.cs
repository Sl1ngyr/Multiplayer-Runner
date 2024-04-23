using Fusion;
using Services.Const;
using UI.Game;

namespace Services.GameStates.States
{
    public class GamePreviewState : GameState
    {
        private PreviewPlayers _previewPlayersUI;
        
        [Networked] private TickTimer _tickTimer { get; set; }
        
        public GamePreviewState(GameStateMachine gameStateMachine, 
            GameStatesManager gameStatesManager, 
            NetworkRunner runner, 
            PreviewPlayers previewPlayersUI) : base(gameStateMachine, gameStatesManager, runner)
        {
            _previewPlayersUI = previewPlayersUI;
        }

        public override void EnterState()
        {
            _previewPlayersUI.gameObject.SetActive(true);
            
            _previewPlayersUI.Init();
            
            _tickTimer = TickTimer.CreateFromSeconds(Runner,Constants.GAME_TIME_FOR_PREVIEW_PLAYERS);
        }

        public override void ExitState()
        {
            _previewPlayersUI.gameObject.SetActive(false);
        }

        public override void Update()
        {
            if (_tickTimer.Expired(Runner))
            {
                GameStatesManager.GameStateMachine.ChangeState(GameStatesManager.GamePreparationForStartState);
            }
        }
    }
}