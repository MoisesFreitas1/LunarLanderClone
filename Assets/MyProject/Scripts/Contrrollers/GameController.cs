using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class GameController : MonoBehaviour {
    public bool death;
    public bool win;
    public bool pause;
    private bool winOneTime;
    public GameObject restartButton;
    public GameObject restartButtonText;
    public GameObject missionFailedText;
    public GameObject winPopup;
    public GameObject pauseButton;
    public GameObject pauseMenu;
    private PlayerController playerController;
    public Text timerText;
    public Text yourTime;
    public Text YourPositionText;
    public Text BestTimeAmountText;
    private float totalTime;
    public string faseName;

    DatabaseReference reference;


    void Start () {
        // Firebase
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship");
        //

        totalTime = 0;
        death = false;
        win = false;
        winOneTime = false;
        restartButton.GetComponent<Image>().enabled = false;
        restartButton.GetComponent<Button>().enabled = false;
        restartButtonText.GetComponent<Text>().enabled = false;
        missionFailedText.GetComponent<Text>().enabled = false;
        winPopup.SetActive(false);
        pauseMenu.SetActive(false);

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
    }
	
	void Update () {
        totalTime += Time.deltaTime;
        UpdateTimer(totalTime);

        if (death)
        {
            restartButton.GetComponent<Image>().enabled = true;
            restartButton.GetComponent<Button>().enabled = true;
            restartButtonText.GetComponent<Text>().enabled = true;
            missionFailedText.GetComponent<Text>().enabled = true;
        }

        if (win && !winOneTime)
        {
            winPopup.SetActive(true);
            playerController.Win();

            if (PlayerPrefs.GetInt("firstTime" + faseName) == 0)
            {
                PlayerPrefs.SetInt("firstTime" + faseName, 1);
                PlayerPrefs.SetFloat("bestTime" + faseName, 1000000f);
            }

            if (PlayerPrefs.GetFloat("bestTime" + faseName) > totalTime)
            {
                PlayerPrefs.SetFloat("bestTime" + faseName, totalTime);
                string serial = GetDeviceID();
                WriteBestTime(serial, totalTime);
            }

            CallFirebase();

            float bestTime = PlayerPrefs.GetFloat("bestTime" + faseName);
            string minutes = ((int)bestTime / 60).ToString("00");
            string seconds = (bestTime % 60).ToString("00");
            string miliseconds = ((bestTime * 100) % 100).ToString("00");
            string TimerString = string.Format("{00}:{01}:{02}", minutes, seconds, miliseconds);
            yourTime.text = TimerString;
            winOneTime = true;
        }

        if (!death && !win && pause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        if (!death && !win && !pause)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    // Write in Database
    private void WriteBestTime(string playerID, float timeFase)
    {
        PlayerScore playerScore = new PlayerScore();
        playerScore.SetPlayerID(playerID);
        playerScore.SetTimeFase(timeFase);
        string json = JsonUtility.ToJson(playerScore);
        reference.Child(faseName).Child(playerID).SetRawJsonValueAsync(json);
    }

    // Load Database
    void CallFirebase()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child(faseName).OrderByChild("timeFase").ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        string id = GetDeviceID();
        int position = 1;
        foreach (DataSnapshot childSnapshot in args.Snapshot.Children)
        {
            PlayerScore playerScore = JsonUtility.FromJson<PlayerScore>(childSnapshot.GetRawJsonValue());

            if(position == 1)
            {
                BestTimeAmountText.text = UpdateTime(playerScore.GetTimeFase());
            }

            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        YourPositionText.text = position + "º";
    }

    // Get Android DeviceID
    public static string GetDeviceID()
    {
        // Get Android ID
        AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
        AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");

        string android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");

        // Get bytes of Android ID
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(android_id);

        // Encrypt bytes with md5
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        string device_id = hashString.PadLeft(32, '0');

        return device_id;
    }

    public void SetPause()
    {
        if(pause == false)
        {
            pause = true;
        }
        else
        {
            pause = false;
        }
    }

    public bool GetPause()
    {
        return pause;
    }

    void UpdateTimer(float totalSeconds)
    {
        if (!death && !win)
        {
            string minutes = ((int)totalSeconds / 60).ToString("00");
            string seconds = (totalSeconds % 60).ToString("00");
            string miliseconds = ((totalSeconds * 100) % 100).ToString("00");
            string TimerString = string.Format("{00}:{01}:{02}", minutes, seconds, miliseconds);
            timerText.text = TimerString;
        }
    }

    string UpdateTime(float totalSeconds)
    {
        string minutes = ((int)totalSeconds / 60).ToString("00");
        string seconds = (totalSeconds % 60).ToString("00");
        string miliseconds = ((totalSeconds * 100) % 100).ToString("00");
        string TimerString = string.Format("{00}:{01}:{02}", minutes, seconds, miliseconds);
        return TimerString;
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
