﻿using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
using System;
using System.Collections.Generic;

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

                [typeof(MainMenuState)] = new MainMenuState
                (
                    this,
                    loadingCurtain,
                    services.Single<IUIElementsProvider>(),
                    services.Single<ISaveloadDataService>()
                ),
                [typeof(ChoiceOfCategoryState)] = new ChoiceOfCategoryState
                (
                    this,
                    services.Single<IChoiceOfCategoryService>(),
                    services.Single<IPopUpMassageService>(),
                    new ChioceListAddation
                    (
                        services.Single<ISaveloadDataService>(),
                        services.Single<IUIElementsProvider>().GetItemsAddationViewElements(),
                        services.Single<IUIElementsProvider>().GetGlobalUIElements()
                    ),
                    services.Single<IUIElementsProvider>().GetGlobalUIElements()
                ),
                [typeof(TableProcessorState)] = new TableProcessorState
                (
                    this,                    
                    services.Single<ITableProcessorService>(),
                    new TableItemAddation
                    (
                        services.Single<ISaveloadDataService>(),
                        services.Single<IUIElementsProvider>().GetItemsAddationViewElements(),
                        services.Single<IUIElementsProvider>().GetGlobalUIElements()
                    ),
                    services.Single<IUIElementsProvider>().GetGlobalUIElements()
                ),               
                [typeof(StatisticProcessorState)] = new StatisticProcessorState(this),

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