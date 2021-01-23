using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossScene : MonoBehaviour
{
    public enum BossNames { GOAT, SNAKE, FINALBOSS, NONE}


    [SerializeField]
    GameObject blockingObject;

    [SerializeField]
    TextMeshProUGUI battleMessage;

    GameObject bossCharacter;

    GameObject newBoss;

    [SerializeField]
    BossNames boss;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void startScene()
    {
        if (!Player.instance.data.hasBossBeenDefeated(boss))
        {
            newBoss = Instantiate(bossCharacter);
            newBoss.GetComponentInChildren<BossEnemy>().setScene(this);
            newBoss.transform.position = new Vector3(4, -1);
            blockingObject.SetActive(true);
            Player.instance.setInput(false);
            battleMessage.gameObject.SetActive(true);


            Invoke("startBattle", 2);
        }
        else
        {
            blockingObject.SetActive(false);
            battleMessage.gameObject.SetActive(false);
        }

        Player.instance.setCameraControlled(false);
        Player.instance.cam.transform.position = new Vector3(3, 3, -10);
        Player.instance.cam.GetComponent<Camera>().orthographicSize = 10;
    }

    void startBattle()
    {
        newBoss.GetComponentInChildren<BossEnemy>().startBattle();
        Player.instance.setInput(true);
    }

    public void openBossArea()
    {
        blockingObject.SetActive(false);
    }

    private void OnDisable()
    {
        Player.instance.cam.GetComponent<Camera>().orthographicSize = 5;
        Player.instance.setCameraControlled(true);
    }

    public void setBossSpawn(GameObject b)
    {
        bossCharacter = b;
    }
}
