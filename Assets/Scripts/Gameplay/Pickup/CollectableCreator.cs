namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// A tool for creating new pickups in the game.
    /// </summary>
    public class CollectableCreator : MonoBehaviour
    {
        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Create Collectable")]
        private static void CreatePickup()
        {
            GameObject pickupPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.COLLECTABLE_PREFAB_PATH);

            if (pickupPrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.COLLECTABLE_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(pickupPrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
    }
}