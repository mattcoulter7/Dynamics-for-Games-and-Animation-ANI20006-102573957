using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSOverallSizeOverLifetime : MonoBehaviour
{
    ParticleSystem _ps;
    ParticleSystem.MainModule _main;
    public AnimationCurve startSizeCurve = AnimationCurve.Linear(0, 0, 100, 10);
    
    public bool inProgress = false;
    public float beginTime = 0;
    public float endTime = 0;
    public float currentDuration = 0;
    // Start is called before the first frame update
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        _main = _ps.main;
    }

    void Update()
    {
        if (inProgress)
        {
            currentDuration = Time.time - beginTime;
            _main.startSize = startSizeCurve.Evaluate(currentDuration);


            if (currentDuration >= endTime){
                Finish();
            }
        }
    }

    public void Run()
    {
        beginTime = Time.time;
        endTime = startSizeCurve[startSizeCurve.length - 1].time;
        inProgress = true;
    }

    public void Finish(){
        inProgress = false;
    }
}
