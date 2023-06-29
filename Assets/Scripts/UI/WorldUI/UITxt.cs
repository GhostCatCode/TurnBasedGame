using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITxt : MonoBehaviour
{
    public Text txt;
    public float moveSpeed = 1f;

    public float liveTime = 0.2f;
    private float timer;

    private bool isFade;
    private bool isMove;

    private void Update()
    {

        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            PoolMgr.Instance.PushObj("UI/UITxt", gameObject);
            return;
        }

        if (isFade)
        {
            Color color = txt.color;
            color.a = timer / liveTime;
            txt.color = color;
        }

        if ( isMove)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
    }

    public void Setup(string text, Color color, float liveTime, bool isFade, bool isMove)
    {
        txt.text = text;
        txt.color = color;
        this.liveTime = liveTime;
        this.isFade = isFade;
        this.isMove = isMove;

        this.timer = liveTime;
    }
}
