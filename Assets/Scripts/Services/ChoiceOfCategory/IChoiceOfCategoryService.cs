using Scripts.Data;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

namespace Scripts.Services
{
    public interface IChoiceOfCategoryService : IService
    {
        void Activate();
        void Deactivate();              
        public void ButtonPressed(int index);
        void Create(List<string> list, MainMenuTypes menuType, UnityEvent<MainMenuTypes, int> choiceButtonPresed,Action<int> onDelete = null);
        void Edit();
    }
}