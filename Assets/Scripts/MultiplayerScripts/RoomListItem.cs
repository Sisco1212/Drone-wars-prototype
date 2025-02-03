using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
  [SerializeField] TMP_Text roomNameText;
  public RoomInfo info;

    public void Setup(RoomInfo _info)
    {
        info = _info;
        roomNameText.text = _info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }
}
