namespace CT6RIGPR
{
    using UnityEngine;

    public class ScenePreviewCamera : MonoBehaviour
    {
        void Start()
        {
            // This camera is just for the preview in game view. This can be set inactive on start.
            gameObject.SetActive(false);
        }
    }
}