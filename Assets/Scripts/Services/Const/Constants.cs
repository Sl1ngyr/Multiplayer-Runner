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
        public const int RUNNER_MAX_PLAYER_IN_SESSION = 2;
        
        public const string PLAYER_PREFS_CAR_ID = "PlayerPrefsCarID";
        public const string PLAYER_PREFS_AVATAR_ID = "PlayerPrefsAvatarID";
        public const string PLAYER_PREFS_NICKNAME = "PlayerPrefsNickname";
        public const string PLAYER_PREFS_SCORE = "PlayerPrefsScore";

        public const int GAME_TIME_FOR_PREPARATION = 3;
        public const int GAME_TIME_FOR_PREVIEW_PLAYERS = 3;
        public const int GAME_TIME_FOR_LEAVE_AFTER_FINISH_PREVIEW = 5;
        
        public const int GAME_RACE_CALCULATION_FIRST_POSITION = 1;
        public const int GAME_RACE_CALCULATION_SECOND_POSITION = 2;

        public const string GAME_RACE_FIRST_POSITION_TEXT = "1st";
        public const string GAME_RACE_SECOND_POSITION_TEXT = "2st";
    }
}