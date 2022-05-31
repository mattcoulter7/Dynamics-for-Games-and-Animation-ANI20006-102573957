using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ParticleSystemExtension
{
    static public ParticleSystem.Particle CreateParticle(this ParticleSystem ps,Vector3 pos,Vector3 dir)
    {
        ParticleSystem.Particle p = new ParticleSystem.Particle();
        p.velocity = dir.normalized * ps.main.startSpeed.Evaluate(Random.value);
        p.rotation3D = new Vector3(ps.main.startRotationX.Evaluate(Random.value), ps.main.startRotationY.Evaluate(Random.value), ps.main.startRotationZ.Evaluate(Random.value));
        p.rotation = ps.main.startRotation.Evaluate(Random.value);
        p.startSize3D = new Vector3(ps.main.startSizeX.Evaluate(Random.value), ps.main.startSizeY.Evaluate(Random.value), ps.main.startSizeZ.Evaluate(Random.value));
        p.startSize = ps.main.startSize.Evaluate(Random.value);
        p.randomSeed = ps.randomSeed;
        p.startColor = ps.main.startColor.Evaluate(Random.value);
        p.startLifetime = ps.main.startLifetime.Evaluate(Random.value);
        p.remainingLifetime = p.startLifetime;
        p.position = pos;
        return p;
    }

    static public void AddParticle(this ParticleSystem ps, ParticleSystem.Particle p)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount + 1];
        ps.GetParticles(particles);
        particles[particles.Length - 1] = p;
        ps.SetParticles(particles);
    }
    static public ParticleSystem.Particle Duplicate(this ParticleSystem.Particle p)
    {

        ParticleSystem.Particle np = new ParticleSystem.Particle();
        np.velocity = p.velocity;
        np.rotation3D = p.rotation3D;
        np.rotation = p.rotation;
        np.startSize3D = p.startSize3D;
        np.startSize = p.startSize;
        np.randomSeed = p.randomSeed;
        np.startColor = p.startColor;
        np.startLifetime = p.startLifetime;
        np.remainingLifetime = p.remainingLifetime;
        np.position = p.position;
        return np;
    }
}
