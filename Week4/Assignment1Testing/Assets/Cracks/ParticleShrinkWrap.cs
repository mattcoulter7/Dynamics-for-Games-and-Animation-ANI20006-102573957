using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShrinkWrap : MonoBehaviour
{
    ParticleSystem _ps;
    public float offset = 0f;
    public bool alignToNormal = true;
    Collider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        _collider = GetComponentInParent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_ps.isPaused) return;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_ps.particleCount];
        _ps.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            Vector3 curPos = particles[i].position;
            Vector3 newPos = _collider.ClosestPoint(curPos);
            particles[i].position = newPos;
        }
        _ps.SetParticles(particles,particles.Length);
    }
}
