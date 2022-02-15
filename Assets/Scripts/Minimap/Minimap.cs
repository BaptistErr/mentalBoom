using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public Camera RenderCamera;
    
    private void Start()
    {
        RenderMinimap();
    }

    private void Update()
    {
        RenderCamera.transform.position = CharacterController.Instance.transform.position;
    }

    [ContextMenu("Render Minimap")]
    public void RenderMinimap()
    {
        /*Sprite sprite = Sprite.Create(
            texture: MinimapRenderer.Active.Render(new Vector2Int(2048, 2048)),
            new Rect(default, )
                
                
                );*/
    }
}
