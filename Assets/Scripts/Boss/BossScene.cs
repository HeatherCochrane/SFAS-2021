using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossScene : MonoBehaviour
{
    [SerializeField]
    GameObject blockingObject;

    [SerializeField]
    BossEnemy boss;

    [SerializeField]
    TextMeshProUGUI battleMessage;

    // Start is called before the first frame update
    void Start()
    {
        blockingObject.SetActive(true);
        Player.instance.setCameraControlled(false);
        Player.instance.cam.transform.position = new Vector3(3, -20, -10);
        Player.instance.cam.GetComponent<Camera>().orthographicSize = 7;

        Player.instance.setInput(false);

        Invoke("startBattle", 2);
    }

    void startBattle()
    {
        boss.startBattle();
        Player.instance.setInput(true);
    }

    public void openBossArea()
    {
        blockingObject.SetActive(false);
    }

    private void OnDisable()
    {
        Player.instance.cam.GetComponent<Camera>().orthographicSize = 4;
        Player.instance.setCameraControlled(true);
    }
}
