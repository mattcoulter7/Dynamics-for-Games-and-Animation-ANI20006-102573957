using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKeeper : MonoBehaviour
{
    ParticleSystem _ps;
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_ps.particleCount];
        _ps.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].remainingLifetime = 100f;
        }
        _ps.SetParticles(particles, particles.Length);
    }
}
