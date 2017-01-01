using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LocaleType
{
    Korean,
    English
}

public struct LocaleData
{
    public LocaleData(LocaleType localeType, string data)
    {
        this.localeType = localeType;
        this.data = data;
    }

    public LocaleType localeType;
    public string data;   
}

public static class LocaleManager
{

    public static LocaleType CurrentLocale = LocaleType.English;

    private static Dictionary<string, LocaleData[]> localeDic = new Dictionary<string, LocaleData[]>();

    static LocaleManager()
    {
        Add("Logo.TouchScreen", new LocaleData[] { new LocaleData(LocaleType.Korean, "화면을 터치하세요"), new LocaleData(LocaleType.English, "Touch the Screen") });
        Add("Menu.SelectHero", new LocaleData[] { new LocaleData(LocaleType.Korean, "원하는 영웅을 고르세요"), new LocaleData(LocaleType.English, "Select a Hero you want to hire.") });
        Add("Menu.ExchangeHero", new LocaleData[] { new LocaleData(LocaleType.Korean, "교체를 원하는 영웅을 선택하세요"), new LocaleData(LocaleType.English, "Select a Hero you want to exchange.") });
        Add("Menu.Cancel", new LocaleData[] { new LocaleData(LocaleType.Korean, "취소"), new LocaleData(LocaleType.English, "Cancel") });
        Add("Menu.Yes", new LocaleData[] { new LocaleData(LocaleType.Korean, "예"), new LocaleData(LocaleType.English, "Yes") });
        Add("Menu.No", new LocaleData[] { new LocaleData(LocaleType.Korean, "아니오"), new LocaleData(LocaleType.English, "No") });
        Add("Menu.ConfirmStart", new LocaleData[] { new LocaleData(LocaleType.Korean, "현재 파티 조합으로 도전하시겠습니끼?"), new LocaleData(LocaleType.English, "Are you sure to start with this party?") });
        Add("Menu.Credit", new LocaleData[] { new LocaleData(LocaleType.Korean, "만든 사람들"), new LocaleData(LocaleType.English, "Credit") });
        Add("Menu.Reset", new LocaleData[] { new LocaleData(LocaleType.Korean, "초기화"), new LocaleData(LocaleType.English, "Reset") });
        /////////////////////////////////////////// InGame

        Add("InGame.Restart", new LocaleData[] { new LocaleData(LocaleType.Korean, "다시 시작"), new LocaleData(LocaleType.English, "Restart") });
        Add("InGame.ReturnToMenu", new LocaleData[] { new LocaleData(LocaleType.Korean, "메뉴로 돌아가기"), new LocaleData(LocaleType.English, "Return to menu") });
        Add("InGame.Continue", new LocaleData[] { new LocaleData(LocaleType.Korean, "계속하기"), new LocaleData(LocaleType.English, "Continue")});
    }

    private static void Add(string key, LocaleData[] locales)
    {
        localeDic.Add(key, locales);
    }

    public static string Get(string key)
    {
        if(localeDic.ContainsKey(key) == true)
        {
            for(int i = 0; i < localeDic[key].Length; ++i)
            {
                if(localeDic[key][i].localeType == CurrentLocale)
                {
                    return localeDic[key][i].data;
                }
            }
        }

        return key;
    }
}