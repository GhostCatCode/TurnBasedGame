using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Text txtInfo;
    public Button btnClose;
    public Button btnCancel;

    public UnityAction OnClose;
    public UnityAction OnCancel;

    protected override void Init()
    {
        btnClose.onClick.AddListener(() =>
        {
            OnClose?.Invoke();
            UIMgr.Instance.HidePanel<TipPanel>();
        });

        btnCancel.onClick.AddListener(() =>
        {
            OnCancel?.Invoke();
            UIMgr.Instance.HidePanel<TipPanel>();
        });
    }

    public void Setup(string info, UnityAction OnClose = null, UnityAction OnCancel = null)
    {
        txtInfo.text = info;
        this.OnClose = OnClose;
        this.OnCancel = OnCancel;
    }
}
