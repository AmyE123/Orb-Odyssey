namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// A script to help designers create impact walls when doing level editing.
    /// </summary>
    public class ImpactWallCreator : MonoBehaviour
    {
        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Create Impact Wall")]
        private static void CreateImpactWall()
        {
            GameObject impactWallPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.IMPACT_WALL_PREFAB_PATH);

            if (impactWallPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.IMPACT_WALL_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(impactWallPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
    }
}