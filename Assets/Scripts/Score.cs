using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Score : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public UnityEvent<float> onDistanceChange;

    private void Update()
    {
       onDistanceChange.Invoke(player.transform.position.x); 
    }
}
