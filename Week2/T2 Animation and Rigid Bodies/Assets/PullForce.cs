using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullForce : MonoBehaviour
{
    public Transform pullTowards;
    public float pullForce = 200;
    public float pullRefresh = 2;
    private Coroutine forceRoutine = null;

    private void OnCollisionEnter(Collision other){
        Debug.Log("Object Entered");
        forceRoutine = StartCoroutine(applyForce(other.gameObject));
    }
    private void OnCollisionExit(Collision other){
        Debug.Log("Object Left");
        if (forceRoutine != null){
            StopCoroutine(forceRoutine);
        }
    }

    private IEnumerator applyForce(GameObject obj){
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        Vector3 toCentre = pullTowards.position - obj.transform.position;
        rb.AddForce(toCentre * pullForce);
        yield return pullRefresh;
        applyForce(obj);
    }
}
