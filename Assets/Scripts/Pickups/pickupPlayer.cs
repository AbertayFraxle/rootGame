using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupPlayer : MonoBehaviour
{
    bool picked;

    public bool isPicked()
    {
        return picked;
    }

    public void setPicked()
    {
        picked = true;
    }
}
