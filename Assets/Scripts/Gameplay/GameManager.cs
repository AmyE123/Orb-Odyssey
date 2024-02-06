namespace CT6RIGPR
{
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GlobalReferences _globalReferences;

        /// <summary>
        /// A getter for the global references.
        /// </summary>
        public GlobalReferences GlobalReferences => _globalReferences;

        private void Start ()
        {
            if (_globalReferences == null)
            {
                Debug.LogAssertion("[CT6RIGPR] Global References is NULL! Please set this in the inspector.");
            }
        }
    }
}