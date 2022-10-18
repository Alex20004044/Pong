using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
[System.Serializable]
public class PublicTest : MonoBehaviour
{
    [SerializeField]
    CascadeModifier<float> cascadeModifier = new CascadeModifier<float>();
    public CascadeModifier<float> pcascadeModifier;
    [SerializeField]
    PublicCascadeModifier<float> publicCascadeModifier = new PublicCascadeModifier<float>();

    public PublicCascadeModifier<float> ppublicCascadeModifier;
    void Start()
    {
        PublicTest publicTest;
        ClipCore clipCore;
        //clipCore.delayBetweenShoot.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
