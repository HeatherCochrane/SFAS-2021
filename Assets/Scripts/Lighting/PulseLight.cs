using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PulseLight : MonoBehaviour
{
    Light2D lightObj;

    [SerializeField]
    float min = 0;

    [SerializeField]
    float max = 0;

    float current = 0;

    bool brighten = false;
    IEnumerator pulsing()
    {
        while(true)
        {
            if(!brighten)
            {
                current -= 0.005f;
            }
            else
            {
                current += 0.005f;
            }

            if(current >= max)
            {
                brighten = false;
            }
            else if(current <= min)
            {
                brighten = true;
            }

            lightObj.intensity = current;

            yield return new WaitForFixedUpdate();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lightObj = GetComponent<Light2D>();
        StartCoroutine("pulsing");
    }

}
