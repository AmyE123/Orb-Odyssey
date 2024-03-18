namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;

    public class FinalCanvasUI : MonoBehaviour
    {
        private void OnEnable()
        {
            FadeIn();
            DOVirtual.DelayedCall(2f, () => FadeOut());
        }

        private void FadeIn()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, 0.5f);
        }

        private void FadeOut()
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.DOFade(0, 0.5f);
        }
    }
}