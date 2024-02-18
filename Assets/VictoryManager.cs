namespace CT6RIGPR
{
    using DG.Tweening;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class VictoryManager : MonoBehaviour
    {
        [Header("Global References")]
        [Tooltip("References to global game objects and scripts")]
        [SerializeField] private GlobalGameReferences _globalReferences;
        [SerializeField] private DoorScript _doorScript;
        [SerializeField] private Image fadePanel;

        [Header("Player Settings")]
        [Tooltip("Player game object")]
        [SerializeField] private GameObject _player;

        [Tooltip("Distance of the player from the door during victory animation.")]
        [SerializeField] private float _playerDistanceFromDoor = 5f;

        [Header("Door Settings")]
        [Tooltip("Transform representing the center of the door")]
        [SerializeField] private Transform _doorCenter;

        [Header("Camera Settings")]
        [Tooltip("Camera used during victory sequence")]
        [SerializeField] private Camera _victoryCamera;

        [Tooltip("Main player camera")]
        [SerializeField] private Camera _playerCamera;

        [Tooltip("Distance of the camera from the door on the X-axis")]
        [SerializeField] private float _cameraXDistanceFromDoor = 10f;

        [Tooltip("Distance of the camera from the door on the Y-axis")]
        [SerializeField] private float _cameraYDistanceFromDoor = 10f;

        [Tooltip("Downward offset of the camera after setting the look-at position")]
        [SerializeField] private float _cameraDownwardOffset = 2.5f;      

        public void VictoryScreen()
        {
            _globalReferences.GameSFXManager.PlayVictorySounds();

            Transform playerTransform = _globalReferences.BallController.gameObject.transform;

            Vector3 victoryPosition = _doorCenter.position - _doorCenter.forward * _playerDistanceFromDoor; // 3 units in front of the door
            Quaternion victoryRotation = Quaternion.LookRotation(_doorCenter.forward);

            playerTransform.position = victoryPosition;
            playerTransform.rotation = victoryRotation;

            _globalReferences.BallController.DisableInput(true);
            _globalReferences.BallController.FreezePlayer();

            BlendToVictoryCamera();
            StartCoroutine(DoorOpen());
        }

        private void BlendToVictoryCamera()
        {
            _playerCamera.enabled = false;
            _victoryCamera.enabled = true;

            Vector3 initialPosition = _doorCenter.position - _doorCenter.forward * _cameraXDistanceFromDoor;
            initialPosition.y += _cameraYDistanceFromDoor;

            _victoryCamera.transform.position = initialPosition;
            _victoryCamera.transform.DOLookAt(_doorCenter.position, 1);

            Vector3 finalPosition = _victoryCamera.transform.position - _victoryCamera.transform.up * _cameraDownwardOffset;

            _victoryCamera.transform.DOMove(finalPosition, 1);
        }

        public IEnumerator DoorOpen()
        {
            yield return new WaitForSeconds(2);
            _doorScript.ToggleDoor();

            yield return new WaitForSeconds(_doorScript.DoorOpenDuration);

            MovePlayerToDoorCenter();
        }

        private void MovePlayerToDoorCenter()
        {
            _globalReferences.GameSFXManager.PlayPortalPullSound();
            Transform playerTransform = _globalReferences.BallController.gameObject.transform;
            float moveDuration = 1f;
            Vector3 currentScale = _player.transform.localScale;
            Vector3 disappearScale = Vector3.zero;

            // Start shrinking the player at the same time as moving
            _player.transform.DOScale(disappearScale, moveDuration).From(currentScale).SetEase(Ease.Linear);

            // Move the player with a smoother and less dramatic easing
            _player.transform.DOMove(_doorCenter.position, moveDuration).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    _player.SetActive(false);
                    _globalReferences.LevelManager.FadeToBlackAndLoadNextLevel();
                    //FadeToBlackAndLoadNextLevel();
                });
        }

        public void FadeToBlackAndLoadNextLevel()
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