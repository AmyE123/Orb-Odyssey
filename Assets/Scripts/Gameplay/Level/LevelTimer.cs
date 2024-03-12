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

        private float _startTime;
        private bool _isFlashing;
        private bool _timerActive = true;
        private bool _hasTimerStartedAfterWarning = false;
        private float _lvlTimeLimit;
        private float _warningTimeLimit;
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
            _startTime = Time.time;

            if (_levelManager != null)
            {
                _lvlTimeLimit = _levelManager.LevelTimeLimitMinutes;
                _warningTimeLimit = _levelManager.WarningTimeLimitMinutes;
            }
            else
            {
                _lvlTimeLimit = 999;
                _warningTimeLimit = 998;
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
                _startTime = Time.time;
                _hasTimerStartedAfterWarning = true;
                _isFlashing = false;
            }

            if (_hasTimerStartedAfterWarning)
            {
                float timeSinceStarted = Time.time - _startTime;
                int minutes = (int)(timeSinceStarted / 60);
                int seconds = (int)(timeSinceStarted % 60);

                _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                if (minutes >= _warningTimeLimit && !_isFlashing)
                {
                    StartCoroutine(FlashTimerRed());
                    _isFlashing = true;
                }

                if (minutes >= _lvlTimeLimit)
                {
                    _timerActive = false;
                    StopCoroutine(FlashTimerRed());
                    _timerText.color = _uiTextDefaultColor;
                    FinishLevelFromTimeout();
                }
            }         
        }

        private void FinishLevelFromTimeout()
        {
            _gameManager.ForceLevelCompletion();          
        }

        private IEnumerator FlashTimerRed()
        {
            while (true)
            {
                _timerText.color = Color.red;
                yield return new WaitForSeconds(0.5f);
                _timerText.color = _uiTextDefaultColor;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}