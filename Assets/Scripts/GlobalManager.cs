namespace CT6RIGPR
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// A manager which has persistant data between the entire gameflow.
    /// </summary>
    public class GlobalManager : MonoBehaviour
    {
        public static GlobalManager Instance { get; private set; }

        [Header("Game Values")]
        [SerializeField] private LevelData[] _allLevels;
        [SerializeField] private AudioManager _audioManager;

        [Header("Player Settings")]
        [SerializeField] private float _musicVolume = 1;
        [SerializeField] private float _sfxVolume = 1;

        [Header("Audio")]
        [SerializeField] private AudioClip _defaultButtonPressSFX;
        [SerializeField] private AudioClip _defaultButtonBackSFX;

        /// <summary>
        /// The current volume set for music.
        /// </summary>
        public float MusicVolume => _musicVolume;

        /// <summary>
        /// The current volume set for SFX.
        /// </summary>
        public float SfxVolume => _sfxVolume;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
                UpdateAudioManager();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            UpdateAudioManager();
        }

        private void UpdateAudioManager()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            if (_audioManager != null)
            {
                _audioManager.UpdateVolumes();
            }
        }

        /// <summary>
        /// Gets a reference to all the levels in the game.
        /// </summary>
        /// <returns></returns>
        public LevelData[] GetAllLevels()
        {
            return _allLevels;
        }

        /// <summary>
        /// Plays the default button press SFX.
        /// </summary>
        public void PressButtonAudioFeedback()
        {
            _audioManager.PlayDefaultSFX(_defaultButtonPressSFX);
        }

        /// <summary>
        /// Plays the default button press SFX but with a delay for things like sliders.
        /// </summary>
        public void PressButtonDelayAudioFeedback()
        {
            _audioManager.PlayDefaultDelaySFX(_defaultButtonPressSFX);
        }

        /// <summary>
        /// Plays the default button back SFX.
        /// </summary>
        public void PressButtonBackAudioFeedback() 
        {
            _audioManager.PlayDefaultSFX(_defaultButtonBackSFX);
        }

        /// <summary>
        /// For globally setting the music volume.
        /// </summary>
        /// <param name="newVolume">The new volume to set this to.</param>
        public void SetMusicVolume(float newVolume)
        {
            _musicVolume = newVolume;
            _audioManager.UpdateVolumes();
        }

        /// <summary>
        /// For globally setting the SFX volume.
        /// </summary>
        /// <param name="newVolume">The new volume to set this to.</param>
        public void SetSFXVolume(float newVolume)
        {
            _sfxVolume = newVolume;
            _audioManager.UpdateVolumes();
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}