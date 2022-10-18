using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
public class ClipUse : MonoBehaviour
{
    [SerializeField]
    ClipCore clip;
    private void OnEnable()
    {
        clip.ActivateRechargeRoutine(this);
    }
    public void Shoot()
    {
        clip.TryShoot();
    }
}
