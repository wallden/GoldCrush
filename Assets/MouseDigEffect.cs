using UnityEngine;
using System.Collections;

public class MouseDigEffect : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    void Start ()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _particleSystem.Play();
        }
    }
}