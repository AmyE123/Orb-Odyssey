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
        [MenuItem("Tools/CT6RIGPR/CreateTube")]
        private static void CreateTube()
        {
            string instructionsPath = Constants.TUBE_INSTRUCTIONS_PATH;
            string instructions = File.ReadAllText(instructionsPath);

            InstructionsPopup.ShowWindow(instructions);

            GameObject tubePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Constants.TUBE_PREFAB_PATH);

            if (tubePrefab == null)
            {
                Debug.LogError("[CT6RIGPR] Prefab not found at path: " + Constants.TUBE_PREFAB_PATH);
                return;
            }

            GameObject instance = PrefabUtility.InstantiatePrefab(tubePrefab) as GameObject;
            Selection.activeObject = instance;

            if (instance != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
    }
}