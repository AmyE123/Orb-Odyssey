namespace CT6RIGPR
{
    using System.Runtime.InteropServices;
    using UnityEngine;

    /// <summary>
    /// A test script for switching profiles on the 4DoF through Unity
    /// </summary>
    public class ProfileTest : MonoBehaviour
    {
        [DllImport("libadminclient", EntryPoint = "SendLoadProfile")]
        public static extern void SendLoadProfile(string profile);

        [SerializeField] private bool _initialisedProfile;

        void Start()
        {
            _initialisedProfile = false;
        }

        void Update()
        {
            InitializeProfile();
            HandleSettingProfile();
        }

        /// <summary>
        /// Check if the profile has been initialized, and if not, load the right profile.
        /// </summary>
        private void InitializeProfile()
        {
            if (!_initialisedProfile)
            {
                _initialisedProfile = true;

                // TODO: Const string for profiles
                SendLoadProfile("rigpr2023default");
            }
        }

        /// <summary>
        /// Check if the player is triggering a change of profile and if so, change the profile.
        /// </summary>
        private void HandleSettingProfile()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // TODO: Const string for profiles
                SendLoadProfile("rigpr2023soft"); // Test placeholder.
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // TODO: Const string for profiles
                SendLoadProfile("rigpr2023hard"); // Test placeholder.
            }
        }
    }
}