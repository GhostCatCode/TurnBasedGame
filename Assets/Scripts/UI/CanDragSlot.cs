using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanDragSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ItemSlot slot;
    private Image dragSlot;
    private Transform dragTransform;

    private void Start()
    {
        slot = GetComponent<ItemSlot>();
        dragTransform = UIMgr.Instance.GetLayerFather(E_UI_Layer.System);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.GetItemData() != null && slot.GetItemData().cnt > 0)
        {
            GameObject Obj = ResMgr.Instance.Load<GameObject>("UI/DragItem");
            Obj.transform.SetParent(dragTransform, false);
            dragSlot = Obj.GetComponent<Image>();
            dragSlot.sprite = slot.imgItem.sprite;
            slot.imgItem.gameObject.SetActive(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragSlot != null)
        {
            dragSlot.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragSlot != null)
        {
            Destroy(dragSlot.gameObject);
            slot.imgItem.gameObject.SetActive(true);

            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ItemSlot>(out ItemSlot newSlot))
                {
                    // »¥»»
                    ItemSystem.Instance.SwapItem(slot.GetItemData(), newSlot.GetItemData());
                }
            }
        }
    }
}
