using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSRemoveInvisibleParticles : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.Particle[] allParticles = new ParticleSystem.Particle[ps.particleCount];
        ps.GetParticles(allParticles);
        for (int i = 0; i < allParticles.Length; i++)
        {
            if (allParticles[i].startColor.a == 0)
            {
                allParticles[i].remainingLifetime = -1;
            }
        }
        ps.SetParticles(allParticles);
    }
}
