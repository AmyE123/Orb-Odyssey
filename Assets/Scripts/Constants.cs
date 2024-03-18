using System.Numerics;
using UnityEngine;

namespace CT6RIGPR
{
    public static class Constants
    {
        #region Level Properties

        /// <summary>
        /// The different types of levels.
        /// </summary>
        public enum LevelType { Tutorial, Platformer, Puzzle }

        /// <summary>
        /// The score amount to add when collecting a collectable.
        /// </summary>
        public const int COLLECTABLE_SCORE_AMOUNT = 100;

        #endregion // Level Properties

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

        /// <summary>
        /// The default ball profile for the 4DOF hardware.
        /// </summary>
        public const string DOF_ORB_PROFILE = "rigpr2023monkeyball";
        /// <summary>
        /// The spline ball for the 4DOF hardware.
        /// </summary>
        public const string DOF_ORB_SPLINE_PROFILE = "rigpr2023monkeyballspline";

        /// <summary>
        /// The spline ball for the 4DOF hardware.
        /// </summary>
        public const float DOF_PROFILE_INTERVAL = 0.1f;

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
		/// The default max sped up force for the monkey ball.
		/// </summary>
		public const int BALL_DEFAULT_MAX_SLOWED_FORCE = 10;

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

        /// <summary>
        /// This is the string for the HOTAS WARTHOG Joysticks Y-axis reference.
        /// </summary>
        public const float ROTATION_MULTIPLIER = 1.5f;

        #endregion // Input Constants

        #region Game Constants

        /// <summary>
        /// The different powerups which we have.
        /// </summary>
        public enum PowerupType { Sticky, Fast, Slow, Freeze }

        /// <summary>
        /// The direction for tube reference.
        /// </summary>
        public enum TubeDirection { Forward, Backward }

        /// <summary>
        /// The direction for the jump pad.
        /// </summary>
        public enum JumpPadDirection { Up, Down, Left, Right, Forward, Backward }

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
        /// This is the tag for respawn checkpoints.
        /// </summary>
        public const string CHECKPOINT_TAG = "Checkpoint";

        /// <summary>
        /// This is the tag for the island boundary.
        /// </summary>
        public const string ISLAND_BOUNDARY_TAG = "IslandBoundary";

        /// <summary>
        /// This is the tag for the water.
        /// </summary>
        public const string WATER_TAG = "Water";

        /// <summary>
        /// This is the time that the player takes to respawn when they exit the island boundary.
        /// </summary>
        public const float RESPAWN_TIME = 5;

        /// <summary>
        /// This is the percentage of time that the player takes to fade to black when they exit the island boundary.
        /// </summary>
        public const float RESPAWN_FADE_OUT_TIME = 0.4f;

        /// <summary>
        /// This is the percentage of time that the player spends immobile after fading to black.
        /// </summary>
        public const float RESPAWN_FADE_WAIT_TIME = 0.2f;

        /// <summary>
        /// This is the percentage of time that the player takes to reappear after exiting the island boundary.
        /// </summary>
        public const float RESPAWN_FADE_IN_TIME = 0.4f;

        /// <summary>
        /// The default duration of a powerup.
        /// </summary>
        public const float POWERUP_DEFAULT_DURATION = 10f;

        /// <summary>
        /// The default amount of charges a powerup has.
        /// </summary>
        public const int DEFAULT_POWERUP_CHARGES = 10;

        /// <summary>
        /// The default cooldown for button input for powerups.
        /// </summary>
        public const float DEFAULT_POWERUP_INPUT_COOLDOWN = 0.5f;

        #endregion // Game Constants

        #region Editor Paths

        /// <summary>
        /// The boot scene.
        /// </summary>
        public const string BOOT_SCENE_PATH = "Assets/Scenes/Boot.unity";

        /// <summary>
        /// The string value of the load boot scene pref in player prefs.
        /// </summary>
        public const string LOAD_BOOT_SCENE_PREF = "LoadBootSceneOnPlay";

        /// <summary>
        /// A string of the menu item path for RIGPR tools.
        /// </summary>
        public const string RIGPR_MENU_ITEM_PATH = "Tools/CT6RIGPR/";

        /// <summary>
        /// A string of the menu item path for level tools
        /// </summary>
        public const string LEVEL_TOOLS_MENU_ITEM_PATH = RIGPR_MENU_ITEM_PATH + "Level Tools/";

        /// <summary>
        /// A string of the menu item path for powerups
        /// </summary>
        public const string POWERUP_TOOLS_MENU_ITEM_PATH = RIGPR_MENU_ITEM_PATH + "Powerups/";

