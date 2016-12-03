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

    public string LastClearedStageId;
    public string SelectedStageId;
    public List<CharacterType> OwnedCharacters = new List<CharacterType>();
    public List<CharacterType> SelectedCharacters = new List<CharacterType>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if( LoadData() == false)
        {
            InitData();
        }

        SelectedStageId = "S1";
       // Warrior, Priest, Wizard
    }

    void InitData()
    {
        OwnedCharacters.AddRange(new CharacterType[] { CharacterType.Warrior, CharacterType.Priest, CharacterType.Wizard });
        SelectedCharacters.AddRange(OwnedCharacters);
        SelectedStageId = "S1";
        LastClearedStageId = null;
    }

    bool LoadData()
    {
        if (PlayerPrefs.HasKey("LastClearedStageId") == true)
        {
            LastClearedStageId = PlayerPrefs.GetString("LastClearedStageId");
            if(LastClearedStageId == string.Empty || LastClearedStageId.Length <= 1)
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        if(PlayerPrefs.HasKey("SelectedStageId") == true)
        {
            SelectedStageId = PlayerPrefs.GetString("SelectedStageId");
            if(SelectedStageId == string.Empty || SelectedStageId.Length <= 1)
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
        PlayerPrefs.SetString("LastClearedStageId", LastClearedStageId);
        PlayerPrefs.SetString("SelectedStageId", SelectedStageId);

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
}