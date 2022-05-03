using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireConduction : MonoBehaviour
{
    public float maxExposure = 300f;
    public float thermalConductivity = 1.1f;
    public float growRate = 0.1f;
    FireManager fm;

    void Start()
    {
        fm = GetComponent<FireManager>();
        StartCoroutine("Grow");
    }
    IEnumerator Grow()
    {
        while (true)
        {
            // execute block of code here
            yield return new WaitForSeconds(growRate);

            // update the radius of each point according to convection formula
            foreach (var point in fm.points)
            {
                if (point.exposureArea >= maxExposure) continue;
                point.exposureArea = thermalConductivity * point.exposureArea * (fm.temperature) / fm.depth;
            }
        }
    }

    void OnCollisionStayLimited(Collision collision)
    {
        ContactPoint contactPoint = collision.GetContact(0);
        Vector2? pixelUV = GetComponent<Painter>().GetUVPixel(contactPoint.point, contactPoint.normal);
        if (pixelUV != null)
        {
            fm.Light(pixelUV.Value);
        }
    }

    void OnConductionCollision(List<ParticleCollisionEvent> collisionEvents)
    {
        Debug.Log(collisionEvents);
        foreach (ParticleCollisionEvent contactPoint in collisionEvents)
        {
            Vector2? pixelUV = GetComponent<Painter>().GetUVPixel(contactPoint.intersection, contactPoint.normal);
            if (pixelUV != null)
            {
                fm.Light(pixelUV.Value);
            }
        }
    }
}
