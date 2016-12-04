using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// 처음에 앱 키는 씬? 에서 여기 초기화 하자
public class PlayerData : MonoBehaviour
{
    public static PlayerData instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("PlayerData").AddComponent<PlayerData>();
            }
            return _instance;
        }
    }

    private static PlayerData _instance;

    private int lastClearedStageNum;
    private int selectedStageNum = 1;
    public List<CharacterType> SelectedCharacters = new List<CharacterType>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if( LoadData() == false)
        {
            InitData();
        }

        selectedStageNum = 1;
    }

    public void ResetData()
    {
        lastClearedStageNum = 0;
        selectedStageNum = 1;
        SelectedCharacters.Clear();
        PlayerPrefs.DeleteAll();
    }

    void InitData()
    {
        SelectedCharacters.AddRange(new CharacterType[] { CharacterType.Warrior, CharacterType.Priest, CharacterType.Wizard });
        selectedStageNum = 1;
    }

    bool LoadData()
    {
        if (PlayerPrefs.HasKey("lastClearedStageNum") == true)
        {
            lastClearedStageNum = PlayerPrefs.GetInt("lastClearedStageNum");
        }
        else
        {
            return false;
        }

        if(PlayerPrefs.HasKey("selectedCharactersString")== true)
        {
            string selectedCharactersString = PlayerPrefs.GetString("selectedCharactersString");

            string[] characters = selectedCharactersString.Split(' ');

            SelectedCharacters.Clear();

            foreach(var character in characters)
            {
                if(character.Length <= 2)
                {
                    continue;
                }

                SelectedCharacters.Add((CharacterType)Enum.Parse(Type.GetType("CharacterType"), character));
            }
            if(SelectedCharacters.Count == 0)
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("lastClearedStageNum", lastClearedStageNum);

        string selectedCharactersString = string.Empty;
        foreach(CharacterType eachCharacter in SelectedCharacters)
        {
            selectedCharactersString += eachCharacter.ToString() + " ";
        }

        PlayerPrefs.SetString("selectedCharactersString", selectedCharactersString);
    }

    public void SetLastClearedStage(int stageNum)
    {
        lastClearedStageNum = stageNum;
    }

    public int GetLastClearedStageNum()
    {
        return lastClearedStageNum;
    }

    public void SetSelectedStage(int stageNum)
    {
        selectedStageNum = stageNum;
    }

    public int GetSelectedStageNum()
    {
        return selectedStageNum;
    }
}