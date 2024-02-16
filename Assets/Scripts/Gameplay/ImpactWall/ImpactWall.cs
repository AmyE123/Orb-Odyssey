namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;

    public class ImpactWall : MonoBehaviour
    {
        [SerializeField] private GameObject _wall;
        [SerializeField] private GameObject[] _rocks;
        [SerializeField] private ParticleSystem _impactParticles;

        [Header("Breaking Visuals")]
        [SerializeField] private float _durationUntilDestroy = 3f;
        [SerializeField] private float _breakForce = 20f;
        [SerializeField] private float _rotationForce = 90f;

        [Header("SFX")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _impactClip;

        private void OnTriggerEnter(Collider other)
        {
            // Check if the collider belongs to the player
            if (other.CompareTag(Constants.PLAYER_TAG))
            {
                PlayParticles();
                BreakWall();
                GetComponent<BoxCollider>().enabled = false;
                Destroy(this, _durationUntilDestroy + 0.1f);
            }
        }

        private void PlayParticles()
        {
            // Ensure the particle system is not null
            if (_impactParticles != null)
            {
                _impactParticles.Play();
            }
            else
            {
                Debug.LogWarning("[CT6RIGPR]: Impact particles not assigned to ImpactWall!");
            }
        }

        private void BreakWall()
        {
            _audioSource.PlayOneShot(_impactClip);

            foreach (GameObject wall in _rocks)
            {
                // Generate a random direction for movement, excluding the upward movement by setting y = 0
                Vector3 randomDirection = Random.insideUnitSphere * _breakForce;
                randomDirection.y = 0; // This ensures there's no upward movement

                // Optionally, if you want a slight downward movement, you can set y to a negative value
                // randomDirection.y = -Mathf.Abs(randomDirection.y); // This ensures the movement is slightly downward

                Vector3 randomRotation = new Vector3(Random.Range(-_rotationForce, _rotationForce), Random.Range(-_rotationForce, _rotationForce), Random.Range(-_rotationForce, _rotationForce));

                // Apply movement and rotation to each child
                wall.transform.DOMove(wall.transform.position + randomDirection, _durationUntilDestroy).SetEase(Ease.OutQuad);
                wall.transform.DORotate(randomRotation, _durationUntilDestroy, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad);
            }

            // Schedule the wall destruction after the animation completes
            Destroy(_wall, _durationUntilDestroy);
        }
    }
}