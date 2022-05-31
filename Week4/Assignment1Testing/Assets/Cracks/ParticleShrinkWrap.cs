using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShrinkWrap : MonoBehaviour
{
    ParticleSystem _ps;
    public float offset = 0f;
    public bool alignToNormal = true;
    // Start is called before the first frame update
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
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
            Vector3 targetPos = transform.position;
            Vector3 dir = targetPos - curPos;

            RaycastHit hit = new RaycastHit();

            Ray ray = new Ray(curPos - dir, dir);
            if (!Physics.Raycast(ray, out hit))
                continue;

            particles[i].position = hit.point + hit.normal * offset;
            if (alignToNormal) particles[i].rotation3D = hit.normal;
        }
        _ps.SetParticles(particles,particles.Length);
    }
}
