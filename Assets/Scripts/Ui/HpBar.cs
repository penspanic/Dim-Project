using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour
{
    private GameObject hpRed;
    private GameObject hpBack;


    void Awake()
    {
        hpRed = transform.FindChild("Red").gameObject;
        hpBack = transform.FindChild("Hp Back").gameObject;
    }

    public void SetValue(float ratio)
    {
        Vector3 newScale = Vector3.one;
        newScale.x = 1f - ratio;
        hpRed.transform.localScale = newScale;
    }
}
