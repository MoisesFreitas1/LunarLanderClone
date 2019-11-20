using UnityEngine;

public class DetectCollisionFeetL : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<PlayerController>().feetL = true;
        transform.parent.GetComponent<PlayerController>().CollisionDetected();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent.GetComponent<PlayerController>().feetL = true;
        transform.parent.GetComponent<PlayerController>().CollisionDetected();
        if (collision.tag.Equals("End"))
        {
            transform.parent.GetComponent<PlayerController>().endGoalL = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        transform.parent.GetComponent<PlayerController>().feetL = true;
        transform.parent.GetComponent<PlayerController>().CollisionDetected();
        if (collision.tag.Equals("End"))
        {
            transform.parent.GetComponent<PlayerController>().endGoalL = true;
        }
    }
}
