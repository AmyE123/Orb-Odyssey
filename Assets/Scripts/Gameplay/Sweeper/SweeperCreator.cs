namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// A script to help designers create sweeper obstacles when doing level editing.
    /// </summary>
    public class SweeperCreator : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Create Sweeper Obstacle")]
        private static void CreateSweeper()
        {
            GameObject sweeperPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.SWEEPER_PREFAB_PATH);

            if (sweeperPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.SWEEPER_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(sweeperPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
#endif // UNITY_EDITOR
    }
}