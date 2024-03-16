namespace CT6RIGPR
{
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class LevelCompletionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text Level1ScoreText;
        [SerializeField] private TMP_Text Level2ScoreText;
        [SerializeField] private TMP_Text Level3ScoreText;
        [SerializeField] private TMP_Text TotalScoreText;

        private void OnEnable()
        {
            UpdateUI();
            FadeIn();
            DOVirtual.DelayedCall(4f, () => FadeOut());
        }

        private void UpdateUI()
        {
            Level1ScoreText.text = GlobalManager.Instance.Level1Score.ToString();
            Level2ScoreText.text = GlobalManager.Instance.Level2Score.ToString();
            Level3ScoreText.text = GlobalManager.Instance.Level3Score.ToString();
            int totalScore = GlobalManager.Instance.Level1Score + GlobalManager.Instance.Level2Score + GlobalManager.Instance.Level3Score;
            TotalScoreText.text = totalScore.ToString();
        }

        private void FadeIn()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, 0.5f); // Adjust time as needed
        }

        private void FadeOut()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.DOFade(0, 0.5f);
        }
    }

}
