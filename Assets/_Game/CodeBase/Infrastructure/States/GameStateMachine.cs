using System;
using System.Collections.Generic;
using _Game.CodeBase.Infrastructure.Factory;
using _Game.CodeBase.Infrastructure.Services;
using _Game.CodeBase.Infrastructure.Services.PersistantProgress;
using _Game.CodeBase.Infrastructure.Services.SaveLoad;
using _Game.CodeBase.Ui;

namespace _Game.CodeBase.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, services.Single<IGameFactory>(), services.Single<IPersistantProgressService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistantProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader, curtain),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }


        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }


        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;
    }
}