        /// <summary>
        /// A string of the prefabs path.
        /// </summary>
        public const string PREFABS_PATH = "Assets/Prefabs/";

        /// <summary>
        /// A string of the level tools path.
        /// </summary>
        public const string LEVEL_TOOLS_PATH = PREFABS_PATH + "Gameplay/LevelTools/";

        /// <summary>
        /// A string of the powerups path.
        /// </summary>
        public const string POWERUPS_PATH = PREFABS_PATH + "Gameplay/Powerups/";

        /// <summary>
        /// A string to the tube instructions text files for loading it in in-editor.
        /// </summary>
        public const string TUBE_INSTRUCTIONS_PATH = "Assets/Data/Instructions/Tubes.txt";

        /// <summary>
        /// A string to the tube asset for loading it in in-editor.
        /// </summary>
        public const string TWO_WAY_TUBE_PREFAB_PATH = LEVEL_TOOLS_PATH + "TwoWayTube.prefab";

        /// <summary>
        /// A string to the tube asset for loading it in in-editor.
        /// </summary>
        public const string ONE_WAY_TUBE_PREFAB_PATH = LEVEL_TOOLS_PATH + "OneWayTube.prefab";

        /// <summary>
        /// A string to the jump pad asset for loading it in in-editor.
        /// </summary>
        public const string JUMPPAD_PREFAB_PATH = LEVEL_TOOLS_PATH + "JumpPad.prefab";

        /// <summary>
        /// A string to the impact wall asset for loading it in in-editor.
        /// </summary>
        public const string IMPACT_WALL_PREFAB_PATH = LEVEL_TOOLS_PATH + "ImpactWall.prefab";

        /// <summary>
        /// A string to the moving platform asset for loading it in in-editor.
        /// </summary>
        public const string MOVING_PLATFORM_PREFAB_PATH = LEVEL_TOOLS_PATH + "MovingPlatform.prefab";

        /// <summary>
        /// A string to the button moving platform asset for loading it in in-editor.
        /// </summary>
        public const string BUTTON_MOVING_PLATFORM_PREFAB_PATH = LEVEL_TOOLS_PATH + "ButtonMovingPlatform.prefab";

        /// <summary>
        /// A string to the collectable asset for loading it in in-editor.
        /// </summary>
        public const string COLLECTABLE_PREFAB_PATH = LEVEL_TOOLS_PATH + "Collectable.prefab";

        /// <summary>
        /// A string to the swinging obstacle asset for loading it in in-editor.
        /// </summary>
        public const string SWINGING_OBSTACLE_PREFAB_PATH = LEVEL_TOOLS_PATH + "SwingingObstacle.prefab";

        /// <summary>
        /// A string to the sweeper asset for loading it in in-editor.
        /// </summary>
        public const string SWEEPER_PREFAB_PATH = LEVEL_TOOLS_PATH + "Sweeper.prefab";

        /// <summary>
        /// A string to the timer door asset for loading it in in-editor.
        /// </summary>
        public const string TIMER_DOOR_PREFAB_PATH = LEVEL_TOOLS_PATH + "TimerDoor.prefab";

        /// <summary>
        /// A string to the timer ramp asset for loading it in in-editor.
        /// </summary>
        public const string TIMER_RAMP_PREFAB_PATH = LEVEL_TOOLS_PATH + "TimerRamp.prefab";

        /// <summary>
        /// A string to the timer platform asset for loading it in in-editor.
        /// </summary>
        public const string TIMER_PLATFORM_PREFAB_PATH = LEVEL_TOOLS_PATH + "TimerPlatform.prefab";

        /// <summary>
        /// A string to the sticky powerup asset for loading it in in-editor.
        /// </summary>
        public const string STICKY_POWERUP_PREFAB_PATH = POWERUPS_PATH + "Sticky.prefab";

        /// <summary>
        /// A string to the slow powerup asset for loading it in in-editor.
        /// </summary>
        public const string SLOW_POWERUP_PREFAB_PATH = POWERUPS_PATH + "Slow.prefab";

        /// <summary>
        /// A string to the fast powerup asset for loading it in in-editor.
        /// </summary>
        public const string FAST_POWERUP_PREFAB_PATH = POWERUPS_PATH + "Fast.prefab";

      /// <summary>
      /// A string to the freeze powerup asset for loading it in in-editor.
      /// </summary>
      public const string FREEZE_POWERUP_PREFAB_PATH = POWERUPS_PATH + "Freeze.prefab";

        #endregion // Editor Paths

	}
}