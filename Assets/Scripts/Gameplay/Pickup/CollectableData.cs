namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Pickup Data", menuName = "ScriptableObjects/Level Tools/Pickup Data")]
    public class CollectableData : ScriptableObject
    {
        public float ShrinkDuration = 0.5f;
        public float DestroyDelay = 3f;
        public Ease ShrinkEase = Ease.InCubic;
        public AudioClip CollectableClip;
    }
}