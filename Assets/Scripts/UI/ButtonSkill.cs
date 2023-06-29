using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ButtonSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public KeyCode keyCode;
    public SkillData data;
    public Button btn;
    public Image selected;
    public Text info;

    public GameObject uiInfo;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (data != null && SkillSystem.Instance.IsCanSpendToTakeSkill(data))
            {
                CharacterActionSystem.Instance.SetSelectedSkill(data);
            }
        }
    }

    public void SetInfo(SkillData data)
    {
        this.data = data;
        info.text = data.name;

        if (!SkillSystem.Instance.IsCanSpendToTakeSkill(data))
        {
            info.color = Color.gray;
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
            info.color = Color.white;
        }
    }

    private void OnEnable()
    {
        btn.onClick.AddListener(() =>
        {
            CharacterActionSystem.Instance.SetSelectedSkill(data);
        });

        EventCenter.Instance.AddEventListener<SkillData>(E_Event_Type.OnSelectSkill.ToString(), OnSelectSkill);
        selected.enabled = false;
    }

    private void OnDisable()
    {
        btn.onClick.RemoveAllListeners();
        EventCenter.Instance.RemoveEventListener<SkillData>(E_Event_Type.OnSelectSkill.ToString(), OnSelectSkill);
    }

    private void OnSelectSkill(SkillData data)
    {
        selected.enabled = (data == this.data);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uiInfo != null)
        {
            PoolMgr.Instance.PushObj("UI/UIInfo", uiInfo);
        }

        if (data != null)
        {
            PoolMgr.Instance.GetObj("UI/UIInfo", (Obj) =>
            {
                Obj.transform.SetParent(this.transform);
                Obj.transform.localScale = Vector3.one;
                Obj.transform.localPosition = Vector3.zero;

                this.uiInfo = Obj;
                UIInfo ui = Obj.GetComponent<UIInfo>();
                ui.Setup(data.skillIntroduction);
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
