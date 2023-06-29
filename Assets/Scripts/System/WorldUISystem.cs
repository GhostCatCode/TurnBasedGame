using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WorldUISystem : BaseSystem<WorldUISystem>
{
    [SerializeField] private Transform canvas;

    private void Start()
    {
        EventCenter.Instance.AddEventListener<Character>(E_Event_Type.OnCharacterCreated.ToString(), OnCharacterCreated);
    }

    protected override void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<Character>(E_Event_Type.OnCharacterCreated.ToString(), OnCharacterCreated);
    }

    private void OnCharacterCreated(Character character)
    {
        CreateBlood(character);
    }

    // ÏÔÊ¾½ÇÉ«ÑªÌõ
    public void CreateBlood(Character character)
    {
        PoolMgr.Instance.GetObj("UI/UIBlood", (Obj) =>
        {
            Obj.transform.SetParent(canvas);
            UIBlood ui = Obj.GetComponent<UIBlood>();
            ui.Setup(character);
        });
    }

    public void CreateTxt(Vector2 position, string text, Color color,float liveTime = 0.2f, bool isFade = true, bool isMove = true)
    {
        PoolMgr.Instance.GetObj("UI/UITxt", (Obj) =>
        {
            Obj.transform.SetParent(canvas);
            Obj.transform.position = position + Vector2.right * 0.5f;
            UITxt ui = Obj.GetComponent<UITxt>();
            ui.Setup(text, color, liveTime, isFade, isMove);
        });
    }
}
