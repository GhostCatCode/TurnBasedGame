using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;

public enum E_UI_Layer
{
    Top,
    Mid,
    System
}

public class UIMgr : BaseManager<UIMgr>
{
    public RectTransform canvas;

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform top;
    private Transform mid;
    private Transform system;

    public UIMgr()
    {
        GameObject obj = ResMgr.Instance.Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);

        top = canvas.Find("Top");
        mid = canvas.Find("Mid");
        system = canvas.Find("System");
    }

    public void ShowPanel<T>(E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callBack = null) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }

        GameObject obj = ResMgr.Instance.Load<GameObject>("UI/Panel/" + panelName);
        Transform father = GetLayerFather(layer);
        obj.transform.SetParent(father);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        (obj.transform as RectTransform).offsetMax = Vector2.zero;
        (obj.transform as RectTransform).offsetMin = Vector2.zero;

        T panel = obj.GetComponent<T>();
        callBack?.Invoke(panel);
        panel.ShowMe();

        panelDic.Add(panelName, panel);
    }

    // isFade ÊÇ·ñÁ¢¼´Òþ²Ø
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName]?.HideMe(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }

        }
    }

    public T GetPanel<T>(string panelName) where T : BasePanel
    {
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        return null;
    }

    public Transform GetLayerFather(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.Mid:
                return mid;
            case E_UI_Layer.System:
                return system;
            case E_UI_Layer.Top:
                return top;
            default:
                return null;
        }
    }
}
