namespace CT6RIGPR
{
    using System.Collections;
    using System.Linq;
    using UnityEngine;
    using DG.Tweening;
    using UnityEngine.UI;

    public class GameManager : MonoBehaviour
    {
        private int _collectableTotal;
        private bool _hasCompletedLevel = false;

        [SerializeField] private VictoryManager _victoryManager;
        [SerializeField] private GlobalGameReferences _globalReferences;
        [SerializeField] private int _collectableCount;
        [SerializeField] private Image fadePanel;

        /// <summary>
        /// A getter for the global references.
        /// </summary>
        public GlobalGameReferences GlobalReferences => _globalReferences;

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

        /// <summary>
        /// Increments the collectable count.
        /// </summary>
        public void IncrementCollectableCount()
        {
            _collectableCount++;
            _globalReferences.LevelManager.IncrementLevelScore();
        }

        /// <summary>
        /// A function to force-complete a level. Used when timer for level has ran out.
        /// </summary>
        public void ForceLevelCompletion()
        {
            _hasCompletedLevel = true;
            StartCoroutine(_victoryManager.CompleteLevel());
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
                    StartCoroutine(_victoryManager.CompleteLevel());
                }              
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                _hasCompletedLevel = true;
                StartCoroutine(_victoryManager.CompleteLevel());
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
        }
    }
}