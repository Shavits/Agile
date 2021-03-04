using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level : MonoBehaviour
{

    
    public Transform[] SapwnLocations;
    public Transform person;
    public Transform Throwable;
    public Transform ThrowLocation;
    public Transform Mattress;
    public Text ScoreText;
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    public GameObject PauseScreen;
    public float SpawnTimerMax = 5f;
    public string nextLevel;

    public int[] order;// order of elements in the level

    public static event Action<bool> OnGameOver;


    private const int MATTRESS = 3;
    private const int BIGGERPLATFORM = 4;
    private const int OBSTACLE = 5;
    private const int SLOWMOTION = 6;


    private int NumOfPeople = 5; // Number of people to spawn this level
    private float spawnTimer;
    private float score;
    private int peopleSpawned;
    private static Level instance;
    private List<PowerUp> powerUps;
    private State state;
    private int idx; //Keeps track of which elemnt we look at in the level order

    private enum State
    {
        Playing,
        Won,
        Paused,
        GameOver
    }



    private void Awake()
    {
        instance = this;        
    }

    void Start()
    {
        spawnTimer = 2f;
        score = 0;
        peopleSpawned = 0;
        idx = 0;
        NumOfPeople = CountPeople();
        ScoreText.text = ScoreString();
        state = State.Playing;
        state = State.Playing;
        powerUps = new List<PowerUp>();
        GameOverScreen.SetActive(false);
        WinScreen.SetActive(false);
        PauseScreen.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {


        if(state == State.Playing)
        {
            if (idx < order.Length)
            {
                int current = order[idx];
                if (spawnTimer < 0)
                {
                    if(current <= 2)
                    {
                        if(current == 1)
                        {
                            SpawnPerson(SapwnLocations[0]);

                        }
                        else
                        {
                            SpawnPerson(SapwnLocations[1]);
                        }
                        spawnTimer = SpawnTimerMax;
                        idx++;
                    }
                    else if(current >= 2)
                    {
                        if(current == MATTRESS)
                        {
                            SpawnThrowable("Mattress");
                        }else if (current == BIGGERPLATFORM)
                        {
                            SpawnThrowable("BiggerPlatform");
                        }else if(current == OBSTACLE)
                        {
                            SpawnThrowable("Obstacle");
                        }
                        spawnTimer = SpawnTimerMax/2;
                        idx++;
                    }
                }

                spawnTimer -= Time.deltaTime;
            }
            //Update all powerup timers and remove those who are finished
            for (int i = powerUps.Count - 1; i >= 0; i--)
            {
                if (powerUps[i].UpdateTimer(Time.deltaTime))
                {
                    powerUps.RemoveAt(i);
                }
            }
        }
    }

    private void SpawnPerson(Transform spawnLoc)
    {
        Transform newPerson = Instantiate(person);
        newPerson.position = spawnLoc.position;
        peopleSpawned++;
        
    }

    private void SpawnThrowable(string type)
    {
        Transform newThrow = Instantiate(Throwable);
        newThrow.GetComponent<Throwable>().SetType(type);
        newThrow.position = ThrowLocation.position;
    }
    
    private void Win()
    {
        state = State.Won;
        WinScreen.SetActive(true);
        OnGameOver.Invoke(true);
        SaveData.GetInstance().SetMaxLevelReached(nextLevel);
    }

    private string ScoreString()
    {
        string res = score.ToString() + "\\" + NumOfPeople.ToString();
        return res;
    }

    public void ChangeScore(int newScore)
    {
        score += newScore;
        ScoreText.text = ScoreString();
        if(score == NumOfPeople)
        {
            Win();
        }
    }

    public void ApplyPowerUp(string type)
    {
        powerUps.Add(new PowerUp(type, Mattress));
    }

    public int GetPeopleSpawned()
    {
        return peopleSpawned;
    }
    
    private int CountPeople()
    {
        int res = 0;
        for(int i = 0; i < order.Length; i++)
        {
            if (order[i] <= 2)
            {
                res++;
            }
        }
        return res;
    }

    //Switches state to GameOver and stops all the people, powerups and firemen from moving. also activates gameover screen
    public void GameOver()
    {
        state = State.GameOver;
        OnGameOver.Invoke(true);
        GameOverScreen.SetActive(true);
    }


    public static Level GetInstance()
    {
        return instance;
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevel);
        
    }

    public void Pause()
    {
        if(state == State.Playing)
        {
            Time.timeScale = 0f;
            state = State.Paused;
            PauseScreen.SetActive(true);
        }
    }
    
    public void UnPause()
    {
        float newTimeScale = 1f;
        if(powerUps.Count > 0)
        {
            bool slow = false;
            foreach (PowerUp pu in powerUps)
            {
                if(pu.type == PowerUp.Type.SlowMotion)
                {
                    slow = true;
                }
            }
            if (slow)
            {
                newTimeScale = 0.5f;
            }
        }
        Time.timeScale = newTimeScale;
        PauseScreen.SetActive(false);
        state = State.Playing;
    }


    public void MainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }


    private class PowerUp
    {
        private float timer;
        private float timerMax = 5f;
        private Transform Mattress;
        private Transform InstancedMattress;
        
        public Type type;




        public enum Type
        {
            Mattress,
            BiggerPlatform,
            SlowMotion
        }

        public PowerUp(string type, Transform Mattress)
        {
            switch (type)
            {
                case "Mattress":
                    this.type = Type.Mattress;
                    this.Mattress = Mattress;
                    break;
                case "BiggerPlatform":
                    this.type = Type.BiggerPlatform;
                    break;
            }
            timer = timerMax;
            Activate();
        }

        private void Activate()
        {
            switch (type.ToString())
            {
                case "SlowMotion":
                    Time.timeScale = 0.5f;
                    break;
                case "BiggerPlatform":
                    Fireman.GetInstance().ChangeScale(1.5f);
                    break;
                case "Mattress":
                    InstancedMattress = Instantiate(Mattress);
                    break;
            }
        }

        //Checks if the timer is finished and if so cancels it's effect and returns true
        public bool UpdateTimer(float deltaTime)
        {
            timer -= deltaTime;
            if (timer < 0)
            {
                switch (type.ToString())
                {
                    case "SlowMotion":
                        Time.timeScale = 1f;
                        return true;
                    case "BiggerPlatform":
                        Fireman.GetInstance().ChangeScale(1f);
                        return true;
                    case "Mattress":
                        InstancedMattress.gameObject.GetComponent<Animator>().SetTrigger("Popped");
                        Destroy(InstancedMattress.gameObject, 2f);
                        return true;
                }
            }

            return false;
        }
    }
}
