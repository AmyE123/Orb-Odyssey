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
        [SerializeField] private LevelData _currentLevel;
        [SerializeField] private AudioManager _audioManager;

        [Header("Player Settings")]
        [SerializeField] private float _musicVolume = 1;
        [SerializeField] private float _sfxVolume = 1;

        [Header("Audio")]
        [SerializeField] private AudioClip _defaultButtonPressSFX;
        [SerializeField] private AudioClip _defaultButtonBackSFX;

        public int Level1Score { get; set; }
        public int Level2Score { get; set; }
        public int Level3Score { get; set; }

        public int Level4Score { get; set; }

        /// <summary>
        /// The current volume set for music.
        /// </summary>
        public float MusicVolume => _musicVolume;

        /// <summary>
        /// The current volume set for SFX.
        /// </summary>
        public float SfxVolume => _sfxVolume;

        public AudioManager AudioManager => _audioManager;

        /// <summary>
        /// Sets the current levels score.
        /// </summary>
        /// <param name="newScore">The new score for the level.</param>
        public void SetCurrentLevelScore(int newScore)
        {
            for (int i = 0; i < _allLevels.Length; i++)
            {
                if (_allLevels[i] == _currentLevel)
                {
                    if (i == 0)
                    {
                        Level1Score = newScore;
                    }
                    if (i == 1)
                    {
                        Level2Score = newScore;
                    }
                    if (i == 2)
                    {
                        Level3Score = newScore;
                    }
                    if (i == 3)
                    {
                        Level4Score = newScore;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the score of the current level.
        /// </summary>
        /// <returns>The score of the current level.</returns>
        public int GetCurrentLevelScore()
        {
            for (int i = 0; i < _allLevels.Length; i++)
            {
                if (_allLevels[i] == _currentLevel)
                {
                    if (i == 0)
                    {
                        return Level1Score;
                    }
                    if (i == 1)
                    {
                        return Level2Score;
                    }
                    if (i == 2)
                    {
                        return Level3Score;
                    }
                    if (i == 3)
                    {
                        return Level4Score;
                    }
                }

                return 0;
            }

            return 0;
        }

        /// <summary>
        /// Resets all game values. Scores and current level.
        /// </summary>
        public void ResetAllGameValues()
        {
            Level1Score = 0;
            Level2Score = 0;
            Level3Score = 0;
            Level4Score = 0;

            _currentLevel = _allLevels[0];

            PlayerPrefs.DeleteAll();
        }

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

        /// <summary>
        /// Sets the current level to the right level index.
        /// </summary>
        /// <param name="levelIdx">The level index to set it to.</param>
        public void SetCurrentLevel(int levelIdx)
        {
            _currentLevel = _allLevels[levelIdx];
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}