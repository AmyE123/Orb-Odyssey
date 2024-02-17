namespace CT6RIGPR
{
    using UnityEngine;

    public class GlobalReferences : MonoBehaviour
    {
        [SerializeField] private BallController _ballController;
        [SerializeField] private GameObject[] _checkpoints;

        /// <summary>
        /// The ball controller. For all player inputs.
        /// </summary>
        public BallController BallController => _ballController;
        public GameObject[] Checkpoints => _checkpoints;

        private void Start()
        {
            CheckReferences();
            PopulateArrays();
        }

        private void CheckReferences()
        {
            if (_ballController == null)
            {
                LogNullRef(typeof(BallController).Name, LogType.Error);
            }
        }

        private void PopulateArrays()
        {
            _checkpoints = GameObject.FindGameObjectsWithTag(Constants.CHECKPOINT_TAG);
        }

        private void LogNullRef(string refName, LogType logType)
        {
            string message = $"[CT6RIGPR] {refName} is null. Leaving this null can cause issues in the game. \n Please set this in the GlobalReferences.";

            switch (logType)
            {
                case LogType.Error:
                    Debug.LogError(message);
                    break;
                case LogType.Assert:
                    Debug.LogAssertion(message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogType.Log:
                    Debug.Log(message);
                    break;
            }
        }
    }
}