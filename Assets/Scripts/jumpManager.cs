using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpManager : MonoBehaviour
{

    [SerializeField] private float jumpStrength = 500;
    [SerializeField] private GameObject brain;
    [SerializeField] private GameObject root;

    TreeController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = root.GetComponent<TreeController>();
    }

    public void doBoost()
    {
        //activates once boost powerup is collected
        this.GetComponent<Rigidbody2D>().AddForce((Vector2.up * 2 + Vector2.right) * 500, ForceMode2D.Impulse);
        controller.playTreeSound(5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){ 
          if (brain.GetComponent<pickupManager>().tryJump())
            {
                this.GetComponent<Rigidbody2D>().AddForce(Vector2.up*jumpStrength,ForceMode2D.Impulse);
                controller.playTreeSound(5);
            }
        }
    }
}
