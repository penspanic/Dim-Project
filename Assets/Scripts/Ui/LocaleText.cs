using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocaleText : MonoBehaviour
{
    public string Locale;

    void Awake()
    {
        string newText = LocaleManager.Get(Locale);
        GetComponent<Text>().text = LocaleManager.Get(Locale);
    }
}
