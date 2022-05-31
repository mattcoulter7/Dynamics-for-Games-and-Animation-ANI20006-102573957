using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDuplicator : MonoBehaviour
{
    ParticleSystem _ps;
    public float interval = 1f;
    public float sizeScale = 1f;
    public float lifetimeScale = 1f;
    public float speedScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(Duplicate());
    }

    IEnumerator Duplicate()
    {
        while (!_ps.isPaused)
        {
            if (_ps.particleCount < _ps.main.maxParticles)
            {
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[Mathf.Min(_ps.particleCount * 2, _ps.main.maxParticles)];
                _ps.GetParticles(particles);

                for (int i = 0; i < particles.Length / 2; i++)
                {
                    ParticleSystem.Particle p = particles[i].Duplicate();
                    p.startSize *= sizeScale;
                    p.startLifetime *= lifetimeScale;
                    p.velocity *= speedScale;
                    particles[particles.Length / 2 + i] = p;
                }
                _ps.SetParticles(particles, particles.Length);
            }
            yield return new WaitForSeconds(interval);
        }
    }
}
