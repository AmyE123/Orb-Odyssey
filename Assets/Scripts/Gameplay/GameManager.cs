namespace CT6RIGPR
{
    using System.Collections;
    using System.Linq;
    using UnityEngine;
    using DG.Tweening;

    public class GameManager : MonoBehaviour
    {
        private int _collectableTotal;
        private bool _hasCompletedLevel = false;

        [SerializeField] private GlobalReferences _globalReferences;
        [SerializeField] private int _collectableCount;
        [SerializeField] private Vector3 _victoryPosition, _victoryRotation;
        [SerializeField] private Camera _victoryCamera, _playerCamera;
        [SerializeField] private DoorScript _doorScript;
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _playerCockpit;
        [SerializeField] private Vector3 _victoryCameraPosition;
        [SerializeField] private Vector3 _victoryCameraRotation;

        /// <summary>
        /// A getter for the global references.
        /// </summary>
        public GlobalReferences GlobalReferences => _globalReferences;

        /// <summary>
        /// The total amount of pickups in the level.
        /// </summary>
        public int CollectableTotal => _collectableTotal;

        /// <summary>
        /// The current amount of pickups which are picked up.
        /// </summary>
        public int CollectableCount => _collectableCount;

        /// <summary>
        /// A getter for the level complete status
        /// </summary>
        public bool HasCompletedLevel => _hasCompletedLevel;


        public void IncrementCollectableCount()
        {
            _collectableCount++;
        }

        private void InitializeLevel()
        {
            SetPickupCount();
        }

        private void Update()
        {
            if (!_hasCompletedLevel)
            {
                if (_collectableTotal == _collectableCount)
                {                    
                    Debug.Log("[CT6RIGPR]: You got all pickups! Win!");
                    _hasCompletedLevel = true;
                    VictoryScreen();
                }

                
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _hasCompletedLevel = true;
                VictoryScreen();
            }
        }

        private void Start()
        {
            InitializeLevel();

            if (_globalReferences == null)
            {
                Debug.LogAssertion("[CT6RIGPR] Global References is NULL! Please set this in the inspector.");
            }
        }

        private void SetPickupCount()
        {
            _collectableTotal = FindObjectsOfType<Collectable>().Count();
            _globalReferences.UIManager.SetPickupTotal();
        }

        private void BlendToVictoryCamera()
        {
            // Disable the player camera component and enable the victory camera component
            _playerCamera.enabled = false;
            _victoryCamera.enabled = true;

            // Move and rotate the victory camera to the target position and rotation
            _victoryCamera.transform.DOMove(_victoryCameraPosition, 1);
            _victoryCamera.transform.DORotateQuaternion(Quaternion.Euler(_victoryCameraRotation), 1);
        }

        private void VictoryScreen()
        {
            Transform playerTransform = _globalReferences.BallController.gameObject.transform;           

            //Player
            _globalReferences.BallController.DisableInput(true);
            playerTransform.position = _victoryPosition;
            playerTransform.localRotation = Quaternion.Euler(_victoryRotation);

            //Camera
            BlendToVictoryCamera();

            //Door
            StartCoroutine("DoorOpen");

            //UI

        }

        IEnumerator DoorOpen()
        {
            yield return new WaitForSeconds(1);
            _doorScript.ToggleDoor();
        }
    }
}