using Scripts.Infrastructure.States;
using Scripts.MyTools;
using UnityEngine;

namespace Scripts.Infrastructure
{
  public class Bootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public LoadingCurtain CurtainPrefab;
    private Program _program;

    private void Awake()
    {
      _program = new Program(this, Instantiate(CurtainPrefab));
      _program.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}