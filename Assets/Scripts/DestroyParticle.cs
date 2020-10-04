using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private ParticleSystem fire;
    private void Start()
    {
        fire = GetComponentInChildren<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        fire.Play();
        Invoke("Destroy", 0.3f);
        return;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
