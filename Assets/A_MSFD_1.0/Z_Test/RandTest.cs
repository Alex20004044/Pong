using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD.AS;
using MSFD;
using CorD;

public class RandTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeReference]
    IZoneObserver zoneObserver;
    MonoBehaviour monoBehaviour;
    void Start()
    {
        Vector2 vector2 = new Vector2();
        Rand.RandomPointInRange(vector2);
        IDeltaRange<float> deltaRangeValue = new DeltaRange();
        deltaRangeValue.RandomPoint();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
