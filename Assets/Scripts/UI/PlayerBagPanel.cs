using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBagPanel : BasePanel
{
    public Transform playerBagParent;

    public Text txtGold;

    [SerializeField] private List<ItemSlot> slotList;

    protected override void Init()
    {
        EventCenter.Instance.AddEventListener(E_Event_Type.OnItemChanged.ToString(), OnItemChanged);
        EventCenter.Instance.AddEventListener(E_Event_Type.OnGoldChanged.ToString(), OnGoldChanged);
        UpdatePlayerBag();
        UpdateGold();
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnItemChanged.ToString(), OnItemChanged);
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnGoldChanged.ToString(), OnGoldChanged);
    }

    private void OnItemChanged()
    {
        UpdatePlayerBag();
    }

    private void UpdatePlayerBag()
    {
        List<ItemData> itemList = PlayerBagSystem.Instance.GetPlayerBag().list;

        for (int i = 0; i < itemList.Count; i ++)
        {
            if (i < slotList.Count)
            slotList[i].SetInfo(itemList[i]);
        }
    }

    private void OnGoldChanged()
    {
        UpdateGold();
    }

    private void UpdateGold()
    {
        txtGold.text = PlayerBagSystem.Instance.GoldCnt.ToString();
    }
}
