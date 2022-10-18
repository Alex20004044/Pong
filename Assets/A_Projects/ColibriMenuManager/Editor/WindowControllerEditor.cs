
using UnityEditor;
using UnityEngine;
namespace CorD.ColibriMenuManager
{
    [CustomEditor(typeof(Window))]
    public class WindowControllerEditor : Editor
    {
        Transform transform;
        Transform parent;
        int siblingIndex;

        Transform onSelectParent;
        /*void OnEnable()
        {
            //if window

            transform = (serializedObject.targetObject as Component).transform;
            parent = transform.parent;
            siblingIndex = transform.GetSiblingIndex();

            onSelectParent = transform.GetComponentInParent<WindowController>().transform.GetChild(0);
            transform.SetParent(onSelectParent);
            transform.SetAsLastSibling();

            Debug.Log("Object was selected");
        }
        private void OnDisable()
        {
            transform.SetParent(parent);
            transform.SetSiblingIndex(siblingIndex);

            Debug.Log("Object was deselected");
        }*/
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
