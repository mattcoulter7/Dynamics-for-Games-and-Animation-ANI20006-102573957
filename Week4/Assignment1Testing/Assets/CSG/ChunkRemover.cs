using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshMakerNamespace;
public class ChunkRemover : MonoBehaviour
{
    public string tagName = "";
    public Vector3 scaleModifier = new Vector3(1f,1f,1f);
    public GameObject chunkPrefab;
    public GameObject particleSystemPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(tagName)) return;

        GameObject chunk = Instantiate(chunkPrefab);
        chunk.transform.localScale = new Vector3(
            chunk.transform.localScale.x * scaleModifier.x,
            chunk.transform.localScale.y * scaleModifier.y,
            chunk.transform.localScale.z * scaleModifier.z
        ) * collision.relativeVelocity.magnitude;
        chunk.transform.position = collision.GetContact(0).point;

        // handle updating the main mesh
        CSG csg = new CSG();
        csg.Brush = chunk;
        csg.Target = gameObject;
        csg.OperationType = CSG.Operation.Subtract;
        csg.useCustomMaterial = false;
        csg.keepSubmeshes = true;
        csg.hideGameObjects = false;
        GameObject subtraction = csg.PerformCSG();

        // handle creating the separated sub mesh
        csg.OperationType = CSG.Operation.Intersection;
        GameObject intersection = csg.PerformCSG();
        intersection.GetComponent<Rigidbody>().isKinematic = false;
        MeshCollider col = intersection.AddComponent<MeshCollider>();
        col.convex = true;

        // destroy the chunk as everything has been calculated
        Destroy(chunk);


        // updated the current object to have the new mesh
        GetComponent<MeshFilter>().sharedMesh = subtraction.GetComponent<MeshFilter>().sharedMesh;
        GetComponent<MeshRenderer>().sharedMaterials = subtraction.GetComponent<MeshRenderer>().sharedMaterials;

        // destroy the subtraction created object as we are only keeping the original
        Destroy(subtraction);

        // create the particle system where the object has been separated
        Instantiate(particleSystemPrefab).transform.position = collision.GetContact(0).point;

    }
}
