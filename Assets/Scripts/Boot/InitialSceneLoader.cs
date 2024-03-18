#if UNITY_EDITOR
namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;
    using UnityEditor.SceneManagement;
    using UnityEngine.SceneManagement;

    [InitializeOnLoad] // Ensure the script runs when Unity starts
    public class InitialSceneLoader
    {
        private const string InitialScenePath = Constants.INITIAL_SCENE_PATH;
        private const string LoadInitialScenePref = Constants.LOAD_INITIAL_SCENE_PREF;

        static InitialSceneLoader()
        {
            EditorApplication.playModeStateChanged += LoadInitialScene;

            if (!EditorPrefs.HasKey(LoadInitialScenePref))
            {
                EditorPrefs.SetBool(LoadInitialScenePref, true);
            }
        }

        private static void LoadInitialScene(PlayModeStateChange state)
        {
            bool shouldLoadInitialScene = EditorPrefs.GetBool(LoadInitialScenePref);

            if (shouldLoadInitialScene && state == PlayModeStateChange.ExitingEditMode)
            {
                if (SceneManager.GetActiveScene().path != InitialScenePath)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(InitialScenePath);
                }
            }
        }

        [MenuItem(Constants.RIGPR_MENU_ITEM_PATH + "Toggle Load Initial Scene On Play")]
        private static void ToggleLoadInitialScene()
        {
            bool currentValue = EditorPrefs.GetBool(LoadInitialScenePref);
            EditorPrefs.SetBool(LoadInitialScenePref, !currentValue);
            Debug.Log("Load Initial Scene On Play is now: " + (!currentValue).ToString());
        }
    }
}
#endif