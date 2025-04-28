using Scripts.Infrastructure.States;
using Scripts.MyTools;
using Scripts.Services;

namespace Scripts.Infrastructure
{
    public class Program
    {
        public StateMachine StateMachine;

        public Program(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new StateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container, coroutineRunner);
        }
    }
}