using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{

    public GameObject Tutorial;

    private void Awake()
    {
        HideTutorial();
    }
    public void StartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    public void ShowTutorial()
    {
        Tutorial.SetActive(true);
    }

    public void HideTutorial()
    {
        Tutorial.SetActive(false);
    }
}
