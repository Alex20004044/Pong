using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedEventTest : MonoBehaviour
{
    [SerializeField]
    ExtendedEvent extendedEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Sirenix.OdinInspector.Button]
    void ActivateEvent()
    {
        extendedEvent.Invoke();
    }
}
