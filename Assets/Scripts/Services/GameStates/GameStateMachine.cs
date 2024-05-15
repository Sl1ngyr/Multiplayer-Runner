using Services.GameStates.States;

namespace Services.GameStates
{
    public class GameStateMachine
    {
        public GameState CurrentGameState { get; set; }
        
        public void InitState(GameState gameState)
        {
            CurrentGameState = gameState;
            CurrentGameState.EnterState();
        }

        public void ChangeState(GameState gameState)
        {
            CurrentGameState.ExitState();
            CurrentGameState = gameState;
            CurrentGameState.EnterState();
        }
    }
}