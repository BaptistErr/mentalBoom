using System;
using UnityEngine;

public class MinimapRenderer : MonoBehaviour
{
    public static MinimapRenderer Active;

    private void Awake()
    {
        Active = this;
    }
    
    public RenderTexture Render(Vector2Int resolution)
    {
        GameObject go = new GameObject("Minimap Renderer");
        
        Camera camera = go.AddComponent<Camera>();
        camera.aspect = 1.0F;
        camera.orthographic = true;

        Transform trans = transform;
        Vector3 pos = trans.position;
        Vector3 sca = trans.localScale;

        camera.orthographicSize = Math.Max(sca.x, sca.z) / 2.0F;
        camera.transform.position = new Vector3(pos.x, 0.0F, pos.z);

        camera.nearClipPlane = pos.y - (sca.y / 2.0F) / 2.0F;
        camera.farClipPlane  = pos.y + (sca.y / 2.0F) / 2.0F;
        
        go.transform.forward = Vector3.down;

        RenderTexture texture = new RenderTexture(resolution.x, resolution.y, 0);

        RenderTexture temp = RenderTexture.active;
        try
        {
            RenderTexture.active = texture;
            
            camera.Render();
        }
        catch (Exception e)
        {

        }
        finally
        {
            RenderTexture.active = temp;
        }
        
        return texture;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Transform trans = transform;
        Gizmos.DrawWireCube(trans.position, trans.localScale);
    }
}