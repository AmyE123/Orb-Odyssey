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
        [SerializeField] private Camera _victoryCamera;
        [SerializeField] private DoorScript _doorScript;
        [SerializeField] private GameObject _player;
        [SerializeField] private Vector3 _victoryCameraPosition;

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
                    VictoryScreen();
                    Debug.Log("[CT6RIGPR]: You got all pickups! Win!");
                    _hasCompletedLevel = true;
                }
            }
        }

        private void Start ()
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

        private void VictoryScreen()
        {
            //Player
            _player.transform.position = _victoryPosition;
            _player.transform.localRotation = Quaternion.Euler(_victoryRotation.x, _victoryRotation.y, _victoryRotation.z);

            //Camera
            //_victoryCamera.transform.DOMove()

            //Door
            StartCoroutine("DoorOpen");

            //UI

        }

        IEnumerator DoorOpen()
        {
            yield return new WaitForSeconds(3);
            _doorScript.ToggleDoor();
        }
    }
}