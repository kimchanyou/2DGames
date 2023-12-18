using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    private float height;


    //public bool isGameStart;
    private void Awake()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        height = boxCollider2D.size.y;
    }

    
    public void GameStart()
    {
        StartCoroutine(CoBackGroundLoop());
    }

    //private void Update()
    //{
    //    if (isGameStart)
    //    {
    //        transform.Translate(Vector3.down * 5f * Time.deltaTime);
    //        if (transform.position.y < -height)
    //            Reposition();
    //    }
    //}

    private IEnumerator CoBackGroundLoop()
    {
        while (true)
        {
            transform.Translate(Vector3.down * 3f * Time.deltaTime);
            if (transform.position.y < -(height+5f))
                Reposition();
            yield return null;
        }
    }
    private void Reposition()
    {
        // 현재 위치에서 위쪽으로 세로 길이 * 3 만큼 이동
        Vector2 offset = new Vector2(0, height * 3f);
        transform.position = (Vector2)transform.position + offset;

    }
}
