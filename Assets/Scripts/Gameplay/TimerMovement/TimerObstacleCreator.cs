namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;

    public class TimerObstacleCreator : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Timer Obstacles/" + "Create Timer Door")]
        private static void CreateTimerDoor()
        {
            GameObject timerDoorPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.TIMER_DOOR_PREFAB_PATH);

            if (timerDoorPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.TIMER_DOOR_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(timerDoorPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }

        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Timer Obstacles/" + "Create Timer Platform")]
        private static void CreateTimerPlatform()
        {
            GameObject timerPlatformPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.TIMER_PLATFORM_PREFAB_PATH);

            if (timerPlatformPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.TIMER_PLATFORM_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(timerPlatformPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }

        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Timer Obstacles/" + "Create Timer Ramp")]
        private static void CreateTimerRamp()
        {
            GameObject timerRampPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.TIMER_RAMP_PREFAB_PATH);

            if (timerRampPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.TIMER_RAMP_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(timerRampPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
#endif
    }
}

