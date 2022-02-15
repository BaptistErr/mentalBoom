using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Vector2Int RenderResolution;

    public RenderTexture Texture;

    [ContextMenu("Render Minimap")]
    public void RenderMinimap()
    {
        GameObject go = new GameObject("Minimap Renderer");
        
        Camera camera = go.AddComponent<Camera>();
        camera.aspect = 1.0F;
        camera.orthographic = true;

        Transform trans = transform;
        Vector3 pos = trans.position;
        Vector3 sca = trans.localScale;

        camera.orthographicSize = Math.Max(sca.x, sca.z);
        camera.transform.position = new Vector3(pos.x, 0.0F, pos.z);

        camera.nearClipPlane = pos.y - (sca.y / 2.0F);
        camera.farClipPlane  = pos.y + (sca.y / 2.0F);
        
        go.transform.forward = Vector3.down;

        RenderTexture texture = Texture = new RenderTexture(RenderResolution.x, RenderResolution.y, 0);

        RenderTexture temp = RenderTexture.active;
        try
        {
            //RenderTexture.active = texture;
            //camera.
            //camera.Render();
        }
        catch (Exception e)
        {

        }
        finally
        {
            RenderTexture.active = temp;
        }

        DestroyImmediate(go);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Transform trans = transform;
        Gizmos.DrawWireCube(trans.position, trans.localScale*2.0F);
    }
}
