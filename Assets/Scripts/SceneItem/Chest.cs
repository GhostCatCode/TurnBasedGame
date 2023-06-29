using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private GridPosition gridPosition;
    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject openChest;
    [SerializeField] private GameObject closeChest;

    private ItemBag bag;

    private bool isTest;

    private void Start()
    {
        gridPosition = GridSystem.Instance.GetGridPosition(transform.position);
        GridSystem.Instance.SetInteractableOnGridPosition(gridPosition, this);
        GridSystem.Instance.SetIsUnStandable(gridPosition, true);
        openChest.SetActive(false);
        closeChest.SetActive(true);

        bag = new ItemBag();
        bag.itemBagType = E_ItemBag_Type.Box;
        bag.list = new List<ItemData>();
        for (int i = 0; i < 4; i++)
        {
            ItemData itemData = new ItemData();
            itemData.bag = bag;
            itemData.bagIndex = i;
            bag.list.Add(itemData);
        }


        for (int i = 0; i < 1; i++)
        {
            ItemData itemData = ItemSystem.Instance.GetShopItemList(1)[0];
            itemData.bag = bag;
            itemData.bagIndex = i;
            bag.list[i] = itemData;
        }
    }

    public void Interact(Character character)
    {
        if (!isOpen)
        {
            isOpen = true;
            openChest.SetActive(true);
            closeChest.SetActive(false);
        }
        UIMgr.Instance.ShowPanel<ItemBagPanel>(E_UI_Layer.Mid, (panel) =>
        {
            panel.SetInfo(bag);
        });
    }

    public bool IsCanInteract()
    {
        return true;
    }


}
