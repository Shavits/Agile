using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    
    private static string maxLevelReached;
    private static int spriteIdx;


    private static SaveData instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        maxLevelReached = PlayerPrefs.GetString("MaxLevelReached", "Level1");
        spriteIdx = PlayerPrefs.GetInt("SpriteIdx", 0);
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

}

