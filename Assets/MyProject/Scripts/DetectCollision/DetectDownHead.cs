using UnityEngine;

public class DetectDownHead : MonoBehaviour {

    private GameController gameController;
    public float timeDownHead;
    public GameObject Parent;
    public float rotationParent;
    
    void Start()
    {
        timeDownHead = 0;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    private void Update()
    {
        rotationParent = Parent.transform.rotation.eulerAngles.z;
        if (rotationParent > 70 && rotationParent < 290)
        {
            timeDownHead += Time.deltaTime;
        }
        else
        {
            timeDownHead = 0;
        }

        if(timeDownHead > 7)
        {
            float velocity = Parent.GetComponent<Rigidbody2D>().velocity.magnitude;
            if(velocity < Mathf.Epsilon)
            {
                gameController.deathPlayer();
                timeDownHead = 0;
            }
            else
            {
                timeDownHead = 0;
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "Rocks")
    //    {
    //        timeDownHead = 0;
    //    }
    //}
}
