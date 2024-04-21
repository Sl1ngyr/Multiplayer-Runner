namespace Services.Const
{
    public class Constants
    {
        public const string DATABASE_USERS = "users";
        public const string DATABASE_NICKNAME = "username";
        public const string DATABASE_MAX_SCORE = "maxScore";
        public const string DATABASE_CAR_ID = "carID";
        public const string DATABASE_AVATAR_ID = "avatarID";

        public const string PLAYER_SETTINGS_NICKNAME_SUCCESSFULLY_CHANGED = "Congratulations! You have changed your nickname.";
        public const string PLAYER_SETTINGS_NICKNAME_ERROR_MESSAGE_LESS_CHARACTERS = "The nickname must contain at least three characters!";
        public const string PLAYER_SETTINGS_NICKNAME_ERROR_MESSAGE_SAME_NICKNAME= "You have entered your nickname!";
        
        public const string PLAYER_SETTINGS_AVATAR_SUCCESSFULLY_CHANGED = "Congratulations! You have successfully changed your avatar.";
        public const string PLAYER_SETTINGS_AVATAR_ERROR_MESSAGE_SAME_AVATAR = "You have chosen your avatar!";

        public const int RUNNER_OFFSET_TO_SPAWN_PLAYER = 4;

        public const string PLAYER_PREFS_CAR_ID = "PlayerPrefsCarID";
    }
}