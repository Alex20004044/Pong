using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField]
        float maxX = 5;
        [SerializeField]
        float maxY = 5f;

        [SerializeField]
        Transform topBorder;
        [SerializeField]
        Transform downBorder;
        [SerializeField]
        Transform leftBorder;
        [SerializeField]
        Transform rightBorder;


        [Button]
        void SetWidthAndHeight()
        {
            topBorder.position = new Vector3(0, maxY);
            downBorder.position = new Vector3(0, -maxY);

            leftBorder.position = new Vector3(-maxX, 0);
            rightBorder.position = new Vector3(maxX, 0);
        }

        public Vector2 GetXAxisMaxCoord()
        {
            return new Vector2(-maxX, maxX);
        }        
        public Vector2 GetYAxisMaxCoord()
        {
            return new Vector2(-maxY, maxY);
        }
        public Bounds GetSceneBounds()
        {
            return new Bounds(Vector3.zero, new Vector3(maxX * 2, maxY * 2, 0));
        }
    }
}