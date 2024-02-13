namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// A script to help designers create different types of platforms when doing level editing.
    /// </summary>
    public class PlatformCreator : MonoBehaviour
    {
        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Platforms/Create Moving Platform")]
        private static void CreateMovingPlatform()
        {
            GameObject movingPlatformPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.MOVING_PLATFORM_PREFAB_PATH);

            if (movingPlatformPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.MOVING_PLATFORM_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(movingPlatformPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }

        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Platforms/Create Button Moving Platform")]
        private static void CreateButtonMovingPlatform()
        {
            GameObject buttonMovingPlatformPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.BUTTON_MOVING_PLATFORM_PREFAB_PATH);

            if (buttonMovingPlatformPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.BUTTON_MOVING_PLATFORM_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(buttonMovingPlatformPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
    }
}