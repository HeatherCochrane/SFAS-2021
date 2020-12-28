using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossScene : MonoBehaviour
{
    public enum BossNames { GOAT, SNAKE, NONE}


    [SerializeField]
    GameObject blockingObject;

    [SerializeField]
    TextMeshProUGUI battleMessage;

    [SerializeField]
    GameObject abilityDropScreen;

    [SerializeField]
    GameObject bossCharacter;

    GameObject newBoss;

    [SerializeField]
    BossNames boss;
    // Start is called before the first frame update
    void Start()
    {
        if(!Player.instance.data.hasBossBeenDefeated(boss))
        {
            newBoss = Instantiate(bossCharacter);
            newBoss.GetComponentInChildren<BossEnemy>().setScene(this);
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

        showAbilityDropScreen(false);
        Player.instance.setCameraControlled(false);
        Player.instance.cam.transform.position = new Vector3(3, -20, -10);
        Player.instance.cam.GetComponent<Camera>().orthographicSize = 7;
    }

    void startBattle()
    {
        newBoss.GetComponentInChildren<BossEnemy>().startBattle();
        Player.instance.setInput(true);
    }

    public void openBossArea()
    {
        blockingObject.SetActive(false);
        showAbilityDropScreen(true);
    }

    private void OnDisable()
    {
        Player.instance.cam.GetComponent<Camera>().orthographicSize = 5;
        Player.instance.setCameraControlled(true);
    }

    public void showAbilityDropScreen(bool set)
    {
        abilityDropScreen.SetActive(set);
    }
}
