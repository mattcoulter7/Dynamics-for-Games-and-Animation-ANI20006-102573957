using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public float width;
    public float height;
    public float depth;
    public GameObject cubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (float x = -width / 2; x < width / 2; x+=cubePrefab.transform.localScale.x)
        {
            for (float y = -height / 2; y < height / 2; y+= cubePrefab.transform.localScale.y)
            {
                for (float z = -depth / 2; z < depth / 2; z+= cubePrefab.transform.localScale.z)
                {
                    GameObject obj = Instantiate(cubePrefab, transform);
                    obj.transform.position = new Vector3(x, y, z);
                }
            }
        }
    }
}
