using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceObject : MonoBehaviour
{
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (!source.loop)
        {
            Invoke("destroyObject", source.clip.length);
        }
    }

    void destroyObject()
    {
        Destroy(this.gameObject);
    }

}
