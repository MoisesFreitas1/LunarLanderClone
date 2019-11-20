using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionThrustersR : MonoBehaviour {

    public float maxVelocity;
    private PlayerController playerController;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        float velocity = playerController.GetVelocity();
        if (velocity > maxVelocity)
        {
            playerController.SetBrokeTR(true);
        }
    }
}