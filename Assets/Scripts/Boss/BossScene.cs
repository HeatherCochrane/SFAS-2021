using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : MonoBehaviour
{
    [SerializeField]
    GameObject blockingObject;

    // Start is called before the first frame update
    void Start()
    {
        blockingObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openBossArea()
    {
        blockingObject.SetActive(false);
    }
}
