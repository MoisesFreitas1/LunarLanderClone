using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactFire : MonoBehaviour
{

    public GameObject destroyExplosion;

    private GameController gameController;
    private Collider2D[] colliders;
    private float time;


    void Start()
    {
        time = 0;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        if (colliders.Length == 0)
        {
            Destroy(gameObject);
        }

        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].transform.tag == "Player" || colliders[i].transform.tag == "Start" || colliders[i].transform.tag == "End")
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        time = time + Time.deltaTime;
        if (collision.transform.tag == "Player")
        {
            if (time > 1)
            {
                Instantiate(destroyExplosion, collision.transform.position, Quaternion.identity);
                gameController.deathPlayer();
                Destroy(collision.gameObject);
            }
        }
    }
}
