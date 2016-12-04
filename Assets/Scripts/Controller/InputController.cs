using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{

    void Awake()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hitInfos = Physics2D.RaycastAll(touchPos, Vector2.zero);

            foreach(var eachInfo in hitInfos)
            {
                if(eachInfo.collider != null)
                {
                    GameObject hitObject = eachInfo.collider.gameObject;
                    if(hitObject.GetComponent<ITouchable>()!= null)
                    {
                        hitObject.GetComponent<ITouchable>().OnTouch();
                        return; // 일단 한번 터치에 캐릭터 하나만 움직일 수 있도록 한다.
                    }
                }
            }
        }
    }
}
