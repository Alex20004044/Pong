using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace MSFD.Data
{
    public class DisplayD_Purchase : DisplayBase
    {
        [SerializeField]
        TMP_Text costField;
        [SerializeField]
        Button buyButton;
        [Header("Scrpit choose one state whether object is unlocked or not")]
        [SerializeField]
        GameObject lockStateGO;
        [SerializeField]
        GameObject unlockStateGO;
        [SerializeField]
        GameObject boughtStateGO;
        [SerializeField]
        GameObject notBoughtStateGO;

        [Space]
        [SerializeField]
        string buyRequestMessage = DV.STRING_BUY_REQUEST;



        D_Purchase purchase;
        public override void Initialize()
        {
            purchase = data as D_Purchase;
            if(buyButton != null)
                buyButton.onClick.AddListener(BuyRequest);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (buyButton != null)
                buyButton.onClick.RemoveListener(BuyRequest);
        }
        public override void Display()
        {
            costField.text = purchase.GetCost().ToString();
            if(purchase.IsUnlocked())
            {
                lockStateGO?.SetActive(false);
                unlockStateGO?.SetActive(true);
            }
            else
            {
                lockStateGO?.SetActive(true);
                unlockStateGO?.SetActive(false);
            }
            if (purchase.IsBought())
            {
                boughtStateGO?.SetActive(true);
                notBoughtStateGO?.SetActive(false);
            }
            else
            {
                boughtStateGO?.SetActive(false);
                notBoughtStateGO?.SetActive(true);
            }
            if (buyButton != null)
            {
                if (purchase.IsBought())
                {
                    buyButton.gameObject.SetActive(false);
                }
                else
                {
                    buyButton.gameObject.SetActive(true);
                }
            }
        }
        void BuyRequest()
        {
            /* string parentPath;
             DC.GetLastPathPart(data.GetDataPath(), out parentPath);
             Messenger<string>.Broadcast(buyRequestMessage, parentPath);*/
            Messenger<string>.Broadcast(buyRequestMessage, purchase.GetSaveDataPath());
        }
    }
}
