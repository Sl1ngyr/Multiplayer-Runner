using UnityEngine;

namespace Services.Avatar
{
    [CreateAssetMenu(fileName = "Avatar", menuName = "ScriptableObjects/AvatarData")]
    public class AvatarData : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set;}
        [field: SerializeField] public Sprite AvatarSprite { get; private set;}
    }
}