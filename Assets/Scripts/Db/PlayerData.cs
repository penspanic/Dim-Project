using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 처음에 앱 키는 씬? 에서 여기 초기화 하자
public static class PlayerData
{
    public static string LastClearedStageId="S4";
    public static List<CharacterType> OwnedCharacters = new List<CharacterType>();
    public static List<CharacterType> SelectedCharacters = new List<CharacterType>();
    public static string SelectedStageId = "S3";
    public static void SetData(CharacterType[] ownedCharacters, CharacterType[] selectedCharacters, string lastClearedStageId)
    {
        PlayerData.LastClearedStageId = lastClearedStageId;
        PlayerData.OwnedCharacters.AddRange(ownedCharacters);
        PlayerData.SelectedCharacters.AddRange(selectedCharacters);
    }
}