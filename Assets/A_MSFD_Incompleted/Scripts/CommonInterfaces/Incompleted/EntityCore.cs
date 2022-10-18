using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
public class EntityCore : MonoBehaviour
{
    Dictionary<string, CascadeModifier<Influence>> influenceProcessors = new Dictionary<string, CascadeModifier<Influence>>();
    public void Impact(Influence influence)
    {
        CascadeModifier<Influence> influenceProcessor;
        if(influenceProcessors.TryGetValue(influence.tag, out influenceProcessor))
        {
            //influenceProcessor.
        }
    }

}

public class Influence
{
    public string tag;
    public object value;
    public GameObject source;
}