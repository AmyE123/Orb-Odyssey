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
        public const int BALL_DEFAULT_MAX_FORCE = 20;

		/// <summary>
		/// The default max sped up force for the monkey ball.
		/// </summary>
		public const int BALL_DEFAULT_MAX_BUFFED_FORCE = 40;

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
        /// The different powerups which we have.
        /// </summary>
        public enum PowerupType { Sticky, Fast, Slow, Freeze }

        /// <summary>
        /// The direction for tube reference.
        /// </summary>
        public enum Direction { Forward, Backward }

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

        /// <summary>
        /// The default duration of a powerup.
        /// </summary>
        public const float POWERUP_DEFAULT_DURATION = 10f;

        /// <summary>
        /// The default amount of charges a powerup has.
        /// </summary>
        public const int DEFAULT_POWERUP_CHARGES = 10;

        #endregion // Game Constants

        #region Editor Paths

        /// <summary>
        /// A string of the menu item path for level tools
        /// </summary>
        public const string LEVEL_TOOLS_MENU_ITEM_PATH = "Tools/CT6RIGPR/Level Tools/";

        /// <summary>
        /// A string of the menu item path for powerups
        /// </summary>
        public const string POWERUP_TOOLS_MENU_ITEM_PATH = "Tools/CT6RIGPR/Powerups/";

        /// <summary>
        /// A string to the tube asset for loading it in in-editor.
        /// </summary>
        public const string TWO_WAY_TUBE_PREFAB_PATH = "Assets/Prefabs/Gameplay/LevelTools/TwoWayTube.prefab";

        /// <summary>
        /// A string to the tube asset for loading it in in-editor.
        /// </summary>
        public const string ONE_WAY_TUBE_PREFAB_PATH = "Assets/Prefabs/Gameplay/LevelTools/OneWayTube.prefab";

        /// <summary>
        /// A string to the tube instructions text files for loading it in in-editor.
        /// </summary>
        public const string TUBE_INSTRUCTIONS_PATH = "Assets/Data/Instructions/Tubes.txt";

        /// <summary>
        /// A string to the jump pad asset for loading it in in-editor.
        /// </summary>
        public const string JUMPPAD_PREFAB_PATH = "Assets/Prefabs/Gameplay/LevelTools/JumpPad.prefab";

        /// <summary>
        /// A string to the impact wall asset for loading it in in-editor.
        /// </summary>
        public const string IMPACT_WALL_PREFAB_PATH = "Assets/Prefabs/Gameplay/LevelTools/ImpactWall.prefab";

        /// <summary>
        /// A string to the moving platform asset for loading it in in-editor.
        /// </summary>
        public const string MOVING_PLATFORM_PREFAB_PATH = "Assets/Prefabs/Gameplay/LevelTools/MovingPlatform.prefab";

        /// <summary>
        /// A string to the button moving platform asset for loading it in in-editor.
        /// </summary>
        public const string BUTTON_MOVING_PLATFORM_PREFAB_PATH = "Assets/Prefabs/Gameplay/LevelTools/ButtonMovingPlatform.prefab";

        /// <summary>
        /// A string to the pickup asset for loading it in in-editor.
        /// </summary>
        public const string PICKUP_PREFAB_PATH = "Assets/Prefabs/Gameplay/LevelTools/Pickup.prefab";

        /// <summary>
        /// A string to the sticky powerup asset for loading it in in-editor.
        /// </summary>
        public const string STICKY_POWERUP_PREFAB_PATH = "Assets/Prefabs/Gameplay/Powerups/Sticky.prefab";

        /// <summary>
        /// A string to the slow powerup asset for loading it in in-editor.
        /// </summary>
        public const string SLOW_POWERUP_PREFAB_PATH = "Assets/Prefabs/Gameplay/Powerups/Slow.prefab";

        /// <summary>
        /// A string to the fast powerup asset for loading it in in-editor.
        /// </summary>
        public const string FAST_POWERUP_PREFAB_PATH = "Assets/Prefabs/Gameplay/Powerups/Fast.prefab";

        #endregion // Editor Paths
    }
}