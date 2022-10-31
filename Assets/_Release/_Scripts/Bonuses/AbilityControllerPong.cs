using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class AbilityControllerPong : MonoBehaviour
    {
        [SerializeField]
        List<AbilityBase> startedAbilitiesPrefabs = new List<AbilityBase>();
        GameObject abilityParent;
        //public GameObject abilityParent { get; private set; }
        private void Awake()
        {
            abilityParent = new GameObject("AbilityParent");
            abilityParent.transform.SetParent(transform);
            abilityParent.transform.localPosition = Vector3.zero;
            abilityParent.transform.localRotation = Quaternion.identity;

            for (int i = 0; i < startedAbilitiesPrefabs.Count; i++)
            {
                ActivateAbility(startedAbilitiesPrefabs[i].gameObject);
            }
        }
        public void ActivateAbility(GameObject abilityPrefab, bool isSetParentToAbility = true)
        {
            GameObject abilityGO = Instantiate(abilityPrefab, Vector3.zero, Quaternion.identity);
            if (isSetParentToAbility)
            {
                abilityGO.transform.SetParent(abilityParent.transform);
            }
            abilityGO.transform.localPosition = Vector3.zero;
            abilityGO.transform.localRotation = Quaternion.identity;
            AbilityBase ability = abilityGO.GetComponent<AbilityBase>();
            if (ability.GetAbilityInteractionType() == AbilityBase.AbilityInteractionType.single)
            {
                foreach (Transform x in abilityParent.transform)
                {
                    if (x.name == abilityGO.name && x != abilityGO.transform)
                    {
                        x.GetComponent<AbilityBase>().Deactivate();
                        break;
                    }
                }
            }
            ability.Activate(gameObject);
        }
        public void DeactivateAbility(AbilityBase ability)
        {
            ability.Deactivate();
            //!
            Destroy(ability.gameObject);
        }
    }
}