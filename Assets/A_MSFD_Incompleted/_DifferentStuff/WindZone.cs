using CorD;
using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    Vector3 windDirection;
    [SerializeField]
    float windPower = 1f;
    [SerializeField]
    GameObject windEffect;

    IZoneObserver zoneObserver;
    List<Transform> targets;
    private float zoneObserveUpdateTime;

    /*private void Awake()
    {
        zoneObserver = GetComponent<IZoneObserver>();
        targets = zoneObserver.GetObjectsInZone();
        InvokeRepeating("Wind", 0, GameValuesCH.zoneObserveUpdateTime);
    }*/
    private void Start()
    {
        if(windEffect != null)
        {
            windEffect.transform.rotation = Quaternion.LookRotation(windDirection);
        }
    }

    public void Wind()
    {
        foreach(Transform x in targets)
        {
            Rigidbody rb = x.GetComponent<Rigidbody>();

            rb.MovePosition(x.transform.position + windDirection * windPower * zoneObserveUpdateTime);
        }
    }
}
