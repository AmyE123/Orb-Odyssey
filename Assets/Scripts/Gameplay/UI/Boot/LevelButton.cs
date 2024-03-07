namespace CT6RIGPR
{
    using TMPro;
    using UnityEngine;

    public class LevelButton : MonoBehaviour
    {
        private BootManager _bootManager;
        private string _sceneName;

        [SerializeField] private TMP_Text _levelType;
        [SerializeField] private TMP_Text _levelName;        

        public void Start()
        {
            if (_bootManager == null)
            {
                _bootManager = FindObjectOfType<BootManager>();
            }      
        }

        public void SetLevelButtonValues(LevelData levelData)
        {
            _sceneName = levelData.SceneName;
            _levelType.text = levelData.LevelType.ToString();
            _levelName.text = levelData.LevelName;
        }

        public void LoadLevel()
        {
            if (_sceneName == null)
            {
                Debug.LogError("[CT6RIGPR]: Scene Name not set on Level Button");
                return;
            }

            StartCoroutine(_bootManager.LoadLevel(_sceneName));
        }
    }
}