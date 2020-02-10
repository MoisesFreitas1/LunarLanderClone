using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class GameControllerLevels : MonoBehaviour {

    public Text bestTime1Text, bestTime2Text, bestTime3Text, bestTime4Text, bestTime5Text, bestTime6Text, bestTime7Text, bestTime8Text, bestTime9Text, bestTime10Text;
    public Text PositionLvL1, PositionLvL2, PositionLvL3, PositionLvL4, PositionLvL5, PositionLvL6, PositionLvL7, PositionLvL8, PositionLvL9, PositionLvL10;
    private float bestTime1, bestTime2, bestTime3, bestTime4, bestTime5, bestTime6, bestTime7, bestTime8, bestTime9, bestTime10;
    private readonly float EPSILON = 0.0001f;



    // Use this for initialization
    void Start () {

        bestTime1 = PlayerPrefs.GetFloat("bestTime1");
        if(System.Math.Abs(bestTime1) > EPSILON)
        {
            UpdateTimer(bestTime1, bestTime1Text);
            CallFirebase1();
        }

        bestTime2 = PlayerPrefs.GetFloat("bestTime2");
        if (System.Math.Abs(bestTime2) > EPSILON)
        {
            UpdateTimer(bestTime2, bestTime2Text);
            CallFirebase2();
        }

        bestTime3 = PlayerPrefs.GetFloat("bestTime3");
        if (System.Math.Abs(bestTime3) > EPSILON)
        {
            UpdateTimer(bestTime3, bestTime3Text);
            CallFirebase3();
        }

        bestTime4 = PlayerPrefs.GetFloat("bestTime4");
        if (System.Math.Abs(bestTime4) > EPSILON)
        {
            UpdateTimer(bestTime4, bestTime4Text);
            CallFirebase4();
        }

        bestTime5 = PlayerPrefs.GetFloat("bestTime5");
        if (System.Math.Abs(bestTime5) > EPSILON)
        {
            UpdateTimer(bestTime5, bestTime5Text);
            CallFirebase5();
        }

        bestTime6 = PlayerPrefs.GetFloat("bestTime6");
        if (System.Math.Abs(bestTime6) > EPSILON)
        {
            UpdateTimer(bestTime6, bestTime6Text);
            CallFirebase6();
        }

        bestTime7 = PlayerPrefs.GetFloat("bestTime7");
        if (System.Math.Abs(bestTime7) > EPSILON)
        {
            UpdateTimer(bestTime7, bestTime7Text);
            CallFirebase7();
        }

        bestTime8 = PlayerPrefs.GetFloat("bestTime8");
        if (System.Math.Abs(bestTime8) > EPSILON)
        {
            UpdateTimer(bestTime8, bestTime8Text);
            CallFirebase8();
        }

        bestTime9 = PlayerPrefs.GetFloat("bestTime9");
        if (System.Math.Abs(bestTime9) > EPSILON)
        {
            UpdateTimer(bestTime9, bestTime9Text);
            CallFirebase9();
        }

        bestTime10 = PlayerPrefs.GetFloat("bestTime10");
        if (System.Math.Abs(bestTime10) > EPSILON)
        {
            UpdateTimer(bestTime10, bestTime10Text);
            CallFirebase10();
        }
    }

    void CallFirebase1()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("1").OrderByChild("timeFase").ValueChanged += HandleValueChanged1;
    }

    void CallFirebase2()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("2").OrderByChild("timeFase").ValueChanged += HandleValueChanged2;
    }

    void CallFirebase3()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("3").OrderByChild("timeFase").ValueChanged += HandleValueChanged3;
    }

    void CallFirebase4()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("4").OrderByChild("timeFase").ValueChanged += HandleValueChanged4;
    }

    void CallFirebase5()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("5").OrderByChild("timeFase").ValueChanged += HandleValueChanged5;
    }

    void CallFirebase6()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("6").OrderByChild("timeFase").ValueChanged += HandleValueChanged6;
    }

    void CallFirebase7()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("7").OrderByChild("timeFase").ValueChanged += HandleValueChanged7;
    }

    void CallFirebase8()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("8").OrderByChild("timeFase").ValueChanged += HandleValueChanged8;
    }

    void CallFirebase9()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("9").OrderByChild("timeFase").ValueChanged += HandleValueChanged9;
    }

    void CallFirebase10()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://games-1721d.firebaseio.com/");
        FirebaseDatabase.DefaultInstance.RootReference.Child("LeadSpaceship").Child("10").OrderByChild("timeFase").ValueChanged += HandleValueChanged10;
    }

    void HandleValueChanged1(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL1.text = position + "º";
    }

    void HandleValueChanged2(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL2.text = position + "º";
    }

    void HandleValueChanged3(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL3.text = position + "º";
    }

    void HandleValueChanged4(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL4.text = position + "º";
    }

    void HandleValueChanged5(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL5.text = position + "º";
    }

    void HandleValueChanged6(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL6.text = position + "º";
    }

    void HandleValueChanged7(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL7.text = position + "º";
    }

    void HandleValueChanged8(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL8.text = position + "º";
    }

    void HandleValueChanged9(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL9.text = position + "º";
    }

    void HandleValueChanged10(object sender, ValueChangedEventArgs args)
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
            if (!playerScore.GetPlayerID().Equals(id))
            {
                position += 1;
            }
            else
            {
                break;
            }
        }
        PositionLvL10.text = position + "º";
    }

    void UpdateTimer(float totalSeconds, Text timeText)
    {
        string minutes = ((int)totalSeconds / 60).ToString("00");
        string seconds = (totalSeconds % 60).ToString("00");
        string miliseconds = ((totalSeconds * 100) % 100).ToString("00");
        string TimerString = string.Format("{00}:{01}:{02}", minutes, seconds, miliseconds);
        timeText.text = TimerString;
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
}
