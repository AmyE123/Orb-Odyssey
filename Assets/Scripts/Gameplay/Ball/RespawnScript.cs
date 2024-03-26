namespace CT6RIGPR
{
    using DG.Tweening;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Handles the respawn mechanic when the player moves out of the island's boundary.
    /// </summary>
    public class RespawnScript : MonoBehaviour
    {
        private BallController _ballController;
        private bool isRespawning = false;

        [SerializeField] private GameObject[] _checkpoints;
		[SerializeField] private GameManager _gameManager;
        [SerializeField] private float _respawnTime = Constants.RESPAWN_TIME;
        [SerializeField] private GlobalGameReferences _globalReferences;


        /// <summary>
        /// Bool to check if the player is currently respawning
        /// </summary>
        public bool IsRespawning => isRespawning;

        private void Start()
        {
            if (_gameManager == null)
			{
				_gameManager = FindObjectOfType<GameManager>();
				Debug.LogWarning("[CT6RIGPR] GameManager reference in respawn script not set. Please set this in the inspector.");
			}
			_ballController = _gameManager.GlobalGameReferences.BallController;
			_checkpoints = _gameManager.GlobalGameReferences.Checkpoints;
		}

        private Vector3 FindCheckpoint()
        {
            GameObject closestCheckpoint = null;
            float distanceSqrToPoint = 100000.0f; //High value that will be beaten by the first checkpoint.

            if (_checkpoints.Length > 0)
            {
                foreach (GameObject checkpoint in _checkpoints)
                {
                    float distanceSqr = Vector3.SqrMagnitude(checkpoint.transform.position - transform.position);
                    if (distanceSqr < distanceSqrToPoint)
                    {
                        closestCheckpoint = checkpoint;
                        distanceSqrToPoint = distanceSqr;
                    }
                }
                return closestCheckpoint.transform.position;
            }
            else
            {
                return Vector3.zero;
            }

        }

        private IEnumerator Respawn()
        {
            if (isRespawning)
                yield break;

            Material ballMat = _globalReferences.BallMaterial;

            if (ballMat == null)
            {
                Debug.LogWarning("[CT6RIGPR]: Material to fade is not assigned.");
                yield break;
            }
            ballMat.color = Color.clear;

            isRespawning = true;
            _gameManager.GlobalGameReferences.GameSFXManager.PlayOutOfBoundsNegativeSound();

            _ballController.DisableInput();
            _ballController.FreezePlayer();

            // Fade out and wait for completion
            Tween fadeOutTween = _ballController.FadeOutBall();
            yield return fadeOutTween.WaitForCompletion();

            // Wait for respawn time
            yield return new WaitForSeconds(_respawnTime);

            // Reset position and apply the saved rotation
            transform.position = FindCheckpoint();
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            // Fade in and wait for completion
            Tween fadeInTween = _ballController.FadeInBall();
            yield return fadeInTween.WaitForCompletion();

            _ballController.EnableInput();
            _ballController.UnfreezePlayer();

            isRespawning = false;
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.tag == Constants.ISLAND_BOUNDARY_TAG)
            {
                StartCoroutine(Respawn());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.WATER_TAG)
            {
                Debug.Log("WATER!");
                StartCoroutine(Respawn());
            }
        }
    }
}