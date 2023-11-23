namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// A joystick test script which logs joystick input
    /// </summary>
    public class JoystickTest : MonoBehaviour
    {
        void Update()
        {
            Debug.Log("X: " + Input.GetAxis("HotasX") + " Y: " + Input.GetAxis("HotasY"));
        }
    }
}
