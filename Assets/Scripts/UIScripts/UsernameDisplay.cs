using UnityEngine;
using TMPro;
using Photon.Pun;

public class UsernameDisplay : MonoBehaviour
{
    public PhotonView view;
    public TMP_Text usernameText;
    public TMP_Text teamText;

    void Start()
    {
        if (view.IsMine)
        {
            gameObject.SetActive(false);
        }

        usernameText.text = view.Owner.NickName;

        if (view.Owner.CustomProperties.ContainsKey("Team"))
        {
            int team = (int)view.Owner.CustomProperties["Team"];
            teamText.text = "Team " + team;
        }
    }
}
