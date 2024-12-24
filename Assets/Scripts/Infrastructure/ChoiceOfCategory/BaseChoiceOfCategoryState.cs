using Scripts.Infrastructure.AssetManagement;
using Scripts.Services;
using UnityEngine.Events;

namespace Scripts.Infrastructure.States
{
    public abstract class BaseChoiceOfCategoryState<TStateData> : IPayloadedState<TStateData>
    {
        protected readonly StateMachine _stateMachine;
        protected readonly IChoiceOfCategoryService _choiceOfCategoryService;
        protected readonly IPopUpMassageService _popUpMassageService;
        protected readonly GlobalUIElements _globalUIElements;

        protected UnityEvent<MainMenuTypes, int> _choiceButtonPressed = new UnityEvent<MainMenuTypes, int>();

        protected BaseChoiceOfCategoryState
        (
            StateMachine stateMachine,
            IChoiceOfCategoryService choiceOfCategoryService,
            IPopUpMassageService popUpMassageService,
            GlobalUIElements globalUIElements)
        {
            _stateMachine = stateMachine;
            _choiceOfCategoryService = choiceOfCategoryService;
            _popUpMassageService = popUpMassageService;
            _globalUIElements = globalUIElements;
        }

        public virtual void Enter(TStateData stateData)
        {
            InitializeService(stateData);
            _choiceOfCategoryService.Activate();
            _choiceButtonPressed.AddListener(OnChoiceMade);
            AddUIListeners();
        }

        public virtual void Exit()
        {
            _choiceOfCategoryService.Deactivate();
            RemoveUIListeners();
        }

        protected abstract void OnChoiceMade(MainMenuTypes menuType, int index);

        protected virtual void AddUIListeners()
        {
            _globalUIElements.backButton.onClick.AddListener(OnBack);
        }

        protected virtual void RemoveUIListeners()
        {
            _globalUIElements.backButton.onClick.RemoveListener(OnBack);
        }

        protected abstract void OnBack();

        protected void InitializeService(TStateData stateData)
        {
            var data = GetCategoryData(stateData);
            _choiceOfCategoryService.Create(data.selectedListOfCategotyElements, data.menuType, _choiceButtonPressed);
        }

        protected abstract ChoiceOfCategoryStateData GetCategoryData(TStateData stateData);
    }
}
