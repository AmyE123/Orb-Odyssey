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


        private void SetPanelAlpha(float alpha)
        {
            Color currentColor = fadePanel.color;
            currentColor.a = alpha;
            fadePanel.color = currentColor;
        }
        IEnumerator FadeFromBlack()
        {
            yield return new WaitForSeconds(1);

            float fadeDuration = 0.5f;
            fadePanel.color = Color.black;

            fadePanel.DOFade(0, fadeDuration);
        }

        public void IncrementCollectableCount()
        {
            _collectableCount++;
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
                    _victoryManager.VictoryScreen();
                }

                
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                _hasCompletedLevel = true;
                _victoryManager.VictoryScreen();
            }
        }

        private void Start()
        {
            SetPanelAlpha(1.0f);
            StartCoroutine(FadeFromBlack());

            InitializeLevel();

            if (_globalReferences == null)
            {
                Debug.LogAssertion("[CT6RIGPR] Global References is NULL! Please set this in the inspector.");
            }
        }

        private void SetPickupCount()
        {
            _collectableTotal = FindObjectsOfType<Collectable>().Count();
            _globalReferences.UIManager.SetPickupTotal();
        }
    }
}