namespace CT6RIGPR
{
    public static class Constants
    {
        #region 4DoF Constants

        public const string DOF_DEFAULT_PROFILE = "rigpr2023default";
        public const string DOF_SOFT_PROFILE = "rigpr2023soft";
        public const string DOF_HARD_PROFILE = "rigpr2023hard";

        #endregion // 4DoF Constants

        #region Ball Constants

        public const int BALL_DEFAULT_MAX_FORCE = 500;

        public const float ROLL_CLAMP = 16.8f;
        public const float PITCH_CLAMP_POS = 19.2f;
        public const float PITCH_CLAMP_NEG = -19.8f;

        #endregion // Ball Constants
    }
}