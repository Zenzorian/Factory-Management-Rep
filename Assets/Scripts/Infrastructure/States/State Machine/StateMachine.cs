using Scripts.Infrastructure.AssetManagement;
using Scripts.MyTools;
using Scripts.Services;
using Scripts.Services.Statistics;
using System;
using System.Collections.Generic;
using Scripts.UI.Markers;

namespace Scripts.Infrastructure.States
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public StateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services, ICoroutineRunner сoroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services, сoroutineRunner),

                [typeof(MainMenuState)] = new MainMenuState
                (
                    this,
                    loadingCurtain,
                    services.Single<IElementsProvider>(),
                    services.Single<ISaveloadDataService>()
                ),
                [typeof(ChoiceOfCategoryState)] = new ChoiceOfCategoryState
                (
                    this,
                    services.Single<IChoiceOfCategoryService>(),
                    services.Single<IPopUpService>(),
                    new ChioceListAddation
                    (
                        services.Single<ISaveloadDataService>(),
                        services.Single<IElementsProvider>().ItemsAddationViewElements,
                        services.Single<IElementsProvider>().GlobalUIElements
                    ),
                    services.Single<IElementsProvider>().GlobalUIElements
                ),
                [typeof(TableProcessorState)] = new TableProcessorState
                    (
                        this,
                    services.Single<ITableProcessorService>(),
                    new TableItemAddation
                    (
                        services.Single<ISaveloadDataService>(),
                        services.Single<IElementsProvider>().ItemsAddationViewElements,
                        services.Single<IElementsProvider>().GlobalUIElements
                    ),
                    services.Single<IElementsProvider>().GlobalUIElements
                ),
                [typeof(SelectionOfStatisticsContextState)] = new SelectionOfStatisticsContextState
                (
                    this,
                    services.Single<IChoiceOfStatisticService>(),
                    services.Single<IElementsProvider>().GlobalUIElements
                ),
                [typeof(StatisticSelectionChoiceOfCategoryState)] = new StatisticSelectionChoiceOfCategoryState
                (
                    this,
                    services.Single<IChoiceOfCategoryService>(),
                    services.Single<IPopUpService>(),
                    services.Single<IElementsProvider>().GlobalUIElements
                ),
                [typeof(StatisticTableProcessorState)] = new StatisticTableProcessorState
                (
                    this,
                    services.Single<ITableProcessorService>(),
                    services.Single<IElementsProvider>().GlobalUIElements
                ),
                [typeof(ChoiceOfStatisticDataState)] = new ChoiceOfStatisticDataState
                (
                    this,
                    services.Single<IChoiceOfCategoryService>(),
                    services.Single<ISaveloadDataService>(),
                    services.Single<IPopUpService>(),
                    new StatisticDataItemAddation
                    (
                        services.Single<ISaveloadDataService>(),
                        services.Single<IElementsProvider>().ItemsAddationViewElements,
                        services.Single<IElementsProvider>().GlobalUIElements
                    ),
                     services.Single<IStatisticsInputService>(),
                     services.Single<IElementsProvider>().GlobalUIElements
                ),
                [typeof(StatisticGrafViewState)] = new StatisticGrafViewState
                (
                    this,
                    loadingCurtain,
                    services.Single<IStatisticsGraphViewService>(),
                    services.Single<IElementsProvider>()
                ),
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