using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerUsernameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField playerUsernameInput;
    [SerializeField] TMP_Text errorMessage;

    void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
        PhotonNetwork.NickName = PlayerPrefs.GetString("username");
        playerUsernameInput.text = PlayerPrefs.GetString("username");
        }
    }

    public void OnPlayerInputChanged()
    {
        string username = playerUsernameInput.text;
        if (!string.IsNullOrEmpty(username) && username.Length <= 20)
        {
        PhotonNetwork.NickName = username;
        PlayerPrefs.SetString("Username", username);
        errorMessage.text = "";
        MenuManager.Instance.OpenMenu("TitleMenu");
        }
        else {
            errorMessage.text = "Your input should not be more than 20 characters";
        }
    }

}
