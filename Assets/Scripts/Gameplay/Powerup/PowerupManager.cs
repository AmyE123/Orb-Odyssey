namespace CT6RIGPR
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR;
    using static CT6RIGPR.Constants;
    using UnityEngine.XR.Interaction.Toolkit;
    using UnityEngine.UI;


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

        private float _activationTime;

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

        [SerializeField] private float _buttonCoolDown = DEFAULT_POWERUP_INPUT_COOLDOWN;

        [SerializeField] private Sprite _imageStickyPowerUp;
        [SerializeField] private Sprite _imageFastPowerUp;
        [SerializeField] private Sprite _imageSlowPowerUp;
        [SerializeField] private Sprite _imageFreezePowerUp;

        [SerializeField] private Image _powerUpImagePanel;
        [SerializeField] private Slider _slider;

        [SerializeField] private GameManager _gameManager;



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

        /// <summary>
        /// Whether the player is sticking to a wall.
        /// </summary>
        public bool IsSticking => _isSticking;


        private bool AnyPowerUp()
        {
            return IsFastEnabled || IsFreezeEnabled || IsSlowEnabled || IsStickyEnabled;
        }

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

        private void UpdateSprite()
        {
            switch (_activePowerup)
            {
                case PowerupType.Sticky:
                    _powerUpImagePanel.sprite = _imageStickyPowerUp;
                    break;
                case PowerupType.Fast:
                    _powerUpImagePanel.sprite = _imageFastPowerUp;
                    break;
                case PowerupType.Slow:
                    _powerUpImagePanel.sprite = _imageSlowPowerUp;
                    break;
                case PowerupType.Freeze:
                    _powerUpImagePanel.sprite = _imageFreezePowerUp;
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
            UpdateSprite();
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
            _activationTime = Time.time;
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

        private void UpdateSlider()
        {
            float timeSinceActivation = Time.time - _activationTime;
            float timeRemaining = _fastDuration - timeSinceActivation;
            _slider.value = timeRemaining / _fastDuration;
            switch (_activePowerup)
            {
                case PowerupType.Sticky:
                    _slider.value = (_stickyDuration - (Time.time - _activationTime)) / _stickyDuration;
                    break;
                case PowerupType.Fast:
                    _slider.value = (_fastDuration - (Time.time - _activationTime)) / _fastDuration;
                    break;
                case PowerupType.Slow:
                    _slider.value = (_slowDuration - (Time.time - _activationTime)) / _slowDuration;
                    break;
                case PowerupType.Freeze:
                    _slider.value = (_freezeDuration - (Time.time - _activationTime)) / _freezeDuration;
                    break;
            }

        }

        private void ResetSlider()
        {
            _slider.value = 1.0f;
        }

        private void Update()
        {
            CheckForPowerUpCycle();
            if (AnyPowerUp())
            {
                UpdateSlider();
            }
            CheckForPowerupActivation();
            HandleStickingBehavior();
            // TODO: Layla - Can you implement this into your cycling powerup implementation?
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                InventoryVisualsManager.instance.UsePowerup(0);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                InventoryVisualsManager.instance.UsePowerup(1);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                InventoryVisualsManager.instance.UsePowerup(2);
            }

        }

        private void CyclePowerUp()
        {
            if (AnyPowerUp())
            {
                return; //Don't cycle when using a powerup.
            }
            if (_activePowerup == PowerupType.Freeze)
            {
                _activePowerup = 0;
            }
            else
            {
                _activePowerup++;
            }
            UpdateSprite();
        }


        private void CheckForPowerUpCycle()
        {
            bool buttonA = false;
            bool buttonB = false;
            var leftHandedControllers = new List<InputDevice>();
            var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

            foreach (var device in leftHandedControllers)
            {
                device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonA);
                device.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonB);
            }

            if (buttonA && _primaryButtonEnabled)
            {
                CyclePowerUp();
                _primaryButtonEnabled = false;
                StartCoroutine(PrimaryButtonCooldown(_buttonCoolDown));
            }

            if (buttonB && _secondaryButtonEnabled)
            {
                if (CanActivatePowerUp())
                {
                    UsePowerup(_activePowerup);
                }
                _secondaryButtonEnabled = false;
                StartCoroutine(SecondaryButtonCooldown(_buttonCoolDown));
            }
        }

	private IEnumerator PrimaryButtonCooldown(float time)
        {
            yield return new WaitForSeconds(time);
            _primaryButtonEnabled = true;
        }

        private IEnumerator SecondaryButtonCooldown(float time)
        {
            yield return new WaitForSeconds(time);
            _secondaryButtonEnabled = true;
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

        private bool CanActivatePowerUp()
        {
            if (AnyPowerUp() || !_gameManager.GlobalGameReferences.LevelManager.HasReadWarning || _gameManager.GlobalGameReferences.IsFollowingSpline)
            {
                return false;
            }
            switch (_activePowerup)
            {
                case PowerupType.Sticky:
                    if (StickyCharges > 0)
                    {
                        return true;
                    }
                    break;
                case PowerupType.Fast:
                    if (FastCharges > 0)
                    {
                        return true;
                    }
                    break;
                case PowerupType.Slow:
                    if (SlowCharges > 0)
                    {
                        return true;
                    }
                    break;
                case PowerupType.Freeze:
                    if (FreezeCharges > 0)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private bool CanActivateStickyPowerup()
        {
            return !AnyPowerUp() && StickyCharges > 0 && Input.GetKeyDown(KeyCode.Alpha1);
        }

        private bool CanActivateFastPowerup()
        {
            return !AnyPowerUp() && FastCharges > 0 && Input.GetKeyDown(KeyCode.Alpha2);
        }

        private bool CanActivateSlowPowerup()
        {
            return !AnyPowerUp() && SlowCharges > 0 && Input.GetKeyDown(KeyCode.Alpha3);
        }

        private bool CanActivateFreezePowerup()
        {
			return !AnyPowerUp() && FreezeCharges > 0 && Input.GetKeyDown(KeyCode.Alpha4);
		}

		private void HandleStickingBehavior()
        {
            if (_isSticking && IsBallFalling())
            {
                ActivateGravity();
                _isSticking = false;
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
            ResetSlider();
        }

        private IEnumerator DisableSpeedCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
			_ballController.ChangeMaxForce(_originalForce);
			_fastEnabled = false;
            ResetSlider();
        }

        private IEnumerator DisableSlowCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _ballController.ChangeMaxForce(_originalForce);
            _slowEnabled = false;
            ResetSlider();
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
            ResetSlider();
        }

    }
}
