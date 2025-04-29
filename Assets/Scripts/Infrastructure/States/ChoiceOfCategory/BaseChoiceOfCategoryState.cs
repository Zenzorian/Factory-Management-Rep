using Scripts.Services;
using UnityEngine.Events;
using Scripts.UI.Markers;

namespace Scripts.Infrastructure.States
{
    public abstract class BaseChoiceOfCategoryState<TStateData> : IPayloadedState<TStateData>
    {
        protected readonly StateMachine _stateMachine;
        protected readonly IChoiceOfCategoryService _choiceOfCategoryService;
        protected readonly IPopUpService _popUpService;
        protected readonly GlobalUIElements _globalUIElements;

        protected UnityEvent<MainMenuTypes, int> _choiceButtonPressed = new UnityEvent<MainMenuTypes, int>();

        protected BaseChoiceOfCategoryState
        (
            StateMachine stateMachine,
            IChoiceOfCategoryService choiceOfCategoryService,
            IPopUpService popUpService,
            GlobalUIElements globalUIElements)
        {
            _stateMachine = stateMachine;
            _choiceOfCategoryService = choiceOfCategoryService;
            _popUpService = popUpService;
            _globalUIElements = globalUIElements;
        }

        public virtual void Enter(TStateData stateData)
        {
            RemoveUIListeners();
            InitializeService(stateData);
            _choiceOfCategoryService.Activate();
            _choiceButtonPressed.AddListener(OnChoiceMade);           
        }

        public void Exit()
        {           
            RemoveUIListeners();
            _choiceOfCategoryService.Deactivate();              
        }

        protected abstract void OnChoiceMade(MainMenuTypes menuType, int index);

        protected virtual void AddUIListeners()
        {
            _globalUIElements.backButton.onClick.AddListener(OnBack);
        }

        protected virtual void RemoveUIListeners()
        {           
            _globalUIElements.backButton.onClick.RemoveListener(OnBack);
            _globalUIElements.addationButton.onClick.RemoveAllListeners();
            _globalUIElements.editButton.onClick.RemoveAllListeners();
            _choiceButtonPressed.RemoveAllListeners();
            _globalUIElements.showGraphButton.gameObject.SetActive(false);
            _globalUIElements.showGraphButton.onClick.RemoveAllListeners(); 
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
