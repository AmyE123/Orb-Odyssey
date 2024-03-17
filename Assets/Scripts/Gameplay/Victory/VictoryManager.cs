namespace CT6RIGPR
{
    using DG.Tweening;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class VictoryManager : MonoBehaviour
    {
        [SerializeField] private GlobalGameReferences _globalReferences;
        [SerializeField] private GameObject _levelCompletionUIGO;
        [SerializeField] private GameObject _finalCanvasGO;

        /// <summary>
        /// Complete the level and load the next level in the global manager.
        /// </summary>
        public IEnumerator CompleteLevel()
        {
            _globalReferences.GameSFXManager.PlayVictorySounds();
            _globalReferences.BallCockpit.PlayVictoryParticles();

            FadeOut();
            _levelCompletionUIGO.SetActive(true);

            if (IsLastScene())
            {
                yield return new WaitForSeconds(5);
                _finalCanvasGO.SetActive(true);
                yield return new WaitForSeconds(3);
            }
            else
            {
                yield return new WaitForSeconds(5);
            }           

            LoadNextScene();
        }

        private void FadeOut()
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
                    });
            }
            else
            {
                Debug.LogWarning("[CT6RIGPR]: Material to fade is not assigned.");
            }
        }

        private bool IsLastScene()
        {
            string currentLevelName = SceneManager.GetActiveScene().name;
            LevelData nextLevel = _globalReferences.LevelManager.GetNextLevel(currentLevelName);

            return nextLevel == null;
        }

        private void LoadNextScene()
        {
            string currentLevelName = SceneManager.GetActiveScene().name;
            LevelData nextLevel = _globalReferences.LevelManager.GetNextLevel(currentLevelName);
            LevelData firstLevel = _globalReferences.LevelManager.GetFirstLevel();

            if (nextLevel != null)
            {
                SceneManager.LoadScene(nextLevel.SceneName);
            }
            else
            {
                GlobalManager.Instance.ResetAllGameValues();                
                SceneManager.LoadScene(firstLevel.SceneName);             
            }
        }
    }
}