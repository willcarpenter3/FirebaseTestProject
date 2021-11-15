using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBehavior : MonoBehaviour
{

    public PositionData posData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Updating Position");
        posData.posX = transform.position.x;
        posData.posZ = transform.position.z;
    }
}
