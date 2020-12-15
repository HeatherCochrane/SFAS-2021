using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    ParticleSystem p;
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<ParticleSystem>();
        Invoke("destroyParticles", 1.5f);
    }


    void destroyParticles()
    {
        Destroy(gameObject);
    }
}
