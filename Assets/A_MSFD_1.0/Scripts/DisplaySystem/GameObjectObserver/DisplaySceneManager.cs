using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MSFD
{
    public class DisplaySceneManager : SingletoneBase<DisplaySceneManager>
    {
        [SerializeField]
        Vector3Int goInterval = new Vector3Int(100,0,0);
        [SerializeField]
        Vector3Int startSpawnPosition = new Vector3Int(-100000, -100000, -100000);
        [ReadOnly]
        [ShowInInspector]
        int currentIndex = 0;

        Dictionary<string, GameObject> displayedObjects = new Dictionary<string, GameObject>();

        Scene scene;

        bool isInit = false;

        public void AddGO(string goKey, GameObject go)
        {
            if(!isInit)
            {
                isInit = true;
                scene = SceneManager.CreateScene("DisplayScene");
            }

            go.transform.SetParent(null);
            SceneManager.MoveGameObjectToScene(go, scene);
            LayerMask layerMask = LayerMask.NameToLayer(GameValues.displayLayer);
            go.layer = layerMask;
            foreach(Transform x in go.GetComponentsInChildren<Transform>())
            {
                x.gameObject.layer = layerMask;
            }

            go.transform.position = startSpawnPosition + goInterval * currentIndex;
            currentIndex++;

            displayedObjects.Add(goKey, go);
        }

        public static Bounds GetBound(GameObject go)
        {
            Bounds b = new Bounds(go.transform.position, Vector3.zero);
            var rList = go.GetComponentsInChildren(typeof(Renderer));
            foreach (Renderer r in rList)
            {
                b.Encapsulate(r.bounds);
            }
            return b;
        }

        /// <summary>
        /// Adjust the camera to zoom fit the game object
        /// There are multiple directions to get zoom-fit view of the game object,
        /// if ViewFromRandomDirecion is true, then random viewing direction is chosen
        /// else, the camera's forward direction will be sused
        /// </summary>
        /// <param name="c"> The camera, whose position and view direction will be 
        //                   adjusted to implement zoom-fit effect </param>
        /// <param name="go"> The GameObject which will be zoom-fit. This object may have
        ///                   children objects as well </param>
        /// <param name="ViewFromRandomDirecion"> if random viewing direction is chozen. </param>
        public static void ZoomFit(Camera c, GameObject go, bool ViewFromRandomDirecion = false)
        {
            Bounds b = GetBound(go);
            Vector3 max = b.size;
            float radius = Mathf.Max(max.x, Mathf.Max(max.y, max.z));
            float dist = radius / (Mathf.Sin(c.fieldOfView * Mathf.Deg2Rad / 2f));
            Debug.Log("Radius = " + radius + " dist = " + dist);

            Vector3 view_direction = ViewFromRandomDirecion ? UnityEngine.Random.onUnitSphere : c.transform.InverseTransformDirection(Vector3.forward);

            Vector3 pos = view_direction * dist + b.center;
            c.transform.position = pos;
            c.transform.LookAt(b.center);
        }
    }
}