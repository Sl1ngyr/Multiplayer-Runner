using System.Collections;
using Firebase.Auth;
using Firebase.Database;
using Services.Const;
using UI.MainMenu;
using UnityEngine;

namespace Services.Firebase
{
    public class UpdateDataManager : MonoBehaviour
    {
        private FirebaseUser _user;

        public string UserID => _user.UserId;
        public DatabaseReference DatabaseReference { get; private set; }
        public string UserNickname { get; private set; }
        public string UserScore { get; private set; }
        public int UserAvatarID { get; private set; }
        public int UserCarID { get; private set; }

        public void InitDatabase(MainMenuPlayerDisplayData mainMenuPlayerDisplayData)
        {
            InitializeFirebase();
            StartCoroutine(LoadUserData(mainMenuPlayerDisplayData));
        }
        
        public void InitUpdateNickname(string nickname)
        {
            StartCoroutine(UpdateNickname(nickname));
        }
        
        public void InitUpdateAvatarID(int avatarID)
        {
            StartCoroutine(UpdateAvatarID(avatarID));
        }
        
        public void InitUpdateCarID(int carID)
        {
            StartCoroutine(UpdateCarID(carID));
        }
        
        public void InitUpdateScore(float score)
        {
            StartCoroutine(UpdateScore(score));
        }
        
        private void InitializeFirebase()
        {
            _user = FirebaseAuth.DefaultInstance.CurrentUser;

            DatabaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        private IEnumerator LoadUserData(MainMenuPlayerDisplayData mainMenuPlayerDisplayData)
        {
            var dbTask = DatabaseReference.Child(Constants.DATABASE_USERS)
                .Child(_user.UserId)
                .GetValueAsync();
            
            yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
            }
            else if (dbTask.Result.Value == null)
            {
                SetInitialDataToUserData();
                
                InitInitialDataToDatabase();
            }
            else
            {
                DataSnapshot snapshot = dbTask.Result;
                
                UserNickname = snapshot.Child(Constants.DATABASE_NICKNAME).Value.ToString();
                UserAvatarID = int.Parse(snapshot.Child(Constants.DATABASE_AVATAR_ID).Value.ToString());
                UserCarID = int.Parse(snapshot.Child(Constants.DATABASE_CAR_ID).Value.ToString());
                UserScore = snapshot.Child(Constants.DATABASE_MAX_SCORE).Value.ToString();
            }
            
            SetDatabaseDataToPlayerPrefs(UserAvatarID, UserCarID, UserNickname);

            if (string.IsNullOrEmpty(PlayerPrefs.GetString(Constants.PLAYER_PREFS_SCORE)) == false)
            {
                float scoreDatabase = float.Parse(UserScore);
                float scorePlayerPrefs = float.Parse(PlayerPrefs.GetString(Constants.PLAYER_PREFS_SCORE));

                if (scoreDatabase > scorePlayerPrefs || scoreDatabase == 0)
                {
                    InitUpdateScore(scorePlayerPrefs);
                }
                else
                {
                    PlayerPrefs.SetString(Constants.PLAYER_PREFS_SCORE, UserScore);
                }
            }
            else
            {
                PlayerPrefs.SetString(Constants.PLAYER_PREFS_SCORE, UserScore);
            }
            
            mainMenuPlayerDisplayData.InitPlayerDataUI(UserNickname, UserAvatarID, UserCarID);
        }
        
        private void SetInitialDataToUserData()
        {
            UserNickname = _user.DisplayName;
            UserAvatarID = 0;
            UserCarID = 0;
            UserScore = "0.00";
        }

        private void InitInitialDataToDatabase()
        {
            StartCoroutine(UpdateNickname(UserNickname));
            StartCoroutine(UpdateAvatarID(UserAvatarID));
            StartCoroutine(UpdateCarID(UserCarID));
            StartCoroutine(UpdateScore(0));
        }
        
        private IEnumerator UpdateNickname(string nickname)
        {
            var dbTask = DatabaseReference.Child(Constants.DATABASE_USERS)
                .Child(_user.UserId)
                .Child(Constants.DATABASE_NICKNAME)
                .SetValueAsync(nickname);
            
            yield return new WaitUntil(() => dbTask.IsCompleted);

            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
            }
            else
            {
                UserNickname = nickname;
            }
        }
        
        private IEnumerator UpdateAvatarID(int avatarID)
        {
            var dbTask = DatabaseReference.Child(Constants.DATABASE_USERS)
                .Child(_user.UserId)
                .Child(Constants.DATABASE_AVATAR_ID)
                .SetValueAsync(avatarID);
            
            yield return new WaitUntil(() => dbTask.IsCompleted);

            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
            }
            else
            {
                UserAvatarID = avatarID;
            }
        }
        
        private IEnumerator UpdateCarID(int carID)
        {
            var dbTask = DatabaseReference.Child(Constants.DATABASE_USERS)
                .Child(_user.UserId)
                .Child(Constants.DATABASE_CAR_ID)
                .SetValueAsync(carID);
            
            yield return new WaitUntil(() => dbTask.IsCompleted);

            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
            }
            else
            {
                UserCarID = carID;
            }
            
            PlayerPrefs.SetInt(Constants.PLAYER_PREFS_CAR_ID, carID);
        }
        
        private IEnumerator UpdateScore(float score)
        {
            var dbGetScoreTask = DatabaseReference.Child(Constants.DATABASE_USERS)
                .Child(_user.UserId)
                .Child(Constants.DATABASE_MAX_SCORE)
                .GetValueAsync();
            
            yield return new WaitUntil(() => dbGetScoreTask.IsCompleted);

            if (dbGetScoreTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbGetScoreTask.Exception}");
            }
            else if(dbGetScoreTask.Result.Value != null)
            {
                if (score < float.Parse(dbGetScoreTask.Result.Value.ToString()))
                {
                    score = float.Parse(dbGetScoreTask.Result.Value.ToString());
                }
            }

            var dbTask = DatabaseReference.Child(Constants.DATABASE_USERS)
                .Child(_user.UserId)
                .Child(Constants.DATABASE_MAX_SCORE)
                .SetValueAsync(score);
            
            yield return new WaitUntil(() => dbTask.IsCompleted);
            
            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbGetScoreTask.Exception}");
            }
        }
        
        private void SetDatabaseDataToPlayerPrefs(int avatarID, int carID, string nickname)
        {
            PlayerPrefs.SetInt(Constants.PLAYER_PREFS_CAR_ID, carID);
            PlayerPrefs.SetInt(Constants.PLAYER_PREFS_AVATAR_ID, avatarID);
            PlayerPrefs.SetString(Constants.PLAYER_PREFS_NICKNAME, nickname);
        }
    }
}