using Fusion;

namespace Services.GameStates.States
{
    public abstract class GameState
    {
        protected GameStateMachine GameStateMachine;
        protected GameStatesManager GameStatesManager;
        protected NetworkRunner Runner;
        
        public GameState(GameStateMachine gameStateMachine, GameStatesManager gameStatesManager, NetworkRunner runner)
        {
            GameStateMachine = gameStateMachine;
            GameStatesManager = gameStatesManager;
            Runner = runner;
        }
        
        public virtual void EnterState() {}
        public virtual void Update() {}
        public virtual void ExitState() {}
    }
}