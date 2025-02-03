using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
   public TMP_Text playerUsername;
   public TMP_Text teamText;
   public Player player;
   int team;

   public void Setup(Player _player, int _team)
   {
    player = _player;
    playerUsername.text = _player.NickName;
    team = _team;
    teamText.text = "Team " + _team;

    ExitGames.Client.Photon.Hashtable customProps = new ExitGames.Client.Photon.Hashtable();
    customProps["Team"] = _team;
    _player.SetCustomProperties(customProps);
   }

   public override void OnPlayerLeftRoom(Player otherPlayer)
   {
    if (player == otherPlayer)
    {
        Destroy(gameObject);
    }
   }

   public override void OnLeftRoom()
   {
    Destroy(gameObject);
   }
}
