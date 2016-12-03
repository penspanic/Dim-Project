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

    private string lastClearedStageId = "S1";
    private string selectedStageId;
    public List<CharacterType> OwnedCharacters = new List<CharacterType>();
    public List<CharacterType> SelectedCharacters = new List<CharacterType>();

    void Awake()
    {
        PlayerPrefs.DeleteAll();

        DontDestroyOnLoad(gameObject);

        if( LoadData() == false)
        {
            InitData();
        }

        selectedStageId = "S1";
       // Warrior, Priest, Wizard
    }

    void InitData()
    {
        //OwnedCharacters.AddRange(new CharacterType[] { CharacterType.Warrior, CharacterType.Priest, CharacterType.Wizard });
        OwnedCharacters.AddRange(new CharacterType[] { CharacterType.Lancer, CharacterType.Bard, CharacterType.Assassin, CharacterType.Shielder, CharacterType.Priest });
        SelectedCharacters.AddRange(OwnedCharacters);
        selectedStageId = "S1";
    }

    bool LoadData()
    {
        if (PlayerPrefs.HasKey("lastClearedStageId") == true)
        {
            lastClearedStageId = PlayerPrefs.GetString("lastClearedStageId");
            if(lastClearedStageId == string.Empty || lastClearedStageId.Length <= 1)
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        if(PlayerPrefs.HasKey("selectedStageId") == true)
        {
            selectedStageId = PlayerPrefs.GetString("selectedStageId");
            if(selectedStageId == string.Empty || selectedStageId.Length <= 1)
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        if(PlayerPrefs.HasKey("ownedCharacterString") == true)
        {
            string ownedCharactersString = PlayerPrefs.GetString("ownedCharacterString");

            string[] characters = ownedCharactersString.Split(' ');

            OwnedCharacters.Clear();

            foreach(var character in  characters)
            {
                if(character.Length <= 2)
                {
                    continue;
                }

                OwnedCharacters.Add((CharacterType)Enum.Parse(Type.GetType("CharacterType"), character));
            }
            if(OwnedCharacters.Count == 0)
            {
                return false;
            }
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
        PlayerPrefs.SetString("lastClearedStageId", lastClearedStageId);
        PlayerPrefs.SetString("selectedStageId", selectedStageId);

        string ownedCharactersString = string.Empty;
        foreach(CharacterType eachCharacter in OwnedCharacters)
        {
            ownedCharactersString += eachCharacter.ToString() + " ";
        }

        PlayerPrefs.SetString("ownedCharacterString", ownedCharactersString);

        string selectedCharactersString = string.Empty;
        foreach(CharacterType eachCharacter in SelectedCharacters)
        {
            selectedCharactersString += eachCharacter.ToString() + " ";
        }

        PlayerPrefs.SetString("selectedCharactersString", selectedCharactersString);
    }

    public int GetLastClearedStageNum()
    {
        if(lastClearedStageId == null)
        {
            return 0;
        }
        return int.Parse(lastClearedStageId.Substring(1, lastClearedStageId.Length - 1));
    }

    public void SetSelectedStage(string stageId)
    {
        selectedStageId = stageId;
    }

    public string GetSelectedStageId()
    {
        return selectedStageId;
    }

    public int GetSelectedStageNum()
    {
        return int.Parse(selectedStageId.Substring(1, selectedStageId.Length - 1));
    }
}