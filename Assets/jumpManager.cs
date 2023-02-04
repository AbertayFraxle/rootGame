using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpManager : MonoBehaviour
{

    [SerializeField] private float jumpStrength = 500;
    [SerializeField] private GameObject brain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){ 
          if (brain.GetComponent<pickupManager>().tryJump())
            {
                this.GetComponent<Rigidbody2D>().AddForce(Vector2.up*jumpStrength,ForceMode2D.Impulse);
            }
        }
    }
}
