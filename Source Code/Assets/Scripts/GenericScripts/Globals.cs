public static class Globals
{
    #region tags
    public static string PLAYER_TAG = "Player";
    public static string GROUND_TAG = "Ground";
    public const string COLLECTIBLE_TAG = "collectible";
    public static string SECRET_AREA_TAG = "SecretArea";
    public const string COLOR_ITEM_TAG = "ColorItem";

    public const string END_TAG = "End";
    #endregion

    #region input
    public static string HORIZONTAL_AXIS = "Horizontal";
    #endregion

    #region magic numbers
    public static int SLOW_ON_JUMP = 3;
    public static int DELTA_SMOOTHENING = 5;
    public static float RAYCAST_CHECK_RANGE = 0.3f;
    public static float WRITING_SPEED = 0.025f;
    #endregion

    #region layers

    public static string OBJECT_LAYER = "Object";

    #endregion

    #region enemy magic numbers
    public static float ENEMY_DOWN_SPEED = 1f;
    public static float ENEMY_SPEED = 1.5f;
    public static float ENEMY_GRAVITY = 1f;
    public static float ENEMY_RGRAVITY = 9.80665f;
    public static float ENEMY_TIMER = 4f;
    public static float ENEMY_MOVE_TIMER = 0f;
    #endregion
}
