using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInside : MonoBehaviour
{
    [SerializeField]
    Vector3 camPos;

    [SerializeField]
    float camSize;
    // Start is called before the first frame update
    void Start()
    {
        Player.instance.setCameraControlled(false);
        Player.instance.cam.GetComponent<Camera>().orthographicSize = camSize;
        Player.instance.cam.GetComponent<Camera>().transform.position = camPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Player.instance.setCameraControlled(true);
        Player.instance.cam.GetComponent<Camera>().orthographicSize = 4;
    }
}
