using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTex : MonoBehaviour
{
    float scrollX = 0.01f;
    float offsetX;

    Renderer r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        offsetX = r.material.mainTextureOffset.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offsetX += Time.deltaTime * scrollX;
        r.material.mainTextureOffset = new Vector2(offsetX, 0);
        
    }
}
