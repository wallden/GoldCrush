using UnityEngine;
using System.Collections;

public class ParticlePlay : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    public void Play()
    {
        ParticleSystem.Play();
    }
}