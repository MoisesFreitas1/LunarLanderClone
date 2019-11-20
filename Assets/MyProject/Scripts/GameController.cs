using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public bool death;
    public bool win;
    public GameObject restartButton;
    public GameObject restartButtonText;
    public GameObject missionFailedText;
    public GameObject winPopup;
    private PlayerController playerController;

    void Start () {
        death = false;
        win = false;
        restartButton.GetComponent<Image>().enabled = false;
        restartButton.GetComponent<Button>().enabled = false;
        restartButtonText.GetComponent<Text>().enabled = false;
        missionFailedText.GetComponent<Text>().enabled = false;
        winPopup.SetActive(false);

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
    }
	
	void Update () {
        if (death)
        {
            restartButton.GetComponent<Image>().enabled = true;
            restartButton.GetComponent<Button>().enabled = true;
            restartButtonText.GetComponent<Text>().enabled = true;
            missionFailedText.GetComponent<Text>().enabled = true;
        }

        if (win)
        {
            winPopup.SetActive(true);
            playerController.Win();
        }

    }

    public void deathPlayer()
    {
        death = true;
    }

    public void winPlayer()
    {
        win = true;
    }

    public void TaskOnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
