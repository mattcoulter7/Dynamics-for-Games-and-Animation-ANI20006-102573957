using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullForce : MonoBehaviour
{
    public Transform pullTowards;
    public float pullForce = 200;
    public float pullRefresh = 2;

    private void OnTriggerEnter(Collider other){
        Debug.Log("Object Entered");
        StartCoroutine(applyForce(other.gameObject,true));
    }
    private void OnTriggerExit(Collider other){
        Debug.Log("Object Left");
        StartCoroutine(applyForce(other.gameObject,false));
    }

    private IEnumerator applyForce(GameObject obj,bool shouldPull){
        if (shouldPull){
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            Vector3 toCentre = pullTowards.position - obj.transform.position;
            rb.AddForce(toCentre * pullForce);
            yield return pullRefresh;
            StartCoroutine(applyForce(obj,shouldPull));
        }
    }
}
