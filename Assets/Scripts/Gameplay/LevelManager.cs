namespace CT6RIGPR
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using DG.Tweening;

    public class LevelManager : MonoBehaviour
    {
        private GlobalManager _globalManager;

        [SerializeField] private Image fadePanel;

        private void Start()
        {
            _globalManager = GlobalManager.Instance;
        }

        public void FadeToBlackAndLoadNextLevel()
        {
            string currentLevelName = SceneManager.GetActiveScene().name;
            LevelData nextLevel = GetNextLevel(currentLevelName);

            if (nextLevel != null)
            {
                float fadeDuration = 1.0f;
                fadePanel.DOFade(1, fadeDuration).OnComplete(() =>
                {
                    Cursor.lockState = CursorLockMode.None;
                    SceneManager.LoadScene(nextLevel.SceneName);
                });
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(Constants.BOOT_SCENE_PATH);
            }
        }

        private LevelData GetNextLevel(string currentLevelName)
        {
            LevelData[] allLevels = _globalManager.GetAllLevels();

            for (int i = 0; i < allLevels.Length; i++)
            {
                if (allLevels[i].SceneName == currentLevelName)
                {
                    int nextIndex = i + 1;
                    if (nextIndex < allLevels.Length)
                    {
                        return allLevels[nextIndex];
                    }
                    break;
                }
            }

            return null;
        }
    }
}