using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVictoryPanel : BasePanel
{
    [SerializeField] private Button btnContinue;

    protected override void Init()
    {
        btnContinue.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<GameVictoryPanel>();
            UIMgr.Instance.ShowPanel<ShoppingPanel>();
        });
    }
}
