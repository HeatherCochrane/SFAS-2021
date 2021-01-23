using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notifications : MonoBehaviour
{
    [SerializeField]
    GameObject questStarted;

    [SerializeField]
    GameObject questCompleted;

    GameObject notification;

    [SerializeField]
    Transform notificationParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnQuestStarted(string quest)
    {
        notification = Instantiate(questStarted);
        notification.transform.SetParent(notificationParent);

        notification.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest + " started!";

        Invoke("destroyNotification", 3);
    }

    public void spawnQuestFinished(string quest)
    {
        notification = Instantiate(questStarted);
        notification.transform.SetParent(notificationParent);

        notification.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest + " completed!";

        Invoke("destroyNotification", 3);
    }

    void destroyNotification()
    {
        Destroy(notification);
    }

}
