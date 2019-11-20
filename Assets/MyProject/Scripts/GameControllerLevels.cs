using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerLevels : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TaskOnClickLevel1()
    {
        SceneManager.LoadScene(2);
    }

    public void TaskOnClickHome()
    {
        SceneManager.LoadScene(0);
    }
}
