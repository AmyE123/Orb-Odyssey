namespace CT6RIGPR
{
    using UnityEngine;
    using UnityEngine.UI;

    public class MainMenuSettings : MonoBehaviour
    {
        private GlobalManager _globalManager;

        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;

        private void Start()
        {
            _globalManager = GlobalManager.Instance;
        }

        /// <summary>
        /// Sets the music volume from the slider.
        /// </summary>
        public void SetMusicVolume()
        {
            _globalManager.SetMusicVolume(_musicVolumeSlider.value);
        }

        /// <summary>
        /// Sets the SFX volume from the slider.
        /// </summary>
        public void SetSFXVolume()
        {
            _globalManager.SetSFXVolume(_sfxVolumeSlider.value);
        }
    }
}