using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private GameObject mainCamera;
    private Vector3 positionCamera;
    private Vector3 positionPlayer;
    private Vector3 offset;
    public GameObject RThrusters;
    public GameObject LThrusters;
    public GameObject RfireThrusters;
    public GameObject LfireThrusters;
    public GameObject RsparkThrusters;
    public GameObject LsparkThrusters;
    private GameObject FireL;
    private GameObject FireR;
    private GameObject SparkL;
    private GameObject SparkR;
    public GameObject RFeet;
    public GameObject LFeet;
    public GameObject SmokeL;
    public GameObject SmokeR;
    public bool brokeEngineL;
    public bool brokeEngineR;
    public float timeBrokeL;
    public float timeBrokeR;
    public bool engineL;
    public bool engineR;
    public bool feetR;
    public bool feetL;
    public bool endGoalR;
    public bool endGoalL;
    public float maxForce;
    public bool win;
    private float Force;
    private float cameraSize = 12.5f;
    private float EPSILON = 0.1f;
    public float forcePerCent;
    public GameObject hitExplosion;
    public GameObject destroyExplosion;
    private float velocity;
    public float maxVelocityHit;
    public float maxVelocityDestroy;
    private GameController gameController;
    public AudioClip[] audioClips;

    //Filtro de Kalman
    private float pnX, pnY, pnV;
    private float qnX, qnY, qnV;
    private float rnX, rnY, rnV;
    private float knX, knY, knV;
    private float[] Xk, Yk, Vk;
    private float erroX, erroY, erroV;


    void Start () {
        feetL = false;
        feetR = false;
        endGoalR = false;
        endGoalL = false;
        brokeEngineR = false;
        brokeEngineL = false;
        win = false;
        mainCamera = GameObject.FindWithTag("MainCamera");
        positionCamera = mainCamera.GetComponent<Transform>().position;
        positionPlayer = GetComponent<Transform>().position;
        offset = positionCamera - positionPlayer;
        Input.multiTouchEnabled = true;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }


        //Filtro de Kalman
        //Inicializacao
        //X
        pnX = 1;
        qnX = 1;
        rnX = 1;
        Xk = new float[2];
        Xk[0] = positionPlayer.x;
        pnX = pnX + qnX;
        knX = pnX / (pnX + rnX);
        pnX = (1 - knX) * pnX;
        rnX = 1 + (rnX / (rnX + knX));

        //Y
        pnY = 1;
        qnY = 1;
        rnY = 1;
        Yk = new float[2];
        Yk[0] = positionPlayer.y;
        pnY = pnY + qnY;
        knY = pnY / (pnY + rnY);
        pnY = (1 - knY) * pnY;
        rnY = 1 + (rnY / (rnY + knY));

        //V
        pnV = 1;
        qnV = 1;
        rnV = 1;
        Vk = new float[2];
        Vk[0] = GetComponent<Rigidbody2D>().velocity.magnitude;
        pnV = pnV + qnV;
        knV = pnV / (pnV + rnV);
        pnV = (1 - knV) * pnV;
        rnV = 1 + (rnV / (rnV + knV));
    }

    void Update()
    {

        velocity = GetComponent<Rigidbody2D>().velocity.magnitude;

        //Filtro de Kalman
        //X
        Xk[1] = Xk[0];
        pnX = pnX + qnX;
        knX = pnX / (pnX + rnX);
        erroX = GetComponent<Transform>().position.x - Xk[1];
        Xk[1] = Xk[1] + knX * erroX;
        pnX = (1 - knX) * pnX;
        rnX = 1 + (rnX / (rnX + knX));
        Xk[0] = Xk[1];

        //Y
        Yk[1] = Yk[0];
        pnY = pnY + qnY;
        knY = pnY / (pnY + rnY);
        erroY = GetComponent<Transform>().position.y - Yk[1];
        Yk[1] = Yk[1] + knY * erroY;
        pnY = (1 - knY) * pnY;
        rnY = 1 + (rnY / (rnY + knY));
        Yk[0] = Yk[1];

        //V
        Vk[1] = Vk[0];
        pnV = pnV + qnV;
        knV = pnV / (pnV + rnV);
        erroV = velocity - Vk[1];
        Vk[1] = Vk[1] + knV * erroV;
        pnV = (1 - knV) * pnV;
        rnV = 1 + (rnV / (rnV + knV));
        Vk[0] = Vk[1];

        offset = new Vector3(Xk[0], Yk[0], mainCamera.transform.position.z) - new Vector3(Xk[1], Yk[1], GetComponent<Transform>().position.z);
        mainCamera.transform.position = new Vector3(Xk[1], Yk[1], GetComponent<Transform>().position.z) + offset;

        if (Vk[1] < 0)
        {
            Force = forcePerCent * maxForce;
        }
        else
        {
            if (Force < maxForce)
            {
                Force = Force + maxForce / 10;
            }
            else
            {
                Force = maxForce;
            }
        }
        if (Vk[1] > 0)
        {
            mainCamera.GetComponent<Camera>().orthographicSize = cameraSize + 0.45f * Vk[1];
            if (mainCamera.GetComponent<Camera>().orthographicSize > 30)
            {
                mainCamera.GetComponent<Camera>().orthographicSize = 30;
            }
        }

        if(win == false) {

        Touch[] myTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (myTouches[i].position.x < Screen.width / 2)
            {
                if (!brokeEngineL)
                {
                    GetComponent<Rigidbody2D>().AddForceAtPosition(transform.up * Force, LThrusters.transform.position);
                    if (myTouches[i].phase == TouchPhase.Began)
                    {
                        Quaternion spawnRotation = Quaternion.identity;
                        FireL = Instantiate(LfireThrusters, LThrusters.transform.position, LThrusters.transform.rotation) as GameObject;
                        FireL.transform.parent = LThrusters.transform;
                        LThrusters.GetComponent<AudioSource>().volume = 1;
                        LThrusters.GetComponent<AudioSource>().Play(0);
                        engineL = true;
                    }
                    if (myTouches[i].phase == TouchPhase.Ended)
                    {
                        Destroy(FireL);
                        engineL = false;
                        StartCoroutine(VolumeFadeL(LThrusters.GetComponent<AudioSource>(), 0f, 0.3f));
                    }
                }
                else
                {
                    if (myTouches[i].phase == TouchPhase.Stationary)
                    {
                        Destroy(FireL);
                        float onoffL = Random.Range(0, 10);
                        if (Mathf.Round(onoffL) % 2 <= EPSILON)
                        {
                            timeBrokeL += Time.deltaTime;
                            if (timeBrokeL > 1)
                            {
                                timeBrokeL = 0;
                                onoffL = Random.Range(0, 10);
                            }
                            if (Mathf.Round(timeBrokeL) % 2f >= 0f)
                            {
                                GetComponent<Rigidbody2D>().AddForceAtPosition(transform.up * Random.Range(1.5f, 1.8f) * Force, LThrusters.transform.position);
                                SparkL = Instantiate(LsparkThrusters, LThrusters.transform.position, LThrusters.transform.rotation) as GameObject;
                                SparkL.transform.parent = LThrusters.transform;
                                Quaternion spawnRotation = Quaternion.identity;
                                FireL = Instantiate(LfireThrusters, LThrusters.transform.position, LThrusters.transform.rotation) as GameObject; ;
                                FireL.transform.parent = LThrusters.transform;
                                LThrusters.GetComponent<AudioSource>().volume = 1;
                                LThrusters.GetComponent<AudioSource>().Play(0);
                                engineL = true;
                            }
                            else
                            {
                              Destroy(FireL);                                 
                              engineL = false;
                              StartCoroutine(VolumeFadeL(LThrusters.GetComponent<AudioSource>(), 0f, 0.3f));
                            }
                        }
                    }

                    if (myTouches[i].phase == TouchPhase.Ended)
                    {
                        Destroy(FireL);
                        engineL = false;
                        StartCoroutine(VolumeFadeL(LThrusters.GetComponent<AudioSource>(), 0f, 0.3f));
                    }

                }
            }
            if (myTouches[i].position.x > Screen.width / 2)
            {
                if (!brokeEngineR)
                {
                    GetComponent<Rigidbody2D>().AddForceAtPosition(transform.up * Force, RThrusters.transform.position);
                    if (myTouches[i].phase == TouchPhase.Began)
                    {
                        Quaternion spawnRotation = Quaternion.identity;
                        FireR = Instantiate(RfireThrusters, RThrusters.transform.position, RThrusters.transform.rotation) as GameObject; ;
                        FireR.transform.parent = RThrusters.transform;
                        RThrusters.GetComponent<AudioSource>().volume = 1;
                        RThrusters.GetComponent<AudioSource>().Play(0);
                        engineR = true;
                    }
                    if (myTouches[i].phase == TouchPhase.Ended)
                    {
                        Destroy(FireR);
                        engineR = false;
                        StartCoroutine(VolumeFadeR(RThrusters.GetComponent<AudioSource>(), 0f, 0.3f));
                    }
                }
                else
                {
                    if (myTouches[i].phase == TouchPhase.Stationary)
                    {
                        Destroy(FireR);
                        float onoffR = Random.Range(0, 10);
                        if (Mathf.Round(onoffR) % 2 <= EPSILON)
                        {
                            timeBrokeR += Time.deltaTime;
                            if (timeBrokeR > 1)
                            {
                                timeBrokeR = 0;
                                onoffR = Random.Range(0, 10);
                            }
                            if (Mathf.Round(timeBrokeR) % 2f >= 0f)
                            {
                                GetComponent<Rigidbody2D>().AddForceAtPosition(transform.up * Random.Range(1.5f, 1.8f) * Force, RThrusters.transform.position);
                                SparkR = Instantiate(RsparkThrusters, RThrusters.transform.position, RThrusters.transform.rotation) as GameObject;
                                SparkR.transform.parent = RThrusters.transform;
                                Quaternion spawnRotation = Quaternion.identity;
                                FireR = Instantiate(RfireThrusters, RThrusters.transform.position, RThrusters.transform.rotation) as GameObject; ;
                                FireR.transform.parent = RThrusters.transform;
                                RThrusters.GetComponent<AudioSource>().volume = 1;
                                RThrusters.GetComponent<AudioSource>().Play(0);
                                engineR = true;
                            }
                            else
                            {
                                Destroy(FireR);
                                engineR = false;
                                StartCoroutine(VolumeFadeR(RThrusters.GetComponent<AudioSource>(), 0f, 0.3f));
                            }
                        }
                    }
                    if (myTouches[i].phase == TouchPhase.Ended)
                    {
                        Destroy(FireR);
                        engineR = false;
                        StartCoroutine(VolumeFadeR(RThrusters.GetComponent<AudioSource>(), 0f, 0.3f));
                    }
                }
            }
        }
        }
    }

    public void CollisionDetected()
    {
        if(feetL && feetR)
        {
            if (engineL)
            {
                Instantiate(SmokeL, LFeet.transform.position, Quaternion.Euler(90 + RFeet.transform.rotation.x, RFeet.transform.rotation.y, RFeet.transform.rotation.z));
            }
            if (engineR)
            {
                Instantiate(SmokeR, RFeet.transform.position, Quaternion.Euler(90 + RFeet.transform.rotation.x, RFeet.transform.rotation.y, RFeet.transform.rotation.z));
            }
            feetL = false;
            feetR = false;

            if(endGoalL && endGoalR && Vk[1] < 0.01)
            {
                gameController.winPlayer();
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(Vk[1] > maxVelocityHit)
        {
            foreach (ContactPoint2D rockHit in collision.contacts)
            {
                Vector2 hitPoint = rockHit.point;
                Instantiate(hitExplosion, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
                AudioSource audioS = GetComponent<AudioSource>();
                int audioNumber = (int) Mathf.Round(Random.Range(0, 2));
                audioS.clip = audioClips[audioNumber];
                audioS.Play();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Vk[1] > maxVelocityDestroy)
        {
            Instantiate(destroyExplosion, gameObject.transform.position, Quaternion.identity);
            gameController.deathPlayer();
            Destroy(gameObject);
        }
    }

    public float GetVelocity()
    {
        return Vk[1];
    }

    public void SetBrokeTR(bool broke)
    {
        brokeEngineR = broke;
    }

    public void SetBrokeTL(bool broke)
    {
        brokeEngineL = broke;
    }

    IEnumerator VolumeFadeL(AudioSource _AudioSource, float _EndVolume, float _FadeLength)
    {
        float _StartTime = Time.time;
        while (!engineL && Time.time < _StartTime + _FadeLength)
        {
            float alpha = (_StartTime + _FadeLength - Time.time) / _FadeLength;
            alpha = alpha * alpha;
            _AudioSource.volume = alpha + _EndVolume * (1.0f - alpha);

            yield return null;
        }

        if (_EndVolume == 0) { _AudioSource.UnPause(); }
    }

    IEnumerator VolumeFadeR(AudioSource _AudioSource, float _EndVolume, float _FadeLength)
    {
        float _StartTime = Time.time;
        while (!engineR && Time.time < _StartTime + _FadeLength)
        {
            float alpha = (_StartTime + _FadeLength - Time.time) / _FadeLength;
            alpha = alpha * alpha;
            _AudioSource.volume = alpha + _EndVolume * (1.0f - alpha);

            yield return null;
        }

        if (_EndVolume == 0) { _AudioSource.UnPause(); }
    }

    public void Win()
    {
        win = true;
    }
}
