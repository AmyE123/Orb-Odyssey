namespace CT6RIGPR
{
    using System.Collections;
    using System.Runtime.InteropServices;
    using UnityEngine;

    /// <summary>
    /// A test script for switching profiles on the 4DoF through Unity
    /// </summary>
    public class ProfileManager : MonoBehaviour
    {
        [DllImport("libadminclient", EntryPoint = "SendLoadProfile")]
        public static extern void SendLoadProfile(string profile);

        void Start()
        {
            StartCoroutine(InitialiseProfile(Constants.DOF_PROFILE_INTERVAL * 5, Constants.DOF_ORB_PROFILE));
        }

        /// <summary>
        /// Load the monkey ball profile.
        /// </summary>
        public IEnumerator InitialiseProfile(float time, string profile)
        {
            yield return new WaitForSeconds(time);
            SendLoadProfile(Constants.DOF_ORB_PROFILE);
        }

        public void setProfile(string profile)
        {
            SendLoadProfile(profile);
        }

        /// <summary>
        /// Check if the player is triggering a change of profile and if so, change the profile.
        /// </summary>
        private void HandleSettingProfile()
        {
            // Test placeholders for updating the profile.
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SendLoadProfile(Constants.DOF_SOFT_PROFILE);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SendLoadProfile(Constants.DOF_HARD_PROFILE);
            }
        }
    }
}