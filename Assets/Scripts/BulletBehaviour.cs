using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private CharacterController target;

    [SerializeField] private float _damage;
    /// <summary> Amount of damages dealt by each projectile </summary>
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        target = FindObjectOfType<CharacterController>();
        transform.LookAt(target.transform.position);
        transform.rotation *= Quaternion.Euler(90, 0, 0);
        GetComponent<Rigidbody>().AddForce(transform.up * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            target.GetDamage(Damage);
            Destroy(gameObject, 0);
        }
        if (other.gameObject.CompareTag("BulletProof"))
        {
            Destroy(gameObject, 0);
        }
    }
}
