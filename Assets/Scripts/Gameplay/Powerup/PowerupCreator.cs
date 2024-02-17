namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// A script to help designers create different types of powerups when doing level editing.
    /// </summary>
    public class PowerupCreator : MonoBehaviour
    {
        [MenuItem(Constants.POWERUP_TOOLS_MENU_ITEM_PATH + "Create Sticky Powerup")]
        private static void CreateStickyPowerup()
        {
            GameObject stickyPowerupPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.STICKY_POWERUP_PREFAB_PATH);

            if (stickyPowerupPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.STICKY_POWERUP_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(stickyPowerupPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }

        [MenuItem(Constants.POWERUP_TOOLS_MENU_ITEM_PATH + "Create Slow Powerup")]
        private static void CreateSlowPowerup()
        {
            GameObject slowPowerupPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.SLOW_POWERUP_PREFAB_PATH);

            if (slowPowerupPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.SLOW_POWERUP_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(slowPowerupPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }

        [MenuItem(Constants.POWERUP_TOOLS_MENU_ITEM_PATH + "Create Fast Powerup")]
        private static void CreateFastPowerup()
        {
            GameObject fastPowerupPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.FAST_POWERUP_PREFAB_PATH);

            if (fastPowerupPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.FAST_POWERUP_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(fastPowerupPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
    }
}