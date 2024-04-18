using Services.Firebase;
using UnityEngine;

namespace Services
{
    public class EntryPointMainMenu : MonoBehaviour
    {
        [SerializeField] private UpdateDataManager _updateDataManager;
        [SerializeField] private PlayerDataDisplayMainMenu playerDataDisplayMainMenu;

        private void Awake()
        {
            _updateDataManager.InitDatabase(playerDataDisplayMainMenu);
            
        }
    }
}