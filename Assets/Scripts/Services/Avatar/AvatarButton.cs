using UnityEngine;
using UnityEngine.UI;

namespace Services.Avatar
{
    public class AvatarButton : Button
    {
        [field: SerializeField] public int AvatarID { get; private set; }
    }
}