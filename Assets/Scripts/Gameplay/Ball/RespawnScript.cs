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
        [SerializeField] private float _fadeInTime = 0.4f; //Percentage of time spent fading in
        [SerializeField] private float _fadeWaitTime = 0.2f; //Percentage of time waiting.
        [SerializeField] private float _fadeOutTime = 0.4f;  //Percentage of time fading out.
        [SerializeField] private float _respawnTime = Constants.RESPAWN_TIME;
        [SerializeField] private GlobalGameReferences _globalReferences;

        private void Start()
        {
            if (_gameManager == null)
			{
				_gameManager = FindObjectOfType<GameManager>();
				Debug.LogWarning("[CT6RIGPR] GameManager reference in respawn script not set. Please set this in the inspector.");
			}
			_ballController = _gameManager.GlobalReferences.BallController;
			_checkpoints = _gameManager.GlobalReferences.Checkpoints;
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

            _gameManager.GlobalReferences.GameSFXManager.PlayOutOfBoundsNegativeSound();

            // Disable movement
            _ballController.DisableInput();
            ballMat.DOColor(Color.black, 1).SetDelay(_fadeOutTime);

            yield return new WaitForSeconds(_respawnTime * (_fadeOutTime + _fadeWaitTime / 2));

            // Reset position and apply the saved rotation
            transform.position = FindCheckpoint();
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            yield return new WaitForSeconds(_respawnTime * (_fadeWaitTime / 2));

            ballMat.DOColor(Color.clear, 1).SetDelay(_fadeInTime);
            yield return new WaitForSeconds(_respawnTime * _fadeInTime);

            // Re-enable movement
            _ballController.EnableInput();

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
            if(other.tag == Constants.WATER_TAG)
            {
                Debug.Log("WATER!");
                StartCoroutine(Respawn());
            }
        }
    }
}