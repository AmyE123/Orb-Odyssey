namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class VictoryManager : MonoBehaviour
    {
        [SerializeField] private GlobalGameReferences _globalReferences;

        /// <summary>
        /// Complete the level and load the next level in the global manager.
        /// </summary>
        public void CompleteLevel()
        {
            Material ballMat = _globalReferences.BallMaterial;

            string currentLevelName = SceneManager.GetActiveScene().name;
            LevelData nextLevel = _globalReferences.LevelManager.GetNextLevel(currentLevelName);
            LevelData firstLevel = _globalReferences.LevelManager.GetFirstLevel();

            if (ballMat != null)
            {
                ballMat.color = Color.clear;
                _globalReferences.BallController.FreezePlayer();

                ballMat.DOColor(Color.black, 1).SetDelay(1)
                    .OnComplete(() =>
                    {
                        Cursor.lockState = CursorLockMode.None;

                        if (nextLevel != null)
                        {
                            SceneManager.LoadScene(nextLevel.SceneName);
                        }
                        else
                        {
                            SceneManager.LoadScene(firstLevel.SceneName);
                        }
                    });
            }
            else
            {
                Debug.LogWarning("[CT6RIGPR]: Material to fade is not assigned.");
            }
        }
    }
}