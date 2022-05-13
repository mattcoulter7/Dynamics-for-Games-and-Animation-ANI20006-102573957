using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public Color color = Color.black;
    public Material material;
    Texture2D _texture;
    Renderer _renderer;
    void Start(){
        _renderer = GetComponentInParent<Renderer>();

        // Ensure the texture is an instance
        material = new Material(material);
        _texture = DuplicateTexture((Texture2D)material.mainTexture); 
        material.mainTexture = _texture;

        Material[] newMaterials = new Material[_renderer.materials.Length + 1];
        for (int i = 0; i < _renderer.materials.Length; i++){
            newMaterials[i] = _renderer.materials[i];
        }
        newMaterials[_renderer.materials.Length] = material;

        _renderer.materials = newMaterials;

        BroadcastMessage("OnNewTexture",_texture);
    }
    // Start is called before the first frame update
    Texture2D DuplicateTexture(Texture2D tex)
    {
        // instantiate the dynamic texture
        Texture2D dup = new Texture2D(
            tex.width,
            tex.height,
            tex.format,
            tex.mipmapCount,
            false
        );
        Graphics.CopyTexture(tex, dup);

        return dup;
    }

    public void PaintCircle(Vector2 textureCoord, float radius = 20)
    {
        // https://stackoverflow.com/questions/30410317/how-to-draw-circle-on-texture-in-unity
        int x = (int)textureCoord.x;
        int y = (int)textureCoord.y;

        float rSquared = radius * radius;

        for (float u = x - radius; u < x + radius + 1; u++)
            for (float v = y - radius; v < y + radius + 1; v++)
                if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                    _texture.SetPixel((int)u, (int)v, color);
        
        _texture.Apply();
    }

    public void PaintPixel(Vector2 textureCoord)
    {
        int x = (int)textureCoord.x;
        int y = (int)textureCoord.y;

        // set the pixel on texture hwere contact happened
        _texture.SetPixel(x, y, color);

        // Apply all SetPixel calls
        _texture.Apply();
    }

    public Vector2? GetUVPixel(Vector3 pos,Vector3 normal){
        Vector2 pixelUV = new Vector2(-1,-1);
        RaycastHit hit = new RaycastHit();

        // cast ray to find texture coord
        Ray ray = new Ray(pos - normal, normal);
        if (!Physics.Raycast(ray, out hit))
            return null;

        Renderer rend = hit.transform.GetComponentInParent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || meshCollider == null)
            return null;

        pixelUV = hit.textureCoord;
        pixelUV.x *= _texture.width;
        pixelUV.y *= _texture.height;
        return pixelUV;
    }

}
