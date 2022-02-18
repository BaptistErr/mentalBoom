using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum CameraMode
{
    FollowPlayer,  // The camera statically follows the player. This is the default mode.
    Weighed     ,  // The camera is placed in-between two objects where each of the objects weight can be adjusted.
}

[Serializable] public class Weight
{
    public Transform Transform;
    public float Value;

    public Weight(Transform transform, float value)
    {
        Transform = transform;
        Value = value;
    }
}

public class CameraController : MonoBehaviour
{
    public CharacterController Character;
    public Vector3 Offset;

    public Vector3 BackwardAxis = -(Quaternion.Euler(30.0F, -45.0F, 0.0F) * Vector3.forward);
    

    [SerializeField] private CameraMode _cameraMode = CameraMode.FollowPlayer;

    [SerializeField]
    private BossController boss;

    private Vector3 ShakeOffset = Vector3.zero;
    
    public static CameraController Instance { get; private set; }

    public CameraMode CameraMode
    {
        get => _cameraMode;
        set => _translationMethod = (_cameraMode = value) switch
        {
            CameraMode.FollowPlayer => FollowPlayerPosition,
            CameraMode.Weighed => WeightedPosition,
            _ => throw new Exception("QUI AS FOUTU UNE VALEUR INVALIDE???")
        };
    }

    [SerializeField] private List<Weight> _weights = new List<Weight>();

    public List<Weight> Weights
    {
        get => _weights;
        set => _weights = value;
    }

    public float MinSize = 10.0F;
    public float MaxSize = 14.0F;
    
    public float FollowPlayerSize = 10.0F;


    private Action _translationMethod;
    private Camera _camera;
    
    private void Awake()
    {
        CameraMode = _cameraMode;
        _camera = Camera.main;
        _camera!.targetTexture = RenderTexture.active;

        Instance = this;
    }
    
    private void LateUpdate()
    {
        _translationMethod();
    }

    private void FollowPlayerPosition()
    {
        transform.position = Character.transform.position + Offset + ShakeOffset;
        _camera.orthographicSize = FollowPlayerSize;
    }

    private void WeightedPosition()
    {
        if (Weights.Count > 1)
        {
            Vector3 min = Vector3.positiveInfinity;
            Vector3 max = Vector3.negativeInfinity;
            
            Vector3 average = Vector3.zero;
            float totWeight = 0.0F;
            foreach (Weight weight in Weights)
            {
                if (weight == null || weight.Transform == null) continue; 
                
                Vector3 pos = weight.Transform.position;
                
                min = Vector3.Min(min, pos);
                max = Vector3.Max(max, pos);

                totWeight += weight.Value;
                average += pos * weight.Value;
            }

            average /= totWeight;

            if (totWeight == 0) return;

            transform.position = new Vector3(average.x, 0.0F, average.z) + ShakeOffset;

            Vector3 d = max - min;

            float size = Mathf.Clamp(new Vector2(d.x, d.z).magnitude, MinSize, MaxSize);
            
            _camera.orthographicSize = size;
        }
        else
        {
            FollowPlayerPosition();
        }
    }

    private void OnValidate()
    {
        CameraMode = _cameraMode;
    }

    public void BossZoom()
    {
        StartCoroutine(Transition());
        transform.LookAt(boss.transform.position);
    }

    IEnumerator Transition()
    {
        transform.LookAt(boss.transform.position);
        yield return new WaitForSeconds(2.0F);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        ShakeOffset = Vector3.zero;

        float elapsed = 0.0F;

        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1.0F, 1.0F) * magnitude;
            float y = UnityEngine.Random.Range(-1.0F, 1.0F) * magnitude;

            ShakeOffset = transform.right * x + transform.up  * y;
            
            elapsed += Time.deltaTime;

            yield return null;
        }

        ShakeOffset = Vector3.zero;
    }
}
