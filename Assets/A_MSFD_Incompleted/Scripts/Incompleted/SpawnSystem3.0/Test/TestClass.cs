using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TestData_", menuName = "DataList/TestData")]
[System.Serializable]
public class TestClass : ScriptableObject
{
    public int[] intArray = new int[1];
    public string nameObj = "RefClass";
}
