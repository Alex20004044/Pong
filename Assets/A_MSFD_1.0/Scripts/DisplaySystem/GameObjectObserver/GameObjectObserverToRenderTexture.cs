using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MSFD
{
    public class GameObjectObserverToRenderTexture : FieldObserverToBase<GameObject>
    {
        [SerializeField]
        RenderTexture renderTexture;

        new Camera camera;

        GameObject prefab;
        GameObject go;

        protected override void Awake()
        {
            base.Awake();
            camera = new GameObject("Camera").AddComponent<Camera>();

            camera.targetTexture = renderTexture;
            camera.cullingMask = GameValues.ReturnDisplayLayerMask();   
        }

        public override void OnNext(GameObject value)
        {
            Refresh(value);
        }

        void Refresh(GameObject prefab)
        {
            this.prefab = prefab;
            go = PC.Spawn(prefab);
            camera.transform.SetParent(go.transform);
            DisplaySceneManager.ZoomFit(camera, go, true);
            DisplaySceneManager.Instance.AddGO(prefab.name, go);
        }
    }
}