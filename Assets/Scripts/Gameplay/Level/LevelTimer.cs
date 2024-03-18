namespace CT6RIGPR
{
    using System.Collections;
    using TMPro;
    using UnityEngine;

    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private TMP_Text _timerText;

        [Header("Timer Visual Values")]
        [SerializeField] private float _warningTimeSeconds = 10f;
        [SerializeField] private float _flashingDelay = 0.5f;

        private bool _isFlashing;
        private bool _timerActive = true;
        private float _timeRemaining;
        private bool _hasTimerStartedAfterWarning = false;
        
        private Color32 _uiTextDefaultColor = new Color32(255, 149, 0, 255);

        private void Start()
        {
            if (_levelManager == null)
            {
                Debug.LogWarning("[CT6RIGPR]: Please set the level manager within the level timer component.");
                _levelManager = GetComponent<LevelManager>();
            }

            if (_gameManager == null)
            {
                _gameManager = FindObjectOfType<GameManager>();
                Debug.LogWarning("[CT6RIGPR] GameManager reference in level timer script not set. Please set this in the inspector.");
            }

            InitializeValues();
        }

        private void InitializeValues()
        {
            if (_levelManager != null)
            {
                _timeRemaining = _levelManager.LevelTimeLimitMinutes * 60;
            }
            else
            {
                Debug.LogWarning("[CT6RIGPR]: Please set the level manager within the level timer component. Level properties are invalid.");
                _timeRemaining = 999 * 60;
            }
        }

        private void Update()
        {
            if (!_timerActive)
            {
                return;
            }

            if (_levelManager.HasReadWarning && !_hasTimerStartedAfterWarning)
            {
                _hasTimerStartedAfterWarning = true;
                _timeRemaining = _levelManager.LevelTimeLimitMinutes * 60;
            }

            if (_hasTimerStartedAfterWarning)
            {
                _timeRemaining -= Time.deltaTime;

                if (_timeRemaining <= 0)
                {
                    FinishLevelFromTimeout();
                    return;
                }

                UpdateTimerText();

                if (_timeRemaining <= _warningTimeSeconds && !_isFlashing)
                {
                    _isFlashing = true;
                    StartCoroutine(FlashTimer());                    
                }
            }
        }

        private void UpdateTimerText()
        {
            int minutes = (int)(_timeRemaining / 60);
            int seconds = (int)(_timeRemaining % 60);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void FinishLevelFromTimeout()
        {
            _timerActive = false;
            _gameManager.ForceLevelCompletion();
            StopCoroutine(FlashTimer());
            _timerText.color = _uiTextDefaultColor;
        }

        private IEnumerator FlashTimer()
        {
            while (_timerActive && _isFlashing)
            {
                _gameManager.GlobalGameReferences.GameSFXManager.PlayTimerBeepSound();
                _timerText.color = _timerText.color == Color.red ? _uiTextDefaultColor : Color.red;
                yield return new WaitForSeconds(_flashingDelay);
            }
        }
    }
}