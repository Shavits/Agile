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
        //Debug progress reset
        SaveData.GetInstance().SetSpriteIdx(0);
        SaveData.GetInstance().SetMaxLevelReached("level1");



        startLevel = SaveData.GetInstance().GetMaxLevelReched();
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
