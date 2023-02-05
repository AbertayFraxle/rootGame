using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class jumpCounter : MonoBehaviour
{
    [SerializeField] private GameObject player;


    // Update is called once per frame
    void Update()
    {
        pickupManager jumpMan =player.GetComponent<pickupManager>();
        GetComponent<TextMeshProUGUI>().text = "Oxyjumps: " + jumpMan.jumps + "/"+jumpMan.maxJumps;
    }
}
