using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfter : MonoBehaviour
{
    public float seconds = 4f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
