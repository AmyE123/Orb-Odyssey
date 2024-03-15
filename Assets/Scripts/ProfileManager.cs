namespace CT6RIGPR
{
    using System.Collections;
    using System.Runtime.InteropServices;
    using UnityEngine;

    /// <summary>
    /// A script for switching profiles on the 4DoF through Unity
    /// </summary>
    public class ProfileManager : MonoBehaviour
    {
        [DllImport("libadminclient", EntryPoint = "SendLoadProfile")]
        public static extern void SendLoadProfile(string profile);

        private BallController _ballController;

        void Start()
        {
            _ballController = FindAnyObjectByType<BallController>();
            StartCoroutine(InitialiseProfile(Constants.DOF_PROFILE_INTERVAL * 5, Constants.DOF_ORB_PROFILE));
        }

        /// <summary>
        /// Initialize the default ball profile.
        /// </summary>
        public IEnumerator InitialiseProfile(float time, string profile)
        {
            yield return new WaitForSeconds(time);
            SetProfile(profile);          
        }

        /// <summary>
        /// Sets a profile to load on the 4DoF.
        /// </summary>
        /// <param name="profile">The string of the profile.</param>
        public void SetProfile(string profile)
        {
            if (_ballController != null)
            {
                if (_ballController.DebugInput == false)
                {
                    SendLoadProfile(profile);
                }
            }
        }
    }
}