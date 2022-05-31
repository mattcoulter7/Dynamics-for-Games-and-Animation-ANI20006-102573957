using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationCurveExtension
{
    public static List<float> GeneratePoints(this AnimationCurve curve, float accuracy = 0.01f)
    {
        List<float> points = new List<float>();
        float t = 0f;
        float? value = curve.Evaluate(t);

        while (value.HasValue)
        {
            points.Add(value.Value);
            t += accuracy;
            value = curve.Evaluate(t);
        }
        return points;
    }
    public static Tuple<float, float> GetLocalMinMax(this AnimationCurve curve,float accuracy = 0.01f)
    {
        float[] points = curve.GeneratePoints(accuracy).ToArray();
        return new Tuple<float, float>(
            Mathf.Min(points),
            Mathf.Max(points)
        );
    }
}
