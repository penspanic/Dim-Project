using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocaleText : MonoBehaviour
{
    public string Locale;

    void Awake()
    {
        GetComponent<Text>().text = LocaleManager.Get(Locale);
    }
}
