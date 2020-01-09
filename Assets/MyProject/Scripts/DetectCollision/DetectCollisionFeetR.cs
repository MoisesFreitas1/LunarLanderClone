using UnityEngine;

public class DetectCollisionFeetR : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<PlayerController>().feetR = true;
        transform.parent.GetComponent<PlayerController>().CollisionDetected();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent.GetComponent<PlayerController>().feetR = true;
        transform.parent.GetComponent<PlayerController>().CollisionDetected();
        {
            transform.parent.GetComponent<PlayerController>().endGoalR = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        transform.parent.GetComponent<PlayerController>().feetR = true;
        transform.parent.GetComponent<PlayerController>().CollisionDetected();
        if (collision.tag.Equals("End"))
        {
            transform.parent.GetComponent<PlayerController>().endGoalR = true;
        }
    }
}
