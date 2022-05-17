using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class FireManager : MonoBehaviour
{
    [System.Serializable]
    public class FirePoint
    {

        float _exposureArea;
        FireManager fireManager;
        public FirePoint(FireManager fm, Vector2 p, float e)
        {
            fireManager = fm;
            texUV = p;
            _exposureArea = e;
        }
        public readonly Vector2 texUV;
        public float exposureArea
        {
            set
            {
                _exposureArea = value;
                fireManager.SendMessage("OnPointChanged", this);
            }
            get
            {
                return _exposureArea;
            }
        }
    }
    public List<FirePoint> points = new List<FirePoint>();
    public PSOverallSizeOverLifetime[] psSizeControllers;
    
    public int maxPoints = 10;
    public float depth = 5f; // depth of solid, way may be able to calculate this
    public float temperature = 60f;
    
    bool fireStarted = false;
    private float _exposureArea;
    void Start()
    {
        psSizeControllers = GetComponentsInChildren<PSOverallSizeOverLifetime>();
    }

    public void Light(Vector2 pos)
    {
        if (points.Count >= maxPoints) return;
        if (fireStarted == false)
        {
            OnBeginFire();
        }
        FirePoint point = new FirePoint(this, pos, 1);
        points.Add(point);
        OnPointChanged(point);
    }

    void OnBeginFire()
    {
        fireStarted = true;
        foreach (PSOverallSizeOverLifetime sizeController in psSizeControllers)
        {
            sizeController.Run();
        }
    }

    void OnPointChanged(FirePoint pt)
    {
        _exposureArea = CalculateExposureArea();
        GetComponent<Painter>().PaintCircle(pt.texUV, pt.exposureArea);
    }

    float CalculateExposureArea()
    {
        float result = 0f;
        foreach (var point in points)
        {
            result += point.exposureArea;
        }
        return result;
    }
}
