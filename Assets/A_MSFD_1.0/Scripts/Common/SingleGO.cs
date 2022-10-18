using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGO : MonoBehaviour
{
    [SerializeField]
    string singletoneName;
    public static Dictionary<string, GameObject> singletones = new Dictionary<string, GameObject>();
    private void Awake()
    {
        GameObject go;
        if (singletones.TryGetValue(singletoneName, out go))
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            singletones.Add(singletoneName, gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
    
}

