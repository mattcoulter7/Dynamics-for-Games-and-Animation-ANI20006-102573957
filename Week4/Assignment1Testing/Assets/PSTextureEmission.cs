using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSTextureEmission : MonoBehaviour
{
    void Start(){
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = ps.shape;

        shapeModule.enabled = true;
        shapeModule.textureAlphaAffectsParticles = true;
        shapeModule.textureColorAffectsParticles = false;
        shapeModule.textureClipThreshold = 0.5f;
        shapeModule.scale = transform.parent.localScale;

        MeshFilter meshFilter = GetComponentInParent<MeshFilter>();
        shapeModule.mesh = meshFilter.mesh;
    }

    // UnityMessageEvent
    void OnNewTexture(Texture2D tex)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = ps.shape;
        shapeModule.texture = tex;
    }
}
