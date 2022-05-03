using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float size = 1f;
    public float deleteThreshold = 0.01f;
    private float _lastSize = 1f;
    public ParticleSystem[] _particleSystems;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (size != _lastSize){
            // the size has changed
            foreach (ParticleSystem particleSystem in _particleSystems){
                particleSystem.gameObject.transform.localScale = new Vector3(size,size,size);
            }

            if (size <= deleteThreshold){
                Destroy(gameObject);
            }
        }

        _lastSize = size;
    }
}
