using Scripts.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MyTools
{
    public interface IButtonCreator : IService
    {
        List<Transform> Create(string[] names, Transform contentTransform);
    }
}