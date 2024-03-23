namespace CT6RIGPR
{
    using DG.Tweening;
    using TMPro;
    using UnityEngine;

    public class LevelCompletionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text Level1ScoreText;
        [SerializeField] private TMP_Text Level2ScoreText;
        [SerializeField] private TMP_Text Level3ScoreText;
        [SerializeField] private TMP_Text Level4ScoreText;
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
            Level4ScoreText.text = GlobalManager.Instance.Level4Score.ToString();
            int totalScore = GlobalManager.Instance.Level1Score + GlobalManager.Instance.Level2Score + GlobalManager.Instance.Level3Score + GlobalManager.Instance.Level4Score;
            TotalScoreText.text = totalScore.ToString();
        }

        private void FadeIn()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            RectTransform rectTransform = GetComponent<RectTransform>();

            // Set initial conditions
            canvasGroup.alpha = 0;
            rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // Start smaller

            // Fade in
            canvasGroup.DOFade(1, 0.5f); // Adjust time as needed for the fade

            // Bounce in by scaling up. You might need to adjust the values to suit your needs
            rectTransform.DOScale(new Vector3(1, 1, 1), 0.5f)
                .SetEase(Ease.OutBounce); // Use OutBounce for a bounce effect
        }

        private void FadeOut()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.DOFade(0, 0.5f);
        }
    }

}
