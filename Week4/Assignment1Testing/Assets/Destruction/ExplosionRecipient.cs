using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRecipient : MonoBehaviour
{
    public List<GameObject> rigidBodyObjects;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExplosion(){
        foreach (GameObject go in rigidBodyObjects){
            go.AddComponent<Rigidbody>();
        }
    }
}
