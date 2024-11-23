using System;
using System.Collections.Generic;
using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;       
        public StateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {   
            _states = new Dictionary<Type, IExitableState>
            {                  
                [typeof(BootstrapState)] = new BootstrapState(this, services),                

                [typeof(MainMenuState)] = new MainMenuState(this, loadingCurtain, services.Single<IAssetProvider>()),
                [typeof(ChoiceOfCategoryState)] = new ChoiceOfCategoryState(this, services.Single<IChoiceOfCategoryService>()),

                //[typeof(TableProcessorState)] = new TableProcessorState(this),
                //[typeof(StatisticProcessorState)] = new StatisticProcessorState(this),

            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
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

        private TState GetState<TState>() where TState : class, IExitableState =>
          _states[typeof(TState)] as TState;
    }
}