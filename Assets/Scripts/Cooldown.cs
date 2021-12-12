using System;
using UnityEngine;

[Serializable] public struct Cooldown
{
    [SerializeField] private float _duration;
    public float Duration
    {
        get => _duration;
        set => _duration = value;
    }

    public float Time { get; private set; }

    public bool Elasped => Time <= 0.0F;

    public void Elapse(float time)
    {
        Time -= time;
    }

    public void Reset()
    {
        Time = _duration;
    }
}
