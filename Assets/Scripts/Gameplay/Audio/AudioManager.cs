namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// The main audio manager. This should be in every scene.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private GlobalManager _globalManager;
        private float _lastSoundTime;

        [Header("All Audio Sources (Volume Reference)")]
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource[] _sfxAudioSources;

        [Header("SFX Types")]
        [SerializeField] private AudioSource _defaultSFXSource;
        [SerializeField] private AudioSource _movementSFXSource;

        [Header("Audio Manager Settings")]
        [SerializeField] private float _minimumTimeBetweenSounds = 0.1f;

        public AudioSource DefaultSFXSource => _defaultSFXSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _globalManager = GlobalManager.Instance;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Plays SFX sound instantly.
        /// </summary>
        /// <param name="audioClip">The SFX clip to play.</param>
        public void PlayDefaultSFX(AudioClip audioClip)
        {
            _defaultSFXSource.PlayOneShot(audioClip);
        }

        /// <summary>
        /// Plays SFX with a delay. Good for things like Sliders.
        /// </summary>
        /// <param name="audioClip">The SFX clip to play.</param>
        public void PlayDefaultDelaySFX(AudioClip audioClip)
        {
            if (Time.time - _lastSoundTime >= _minimumTimeBetweenSounds)
            {
                _defaultSFXSource.PlayOneShot(audioClip);
                _lastSoundTime = Time.time;
            }
        }

        /// <summary>
        /// Update the volumes of the music and SFX.
        /// </summary>
        public void UpdateVolumes()
        {
            if(_globalManager != null)
            {
                if(_musicAudioSource != null)
                {
                    _musicAudioSource.volume = _globalManager.MusicVolume;
                }

                if (_sfxAudioSources.Length > 0)
                {
                    foreach (AudioSource sfxAS in _sfxAudioSources)
                    {
                        sfxAS.volume = _globalManager.SfxVolume;
                    }
                }
            }
        }
    }
}