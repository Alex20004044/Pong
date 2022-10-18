using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicTest2 : MonoBehaviour
{
    [SerializeField]
    CascadeModifier<int> cascadeModifier;
    // Start is called before the first frame update
    void Start()
    {
        PublicTest publicTest;
        //publicTest.pcascadeModifier = new MSFD.CascadeModifier<float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
