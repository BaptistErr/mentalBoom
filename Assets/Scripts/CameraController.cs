using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CameraMode
{
    FollowPlayer = 0,  // The camera statically follows the player. This is the default mode.
    Weighed      = 1,  // The camera is placed in-between two objects where each of the objects weight can be adjusted.
}

public class CameraController : MonoBehaviour
{
    public CharacterController Character;
    public Vector3 Offset;

    private CameraMode _cameraMode = CameraMode.FollowPlayer;
    public CameraMode CameraMode
    {
        get => _cameraMode;
        set => _cameraMode = value;
    }
    
    
    /*
    private void LateUpdate()
    {
        this.transform.position = CameraMode switch
        {
            CameraMode.FollowPlayer => Character.transform.position + Offset,
            
        };
    }*/
}
