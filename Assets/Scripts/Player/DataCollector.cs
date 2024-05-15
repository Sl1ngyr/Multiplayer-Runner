using Fusion;
using Services.Const;
using UnityEngine;

namespace Player
{
    public class DataCollector : NetworkBehaviour
    {
        public string BestScore { get; private set; }
        public int AvatarID { get; private set; }
        public string Nickname { get; private set; }

        public override void Spawned()
        {
            if(!HasStateAuthority) return;
            
            BestScore = PlayerPrefs.GetString(Constants.PLAYER_PREFS_SCORE);
            AvatarID = PlayerPrefs.GetInt(Constants.PLAYER_PREFS_AVATAR_ID);
            Nickname = PlayerPrefs.GetString(Constants.PLAYER_PREFS_NICKNAME);
        }

        public void SetNewRecord(string record)
        {
            PlayerPrefs.SetString(Constants.PLAYER_PREFS_SCORE, record);
        }
        
    }
}