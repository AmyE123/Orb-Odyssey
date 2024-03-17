namespace CT6RIGPR
{
    using TMPro;
    using UnityEngine;

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [Header("Score UI Components")]
        [SerializeField] private TMP_Text _scoreValue;

        [Header("Debug UI Components")]
        [SerializeField] private TMP_Text _speedValue;
        [SerializeField] private TMP_Text _groundedValue;

        /// <summary>
        /// Sets the total score once it has been set in Game Manager
        /// </summary>
        public void SetScoreUIValue()
        {
            _scoreValue.text = GlobalManager.Instance.Level1Score.ToString();
        }

        // Update is called once per frame
        private void Update()
        {
            SetScoreUIValue();

            _speedValue.text = _gameManager.GlobalReferences.BallController.CurrentSpeed.ToString("F1");
            _groundedValue.text = _gameManager.GlobalReferences.BallController.Grounded.ToString();
        }
    }
}