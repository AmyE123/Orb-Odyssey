namespace CT6RIGPR
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// A tool for creating new tubes in the game.
    /// </summary>
    public class TubeCreator : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Create Tube/One-Way Tube")]
        private static void CreateOneWayTube()
        {
            string instructionsPath = Constants.TUBE_INSTRUCTIONS_PATH;
            string instructions = File.ReadAllText(instructionsPath);

            InstructionsPopup.ShowWindow(instructions);

            GameObject tubePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.ONE_WAY_TUBE_PREFAB_PATH);

            if (tubePrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.ONE_WAY_TUBE_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(tubePrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }

        [MenuItem(Constants.LEVEL_TOOLS_MENU_ITEM_PATH + "Create Tube/Two-Way Tube")]
        private static void CreateTwoWayTube()
        {
            string instructionsPath = Constants.TUBE_INSTRUCTIONS_PATH;
            string instructions = File.ReadAllText(instructionsPath);

            InstructionsPopup.ShowWindow(instructions);

            GameObject tubePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.TWO_WAY_TUBE_PREFAB_PATH);

            if (tubePrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.TWO_WAY_TUBE_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(tubePrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
#endif
    }
}