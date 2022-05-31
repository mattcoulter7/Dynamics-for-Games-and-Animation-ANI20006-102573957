using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleVelocityRandomizer : MonoBehaviour
{
    ParticleSystem _ps;
    public float interval = 1f;
    public float offset = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(ChangeDirection());
    }

    IEnumerator ChangeDirection()
    {
        while (!_ps.isPaused)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_ps.particleCount];
            _ps.GetParticles(particles);
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].velocity.magnitude == 0f) continue;
                particles[i].velocity = (particles[i].velocity.normalized + new Vector3(
                    Random.Range(-offset,offset), 
                    Random.Range(-offset, offset), 
                    Random.Range(-offset, offset)
                ).normalized).normalized * particles[i].velocity.magnitude;
            }
            _ps.SetParticles(particles, particles.Length);
            yield return new WaitForSeconds(interval);
        }
    }
}
