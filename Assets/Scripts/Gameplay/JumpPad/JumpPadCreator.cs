namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;

    public class JumpPadCreator : MonoBehaviour
    {
        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Create Jump Pad")]
        private static void CreateJumpPad()
        {
            GameObject jumpPadPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.JUMPPAD_PREFAB_PATH);

            if (jumpPadPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.JUMPPAD_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(jumpPadPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
    }
}