using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryManager
{
    public class WorkerForm : MonoBehaviour
    {
        public InputField idInputField;
        public Dropdown typeDropdown;
        public InputField firstNameInputField;
        public InputField lastNameInputField;
        public InputField weeklyNormInputField;
        public InputField overtimeAllowedInputField;
        public InputField hourlyWageInputField;
        public InputField overtimeSurchargeInputField;
        public InputField nightShiftSurchargeInputField;
                
        public void Open(List<string> list)
        {          
            typeDropdown.ClearOptions();
            List<string> options = new List<string> { "-" };
            options.AddRange(list);
            typeDropdown.AddOptions(options);
            typeDropdown.RefreshShownValue();

            gameObject.SetActive(true);
        } 
        public void Close() 
        {
            typeDropdown.ClearOptions();
            gameObject.SetActive(false);
        }     
    }
}
