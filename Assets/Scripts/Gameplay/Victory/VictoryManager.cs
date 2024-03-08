namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class VictoryManager : MonoBehaviour
    {
        [SerializeField] private GlobalGameReferences _globalReferences;

        public void CompleteLevel(string nextLevelName = "null")
        {
            Material ballMat = _globalReferences.BallMaterial;

            if (ballMat != null)
            {
                ballMat.color = Color.clear;
                _globalReferences.BallController.FreezePlayer();

                ballMat.DOColor(Color.black, 1).SetDelay(1)
                    .OnComplete(() =>
                    {
                        Cursor.lockState = CursorLockMode.None;

                        if (nextLevelName == "null" || !SceneManager.GetSceneByName(nextLevelName).IsValid())
                        {
                            Debug.LogWarning("[CT6RIGPR]: Inputted scene name is invalid or null. Reloading current scene.");
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                        else
                        {
                            SceneManager.LoadScene(nextLevelName);
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