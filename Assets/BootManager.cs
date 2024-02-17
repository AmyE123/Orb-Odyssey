namespace CT6RIGPR
{
    using DG.Tweening;
    using System.Collections;
    using UnityEngine;

    public class BootManager : MonoBehaviour
    {
        [Header("Start Menu Values")]
        [SerializeField] private CanvasGroup _startMenuCG;
        [SerializeField] private RectTransform _startMenuTransform;
        [SerializeField] private Transform _startMenuTitleTransform;
        [SerializeField] private Transform[] _startMenuButtonTransforms;
        [SerializeField] private float _startMenuSpawnSpeed = 1f;
        [SerializeField] private float _startMenuButtonSpawnDelay = 0.4f;

        [Header("Level Select Values")]
        [SerializeField] private CanvasGroup _levelSelectCG;
        [SerializeField] private RectTransform _levelSelectTransform;

        [Header("Settings Screen Values")]
        [SerializeField] private CanvasGroup _settingsCG;
        [SerializeField] private RectTransform _settingsTransform;

        [Header("General Transition Settings")]
        [SerializeField] private float _fadeDuration = 0.2f;
        [SerializeField] private float _transitionDuration = 0.5f;
        [SerializeField] private Vector3 _offScreenRight;
        [SerializeField] private Vector3 _offScreenLeft;

        public void OnPlayButtonPressed()
        {
            StartCoroutine(TransitionToLevelSelect());
        }
        public void OnBackButtonPressed()
        {
            StartCoroutine(TransitionToStartMenu());
        }

        public void OnSettingsBackButtonPressed()
        {
            StartCoroutine(TransitionFromSettings());
        }

        public void OnSettingsButtonPressed()
        {
            StartCoroutine(TransitionToSettings());
        }

        private void Awake()
        {
            _levelSelectCG.alpha = 0;
            _levelSelectTransform.anchoredPosition = _offScreenRight;

            _settingsCG.alpha = 0;
            _settingsTransform.anchoredPosition = _offScreenRight;

            _startMenuCG.alpha = 1;
            _startMenuTransform.anchoredPosition = Vector2.zero;
        }

        private void Start()
        { 
            InitializeStartMenuUI();        
        }

        private void InitializeStartMenuUI()
        {
            _startMenuTitleTransform.localScale = Vector3.zero;
            foreach (var button in _startMenuButtonTransforms)
            {
                button.localScale = Vector3.zero;
            }

            StartCoroutine(StartMenuSequence());
        }

        private IEnumerator StartMenuSequence()
        {        
            _startMenuTitleTransform.DOScale(1, _startMenuSpawnSpeed).SetEase(Ease.OutBounce);

            yield return new WaitForSeconds(_startMenuSpawnSpeed);

            foreach (var button in _startMenuButtonTransforms)
            {
                button.DOScale(1, _startMenuSpawnSpeed).SetEase(Ease.OutBounce);
                yield return new WaitForSeconds(_startMenuButtonSpawnDelay);
            }
        }


        private IEnumerator TransitionToLevelSelect()
        {
            _startMenuCG.DOFade(0, _fadeDuration);
            _startMenuTransform.DOAnchorPosX(_offScreenLeft.x, _transitionDuration);

            _levelSelectCG.DOFade(1, _fadeDuration);
            _levelSelectTransform.DOAnchorPosX(0, _transitionDuration);

            yield return new WaitForSeconds(_transitionDuration);

            _startMenuCG.alpha = 0;
            _startMenuTransform.anchoredPosition = new Vector2(_offScreenLeft.x, _startMenuTransform.anchoredPosition.y);
        }

        private IEnumerator TransitionToSettings()
        {
            _startMenuCG.DOFade(0, _fadeDuration);
            _startMenuTransform.DOAnchorPosX(_offScreenLeft.x, _transitionDuration);

            _settingsCG.DOFade(1, _fadeDuration);
            _settingsTransform.DOAnchorPosX(0, _transitionDuration);

            yield return new WaitForSeconds(_transitionDuration);

            _startMenuCG.alpha = 0;
            _startMenuTransform.anchoredPosition = new Vector2(_offScreenLeft.x, _startMenuTransform.anchoredPosition.y);
        }

        private IEnumerator TransitionFromSettings()
        {
            _startMenuCG.DOFade(1, _fadeDuration);
            _startMenuTransform.DOAnchorPosX(0, _transitionDuration);

            _settingsCG.DOFade(0, _fadeDuration);
            _settingsTransform.DOAnchorPosX(_offScreenRight.x, _transitionDuration);

            yield return new WaitForSeconds(_transitionDuration);

            // Completely hide the settings screen
            _settingsCG.alpha = 0;
            _settingsTransform.anchoredPosition = new Vector2(_offScreenRight.x, _levelSelectTransform.anchoredPosition.y);
        }

        private IEnumerator TransitionToStartMenu()
        {
            _startMenuCG.DOFade(1, _fadeDuration);
            _startMenuTransform.DOAnchorPosX(0, _transitionDuration);

            _levelSelectCG.DOFade(0, _fadeDuration);
            _levelSelectTransform.DOAnchorPosX(_offScreenRight.x, _transitionDuration);

            yield return new WaitForSeconds(_transitionDuration);

            _levelSelectCG.alpha = 0;
            _levelSelectTransform.anchoredPosition = new Vector2(_offScreenRight.x, _levelSelectTransform.anchoredPosition.y);
        }
    }
}