using UnityEngine;

namespace Scripts.Services
{
    public class ServiceInitializer : MonoBehaviour
    {
        [SerializeField] private SaveloadDataService _data;         

        [SerializeField] private ChoiceOfCategoryService _choiceOfCategory;

        [SerializeField] private GraphPlane _graphPlane;
        [SerializeField] private TableView _tableView;
               

        public void Initialize(AllServices services)
        {
            services.RegisterSingle<ISaveloadDataService>(_data);
            Debug.Log("SaveloadDataService Initialized");            
    

            services.RegisterSingle<IChoiceOfCategoryService>(_choiceOfCategory);
            Debug.Log("ChoiceOfCategoryService Initialized");

            //services.RegisterSingle<ITutorialService>(_tutorial);
            //services.RegisterSingle<ITutorialService>(_tutorial);

            services.RegisterSingle<ITableView>(_tableView);
        }
    }
   
}
