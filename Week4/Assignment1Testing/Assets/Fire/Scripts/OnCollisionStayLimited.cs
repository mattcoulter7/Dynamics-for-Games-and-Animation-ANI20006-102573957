using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionStayLimited : MonoBehaviour
{
    public float limitSeconds = 1f;
    bool canForward = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("EnableCanForward");
    }
    IEnumerator EnableCanForward()
    {
        while (true)
        {
            // execute block of code here
            yield return new WaitForSeconds(limitSeconds);
            canForward = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (canForward)
        {
            BroadcastMessage("OnCollisionStayLimited", collision);
            canForward = false;
        }
    }
}
