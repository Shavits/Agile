using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{

    public GameObject Tutorial;
    private string startLevel;

    private void Awake()
    {
        HideTutorial();
    }

    private void Start()
    {
        startLevel = SaveData.GetInstance().GetMaxLevelReched();
        //SaveData.GetInstance().SetSpriteIdx(2);
    }

    public void StartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(startLevel);
    }

    public void ShowTutorial()
    {
        Tutorial.SetActive(true);
    }

    public void HideTutorial()
    {
        Tutorial.SetActive(false);
    }

    public void PopulateSaveData(SaveData saveData)
    {
        throw new System.NotImplementedException();
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        throw new System.NotImplementedException();
    }
}
