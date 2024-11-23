using UnityEngine;

namespace Scripts.Infrastructure
{
  public class ProgramRunner : MonoBehaviour
  {
    public Bootstrapper BootstrapperPrefab;
    private void Awake()
    {
      var bootstrapper = FindFirstObjectByType<Bootstrapper>();
      
      if(bootstrapper != null) return;

      Instantiate(BootstrapperPrefab);
    }
  }
}