namespace CT6RIGPR
{
    using System.Collections;
    using UnityEngine;
    using static CT6RIGPR.Constants;

    /// <summary>
    /// A script to manage powerups for the player.
    /// </summary>
    public class PowerupManager : MonoBehaviour
    {
        private bool _stickyEnabled;
        private bool _fastEnabled;
        private bool _freezeEnabled;
        private bool _slowEnabled;

        private bool _isSticking;

        [SerializeField] private int _stickyCharges;
        [SerializeField] private int _fastCharges;
        [SerializeField] private int _slowCharges;
        [SerializeField] private int _freezeCharges;

        [SerializeField] private BallController _ballController;

        /// <summary>
        /// The amount of sticky powerup charges the player has.
        /// </summary>
        public int StickyCharges => _stickyCharges;

        /// <summary>
        /// Whether a sticky powerup is enabled.
        /// </summary>
        public bool IsStickyEnabled => _stickyEnabled;

        /// <summary>
        /// The amount of fast powerup charges the player has.
        /// </summary>
        public int FastCharges => _fastCharges;

        /// <summary>
        /// Whether a fast powerup is enabled.
        /// </summary>
        public bool IsFastEnabled => _fastEnabled;

        /// <summary>
        /// The amount of slow powerup charges the player has.
        /// </summary>
        public int SlowCharges => _slowCharges;

        /// <summary>
        /// Whether a slow powerup is enabled.
        /// </summary>
        public bool IsSlowEnabled => _slowEnabled;

        /// <summary>
        /// The amount of freeze powerup charges the player has.
        /// </summary>
        public int FreezeCharges => _freezeCharges;

        /// <summary>
        /// Whether a freeze powerup is enabled.
        /// </summary>
        public bool IsFreezeEnabled => _freezeEnabled;

        public void AddCharge(PowerupType powerupType)
        {
            switch (powerupType)
            {
                case PowerupType.Sticky:
                    _stickyCharges++;
                    break;
                case PowerupType.Fast:
                    _fastCharges++;
                    break;
                case PowerupType.Slow:
                    _slowCharges++;
                    break;
            }
        }

        private void Start()
        {
            if (_ballController == null)
            {
                Debug.LogWarning("[CT6RIGPR] BallController reference in OuterBall not set. Please set this in the inspector.");
                _ballController = gameObject.GetComponent<BallController>();
            }

            // TODO: Layla - I don't know if you need this but I made it into a function anyway.
            InitializeDefaultCharges();
        }

        private void InitializeDefaultCharges()
        {
            _stickyCharges = DEFAULT_POWERUP_CHARGES;
            _fastCharges = DEFAULT_POWERUP_CHARGES;
            _freezeCharges = DEFAULT_POWERUP_CHARGES;
            _slowCharges = DEFAULT_POWERUP_CHARGES;
        }

        private void UsePowerup(PowerupType powerupType)
        {
            switch (powerupType)
            {
                case PowerupType.Sticky:
                    _stickyEnabled = true;
                    _stickyCharges--;
                    StartCoroutine(DisableStickyCoroutine(POWERUP_DEFAULT_DURATION));
                    break;
                case PowerupType.Fast:
                    _fastEnabled = true;
                    _fastCharges--;
                    _ballController.ChangeMaxForce(BALL_DEFAULT_MAX_BUFFED_FORCE);
                    StartCoroutine(DisableSpeedCoroutine(POWERUP_DEFAULT_DURATION));
                    break;
                case PowerupType.Slow:
                    _slowEnabled = true;
                    _slowCharges--;
                    // TODO: Note for Layla - Update with whatever force we are using for slowdown
                    _ballController.ChangeMaxForce(BALL_DEFAULT_MAX_FORCE / 2);
                    StartCoroutine(DisableSlowCoroutine(POWERUP_DEFAULT_DURATION));
                    break;
            }
        }

        private void Update()
        {
            CheckForPowerupActivation();
            HandleStickingBehavior();
        }

        private void CheckForPowerupActivation()
        {
            if (CanActivateStickyPowerup())
            {
                UsePowerup(PowerupType.Sticky);
            }

            if (CanActivateFastPowerup())
            {
                UsePowerup(PowerupType.Fast);
            }

            if (CanActivateSlowPowerup())
            {
                UsePowerup(PowerupType.Slow);
            }
        }

        private bool CanActivateStickyPowerup()
        {
            return !_stickyEnabled && StickyCharges > 0 && Input.GetKeyDown(KeyCode.Alpha1);
        }

        private bool CanActivateFastPowerup()
        {
            return !_fastEnabled && FastCharges > 0 && Input.GetKeyDown(KeyCode.Alpha2);
        }

        private bool CanActivateSlowPowerup()
        {
            return !_slowEnabled && SlowCharges > 0 && Input.GetKeyDown(KeyCode.Alpha3);
        }

        private void HandleStickingBehavior()
        {
            if (_isSticking && IsBallFalling())
            {
                ActivateGravity();
            }
        }

        private bool IsBallFalling()
        {
            return !Physics.Linecast(transform.position, transform.position + (_ballController._ballGravity * 1.1f));
        }

        private void ActivateGravity()
        {
            _ballController._ballGravity = Physics.gravity;
            GetComponent<Rigidbody>().useGravity = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_stickyEnabled && collision.gameObject.tag == "Wall")
            {
                Vector3 normal = collision.contacts[0].normal;
                _ballController._ballGravity = -normal;

                GetComponent<Rigidbody>().useGravity = false;
                _isSticking = true;
            }
        }

        private IEnumerator DisableStickyCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _ballController._ballGravity = Physics.gravity;
            GetComponent<Rigidbody>().useGravity = true;
            _stickyEnabled = false;
            _isSticking = false;
        }

        private IEnumerator DisableSpeedCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _ballController.ChangeMaxForce(BALL_DEFAULT_MAX_FORCE);
            _fastEnabled = false;
        }

        private IEnumerator DisableSlowCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _ballController.ChangeMaxForce(BALL_DEFAULT_MAX_FORCE);
            _slowEnabled = false;
        }
    }
}