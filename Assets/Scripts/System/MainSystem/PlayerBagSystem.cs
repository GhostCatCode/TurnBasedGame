using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBagSystem : BaseSystem<PlayerBagSystem>
{
    public ItemData selectedItem;

    private ItemBag playerBag = new ItemBag()
    {
        itemBagType = E_ItemBag_Type.Player,
        list = new List<ItemData>()
    };
    private int goldCnt;
    public int GoldCnt => goldCnt;
    private int maxCnt;
    public int MaxCnt => maxCnt;

    private void Start()
    {
        UIMgr.Instance.ShowPanel<PlayerBagPanel>(E_UI_Layer.Top);
        InitPlayerBag();

        EventCenter.Instance.AddEventListener(E_Event_Type.OnGameLoad.ToString(), OnGameLoad);
        EventCenter.Instance.AddEventListener<Character>(E_Event_Type.OnCharacterDestroy.ToString(), OnCharacterDestroy);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnGameLoad.ToString(), OnGameLoad);
        EventCenter.Instance.RemoveEventListener<Character>(E_Event_Type.OnCharacterDestroy.ToString(), OnCharacterDestroy);
        UIMgr.Instance.HidePanel<PlayerBagPanel>();
    }

    private void InitPlayerBag()
    {
        PlayerBagData playerBagData = GameDataMgr.Instance.PlayerBagData;
        goldCnt = playerBagData.goldCnt;
        maxCnt = playerBagData.maxCnt;
        for (int i = 0; i < playerBagData.maxCnt; i ++)
        {
            if (i < playerBagData.list.Count)
            {
                ItemData itemData = ItemSystem.Instance.GetItemData(playerBagData.list[i].id);
                itemData.cnt = playerBagData.list[i].cnt;
                itemData.bag = playerBag;
                itemData.bagIndex = i;
                playerBag.list.Add(itemData);
            }
            else
            {
                ItemData itemData = new ItemData();
                itemData.bag = playerBag;
                itemData.bagIndex = i;
                playerBag.list.Add(itemData);
            }
        }
    }

    public bool TryAddItem(ItemData itemData)
    {
        for (int i = 0; i < playerBag.list.Count; i ++)
        {
            ItemData bagItem = playerBag.list[i];
            if (bagItem == null || bagItem.cnt == 0)
            {
                return true;
            }
            else if (bagItem.itemId == itemData.itemId && bagItem.cnt + itemData.cnt <= itemData.MaxCnt)
            {
                return true;
            }
        }

        return false;
    }

    public void AddItem(ItemData itemData)
    {
        for (int i = 0; i < playerBag.list.Count; i++)
        {
            ItemData bagItem = playerBag.list[i];
            if (bagItem != null && bagItem.itemId == itemData.itemId && bagItem.cnt + itemData.cnt <= itemData.MaxCnt)
            {
                bagItem.cnt = itemData.cnt + bagItem.cnt;
                EventCenter.Instance.EventTrigger(E_Event_Type.OnItemChanged.ToString());
                return;
            }
        }

        for (int i = 0; i < playerBag.list.Count; i++)
        {
            ItemData bagItem = playerBag.list[i];
            if (bagItem == null || bagItem.cnt == 0)
            {
                playerBag.list[i] = itemData;
                itemData.bag = playerBag;
                itemData.bagIndex = i;
                EventCenter.Instance.EventTrigger(E_Event_Type.OnItemChanged.ToString());
                return;
            }
        }
    }

    public void GetGold(int value)
    {
        goldCnt += value;
        EventCenter.Instance.EventTrigger(E_Event_Type.OnGoldChanged.ToString());
    }

    public void SpendGold(int value)
    { 
        goldCnt -= value;
        EventCenter.Instance.EventTrigger(E_Event_Type.OnGoldChanged.ToString());
    }

    private void OnGameLoad()
    {
        PlayerBagData playerBagData = new PlayerBagData();
        playerBagData.isNotFirst = true;
        playerBagData.goldCnt = goldCnt;
        playerBagData.maxCnt = maxCnt;
        playerBagData.list = new List<ItemSaveData>();
        for (int i = 0; i < playerBag.list.Count; i++)
        {
            if (playerBag.list[i] != null && playerBag.list[i].cnt != 0)
            {
                ItemSaveData itemSaveData = new ItemSaveData();
                itemSaveData.cnt = playerBag.list[i].cnt;
                itemSaveData.id = playerBag.list[i].itemId;
                playerBagData.list.Add(itemSaveData);
            }
        }
        GameDataMgr.Instance.LoadPlayerBagData(playerBagData);
    }

    private void OnCharacterDestroy(Character character)
    {
        if (character != null && character.GetCampType() == E_Camp_Type.Enemy)
        {
            GetGold(20);
        }
    }

    // 设置选择的道具
    public void SetSelectedItem(ItemData data)
    {
        if (data != null)
        {
            selectedItem = data;
            EventCenter.Instance.EventTrigger<ItemData>(E_Event_Type.OnSelectItem.ToString(), selectedItem);
        }
    }

    public ItemBag GetPlayerBag() => playerBag;

}
