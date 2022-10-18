using MSFD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_Test_2 : MonoBehaviour
{
    [SerializeField]
    ExternalField<ClipCore> clipCoreField;

    public int PublicFuncInt()
    {
        return 123;
    }
    public int FuncInt()
    {
        return -123;
    }
    [Obsolete]
    public int ObsoleteFuncInt()
    {
        return -11;
    }
    public int FuncIntArgs(int arg0)
    {
        return -113223;
    }


}
