using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject player;
    
    private void Update()
    {
        var position = transform.position;
        position = new Vector3(position.x + speed * Time.deltaTime, position.y, position.z);
        transform.position = position;
        
        
        //teleport back if behind player
        if (transform.position.x > player.transform.position.x + GetScreenWidthInWorldDistance()) 
        {
            TeleportBack();
        }
    }

    private float GetScreenWidthInWorldDistance()
    {
        return Camera.main.orthographicSize * Camera.main.aspect * 3;
    }
    
    private void TeleportBack()
    {
        transform.position = new Vector3(player.transform.position.x - GetScreenWidthInWorldDistance(),
            transform.position.y, transform.position.z);
    }
}
