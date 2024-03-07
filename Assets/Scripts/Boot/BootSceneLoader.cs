#if UNITY_EDITOR
namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;
    using UnityEditor.SceneManagement;
    using UnityEngine.SceneManagement;

    [InitializeOnLoad] // Ensure the script runs when Unity starts
    public class BootSceneLoader
    {
        private const string BootScenePath = Constants.BOOT_SCENE_PATH;
        private const string LoadBootScenePref = Constants.LOAD_BOOT_SCENE_PREF;

        static BootSceneLoader()
        {
            EditorApplication.playModeStateChanged += LoadBootScene;

            if (!EditorPrefs.HasKey(LoadBootScenePref))
            {
                EditorPrefs.SetBool(LoadBootScenePref, true);
            }
        }

        private static void LoadBootScene(PlayModeStateChange state)
        {
            bool shouldLoadBootScene = EditorPrefs.GetBool(LoadBootScenePref);

            if (shouldLoadBootScene && state == PlayModeStateChange.ExitingEditMode)
            {
                if (SceneManager.GetActiveScene().path != BootScenePath)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(BootScenePath);
                }
            }
        }

        [MenuItem(Constants.RIGPR_MENU_ITEM_PATH + "Toggle Load Boot Scene On Play")]
        private static void ToggleLoadBootScene()
        {
            bool currentValue = EditorPrefs.GetBool(LoadBootScenePref);
            EditorPrefs.SetBool(LoadBootScenePref, !currentValue);
            Debug.Log("Load Boot Scene On Play is now: " + (!currentValue).ToString());
        }
    }
}
#endif