namespace CT6RIGPR
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR;

    public class VRControllerTest : MonoBehaviour
    {
        private enum ControllerSide { Left, Right, Both }

        [SerializeField] private List<InputDevice> _controllers;

        void Update()
        {
            _controllers = GetLeftHandedControllers();
            TestControllerInputs(_controllers);
        }

        /// <summary>
        /// Gets all left-hand controllers with the characteristics: HeldInHand and Controller
        /// </summary>
        /// <returns>A list of all left-hand input devices</returns>
        private List<InputDevice> GetLeftHandedControllers()
        {
            // TODO: If this works, make the controller side visible in inspector for quicker testing.
            return GetControllers(ControllerSide.Left);

            // This is the old code, this can be uncommented if tests with GetControllers() function doesn't work.
            //var leftHandedControllers = new List<InputDevice>();
            //var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
            //InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);
            //return leftHandedControllers;
        }

        /// <summary>
        /// Gets all controllers with the characteristics: HeldInHand and Controller
        /// </summary>
        /// <param name="controllerSide">Which controller side you want (Left, Right or Both)</param>
        /// <returns>A list of all specified input devices</returns>
        private List<InputDevice> GetControllers(ControllerSide controllerSide)
        {
            var controllers = new List<InputDevice>();
            InputDeviceCharacteristics desiredCharacteristics;

            switch (controllerSide)
            {
                case ControllerSide.Left:
                    desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
                    InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);
                    break;

                case ControllerSide.Right:
                    desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
                    InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);
                    break;
                case ControllerSide.Both:
                    desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
                    InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);
                    break;
            }

            return controllers;
        }

        /// <summary>
        /// Takes the controller inputs and debugs test values
        /// </summary>
        /// <param name="controllers">A list of input devices to test</param>
        private void TestControllerInputs(List<InputDevice> controllers)
        {
            foreach (var device in controllers)
            {
                Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
                Vector2 thumbstick;
                if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out thumbstick))
                {
                    Debug.Log(thumbstick.x + " " + thumbstick.y);
                }
            }
        }
    }
}