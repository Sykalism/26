using System;
using UnityEngine;

public class SaveID : MonoBehaviour
{
    public string ID;

    #if UNITY_EDITOR

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(ID))
        {
            ID = Guid.NewGuid().ToString();
        }
    }

    #endif
}
