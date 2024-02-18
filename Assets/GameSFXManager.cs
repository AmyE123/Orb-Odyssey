namespace CT6RIGPR
{
    using UnityEngine;

    public class GameSFXManager : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;

        [SerializeField] private AudioClip _victoryCheerClip;
        [SerializeField] private AudioClip _jumpPadClip;
        [SerializeField] private AudioClip _portalPullClip;
        [SerializeField] private AudioClip _outOfBoundsNegativeClip;

        /// <summary>
        /// Plays the victory cheer sounds.
        /// </summary>
        public void PlayVictorySounds()
        {
            _audioManager.PlayDefaultSFX(_victoryCheerClip);
        }

        /// <summary>
        /// Plays the jump pad SFX.
        /// </summary>
        public void PlayJumpPadSound()
        {
            _audioManager.PlayDefaultSFX(_jumpPadClip);
        }

        /// <summary>
        /// Plays the portal pull SFX.
        /// </summary>
        public void PlayPortalPullSound()
        {
            _audioManager.PlayDefaultSFX(_portalPullClip);
        }

        /// <summary>
        /// Plays the out of bounds SFX.
        /// </summary>
        public void PlayOutOfBoundsNegativeSound()
        {
            _audioManager.PlayDefaultSFX(_outOfBoundsNegativeClip);
        }
    }
}