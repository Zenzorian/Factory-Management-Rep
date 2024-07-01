using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class WorkersTableView : MonoBehaviour
{
    [SerializeField] private TableManager tableManager;

    private void ShowTable<T>(List<T> list)
    {
        if (list.Count == 0) return;
        
        Type type = list[0].GetType();
        FieldInfo[] fields = type.GetFields();
        List<string> fieldsNames = new List<string>();

        foreach (var item in fields)
        {
            fieldsNames.Add(item.Name);
        }
        var tableData = new string[fieldsNames.Count,list.Count];

        foreach (var item in list)
        {
            foreach (var field in fields)
            {

            }
        }

        Table table = new Table(fieldsNames.ToArray(), tableData);
        tableManager.CreateTable(table);
    }
}
