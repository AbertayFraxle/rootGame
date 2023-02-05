using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;

public class LeaderboardController : MonoBehaviour
{
    public TMP_InputField nickname;
    public TextMeshProUGUI scoreDisplay;
    [SerializeField]private GameObject player;
    public string ID;
    private int score;



    private int maxScores = 8;
    public TextMeshProUGUI[] entries;

    private void Start()
    {

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                print("Success!");
            }
            else
            {
                print("Failure");
            }
        });
    }

    private void Update()
    {
        score = (int)player.transform.position.x;
        scoreDisplay.text = score.ToString();
    }

    public void showScores()
    {
        LootLockerSDKManager.GetScoreList(ID, maxScores, (response) =>
        {
            if (response.success)
            {
               
                    LootLockerLeaderboardMember[] scores = response.items;

                    for (int i = 0; i < scores.Length; i++)
                    {
                        entries[i].text = (scores[i].rank + ". " + scores[i].member_id + " - " + scores[i].score);
                    }

                    if (scores.Length < maxScores)
                    {
                        for (int i = scores.Length; i < maxScores; i++)
                        {
                            entries[i].text = (i + 1).ToString() + ". none";
                        }
                    }

            }
            else
            {
                print("Failure");
            }
        });
    }

    public void SubmitScore()
    {

        score = (int)player.transform.position.x;
        LootLockerSDKManager.SubmitScore(nickname.text, score, ID, (response) =>
        {
            if (response.success)
            {
                print("Score posted!");
                showScores();
            }
            else {

                print("something broke...");
            }
        });

    }
}
