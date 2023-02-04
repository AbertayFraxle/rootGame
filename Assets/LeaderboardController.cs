using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;


public class LeaderboardController : MonoBehaviour
{
    public InputField nickname;
    [SerializeField]private GameObject player;
    public string ID;
    private int score;

    private void Start()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            if (response.success)
            {
                print("success");
            }
            else
            {
                print("failed");
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
                print("success");
            }
            else
            {
                print("failed");
            }
        });
    }
}
