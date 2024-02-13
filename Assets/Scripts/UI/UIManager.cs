namespace CT6RIGPR
{
    using TMPro;
    using UnityEngine;

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [Header("Pickup UI Components")]
        [SerializeField] private TMP_Text _pickupCount;
        [SerializeField] private TMP_Text _pickupTotal;

        [Header("Debug UI Components")]
        [SerializeField] private TMP_Text _speedValue;
        [SerializeField] private TMP_Text _groundedValue;

        /// <summary>
        /// Sets the total pickups once it has been set in Game Manager
        /// </summary>
        public void SetPickupTotal()
        {
            _pickupTotal.text = _gameManager.CollectableTotal.ToString();
        }

        // Update is called once per frame
        private void Update()
        {
            _pickupCount.text = _gameManager.CollectableCount.ToString();
            _speedValue.text = _gameManager.GlobalReferences.BallController.CurrentSpeed.ToString("F1");
            _groundedValue.text = _gameManager.GlobalReferences.BallController.Grounded.ToString();
        }
    }
}