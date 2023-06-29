using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBagPanel : BasePanel
{
    private ItemBag bag;

    [SerializeField] private List<ItemSlot> slotList;

    public Button btnClose;

    protected override void Init()
    {
        EventCenter.Instance.AddEventListener(E_Event_Type.OnItemChanged.ToString(), OnItemChanged);
        btnClose.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<ItemBagPanel>();
        });
    }

    public void SetInfo(ItemBag bag)
    {
        this.bag = bag;
        UpdatePlayerBag();
    }

    private void OnItemChanged()
    {
        UpdatePlayerBag();
    }

    private void UpdatePlayerBag()
    {
        List<ItemData> itemList = bag.list;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (i < slotList.Count)
            {
                slotList[i].SetInfo(itemList[i]);
            }
        }
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnItemChanged.ToString(), OnItemChanged);
    }
}
