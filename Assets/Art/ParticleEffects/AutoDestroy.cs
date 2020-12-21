using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    float time = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", time);
    }


    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
