using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStopper : MonoBehaviour
{
    ParticleSystem _ps;
    Coroutine timer = null;
    public float duration = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    IEnumerator PauseAfter()
    {
        yield return new WaitForSeconds(duration);

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[Mathf.Min(_ps.particleCount * 2, _ps.main.maxParticles)];
        _ps.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].velocity *= 0f;
        }

        _ps.SetParticles(particles, particles.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (_ps.particleCount == 0) return;
        if (timer != null) return;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[Mathf.Min(_ps.particleCount * 2, _ps.main.maxParticles)];
        _ps.GetParticles(particles);
        timer = StartCoroutine(PauseAfter());
    }
}
