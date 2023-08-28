using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class AreaData : MonoBehaviour
{
    public List<AreaManager.AreaConnection> connections;

    private void Start()
    {
        if (connections == null || connections.Count == 0)
        {
          //
        }
        else
        {
            
            foreach (var connection in connections)
            {
              //
            }
        }
    }




}