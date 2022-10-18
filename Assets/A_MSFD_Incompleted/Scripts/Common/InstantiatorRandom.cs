using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
namespace MSFD
{
    public class InstantiatorRandom : MonoBehaviour
    {
        [SerializeField]
        GameObjectsSource gameObjectsSource = GameObjectsSource.DC_Container;
        [ShowIf("@gameObjectsSource == GameObjectsSource.manual")]
        [SerializeField]
        List<GameObject> gameObjectsPrefabs = new List<GameObject>();
        [ShowIf("@gameObjectsSource == GameObjectsSource.DC_Container")]
        [SerializeField]
        string containerPath;

        [SerializeField]
        bool isSetParentToGo = false;
        [SerializeField]
        bool isDestroyInstantiatorAfterSpawn = true;
        [SerializeField]
        DespawnType instantiatorDespawnType = DespawnType.destroy;
        [SerializeField]
        bool isSpawnGOOnEnable = true;
        [SerializeField]
        UnityEvent onSpawnGO;
        private void OnEnable()
        {
            RefreshGOPrefabs();
            if(isSpawnGOOnEnable)
            {
                SpawnGO();
            }
        }
        
        public void RefreshGOPrefabs()
        {
            if(gameObjectsSource == GameObjectsSource.DC_Container)
            {
                gameObjectsPrefabs = DCProcessor.GetItems(containerPath, DCProcessor.IsItemBought);
            }
        }
        [Button]
        public void SpawnGO()
        {   
            GameObject go = PC.Spawn(gameObjectsPrefabs[AuxiliarySystem.GetRandomIndex(gameObjectsPrefabs.Count)], transform.position, transform.rotation);
            if(isSetParentToGo)
            {
                go.transform.SetParent(transform);
            }
            onSpawnGO.Invoke();
            if(isDestroyInstantiatorAfterSpawn)
            {
                PC.Despawn(gameObject, 0, SpawnType.local, instantiatorDespawnType);
            }
        }

        enum GameObjectsSource { manual, DC_Container};
    }
}