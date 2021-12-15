using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Potion : MonoBehaviour
{
    public static float MinHeal = 2.0F;
    public static float MaxHeal = 10.0F;
    
    private float HealPoints { get; } = Random.value * (MaxHeal - MinHeal) + MinHeal;

    public float Drink()
    {
        Destroy(this.gameObject);
        return HealPoints;
    }
}
