using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public string scene = "<Insert scene name>";
    public float duration = 1.0f;
    public Color color = Color.black;
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    public void PerformTransition()
    {
        if(gameController != null)
        {
            if (gameController.GetPause())
            {
                gameController.SetPause();
            }
        }
        Transition.LoadLevel(scene, duration, color);
    }
}
