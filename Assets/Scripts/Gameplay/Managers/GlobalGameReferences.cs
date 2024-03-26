namespace CT6RIGPR
{
    using UnityEngine;

    public class GlobalGameReferences : MonoBehaviour
    {
        [SerializeField] private BallController _ballController;
        [SerializeField] private BallCockpit _ballCockpit;
        [SerializeField] private LevelManager _levelManager;
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

        private void Start()
        {
            CheckReferences();
            PopulateArrays();
        }

        private void Update()
        {
            if (_gameSFXManager == null)
            {
                _gameSFXManager = FindObjectOfType<GameSFXManager>();
            }
        }

        private void CheckReferences()
        {
            if (_ballController == null)
            {
                LogNullRef(typeof(BallController).Name, LogType.Error);
            }

            if (_cameraController == null)
            {
                LogNullRef(typeof(CameraController).Name, LogType.Error);
            }

            if (_gameSFXManager == null)
            {
                _gameSFXManager = FindObjectOfType<GameSFXManager>();
            }
        }

        private void PopulateArrays()
        {
            _checkpoints = GameObject.FindGameObjectsWithTag(Constants.CHECKPOINT_TAG);
            _powerups = GameObject.FindGameObjectsWithTag(Constants.POWERUP_TAG);
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