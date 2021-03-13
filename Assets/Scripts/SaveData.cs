using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    
    private static string maxLevelReached;
    private static int spriteIdx;
    private int finished;


    private static SaveData instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        maxLevelReached = PlayerPrefs.GetString("MaxLevelReached", "Tutorial");
        spriteIdx = PlayerPrefs.GetInt("SpriteIdx", 0);
        finished = PlayerPrefs.GetInt("Finished", 0);
    }

    public static SaveData GetInstance()
    {
        return instance;
    }

    public string GetMaxLevelReched()
    {
        return maxLevelReached;
    }

    public void SetMaxLevelReached(string newMaxLevel)
    {
        maxLevelReached = newMaxLevel;

        PlayerPrefs.SetString("MaxLevelReached", maxLevelReached);
        if (newMaxLevel.Equals("Finished"))
        {
            finished = 1;
            PlayerPrefs.SetInt("Finished", 1);
        }
    }
    public int GetSpriteIdx()
    {
        return spriteIdx;
    }

    public void SetSpriteIdx(int newSpriteIdx)
    {
        spriteIdx = newSpriteIdx;
        PlayerPrefs.SetInt("SpriteIdx", spriteIdx);
    }

    public int GetMaxSkinUnlocked()
    {
        if (maxLevelReached.Equals("Finished") || finished == 1)
        {
            return 5;
        }
        else
        {
            int skinIdx = int.Parse(maxLevelReached.Substring(maxLevelReached.Length -1)) -1;
            return skinIdx;
        }
    }

}

