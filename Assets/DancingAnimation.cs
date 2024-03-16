namespace CT6RIGPR
{
    using UnityEngine;
    using DG.Tweening;

    public class DancingAnimation : MonoBehaviour
    {
        void Start()
        {
            // Spinning
            transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
                     .SetEase(Ease.Linear)
                     .SetLoops(-1, LoopType.Restart);

            // Dancing
            Sequence danceSequence = DOTween.Sequence();
            danceSequence.Append(transform.DORotate(new Vector3(0, 0, -10), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutSine))
                         .Append(transform.DORotate(new Vector3(0, 0, 10), 1, RotateMode.LocalAxisAdd).SetEase(Ease.InOutSine))
                         .Append(transform.DORotate(new Vector3(0, 0, 0), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutSine))
                         .SetLoops(-1, LoopType.Restart);

            // Bouncing
            Sequence bounceSequence = DOTween.Sequence();
            bounceSequence.Append(transform.DOMoveY(transform.position.y + 0.5f, 0.5f).SetEase(Ease.InOutQuad))
                          .Append(transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.InOutQuad))
                          .SetLoops(-1, LoopType.Restart);
        }
    }
}