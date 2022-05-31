using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionInstantiator : MonoBehaviour
{
    public GameObject particleSystemPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = Instantiate(particleSystemPrefab,transform);
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        var newParticle = ps.CreateParticle(collision.GetContact(0).point, -collision.GetContact(0).normal);
        ps.AddParticle(newParticle);
    }
}
