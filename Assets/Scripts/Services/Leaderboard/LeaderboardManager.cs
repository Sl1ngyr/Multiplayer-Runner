using System;
using System.Collections;
using System.Linq;
using Firebase.Database;
using Services.Const;
using Services.Firebase;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services.Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private LeaderboardRow _leaderboardRowPrefab;
        [SerializeField] private GameObject _rowParent;

        private DatabaseReference _databaseReference;
        
        private UpdateDataManager _updateDataManager;
        private UIMainMenuManager _uiMainMenuManager;
        private MainMenuPlayerDataConfig _mainMenuPlayerDataConfig;
        
        private int _currentPlayerInTop;
        private int _maxPlayersInTop = 5;
        
        [Inject]
        private void Construct (UpdateDataManager updateDataManager, 
            UIMainMenuManager uiMainMenuManager,
            MainMenuPlayerDataConfig mainMenuPlayerDataConfig)
        {
            _updateDataManager = updateDataManager;
            _uiMainMenuManager = uiMainMenuManager;
            _mainMenuPlayerDataConfig = mainMenuPlayerDataConfig;
        }

        public void Init()
        {
            _databaseReference = _updateDataManager.DatabaseReference;

            _currentPlayerInTop = 1;
            
            ClearLeaderboardRows();
            StartCoroutine(LoadLeaderboardData());
        }

        private void CreateLeaderboardRow(DataSnapshot childSnapshot, int position, bool isLocalPlayerInTop)
        {
            string nickname = childSnapshot.Child(Constants.DATABASE_NICKNAME).Value.ToString();
            string score = childSnapshot.Child(Constants.DATABASE_MAX_SCORE).Value.ToString();
            int avatarId = int.Parse(childSnapshot.Child(Constants.DATABASE_AVATAR_ID).Value.ToString());
            
            LeaderboardRow leaderboardRow = Instantiate(_leaderboardRowPrefab, _rowParent.transform);
            
            leaderboardRow.Init(position, nickname, _mainMenuPlayerDataConfig.AvatarData[avatarId].AvatarSprite, score, isLocalPlayerInTop);
        }

        private void InitLeaderboardData(DataSnapshot snapshot)
        {
            bool isLocalPlayerInTop = false;
            bool isFoundLocalPlayer = false;
            bool isInitRowLocalPlayer = false;
            
            foreach (var childSnapshot in snapshot.Children.Reverse())
            {
                if (childSnapshot.Key == _updateDataManager.UserID)
                {
                    isLocalPlayerInTop = _currentPlayerInTop <= _maxPlayersInTop;
                    isFoundLocalPlayer = true;
                }

                if (_currentPlayerInTop > _maxPlayersInTop && isInitRowLocalPlayer)
                {
                    return;
                }
                
                if (isFoundLocalPlayer)
                {
                    CreateLeaderboardRow(childSnapshot, _currentPlayerInTop, isLocalPlayerInTop);
                    
                    isInitRowLocalPlayer = true;
                    isFoundLocalPlayer = false;
                    isLocalPlayerInTop = false;
                }
                else if(_currentPlayerInTop <= _maxPlayersInTop)
                {
                    CreateLeaderboardRow(childSnapshot, _currentPlayerInTop, isLocalPlayerInTop);
                }

                _currentPlayerInTop++;
            }
        }
        
        private IEnumerator LoadLeaderboardData()
        {
            var dbTask = _databaseReference.Child(Constants.DATABASE_USERS)
                .OrderByChild(Constants.DATABASE_MAX_SCORE)
                .GetValueAsync();

            yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
            }
            else
            {
                DataSnapshot snapshot = dbTask.Result;

                InitLeaderboardData(snapshot);
            }
        }
        
        private void ClearLeaderboardRows()
        {
            for (int i = 0; i < _rowParent.transform.childCount; i++)
            {
                Destroy(_rowParent.transform.GetChild(i).gameObject);
            }
        }
        
        private void CloseLeaderboard()
        {
            _uiMainMenuManager.ManagementStatusStartCanvases(true);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseLeaderboard);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseLeaderboard);
        }
    }
}