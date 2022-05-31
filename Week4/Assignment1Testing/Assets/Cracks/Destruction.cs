using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    [System.Serializable]
    public class CrackPoint
    {
        public List<Vector2> path;
        public Vector2 direction;
        public float progress = 0f;
        public float sizeScale = 1f;
        public float size = 1f;
        public float creationTime;
        public Vector2 currentTarget { 
            get
            {
                return path[path.Count - 1];
            }
        }
        public Vector2 previousTarget { 
            get
            {
                return path[path.Count - 2];
            }
        }
        public CrackPoint(Vector2 initialPoint,Vector2 dir,float s)
        {
            path = new List<Vector2> { initialPoint };
            direction = dir;
            size = s;
            creationTime = Time.time;
        }
        public void GenerateNewTarget(float length,float dirShift)
        {
            direction = direction + new Vector2(Random.Range(-dirShift, dirShift), Random.Range(-dirShift, dirShift));
            direction.Normalize();
            path.Add(currentTarget + direction * length);
        }
    }

    public float crackLength = 10f; // length of a straight line in the crack
    public float penetrationResistance = 1f; // scale the size of initial impact
    public float spreadRate = 5f; // seconds between each branches created
    public int maxCracks = 30; // max number of cracks (fixes performance issues)
    public float directionShift = 0.5f; // amount on x and y in which direction can vary from the previous point
    public float branchSizeReductionScale = 0.8f; // size of crack branches
    public AnimationCurve sizeReduction; // size of crack branches
    public float drawIncrement = 0.1f; // time incremenet lerping over line distance
    public float frequency = 0.001f;

    public List<CrackPoint> crackPoints = new List<CrackPoint>(); // list of all active cracks being drawn

    private Painter _painter; // painter reference for drawing lines
    private Coroutine spreadCoroutine = null; // spread coroutine (one per object)
    private Coroutine crackCoroutine = null; // spread coroutine (one per object)

    // Start is called before the first frame update
    void Start()
    {
        _painter = GetComponent<Painter>();
    }

    public void AttackPoint(Vector3 point,Vector3 direction,float strength){
        // handle creating a new crack
        Vector2? pixel = _painter.GetUVPixel(point,direction);
        if (!pixel.HasValue)
        {
            Debug.Log("No Pixel UV Found!");
            return;
        }

        CrackPoint crackPoint = new CrackPoint(
            pixel.Value,
            new Vector2(Random.Range(-1f,1f), Random.Range(-1f, 1f)),
            strength
        );
        crackPoint.GenerateNewTarget(crackLength,directionShift);
        crackPoints.Add(crackPoint);
        if (crackCoroutine == null)
        {
            crackCoroutine = StartCoroutine(Crack());
        }
        if (spreadCoroutine == null)
        {
            spreadCoroutine = StartCoroutine(Spread());
        }
    }
    void OnCollisionEnter(Collision other)
    {
        // handle creating crack from collision
        float penetration = other.relativeVelocity.magnitude / penetrationResistance;
        foreach (var cp in other.contacts)
        {
            AttackPoint(cp.point, cp.normal, penetration);
        }

        Debug.Log(other);
    }
    IEnumerator Crack()
    {
        while (crackPoints.Count > 0)
        {
            // handle removing the cracks once they are smaller than 1 pixel
            for (int i = crackPoints.Count - 1; i >= 0; i--)
            {
                if (crackPoints[i].size * crackPoints[i].sizeScale <= 0.5f)
                {
                    crackPoints.RemoveAt(i);
                }
            }

            // handle drawing the cracks
            foreach (CrackPoint cp in crackPoints)
            {
                cp.sizeScale = sizeReduction.Evaluate(Time.time - cp.creationTime);
                _painter.PaintCircle(Vector2.Lerp(cp.previousTarget, cp.currentTarget, cp.progress), cp.size * cp.sizeScale);
                cp.progress += drawIncrement;

                if (cp.progress >= 1f)
                {
                    cp.GenerateNewTarget(crackLength, directionShift);
                    cp.progress = 0f;
                }
            }

            yield return new WaitForSeconds(frequency);
        }

    }

    IEnumerator Spread()
    {
        // handle creating branches from cracks
        while (crackPoints.Count <= maxCracks)
        {
            for (int i = crackPoints.Count - 1; i >= 0; i--)
            {
                CrackPoint dup = new CrackPoint(
                    crackPoints[i].previousTarget, 
                    crackPoints[i].direction + new Vector2(Random.Range(-directionShift, directionShift), Random.Range(-directionShift, directionShift)),
                    crackPoints[i].sizeScale * branchSizeReductionScale
                );
                dup.GenerateNewTarget(crackLength,directionShift);
                crackPoints.Add(dup);
            }
            yield return new WaitForSeconds(spreadRate);
        }
    }
}
