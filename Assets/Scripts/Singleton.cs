using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component //Clase singleton generica para tener un codigo mas clinico ;)
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                if(instance == null)
                {
                    GameObject nuevo = new GameObject();
                    instance = nuevo.AddComponent<T>();
                }
            }
            return instance;
        }
    }


    protected virtual void Awake() {
        instance = this as T; 
    }
}
