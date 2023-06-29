using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image imgItem;
    public Image selected;
    public Text itemCnt;

    private bool isSelected;
    private ItemData itemData;

    public GameObject uiInfo;

    private void Awake()
    {
        EventCenter.Instance.AddEventListener<ItemData>(E_Event_Type.OnSelectItem.ToString(), OnSelectItem);
        imgItem.sprite = null;
        imgItem.enabled = false;
        selected.enabled = false;
        itemCnt.text = string.Empty;
        isSelected = false;
    }

    // ³õÊ¼»¯
    public void SetInfo(ItemData itemData)
    {
        this.itemData = itemData;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        if (itemData != null && itemData.cnt > 0)
        {
            SpriteAtlas sa = ResMgr.Instance.Load<SpriteAtlas>("Sprite/SpriteAtlas");
            imgItem.sprite = sa.GetSprite(itemData.spriteName);
            imgItem.enabled = true;
            isSelected = (PlayerBagSystem.Instance.selectedItem == itemData);
            selected.enabled = isSelected;
            if (itemData.cnt > 0)
            {
                itemCnt.text = itemData.cnt.ToString();
            }
        }
        else
        {
            imgItem.sprite = null;
            imgItem.enabled = false;
            isSelected = (PlayerBagSystem.Instance.selectedItem == itemData);
            selected.enabled = isSelected;
            itemCnt.text = string.Empty;
            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected)
        {
            CharacterActionSystem.Instance.TryHandleSelectedItem(itemData);
        }
        else
        {
            PlayerBagSystem.Instance.SetSelectedItem(itemData);
        }
    }

    private void OnSelectItem(ItemData selectedItem)
    {
        if (selectedItem != null)
        {
            isSelected = (selectedItem == itemData);
            selected.enabled = isSelected;
        }
    }

    public ItemData GetItemData() => itemData;

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<ItemData>(E_Event_Type.OnSelectItem.ToString(), OnSelectItem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemData != null && itemData.cnt > 0)
        {
            PoolMgr.Instance.GetObj("UI/UIInfo", (Obj) =>
            {
                Obj.transform.SetParent(this.transform);
                Obj.transform.localScale = Vector3.one;
                Obj.transform.localPosition = Vector3.zero;

                this.uiInfo = Obj;
                UIInfo ui = Obj.GetComponent<UIInfo>();
                ui.Setup(itemData.introduction);
            });
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (uiInfo != null)
        {
            PoolMgr.Instance.PushObj("UI/UIInfo", uiInfo);
            uiInfo = null;
        }
    }
}
