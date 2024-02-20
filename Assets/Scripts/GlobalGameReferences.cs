namespace CT6RIGPR
{
    using UnityEngine;

    public class GlobalGameReferences : MonoBehaviour
    {
        [SerializeField] private BallController _ballController;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private GameObject[] _checkpoints;

        [SerializeField] private CameraController _cameraController;
        [SerializeField] private GameSFXManager _gameSFXManager;

        [SerializeField] private UIManager _uiManager;

        [SerializeField] private Rigidbody _playerRigidbody;

        [SerializeField] private ProfileManager _profileManager;

        /// <summary>
        /// The ball controller. For all player inputs.
        /// </summary>
        public BallController BallController => _ballController;

        public Rigidbody PlayerRigidbody => _playerRigidbody;

        public GameObject[] Checkpoints => _checkpoints;

        public ProfileManager ProfileManager => _profileManager;

        /// <summary>
        /// The main camera controller.
        /// </summary>
        public CameraController CameraController => _cameraController;

        public GameSFXManager GameSFXManager => _gameSFXManager;

        public UIManager UIManager => _uiManager;

        public LevelManager LevelManager => _levelManager;

        private void Start()
        {
            CheckReferences();
            PopulateArrays();
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
        }

        private void PopulateArrays()
        {
            _checkpoints = GameObject.FindGameObjectsWithTag(Constants.CHECKPOINT_TAG);
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