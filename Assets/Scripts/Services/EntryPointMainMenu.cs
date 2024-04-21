using Services.Firebase;
using UnityEngine;

namespace Services
{
    public class EntryPointMainMenu : MonoBehaviour
    {
        [SerializeField] private UpdateDataManager _updateDataManager;
        [SerializeField] private MainMenuPlayerDisplayData mainMenuPlayerDisplayData;

        private void Awake()
        {
            _updateDataManager.InitDatabase(mainMenuPlayerDisplayData);
            
        }
    }
}