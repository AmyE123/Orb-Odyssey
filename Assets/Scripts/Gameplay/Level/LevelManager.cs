namespace CT6RIGPR
{
    using UnityEngine;
    using DG.Tweening;
    using TMPro;
    using System.Collections.Generic;
    using UnityEngine.XR;

    public class LevelManager : MonoBehaviour
    {
        private GlobalManager _globalManager;

        [SerializeField] private bool _isBeginningLevel = false;
        [SerializeField] private GameObject _warningGO;
        [SerializeField] private CanvasGroup _warningCanvasGroup;
        [SerializeField] private bool _hasReadWarning = false;

        [Header("Level Properties")]
        [SerializeField] private int _levelTimeLimitMinutes;
        [SerializeField] private int _warningTimeLimitMinutes;

        [Header("Score Properties")]
        [SerializeField] private int _levelScore;

        [Header("Score UI Components")]
        [SerializeField] private TMP_Text _scoreValueText;

        /// <summary>
        /// Whether the player has read the warning for the game.
        /// </summary>
        public bool HasReadWarning => _hasReadWarning;

        /// <summary>
        /// A getter for how many minutes the level is.
        /// </summary>
        public int LevelTimeLimitMinutes => _levelTimeLimitMinutes;

        /// <summary>
        /// A getter for the warning time limit in minutes.
        /// </summary>
        public int WarningTimeLimitMinutes => _warningTimeLimitMinutes;

        /// <summary>
        /// Get the next level from the global manager
        /// </summary>
        /// <param name="currentLevelName">The name of the current level.</param>
        /// <returns>Level data for the next level.</returns>
        public LevelData GetNextLevel(string currentLevelName)
        {
            LevelData[] allLevels = _globalManager.GetAllLevels();

            for (int i = 0; i < allLevels.Length; i++)
            {
                if (allLevels[i].SceneName == currentLevelName)
                {
                    int nextIndex = i + 1;
                    if (nextIndex < allLevels.Length)
                    {
                        GlobalManager.Instance.SetCurrentLevel(nextIndex);
                        return allLevels[nextIndex];
                    }
                    break;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the first level from the global manager.
        /// </summary>
        /// <returns>Level data for the first level.</returns>
        public LevelData GetFirstLevel()
        {
            LevelData[] allLevels = _globalManager.GetAllLevels();
            return allLevels[0];
        }

        /// <summary>
        /// Increment the level score.
        /// </summary>
        public void IncrementLevelScore()
        {
            _levelScore += Constants.COLLECTABLE_SCORE_AMOUNT;
            _globalManager.SetCurrentLevelScore(_levelScore);
        }

        private void Start()
        {
            _globalManager = GlobalManager.Instance;

            if (_isBeginningLevel)
            {
                ShowWarning();
            }
            else
            {
                _hasReadWarning = true;
                HideWarning();
            }
        }

        private void Update()
        {

            if (!_hasReadWarning)
            {
                bool buttonB = false;
                var leftHandedControllers = new List<InputDevice>();
                var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
                InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

                foreach (var device in leftHandedControllers)
                {
                    device.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonB);
                }

                if (buttonB)
                {
                    _hasReadWarning = true;
                    HideWarning();

                }
            }

            // TODO: Debug here for showcase purposes. Delete at some point before building final build.
            if(Input.GetKeyDown(KeyCode.Equals))
            {
                Debug.Log("Adding 100 to score");
                IncrementLevelScore();
            }

            _scoreValueText.text = _levelScore.ToString();
        }

        private void ShowWarning()
        {
            _warningGO.SetActive(true);

            _warningCanvasGroup.alpha = 0;
            _warningCanvasGroup.DOFade(1, 1);
        }

        private void HideWarning()
        {
            _warningCanvasGroup.DOFade(0, 1)
            .OnComplete(() =>
            {
                _warningGO.SetActive(false);
            });
        }
    }
}