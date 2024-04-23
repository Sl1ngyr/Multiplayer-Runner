using System.Collections.Generic;
using Fusion;
using Services.Const;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class PreviewPlayers : NetworkBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> _nicknames;

        public void Init()
        {
            for (int i = 0; i < Constants.RUNNER_MAX_PLAYER_IN_SESSION; i++)
            {
                _nicknames[i].text = "Player " + (i + 1);
            }
        }
    }
}