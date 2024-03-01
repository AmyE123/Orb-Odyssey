namespace CT6RIGPR
{
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
		[SerializeField] private Image _fadeImage;
        [SerializeField] private float _fadeInTime = 0.4f; //Percentage of time spent fading in
        [SerializeField] private float _fadeWaitTime = 0.2f; //Percentage of time waiting.
        [SerializeField] private float _fadeOutTime = 0.4f;  //Percentage of time fading out.
        [SerializeField] private float _respawnTime = Constants.RESPAWN_TIME;

        private void Start()
        {
            if (_gameManager == null)
			{
				_gameManager = FindObjectOfType<GameManager>();
				Debug.LogWarning("[CT6RIGPR] GameManager reference in respawn script not set. Please set this in the inspector.");
			}
			_ballController = _gameManager.GlobalReferences.BallController;

		}

        private IEnumerator FadeOut()
        {
            Color colour = Color.black;
            float fadeAmount = 0.0f;
            while (fadeAmount <= 1.0f)
            {
                //reduce fade amount
                fadeAmount += Time.deltaTime / (_respawnTime * _fadeOutTime);
                colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
				//set that colour
				_fadeImage.color = colour;
                //wait for the frame to end
                yield return null;
            }
        }

        private IEnumerator FadeIn()
        {
            Color colour = Color.black;
            float fadeAmount = 1.0f;
            while (fadeAmount >= 0.0f)
            {
                //reduce fade amount
                fadeAmount -= Time.deltaTime / (_respawnTime * _fadeInTime);
                colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
				//set that colour
				_fadeImage.color = colour;
                //wait for the frame to end
                yield return null;
            }
        }

        private Vector3 FindCheckpoint()
        {
            GameObject closestCheckpoint = null;
            float distanceSqrToPoint = 100000.0f; //High value that will be beaten by the first checkpoint.
            _checkpoints = _gameManager.GlobalReferences.Checkpoints;

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

            isRespawning = true;

            _gameManager.GlobalReferences.GameSFXManager.PlayOutOfBoundsNegativeSound();

            // Disable movement
            _ballController.DisableInput(true);
            StartCoroutine(FadeOut());

            yield return new WaitForSeconds(_respawnTime * (_fadeOutTime + _fadeWaitTime / 2));

            // Reset position and apply the saved rotation
            transform.position = FindCheckpoint();
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            yield return new WaitForSeconds(_respawnTime * (_fadeWaitTime / 2));

            StartCoroutine(FadeIn());
            yield return new WaitForSeconds(_respawnTime * _fadeInTime);

            _fadeImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

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