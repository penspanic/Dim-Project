using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour
{
    public float startScaleX;
    void Awake()
    {
        startScaleX = transform.localScale.x;
    }

    public void SetValue(float ratio)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = startScaleX * ratio;
        transform.localScale = newScale;
    }
}
