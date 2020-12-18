using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level : MonoBehaviour
{

    
    public Transform SapwnLocation;
    public Transform person;
    public Text ScoreText;
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    public float SpawnTimerMax = 5f;
    public int NumOfPeople = 5; // Number of people to spawn this level

    public static event Action<bool> OnGameOver;


    private float spawnTimer;
    private float score;
    private int peopleSpawned;
    private static Level instance;
    private List<Transform> people;
    private State state;


    private enum State
    {
        Playing,
        Won,
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
        ScoreText.text = ScoreString();
        state = State.Playing;
        people = new List<Transform>();
        GameOverScreen.SetActive(false);
        WinScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Playing && peopleSpawned < NumOfPeople)
        {
            if (spawnTimer < 0)
            {
                SpawnPerson();
                spawnTimer = SpawnTimerMax;
            }

            spawnTimer -= Time.deltaTime;
        }


    }

    private void SpawnPerson()
    {
        Transform newPerson = Instantiate(person);
        newPerson.position = SapwnLocation.position;
        people.Add(newPerson);
        peopleSpawned++;
        
    }
    
    private void Win()
    {
        state = State.Won;
        WinScreen.SetActive(true);
        OnGameOver.Invoke(true);
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

    public int GetPeopleSpawned()
    {
        return peopleSpawned;
    }
    

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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}
