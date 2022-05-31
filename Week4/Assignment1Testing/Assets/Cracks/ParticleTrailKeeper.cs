using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrailKeeper : MonoBehaviour
{
    ParticleSystem _ps;
    Coroutine timer = null;
    public float duration = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    IEnumerator PauseAfter(float x)
    {
        yield return new WaitForSeconds(x);
        _ps.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (_ps.particleCount == 0) return;
        if (timer != null) return;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[Mathf.Min(_ps.particleCount * 2, _ps.main.maxParticles)];
        _ps.GetParticles(particles);
        timer = StartCoroutine(PauseAfter(duration));
    }
}
