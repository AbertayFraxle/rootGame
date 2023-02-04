using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetScoreText(float distance)
    {
        scoreText.text = "Distance: " + distance;
    }
}
