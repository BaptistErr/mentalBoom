using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CharacterController Character;
    public Vector3 Offset;
    
    private void LateUpdate()
    {
        this.transform.position = Character.transform.position + Offset;
    }
}
