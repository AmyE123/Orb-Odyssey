namespace CT6RIGPR
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Level Data", menuName = "ScriptableObjects/Level Data")]
    public class LevelData : ScriptableObject
    {
        public string SceneName;
        public string LevelName;
        public Constants.LevelType LevelType;       
    }

}
