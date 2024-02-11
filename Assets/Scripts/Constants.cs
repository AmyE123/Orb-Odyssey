namespace CT6RIGPR
{
    public static class Constants
    {
        #region 4DoF Constants

        /// <summary>
        /// The default profile for the 4DOF hardware.
        /// </summary>
        public const string DOF_DEFAULT_PROFILE = "rigpr2023default";

        /// <summary>
        /// The soft profile for the 4DOF hardware.
        /// </summary>
        public const string DOF_SOFT_PROFILE = "rigpr2023soft";

        /// <summary>
        /// The hard profile for the 4DOF hardware.
        /// </summary>
        public const string DOF_HARD_PROFILE = "rigpr2023hard";

        #endregion // 4DoF Constants

        #region Ball Constants

        /// <summary>
        /// The default max force for the monkey ball.
        /// </summary>
        public const int BALL_DEFAULT_MAX_FORCE = 500;

        /// <summary>
        /// The roll clamp for the cockpit.
        /// </summary>
        public const float ROLL_CLAMP = 16.8f;

        /// <summary>
        /// The pitch positive clamp for the cockpit.
        /// </summary>
        public const float PITCH_CLAMP_POS = 19.2f;

        /// <summary>
        /// The pitch negative clamp for the cockpit.
        /// </summary>
        public const float PITCH_CLAMP_NEG = -19.8f;

        #endregion // Ball Constants

        #region Input Constants

        /// <summary>
        /// This is the string for the HOTAS WARTHOG Joysticks X-axis reference.
        /// </summary>
        public const string HOTAS_X = "HotasX";

        /// <summary>
        /// This is the string for the HOTAS WARTHOG Joysticks Y-axis reference.
        /// </summary>
        public const string HOTAS_Y = "HotasY";

        #endregion // Input Constants

        #region Game Constants

        /// <summary>
        /// This is the Y offset for the camera used for the camera controller.
        /// </summary>
        public const float CAMERA_Y_OFFSET = 0.2f;

        /// <summary>
        /// This is the tag for the player object.
        /// This is set on the Monkey Ball.
        /// </summary>
        public const string PLAYER_TAG = "Player";

        /// <summary>
        /// This is the tag for the cockpit object.
        /// </summary>
        public const string COCKPIT_TAG = "Cockpit";

        #endregion // Game Constants

        #region Editor Paths

        /// <summary>
        /// A string of the menu item path for level tools
        /// </summary>
        public const string LEVEL_TOOLS_MENU_ITEM_PATH = "Tools/CT6RIGPR/Level Tools/";

        /// <summary>
        /// A string to the tube asset for loading it in in-editor.
        /// </summary>
        public const string TUBE_PREFAB_PATH = "Assets/Prefabs/Gameplay/LevelTools/Tube.prefab";

        /// <summary>
        /// A string to the tube instructions text files for loading it in in-editor.
        /// </summary>
        public const string TUBE_INSTRUCTIONS_PATH = "Assets/Data/Instructions/Tubes.txt";

        /// <summary>
        /// A string to the jump pad asset for loading it in in-editor.
        /// </summary>
        public const string JUMPPAD_PREFAB_PATH = "Assets/Prefabs/Gameplay/LevelTools/JumpPad.prefab";

        #endregion // Editor Paths
    }
}