using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    }

    private void Update()
    {
        score = (int)player.transform.position.x;
        scoreDisplay.text = score.ToString();
    }

    public void SubmitScore()
    {
        score = (int)player.transform.position.x;


    }
}
