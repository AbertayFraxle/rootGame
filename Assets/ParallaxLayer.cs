using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    Vector2 camPos;

    public float divider;

    public float offsetWidth;
    public float offsetHeight;

    public bool infiniteY;

    int offsetX = 0;
    int offsetY = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    void setlayerPos()
    {
        transform.position = new Vector3((camPos.x / divider) + offsetWidth * offsetX, (camPos.y / divider) + offsetHeight * offsetY, 0);
    }

    void checklayerOffset()
    {

        if (camPos.x - transform.position.x > offsetWidth)
        {
            offsetX += 1;
            setlayerPos();

            while (camPos.x - transform.position.x > offsetWidth)
            {
                offsetX += 1;
                setlayerPos();
            }
        }

        if (camPos.x - transform.position.x < -offsetWidth)
        {
            offsetX -= 1;
            setlayerPos();

            while (camPos.x - transform.position.x < -offsetWidth)
            {
                offsetX -= 1;
                setlayerPos();
            }
        }

        if (camPos.y - transform.position.y > offsetHeight && infiniteY)
        {
            offsetY += 1;
            setlayerPos();

            while (camPos.y - transform.position.y > offsetHeight)
            {
                offsetY += 1;
                setlayerPos();
            }
        }

        if (camPos.y - transform.position.y < -offsetHeight && infiniteY)
        {
            offsetY -= 1;
            setlayerPos();

            while (camPos.y - transform.position.y < -offsetHeight)
            {
                offsetY -= 1;
                setlayerPos();
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        camPos = Camera.main.transform.position;
        setlayerPos();
        checklayerOffset();
    }
}
