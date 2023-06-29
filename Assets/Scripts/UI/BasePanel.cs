using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float alphaSpeed = 10f;

    private bool isShow;
    private UnityAction hideCallBack;

    protected virtual void Awake()
    {
        //ªÒ»°canvasGroup
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        if (isShow && canvasGroup.alpha != 1f)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1f)
            {
                canvasGroup.alpha = 1f;
            }
        }
        else if (!isShow)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0f)
            {
                canvasGroup.alpha = 0f;

                hideCallBack?.Invoke();
            }
        }
    }

    protected abstract void Init();

    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0f;
    }

    public virtual void HideMe(UnityAction callBack)
    {

        isShow = false;
        canvasGroup.alpha = 1f;

        hideCallBack = callBack;
    }
}
