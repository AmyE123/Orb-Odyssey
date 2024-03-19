namespace CT6RIGPR
{
    using TMPro;
    using UnityEngine;

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [Header("Debug UI Components")]
        [SerializeField] private TMP_Text _speedValue;
        [SerializeField] private TMP_Text _groundedValue;

        // Update is called once per frame
        private void Update()
        {
            _speedValue.text = _gameManager.GlobalGameReferences.BallController.CurrentSpeed.ToString("F1");
            _groundedValue.text = _gameManager.GlobalGameReferences.BallController.Grounded.ToString();
        }
    }
}