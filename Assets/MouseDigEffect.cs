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
        var mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseInWorld.x, mouseInWorld.y, transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            _particleSystem.Play();
        }
    }

    public void Dig()
    {
        _particleSystem.Play();
    }
}