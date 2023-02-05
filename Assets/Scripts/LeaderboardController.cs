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

    public void SubmitScore()
    {
        score = (int)player.transform.position.x;
        LootLockerSDKManager.SubmitScore(nickname.text, score, ID, (response) =>
        {
            if (response.success)
            {
                print("Score posted!");
            }
            else {
                print("something broke...");
            }
        });

    }
}
