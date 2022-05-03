using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public int paintMaterialIndex = 0;
    public Color color = Color.black;

    void Start(){
        // Ensure the texture is an instance
        Renderer renderer = GetComponent<Renderer>();
        Texture2D texInstance = DuplicateTexture(GetPaintTexture());
        renderer.materials[paintMaterialIndex].mainTexture = texInstance;

        BroadcastMessage("OnNewTexture",texInstance);
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

    public Texture2D GetPaintTexture()
    {
        return GetComponent<Renderer>().materials[paintMaterialIndex].mainTexture as Texture2D;
    }

    public void PaintCircle(Vector2 textureCoord, float radius = 20)
    {
        // https://stackoverflow.com/questions/30410317/how-to-draw-circle-on-texture-in-unity
        int x = (int)textureCoord.x;
        int y = (int)textureCoord.y;

        Texture2D tex = GetPaintTexture();
        float rSquared = radius * radius;

        for (float u = x - radius; u < x + radius + 1; u++)
            for (float v = y - radius; v < y + radius + 1; v++)
                if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                    tex.SetPixel((int)u, (int)v, color);
        
        tex.Apply();
    }

    public void PaintPixel(Vector2 textureCoord)
    {
        int x = (int)textureCoord.x;
        int y = (int)textureCoord.y;
        
        Texture2D tex = GetPaintTexture();

        // set the pixel on texture hwere contact happened
        tex.SetPixel(x, y, color);

        // Apply all SetPixel calls
        tex.Apply();
    }

    public Vector2? GetUVPixel(Vector3 pos,Vector3 normal){
        Vector2 pixelUV = new Vector2(-1,-1);
        RaycastHit hit = new RaycastHit();

        // cast ray to find texture coord
        Ray ray = new Ray(pos - normal, normal);
        if (!Physics.Raycast(ray, out hit))
            return null;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return null;

        pixelUV = hit.textureCoord;
        Texture2D tex = GetPaintTexture();
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        return pixelUV;
    }

}
