using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : BaseSystem<ItemSystem>
{
    [SerializeField] private ItemData_SO itemDataSO;
    [SerializeField] private WeaponData_SO weaponDataSO;

    // 测试用加载数据
    public List<int> items;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < items.Count; i++)
            {
                ItemData itemData = GetItemData(items[i]);
                itemData.bag = PlayerBagSystem.Instance.GetPlayerBag();
                itemData.bagIndex = i;
                PlayerBagSystem.Instance.GetPlayerBag().list[i] = itemData;
            }

            EventCenter.Instance.EventTrigger(E_Event_Type.OnItemChanged.ToString());
        }
    }


    public ItemData GetItemData(int itemId)
    {
        return itemDataSO.itemDataList.Find(x => x.itemId == itemId)?.Clone();
    }

    public WeaponData GetWeaponData(int weaponId)
    {
        return weaponDataSO.weaponDataList.Find(x => x.weaponId == weaponId)?.Clone();
    }

    public List<ItemData> GetShopItemList(int cnt)
    {
        List<ItemData> list = new List<ItemData>();
        while (list.Count < cnt)
        {
            int index = Random.Range(0, itemDataSO.itemDataList.Count);
            ItemData itemData = itemDataSO.itemDataList[index].Clone();
            if (itemData.isCanSell)
            {
                list.Add(itemData);
            }
        }
        return list;
    }

    public void SwapItem(ItemData leftItem, ItemData rightItem)
    {
        if (leftItem != null || rightItem != null)
        {
            if ( leftItem.itemId == rightItem.itemId )
            {
                int cnt = rightItem.cnt + leftItem.cnt;
                rightItem.cnt = Mathf.Min(rightItem.MaxCnt, cnt);
                leftItem.cnt = cnt - rightItem.cnt;

                EventCenter.Instance.EventTrigger(E_Event_Type.OnItemChanged.ToString());
                return;
            }
        }

        ItemBag leftItemBag = leftItem.bag;
        int leftItemBagIndex = leftItem.bagIndex;

        ItemBag rightItemBag = rightItem.bag;
        int rightItemBagIndex = rightItem.bagIndex;

        leftItemBag.list[leftItemBagIndex] = rightItem;
        rightItem.bagIndex = leftItemBagIndex;
        rightItem.bag = leftItemBag;

        rightItemBag.list[rightItemBagIndex] = leftItem;
        leftItem.bagIndex = rightItemBagIndex;
        leftItem.bag = rightItemBag;

        EventCenter.Instance.EventTrigger(E_Event_Type.OnItemChanged.ToString());
    }
}
