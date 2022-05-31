using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialForce : MonoBehaviour
{
    public Vector3 force = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(force);
    }
}
