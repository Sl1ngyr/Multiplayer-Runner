using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace GameComponents.Level
{
    public class LevelGeneration : NetworkBehaviour
    {
        [SerializeField] private GameObject _startPartLevel;
        [SerializeField] private GameObject _finishPartLevel;
        
        [SerializeField] private List<GameObject> _levelParts;
        
        [SerializeField] private int _levelLength;
        [SerializeField] private Vector3 _offsetPart;
        
        [SerializeField] private GameObject _parentLevelParts;
        
        public override void Spawned()
        {
            if (Runner.LocalPlayer.PlayerId == 1)
            {
                Vector3 spawnPosition = Vector3.zero;
                
                NetworkObject levelPart = Runner.Spawn(_startPartLevel, spawnPosition, Quaternion.identity);
                levelPart.transform.parent = _parentLevelParts.transform;
                
                for (int i = 1; i < _levelLength + 1; i++)
                {
                    int randomLevelPart = Random.Range(0, _levelParts.Count);

                    levelPart = Runner.Spawn(_levelParts[randomLevelPart], spawnPosition + _offsetPart * i, Quaternion.identity);
                    
                    levelPart.transform.parent = _parentLevelParts.transform;
                }
                
                levelPart = Runner.Spawn(_finishPartLevel, spawnPosition + _offsetPart * (_levelLength + 1), Quaternion.identity);
                levelPart.transform.parent = _parentLevelParts.transform;
            }
        }
    }
}