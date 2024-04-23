using Fusion;
using Services.GameStates.States;
using Services.Scene;
using UI.Game;
using UnityEngine;
using Zenject;

namespace Services.GameStates
{
    public class GameStatesManager : NetworkBehaviour
    {
        [SerializeField] private Canvas _waitingUI;
        [SerializeField] private PreviewPlayers _previewPlayersUI;
        [SerializeField] private PreparationForStart _preparationForStartUI;
        [SerializeField] private RaceCalculation _raceCalculationUI;
        [SerializeField] private SpeedHandlerUI _speedHandlerUI;
        [SerializeField] private FinishUI _finishUI;

        [SerializeField] public int SceneBuildIndexAfterPlayersFinished = 1;
        
        [Networked] public NetworkBool IsRemotePlayerLeft { get; set; }
        [Networked] public int NumberFinishedPlayers { get; set; }
        
        public GameStateMachine GameStateMachine { get; private set; }
        public GameWaitingState GameWaitingState { get; private set; }
        public GamePreviewState GamePreviewState { get; private set; }
        public GamePreparationForStartState GamePreparationForStartState { get; private set; }
        public GameRaceState GameRaceState { get; private set; }
        public GameFinishState GameFinishState { get; private set; }

        [HideInInspector] public SceneLoader SceneLoader;
        
        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            SceneLoader = sceneLoader;
        }
        
        public override void Spawned()
        {
            GameStateMachine = new GameStateMachine();
            GameWaitingState = new GameWaitingState(GameStateMachine, this, Runner, _waitingUI);
            GamePreviewState = new GamePreviewState(GameStateMachine, this, Runner, _previewPlayersUI);
            GamePreparationForStartState = new GamePreparationForStartState(GameStateMachine, this, Runner, _preparationForStartUI);
            GameRaceState = new GameRaceState(GameStateMachine, this, Runner, _raceCalculationUI, _speedHandlerUI);
            GameFinishState = new GameFinishState(GameStateMachine, this, Runner, _finishUI);
            GameStateMachine.InitState(GameWaitingState);
        }

        private void Update()
        {
            if(GameStateMachine == null) return;
            
            GameStateMachine.CurrentGameState.Update();
        }
    }
}