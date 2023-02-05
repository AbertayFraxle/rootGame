using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDraw : MonoBehaviour
{

    public GameObject checker;
    public GameObject frontChecker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().isVisible == false)
        {
            if (transform.position.x < checker.transform.position.x)
            {
                Vector3 move = new Vector3(10, 0, 0);
                transform.Translate(move * 8f);
            }
            else if (transform.position.x > frontChecker.transform.position.x)
            {
                Vector3 move = new Vector3(10, 0, 0);
                transform.Translate(move * -8f);
            }
        }


    }
}
