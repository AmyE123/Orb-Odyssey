#if UNITY_EDITOR
namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;
    using static CT6RIGPR.Constants;

    /// <summary>
    /// Editor class for ball controller. Mainly for manipulating values via editor tools.
    /// </summary>
    [CustomEditor(typeof(BallController))]
    public class BallControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }

        [MenuItem(RIGPR_MENU_ITEM_PATH + "Toggle Debug Input")]
        private static void ToggleDebugInput()
        {
            foreach (var obj in FindObjectsOfType<BallController>())
            {           
                Undo.RecordObject(obj, "Toggle Debug Input");
                obj.ToggleDebugInput();
                EditorUtility.SetDirty(obj);
                Debug.Log("[CT6RIGPR]: Toggled Debug Input to: " + obj.DebugInput);
            }
        }
    }
}
#endif //UNITY_EDITOR