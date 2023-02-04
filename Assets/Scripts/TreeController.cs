using UnityEngine;

public class TreeController : MonoBehaviour
{
    //root from left to right, 1 to 5.
    [SerializeField] private SpringJoint2D rootJoint1;
    [SerializeField] private SpringJoint2D rootJoint2;
    [SerializeField] private SpringJoint2D rootJoint3;
    [SerializeField] private SpringJoint2D rootJoint4;
    [SerializeField] private SpringJoint2D rootJoint5;
    
    //this controls the distance of the spring. The force the leg sprung up. 
    [SerializeField] private float springDist = 200f;

    private void Update()
    {
        rootJoint1.distance = Input.GetKey(KeyCode.Q) ? springDist : 0;
        rootJoint2.distance = Input.GetKey(KeyCode.W) ? springDist : 0;
        rootJoint3.distance = Input.GetKey(KeyCode.Space) ? springDist : 0;
        rootJoint4.distance = Input.GetKey(KeyCode.O) ? springDist : 0;
        rootJoint5.distance = Input.GetKey(KeyCode.P) ? springDist : 0;
    }
    
}
