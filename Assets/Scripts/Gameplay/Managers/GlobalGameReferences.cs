namespace CT6RIGPR
{
    using UnityEngine;

    public class GlobalGameReferences : MonoBehaviour
    {
        private bool _isFollowingSpline = false;

        [SerializeField] private BallController _ballController;
        [SerializeField] private BallCockpit _ballCockpit;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private PowerupManager _powerupManager;
        [SerializeField] private GameObject[] _checkpoints;
        [SerializeField] private GameObject[] _powerups;

        [SerializeField] private CameraController _cameraController;
        [SerializeField] private GameSFXManager _gameSFXManager;

        [SerializeField] private UIManager _uiManager;

        [SerializeField] private Rigidbody _playerRigidbody;
        
        [SerializeField] private ProfileManager _profileManager;

        /// <summary>
        /// The material around the ball. Used for fading between levels.
        /// </summary>
        public Material BallMaterial;

        /// <summary>
        /// The ball controller. For all player inputs.
        /// </summary>
        public BallController BallController => _ballController;

        /// <summary>
        /// The ball cockpit.
        /// </summary>
        public BallCockpit BallCockpit => _ballCockpit;

        /// <summary>
        /// The rigidbody for the player.
        /// </summary>
        public Rigidbody PlayerRigidbody => _playerRigidbody;

        /// <summary>
        /// The powerup manager.
        /// </summary>
        public PowerupManager PowerupManager => _powerupManager;

        /// <summary>
        /// The checkpoints in a level.
        /// </summary>
        public GameObject[] Checkpoints => _checkpoints;

        /// <summary>
        /// The powerups in a level.
        /// </summary>
        public GameObject[] Powerups => _powerups;

        /// <summary>
        /// The global profile manager for the 4DoF.
        /// </summary>
        public ProfileManager ProfileManager => _profileManager;

        /// <summary>
        /// The main camera controller.
        /// </summary>
        public CameraController CameraController => _cameraController;

        /// <summary>
        /// The SFX manager for the game.
        /// </summary>
        public GameSFXManager GameSFXManager => _gameSFXManager;

        /// <summary>
        /// The UI manager for the game.
        /// </summary>
        public UIManager UIManager => _uiManager;

        /// <summary>
        /// The level manager for the game.
        /// </summary>
        public LevelManager LevelManager => _levelManager;

        public bool IsFollowingSpline => _isFollowingSpline;

        private void Start()
        {
            CheckAndSetReferences();
            PopulateArrays();
        }

        private void Update()
        {
            if (_gameSFXManager == null)
            {
                _gameSFXManager = FindObjectOfType<GameSFXManager>();
            }
        }

        private void CheckAndSetReferences()
        {
            if (_ballController == null)
            {
                LogNullRef(typeof(BallController).Name, LogType.Error);
                _ballController = FindObjectOfType<BallController>();
            }

            if (_ballCockpit == null)
            {
                LogNullRef(typeof(BallCockpit).Name, LogType.Error);
                _ballCockpit = FindObjectOfType<BallCockpit>();
            }

            if (_checkpoints == null)
            {
                LogNullRef("Checkpoints", LogType.Error);
            }

            if (_profileManager == null)
            {
                LogNullRef(typeof(ProfileManager).Name, LogType.Error);
                _profileManager = FindObjectOfType<ProfileManager>();
            }

            if (_uiManager == null)
            {
                LogNullRef(typeof(UIManager).Name, LogType.Error);
                _uiManager = FindObjectOfType<UIManager>();
            }

            if (_playerRigidbody == null)
            {
                LogNullRef("Player Rigidbody", LogType.Error);
            }

            if (_cameraController == null)
            {
                LogNullRef(typeof(CameraController).Name, LogType.Error);
                _cameraController = FindObjectOfType<CameraController>();
            }

            if (_powerupManager == null)
            {
                LogNullRef(typeof(PowerupManager).Name, LogType.Error);
                _powerupManager = FindObjectOfType<PowerupManager>();
            }

            if (_gameSFXManager == null)
            {
                LogNullRef(typeof(GameSFXManager).Name, LogType.Warning);
                _gameSFXManager = FindObjectOfType<GameSFXManager>();
            }

            if (_levelManager == null)
            {
                LogNullRef(typeof(LevelManager).Name, LogType.Error);
                _levelManager = FindObjectOfType<LevelManager>();
            }

        }

        private void PopulateArrays()
        {
            _checkpoints = GameObject.FindGameObjectsWithTag(Constants.CHECKPOINT_TAG);
            _powerups = GameObject.FindGameObjectsWithTag(Constants.POWERUP_TAG);
        }

        public void SetIsFollowingSpline(bool value)
        {
            _isFollowingSpline = value;
        }

        private void LogNullRef(string refName, LogType logType)
        {
            string message = $"[CT6RIGPR] {refName} is null. Leaving this null can cause issues in the game. \n Please set this in the GlobalReferences.";

            switch (logType)
            {
                case LogType.Error:
                    Debug.LogError(message);
                    break;
                case LogType.Assert:
                    Debug.LogAssertion(message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogType.Log:
                    Debug.Log(message);
                    break;
            }
        }
    }
}