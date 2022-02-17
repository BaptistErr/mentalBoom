using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public Camera RenderCamera;
    public RectTransform PlayerArrow;
    public RectTransform BossIcon;
    public Vector3 BossPosition;
    
    public static Minimap Instance { get; private set; }
    
    private void Start()
    {
        Instance = this;
        
        RenderMinimap();
    }

    private void Update()
    {
        // set minimap position
        Transform playerTransform = CharacterController.Instance.transform;
        Vector3 minimapPosition = playerTransform.position;
        minimapPosition.y = 100;
        RenderCamera.transform.position = minimapPosition;

        // set player arrow rotation
        //Vector3 euler = PlayerArrow.eulerAngles;
        //euler.z = -playerTransform.eulerAngles.y - 45.0F;
        //PlayerArrow.eulerAngles = euler;

        //// set boss icon position
        //Vector3 bossPos = BossPosition;
        //bossPos.z *= -1.0F;
        //Vector3 bossCamPos = RenderCamera.WorldToScreenPoint(bossPos, Camera.MonoOrStereoscopicEye.Mono);

        //RectTransform minimapRectTransform = this.GetComponent<RectTransform>();


        //Rect rect = minimapRectTransform.rect;

        //float halfWidth = rect.width / 2.0F;
        //float halfHeight = rect.height / 2.0F;

        //float bossX = Mathf.Clamp(bossCamPos.x, -halfWidth, halfWidth);
        //float bossY = Mathf.Clamp(bossCamPos.y, -halfHeight, halfHeight);
        //BossIcon.anchoredPosition = new Vector2(bossX, bossY);
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
