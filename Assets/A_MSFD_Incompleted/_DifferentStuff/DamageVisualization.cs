/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MSFD 
{
    public class DamageVisualization : MonoBehaviour
    {
        [SerializeField]
        Renderer[] _renderers;
        [SerializeField]
        Material damageMaterial;
        
        private Material _initialMat;
        private Texture _initialTexture;
        
        float redTime = GameValuesCH.redTimeOnDamage;
        private void Start()
        {
            _renderers = GetComponentsInChildren<Renderer>();
        }
        
        public void OnDamaged()
        {
            foreach (Renderer x in _renderers)
            {
                _initialMat = x.material;
                //_initialTexture = x.material.shader.;
                x.material = damageMaterial;
                
                if (_initialTexture != null)
                {
                    x.material.SetTexture("_BaseTexture", _initialTexture);
                }
                
                

                if (x.material.HasProperty("_IsActive"))
                {
                    x.material.SetInt("_IsActive", 1);
                    Invoke("CancelRed", redTime);
                }
                else
                {
                    Debug.Log("renderer.material.HasNOT");
                }
            }

        }
        public void CancelRed()
        {
            foreach (Renderer x in _renderers)
            {
                if (x.material.HasProperty("_IsActive"))
                {
                    x.material.SetInt("_IsActive", 0);
                    x.material = _initialMat;
                }
            }
        }
    }    
}

*/