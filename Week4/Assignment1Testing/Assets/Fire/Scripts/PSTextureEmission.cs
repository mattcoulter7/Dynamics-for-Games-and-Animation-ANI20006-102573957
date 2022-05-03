using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTextureEmission : MonoBehaviour
{
    public MeshCollider meshCollider;
    void Start(){
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = ps.shape;

        shapeModule.enabled = true;
        shapeModule.textureAlphaAffectsParticles = true;
        shapeModule.textureColorAffectsParticles = false;
        shapeModule.textureClipThreshold = 0.5f;

        shapeModule.scale = meshCollider.transform.localScale;
        transform.position = meshCollider.transform.position;
        transform.rotation = meshCollider.transform.rotation;
        shapeModule.mesh = meshCollider.sharedMesh;
    }

    // UnityMessageEvent
    void OnNewTexture(Texture2D tex)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = ps.shape;
        shapeModule.texture = tex;
    }
}
