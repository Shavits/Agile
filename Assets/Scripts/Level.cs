using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level : MonoBehaviour
{


    public Transform SapwnLocation;
    public Transform person;
    public Text ScoreText;
    public float SpawnTimerMax = 5f;


    private float spawnTimer;
    private float score;
    private static Level instance;



    private void Awake()
    {
        instance = this;        
    }

    void Start()
    {
        spawnTimer = 2f;
        score = 0;
        ScoreText.text = score.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < 0)
        {
            SpawnPerson();
            spawnTimer = SpawnTimerMax;
        }

        spawnTimer -= Time.deltaTime;


    }

    private void SpawnPerson()
    {
        Transform newPerson = Instantiate(person);
        newPerson.position = SapwnLocation.position;
        
    }

    public void ChangeScore(int newScore)
    {
        score += newScore;
        ScoreText.text = score.ToString();
    }
    

    public static Level GetInstance()
    {
        return instance;
    }
}
