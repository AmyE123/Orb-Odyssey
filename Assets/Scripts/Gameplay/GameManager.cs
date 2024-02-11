namespace CT6RIGPR
{
    using System.Linq;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        private int _pickupTotal;
        private bool _hasCompletedLevel = false;

        [SerializeField] private GlobalReferences _globalReferences;
        [SerializeField] private int _pickupCount;

        /// <summary>
        /// A getter for the global references.
        /// </summary>
        public GlobalReferences GlobalReferences => _globalReferences;

        /// <summary>
        /// The total amount of pickups in the level.
        /// </summary>
        public int PickupTotal => _pickupTotal;

        /// <summary>
        /// The current amount of pickups which are picked up.
        /// </summary>
        public int PickupCount => _pickupCount;

        public void IncrementPickupCount()
        {
            _pickupCount++;
        }

        private void InitializeLevel()
        {
            SetPickupCount();
        }

        private void Update()
        {
            if (!_hasCompletedLevel)
            {
                if (_pickupTotal == _pickupCount)
                {
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
            _pickupTotal = FindObjectsOfType<Pickup>().Count();
            _globalReferences.UIManager.SetPickupTotal();
        }
    }
}