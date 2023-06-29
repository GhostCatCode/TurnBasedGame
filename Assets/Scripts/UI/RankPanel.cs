using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : BasePanel
{
    public Transform rankInfoUIParent;

    public Button btnBack;

    protected override void Init()
    {
        RankData rankData = GameDataMgr.Instance.RankData;
        for (int i = 0; i < rankData.list.Count; i++)
        {
            GameObject obj = ResMgr.Instance.Load<GameObject>("UI/RankInfoUI");
            obj.transform.SetParent(rankInfoUIParent, false);
            obj.GetComponent<RankInfoUI>().SetUp(rankData.list[i], i + 1);
        }

        btnBack.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<RankPanel>();
        });
    }
}
