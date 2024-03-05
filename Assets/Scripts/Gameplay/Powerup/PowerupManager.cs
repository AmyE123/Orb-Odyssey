namespace CT6RIGPR
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR;
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
        private bool _primaryButtonEnabled = true;
        private bool _secondaryButtonEnabled = true;

        private bool _isSticking;
        private float _originalForce;

        [SerializeField] private PowerupType _activePowerup;

        [SerializeField] private int _stickyCharges;
        [SerializeField] private int _fastCharges;
        [SerializeField] private int _slowCharges;
        [SerializeField] private int _freezeCharges;

		[SerializeField] private float _stickyDuration = Constants.POWERUP_DEFAULT_DURATION;
		[SerializeField] private float _fastDuration = Constants.POWERUP_DEFAULT_DURATION;
		[SerializeField] private float _slowDuration = Constants.POWERUP_DEFAULT_DURATION;
		[SerializeField] private float _freezeDuration = Constants.POWERUP_DEFAULT_DURATION;

		[SerializeField] private float _buffedForce = BALL_DEFAULT_MAX_BUFFED_FORCE;
		[SerializeField] private float _slowedForce = BALL_DEFAULT_MAX_SLOWED_FORCE;
		[SerializeField] private BallController _ballController;

        [SerializeField] private float _buttonCoolDown;

        GameObject[] _bodiesOfWater;

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
                case PowerupType.Freeze:
                    _freezeCharges++;
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
			_bodiesOfWater = GameObject.FindGameObjectsWithTag("Water");
            _originalForce = _ballController.MaxForce;
			InitializeDefaultCharges();
            _primaryButtonEnabled = true;
            _secondaryButtonEnabled = true;
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
                    StartCoroutine(DisableStickyCoroutine(_stickyDuration));
                    break;
                case PowerupType.Fast:
                    _fastEnabled = true;
                    _fastCharges--;
					_ballController.ChangeMaxForce(_buffedForce);
					StartCoroutine(DisableSpeedCoroutine(_fastDuration));
                    break;
                case PowerupType.Slow:
                    _slowEnabled = true;
                    _slowCharges--;
                    // TODO: Note for Layla - Update with whatever force we are using for slowdown
                    _ballController.ChangeMaxForce(_slowedForce);
                    StartCoroutine(DisableSlowCoroutine(_slowDuration));
                    break;
                case PowerupType.Freeze:
                    _freezeEnabled = true;
                    _freezeCharges--;
                    foreach (GameObject body in _bodiesOfWater)
                    {
                        if (body.GetComponent<Collider>() != null) {
                            Collider collider = body.GetComponent<Collider>();
                            collider.enabled = true;
                        }
                    }
                    StartCoroutine(DisableFreezeCoroutine(_freezeDuration));
                    break;
            }
        }

        private void Update()
        {
            CheckForPowerUpCycle();
            CheckForPowerupActivation();
            HandleStickingBehavior();
        }

        private void CyclePowerUp()
        {
            if (_activePowerup == PowerupType.Freeze)
            {
                _activePowerup = 0;
            }
            else
            {
                _activePowerup++;
            }
        }

        private void CheckForPowerUpCycle()
        {
            bool buttonA = false;
            var leftHandedControllers = new List<InputDevice>();
            var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

            foreach (var device in leftHandedControllers)
            {
                device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonA);
            }

            if (buttonA && _primaryButtonEnabled)
            {
                CyclePowerUp();
                _primaryButtonEnabled = false;
                Debug.Log("Test 1");
                Debug.Log(_buttonCoolDown);
                StartCoroutine(ButtonCooldown(_buttonCoolDown));
                _primaryButtonEnabled = true;
            }
        }

        public IEnumerator ButtonCooldown(float time)
        {
            Debug.Log(time);
            Debug.Log("Test 2");
            yield return new WaitForSeconds(time);
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

            if (CanActivateFreezePowerup())
            {
                UsePowerup(PowerupType.Freeze);
            }
        }

        private bool CanActivateStickyPowerup()
        {
            return !_stickyEnabled && StickyCharges > 0 && Input.GetKeyDown(KeyCode.Alpha1);
        }

        private bool CanActivateFastPowerup()
        {
            return !_fastEnabled && !_slowEnabled && FastCharges > 0 && Input.GetKeyDown(KeyCode.Alpha2);
        }

        private bool CanActivateSlowPowerup()
        {
            return !_slowEnabled && !_fastEnabled && SlowCharges > 0 && Input.GetKeyDown(KeyCode.Alpha3);
        }

        private bool CanActivateFreezePowerup()
        {
			return !_freezeEnabled && FreezeCharges > 0 && Input.GetKeyDown(KeyCode.Alpha4);
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
			_ballController.ChangeMaxForce(_originalForce);
			_fastEnabled = false;
        }

        private IEnumerator DisableSlowCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _ballController.ChangeMaxForce(_originalForce);
            _slowEnabled = false;
        }

        private IEnumerator DisableFreezeCoroutine(float time)
        { 
            yield return new WaitForSeconds(time);
			foreach (GameObject body in _bodiesOfWater)
			{
				if (body.GetComponent<Collider>() != null)
				{
					Collider collider = body.GetComponent<Collider>();
					collider.enabled = false;
				}
			}
			_freezeEnabled = false;
		}

	}
}