namespace CT6RIGPR
{
    using DG.Tweening;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// The manager for all things boot related.
    /// </summary>
    public class BootManager : MonoBehaviour
    {
        [SerializeField] private Image fadePanel;

        public IEnumerator LoadLevel(string levelName)
        {
            FadeToBlack();

            yield return new WaitForSeconds(1);

            SceneManager.LoadScene(levelName);
        }

        private void FadeToBlack()
        {
            float fadeDuration = 0.5f;
            fadePanel.DOFade(1, fadeDuration);
        }

    }
}