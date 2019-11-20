using UnityEngine;

public class DetectDownHead : MonoBehaviour {

    private GameController gameController;
    float timeDownHead;
    void Start()
    {
        timeDownHead = 0;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "Rocks")
        {
            timeDownHead += Time.deltaTime;
            if (timeDownHead > 5)
            {
                gameController.deathPlayer();
                timeDownHead = 0;
            }
        }
    }
}
