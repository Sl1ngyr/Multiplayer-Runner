using Services.Firebase;
using UnityEngine;
using UnityEngine.Serialization;

namespace Services
{
    public class EntryPointMainMenu : MonoBehaviour
    {
        [SerializeField] private UpdateDataManager _updateDataManager;
        [FormerlySerializedAs("playerDataDisplayMainMenu")] [SerializeField] private MainMenuPlayerDisplayData mainMenuPlayerDisplayData;

        private void Awake()
        {
            _updateDataManager.InitDatabase(mainMenuPlayerDisplayData);
            
        }
    }
}