using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Services
{
    public interface IButtonCreator : IService
    {
        List<Transform> Create(string[] names, Transform contentTransform);
        Button CreateDeleteButton(Transform parent);
    }
}