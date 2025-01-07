using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour, new()
{
    protected Singleton() { }

    private static T _instance;

    public static T GetInstance()
    {
        if (_instance == null)
        {
            _instance = new T();
            DontDestroyOnLoad(_instance.gameObject);
        }
        return _instance;
    }
}
