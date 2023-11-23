namespace CT6RIGPR
{
    using UnityEngine;

    public class BallCockpit : MonoBehaviour
    {
        [SerializeField] private GameObject _playerGameObject;
        private Vector3 _lastBallEulers;
        private Vector3 _cockpitEulers;
        private Vector3 _ballEulers;

        void LateUpdate()
        {
            UpdatePosition();
            _ballEulers = _playerGameObject.transform.rotation.eulerAngles;
            _cockpitEulers = CalculateCockpitRotation();
            _cockpitEulers = ClampRotation(_cockpitEulers);
            ApplyRotation(_cockpitEulers);
            _lastBallEulers = _ballEulers;
            DrawDebugRay();
        }

        private void UpdatePosition()
        {
            transform.position = _playerGameObject.transform.position;

            //Optionally: cause cockpit spin here.
            _ballEulers = _playerGameObject.transform.rotation.eulerAngles;
        }

        private Vector3 CalculateCockpitRotation()
        {
            float moveHorizontal = Input.GetAxis("HotasX");
            float moveVertical = Input.GetAxis("HotasY");

            _cockpitEulers = new Vector3(moveVertical * 25, _playerGameObject.GetComponent<BallController>().YRotation, -moveHorizontal * 25);


            //Lerp back to 0,0,0 (if accelerating over cap, this should still leave the player above cap)
            _cockpitEulers = Vector3.Lerp(_cockpitEulers, Vector3.zero, Time.deltaTime / 6); //Takes 6 seconds of inactivity to return to a stable plane.

            return _cockpitEulers;
        }

        private Vector3 ClampRotation(Vector3 rotation)
        {
            rotation.x = Mathf.Clamp(rotation.x, -16.8f, 16.8f);
            rotation.z = Mathf.Clamp(rotation.z, -19.8f, 19.2f);
            return rotation;

            // Old code
            //if (_cockpitEulers.x < -16.8) { _cockpitEulers.x = -16.8f; }
            //else if (_cockpitEulers.x > 16.8) { _cockpitEulers.x = 16.8f; }
            //if (_cockpitEulers.z < -19.8) { _cockpitEulers.z = -19.8f; }
            //else if (_cockpitEulers.z > 19.2) { _cockpitEulers.z = 19.2f; }
        }
        
        private void ApplyRotation(Vector3 rotation)
        {
            transform.rotation = Quaternion.Euler(rotation);
        }

        private void DrawDebugRay()
        {
            Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);
        }
    }
}