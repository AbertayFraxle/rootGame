using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class oneliners : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public string[] fireLines;

    [SerializeField] public string[] rootLines;

    [SerializeField] private GameObject player;

    private void OnEnable()
    {
        if (player.GetComponent<livesManager>().causeOfDeath == 0)
        {
            int randomI = Random.Range(0, fireLines.Length);

            GetComponent<TextMeshProUGUI>().text = fireLines[randomI];
        }
        else
        {
            int randomI = Random.Range(0, rootLines.Length);

            GetComponent<TextMeshProUGUI>().text = rootLines[randomI];
        }
    }
}
