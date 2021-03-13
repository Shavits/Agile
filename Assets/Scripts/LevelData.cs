using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelData : MonoBehaviour
{

    public GameObject Tutorial;
    public GameObject SkinStore;
    public GameObject ResetData;
    private string startLevel;

    private List<Button> skinButtons;

    private void Awake()
    {
        skinButtons = new List<Button>();


        startLevel = SaveData.GetInstance().GetMaxLevelReched();

        GameObject[] skinGo = GameObject.FindGameObjectsWithTag("SkinButton");
        for (int i = 0; i < skinGo.Length; i++)
        {
            skinButtons.Add(skinGo[i].GetComponent<Button>());
        }

        skinButtons.Sort(SortByName);
        HideTutorial();
        HideSkinStore(-1);
        HideResetProgresss(false);
    }

    private void Start()
    {
        //Debug progress reset

        /*SaveData.GetInstance().SetSpriteIdx(0);
        SaveData.GetInstance().SetMaxLevelReached("Tutorial");
        PlayerPrefs.SetInt("Finished", 0);
        startLevel = SaveData.GetInstance().GetMaxLevelReched();*/


    }

    public void StartLevel()
    {
        if (startLevel.Equals("Finished"))
        {
            ResetData.SetActive(true);
        }
        else if (startLevel.Equals("Tutorial"))
        {
            ShowTutorial();
            SaveData sd = SaveData.GetInstance();
            sd.SetMaxLevelReached("Level1");
            startLevel = sd.GetMaxLevelReched();

        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(startLevel);
        }
    }

    public void ShowTutorial()
    {
        Tutorial.SetActive(true);
    }
    public void ShowSkinStore()
    {
        int maxSkin = SaveData.GetInstance().GetMaxSkinUnlocked();
        for(int i = maxSkin + 1; i<skinButtons.Count; i++)
        {
            skinButtons[i].image.color = Color.black;
        }


        SkinStore.SetActive(true);
        
    }

    public void HideTutorial()
    {
        Tutorial.SetActive(false);
    }

    public void HideSkinStore(int skinIdx)
    {
        if(skinIdx <= SaveData.GetInstance().GetMaxSkinUnlocked())
        {
            SkinStore.SetActive(false);
            if(skinIdx > 0 )
            {
                SaveData.GetInstance().SetSpriteIdx(skinIdx);
            }
        }
    }

    public void HideResetProgresss(bool reset)
    {
        if (reset)
        {
            SaveData sd = SaveData.GetInstance();
            sd.SetMaxLevelReached("Level1");
            startLevel = sd.GetMaxLevelReched();
        }
        ResetData.SetActive(false);
    }

    public void PopulateSaveData(SaveData saveData)
    {
        throw new System.NotImplementedException();
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        throw new System.NotImplementedException();
    }

    private static int SortByName(Button o1, Button o2)
    {
        return o1.name.CompareTo(o2.name);
    }
}
