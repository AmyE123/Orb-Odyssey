namespace CT6RIGPR
{
    using UnityEngine;

    public class LevelPopulator : MonoBehaviour
    {
        [SerializeField] private GameObject _levelButtonPrefab;
        [SerializeField] private Transform _levelButtonArea;

        private void Start()
        {
            PopulateLevelMenu();
        }

        private void PopulateLevelMenu()
        {
            LevelData[] allLevels = GlobalManager.Instance.GetAllLevels();

            foreach (LevelData level in allLevels)
            {
                GameObject btnPrefab = Instantiate(_levelButtonPrefab, _levelButtonArea);
                LevelButton lvlBtn = btnPrefab.GetComponentInChildren<LevelButton>();

                if (lvlBtn != null)
                {
                    lvlBtn.SetLevelButtonValues(level);
                }
            }
        }
    }
}
