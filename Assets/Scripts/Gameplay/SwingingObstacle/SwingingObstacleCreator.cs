namespace CT6RIGPR
{
    using UnityEngine;
    using UnityEditor;

    /// <summary>
    /// A tool for creating new swinging obstacles in the game.
    /// </summary>
    public class SwingingObstacleCreator : MonoBehaviour
    {
        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Create Swinging Obstacle")]
        private static void CreateSwingingObstacle()
        {
            GameObject swingingObstaclePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.SWINGING_OBSTACLE_PREFAB_PATH);

            if (swingingObstaclePrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.SWINGING_OBSTACLE_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(swingingObstaclePrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
    }
}

