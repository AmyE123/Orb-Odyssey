namespace CT6RIGPR
{
    using DG.Tweening;
    using System.Collections;
    using UnityEngine;

    public class MenuTransitionManager : MonoBehaviour
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
            InitializeUI();
        }

        private void Start()
        {
            InitializeStartMenuUI();
        }

        private void InitializeUI()
        {
            _levelSelectCG.alpha = 0;
            _levelSelectTransform.anchoredPosition = _offScreenRight;

            _settingsCG.alpha = 0;
            _settingsTransform.anchoredPosition = _offScreenRight;

            _startMenuCG.alpha = 1;
            _startMenuTransform.anchoredPosition = Vector2.zero;
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

        private IEnumerator TransitionUI(CanvasGroup fromCG, RectTransform fromTransform, Vector2 fromExitPosition, CanvasGroup toCG, RectTransform toTransform, Vector2 toEnterPosition)
        {
            fromCG.DOFade(0, _fadeDuration);
            fromTransform.DOAnchorPosX(fromExitPosition.x, _transitionDuration);

            toCG.DOFade(1, _fadeDuration);
            toTransform.DOAnchorPosX(toEnterPosition.x, _transitionDuration);

            yield return new WaitForSeconds(_transitionDuration);

            fromCG.alpha = 0;
            fromTransform.anchoredPosition = fromExitPosition;
        }

        private IEnumerator TransitionToLevelSelect()
        {
            yield return TransitionUI(_startMenuCG, _startMenuTransform, _offScreenLeft, _levelSelectCG, _levelSelectTransform, Vector2.zero);
        }

        private IEnumerator TransitionToSettings()
        {
            yield return TransitionUI(_startMenuCG, _startMenuTransform, _offScreenLeft, _settingsCG, _settingsTransform, Vector2.zero);
        }

        private IEnumerator TransitionFromSettings()
        {
            yield return TransitionUI(_settingsCG, _settingsTransform, _offScreenRight, _startMenuCG, _startMenuTransform, Vector2.zero);
        }

        private IEnumerator TransitionToStartMenu()
        {
            yield return TransitionUI(_levelSelectCG, _levelSelectTransform, _offScreenRight, _startMenuCG, _startMenuTransform, Vector2.zero);
        }
    }
}