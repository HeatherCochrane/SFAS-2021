using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObliviousEnemy : MonoBehaviour
{

    int dir = 1;
    [SerializeField]
    float speed = 0;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(speed * dir * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            dir *= -1;
        }
    }


}
