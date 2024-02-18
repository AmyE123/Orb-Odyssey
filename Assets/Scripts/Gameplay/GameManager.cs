namespace CT6RIGPR
{
    using System.Collections;
    using System.Linq;
    using UnityEngine;
    using DG.Tweening;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        private int _collectableTotal;
        private bool _hasCompletedLevel = false;

        [SerializeField] private GlobalGameReferences _globalReferences;
        [SerializeField] private int _collectableCount;
        [SerializeField] private Vector3 _victoryPosition, _victoryRotation;
        [SerializeField] private Camera _victoryCamera, _playerCamera;
        [SerializeField] private DoorScript _doorScript;
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _playerCockpit;
        [SerializeField] private Vector3 _victoryCameraPosition;
        [SerializeField] private Vector3 _victoryCameraRotation;
        [SerializeField] private Transform _doorCenter;

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
                    VictoryScreen();
                }

                
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _hasCompletedLevel = true;
                VictoryScreen();
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

        private void BlendToVictoryCamera()
        {
            // Disable the player camera component and enable the victory camera component
            _playerCamera.enabled = false;
            _victoryCamera.enabled = true;

            // Move and rotate the victory camera to the target position and rotation
            _victoryCamera.transform.DOMove(_victoryCameraPosition, 1);
            _victoryCamera.transform.DORotateQuaternion(Quaternion.Euler(_victoryCameraRotation), 1);
        }

        private void VictoryScreen()
        {
            _globalReferences.GameSFXManager.PlayVictorySounds();

            Transform playerTransform = _globalReferences.BallController.gameObject.transform;           

            //Player
            _globalReferences.BallController.DisableInput(true);
            playerTransform.position = _victoryPosition;
            playerTransform.localRotation = Quaternion.Euler(_victoryRotation);

            //Camera
            BlendToVictoryCamera();

            //Door
            StartCoroutine(DoorOpen());
        }

        IEnumerator DoorOpen()
        {
            yield return new WaitForSeconds(2);
            _doorScript.ToggleDoor();

            // Wait for the door to fully open
            yield return new WaitForSeconds(_doorScript.DoorOpenDuration);

            // Move the player to the center of the door
            MovePlayerToDoorCenter();
        }

        private void MovePlayerToDoorCenter()
        {
            _globalReferences.GameSFXManager.PlayPortalPullSound();
            Transform playerTransform = _globalReferences.BallController.gameObject.transform;
            float moveDuration = 0.5f;
            float scaleDuration = 0.3f;
            Vector3 disappearScale = Vector3.zero;

            _player.transform.DOMove(_doorCenter.position, moveDuration).SetEase(Ease.InExpo)
                .OnComplete(() =>
                {
                    _player.transform.DOScale(disappearScale, scaleDuration).OnComplete(() =>
                    {
                        _player.SetActive(false);
                        FadeToBlackAndLoadNextLevel();
                    });
                });
        }

        private void FadeToBlackAndLoadNextLevel()
        {
            float fadeDuration = 1.0f;
            fadePanel.DOFade(1, fadeDuration).OnComplete(() =>
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Boot");
            });
        }

    }
}