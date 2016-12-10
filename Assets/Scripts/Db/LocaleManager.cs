using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LocaleType
{
    Korean,
    English
}
public static class LocaleManager
{

    public static LocaleType CurrentLocale;

    private static Dictionary<string, Dictionary<LocaleType, string>> localeDic = new Dictionary<string, Dictionary<LocaleType, string>>();

    static LocaleManager()
    {
        Dictionary<LocaleType, string> tempDic = new Dictionary<LocaleType, string>();

        tempDic.Add(LocaleType.Korean, "화면을 터치하세요");
        tempDic.Add(LocaleType.English, "touch the Screen");
        localeDic.Add("Logo.TouchScreen", tempDic);

        tempDic.Clear();
        tempDic.Add(LocaleType.Korean, "원하는 영웅을 고르세요");
        tempDic.Add(LocaleType.English, "Select a Hero you want to hire.");
        localeDic.Add("Menu.SelectHero", tempDic);

        tempDic.Clear();
        tempDic.Add(LocaleType.Korean, "교체를 원하는 영웅을 선택하세요");
        tempDic.Add(LocaleType.English, "Select a Hero you want to exchange.");
        localeDic.Add("Menu.ExchangeHero", tempDic);
    }

    public static string Get(string key)
    {
        if(localeDic.ContainsKey(key) == true && localeDic[key].ContainsKey(LocaleManager.CurrentLocale) == true)
        {
            return localeDic[key][CurrentLocale];
        }

        return key;
    }
}