using UnityEngine;
using Photon.Pun;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard Instance;
    public TMP_Text team1ScoreText;
    public TMP_Text team2ScoreText;
    public int team1Score;
    public int team2Score;
    private PhotonView view;


    void Awake()
    {
        Instance = this;
        view = GetComponent<PhotonView>();
    }

    public void PlayerDied(int playerTeam)
    {
        if (playerTeam == 2)
        {
            team1Score++;
        }

        if (playerTeam == 1)
        {
            team2Score++;
        }

    view.RPC("UpdateScores", RpcTarget.All, team1Score, team2Score);
    }

    [PunRPC]
    void UpdateScores(int team1, int team2)
    {
        team1Score = team1;
        team2Score = team2;

        team1ScoreText.text = team1Score.ToString();
        team2ScoreText.text = team2Score.ToString();
    }

}
