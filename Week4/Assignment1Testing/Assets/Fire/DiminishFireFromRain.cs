using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiminishFireFromRain : MonoBehaviour
{
    public float diminishSpeed = 0.01f;
    FireController _fireController;
    // Start is called before the first frame update
    void Start()
    {
        _fireController = GetComponent<FireController>();
    }
    
    void OnParticleCollision(GameObject other){
        Debug.Log("Particle Collision");
        _fireController.size -= diminishSpeed;
    }
}
