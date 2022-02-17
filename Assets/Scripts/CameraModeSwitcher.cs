using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    public bool SwitchToWeighed = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            CameraController camInstance = CameraController.Instance;

            camInstance.Weights[1].Transform = BossController.Instance.transform;
            
            camInstance.CameraMode = SwitchToWeighed ? CameraMode.Weighed : CameraMode.FollowPlayer;
        }
    }
}
