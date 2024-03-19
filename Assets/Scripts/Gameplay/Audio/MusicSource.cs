namespace CT6RIGPR
{
    using UnityEngine;

    public class MusicSource : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}