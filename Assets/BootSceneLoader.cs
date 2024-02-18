namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine.SceneManagement;

    [InitializeOnLoad] // Ensure the script runs when Unity starts
    public class BootSceneLoader
    {
        private const string BootScenePath = Constants.BOOT_SCENE_PATH;

        static BootSceneLoader()
        {
            EditorApplication.playModeStateChanged += LoadBootScene;
        }

        private static void LoadBootScene(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (SceneManager.GetActiveScene().path != BootScenePath)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(BootScenePath);
                }
            }
        }
    }
}