using UnityEngine;
using System.Collections;

public class SpeedZone : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if(other.CompareTag("Character") == true)
        {
            other.GetComponent<Character>().RunToMonster();
        }
    }
}
