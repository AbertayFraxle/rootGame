using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadLeaderboard : MonoBehaviour
{

    [SerializeField] private GameObject leaderManager;


    private void OnEnable()
    {
        leaderManager.GetComponent<LeaderboardController>().showScores();
    }
}
