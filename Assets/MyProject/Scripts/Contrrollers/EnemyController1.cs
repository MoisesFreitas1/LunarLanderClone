using UnityEngine;
 
public class EnemyController1 : MonoBehaviour
{

    GameObject target;
    public float maxSpeed;
    GameObject mainCamera;
    public GameObject destroyExplosion;
    private GameController gameController;
    private PlayerController playerController;

    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player")){
            target = GameObject.FindGameObjectWithTag("Player");
        }

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if(playerController.win == false)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                Vector3 targetDir = target.transform.position - transform.position;
                float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90f);

                var locVel = transform.InverseTransformDirection(GetComponent<Rigidbody2D>().velocity);
                locVel.y = maxSpeed;
                locVel.x = 0;
                GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(locVel);
                GetComponent<Rigidbody2D>().angularVelocity = 0f;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerController.win == false)
        {
            if (collision.transform.tag == "Player")
            {
                Instantiate(destroyExplosion, collision.transform.position, Quaternion.identity);
                gameController.deathPlayer();
                Destroy(collision.gameObject);
            }
        }
    }
}