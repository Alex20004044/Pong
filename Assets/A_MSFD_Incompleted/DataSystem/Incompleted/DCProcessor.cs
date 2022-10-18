using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
public static class DCProcessor
{
    /// <summary>
    /// Try to find D_Purchase ("Purchase") in "container" with name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool IsItemBought(string name)
    {
        return DC.GetData<D_Purchase>(name + DV.purchaseSName).IsBought();
    }

    public static List<GameObject> GetItems(string containerPath, System.Predicate<string> predicate)
    {
        List<GameObject> items = new List<GameObject>();
        List<string> itemsInContainer = DC.GetStoredInContainerDataPathes(containerPath, predicate);

        foreach (string x in itemsInContainer)
        {
             items.Add(DC.GetData<D_Assets>(containerPath + "/" + x + DV.assetsSName).GetAsset<GameObject>(DV.prefabInAssetsName));         
        }
        return items;
    }
}
