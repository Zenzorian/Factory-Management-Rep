using Scripts.Services;

namespace Scripts.Infrastructure.States
{
    public class ChoiceOfCategoryState : IState
    {
    public ChoiceOfCategoryState(StateMachine stateMachine, IChoiceOfCategoryService choiceOfCategoryService)
    {

    }
    public void Enter()
    {
        //_categoryMenu.Create(selectedList, menuType);
    }

    public void Exit()
    {
    }   
    }
}