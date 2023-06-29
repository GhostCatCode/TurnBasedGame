using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ShopStatusUI : MonoBehaviour
{
    public Character character;
    public Text characterName;
    public Image image;
    public Text hp;
    public Text maxAc;
    public Text maxSan;
    public Text atk;
    public Text def;
    public List<Text> skillNameList;

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_Event_Type.OnChangedCharacter.ToString(), OnChangedCharacter);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnChangedCharacter.ToString(), OnChangedCharacter);
    }

    public void Setup(Character selectedCharacter)
    {
        if (selectedCharacter == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
            character = selectedCharacter;
            UpdateStatus();
        }
    }

    private void UpdateStatus()
    {
        characterName.text = character.name;
        SpriteAtlas sa = ResMgr.Instance.Load<SpriteAtlas>("Sprite/SpriteAtlas");
        image.sprite = sa.GetSprite(character.Status.imgName);
        hp.text = character.Status.hp + "/" + character.Status.maxHp;
        maxAc.text = character.Status.maxAc.ToString();
        maxSan.text = character.Status.maxSan.ToString();
        atk.text = character.Status.atk.ToString();
        def.text = character.Status.def.ToString();

        for (int i = 0; i < skillNameList.Count; i++)
        {
            skillNameList[i].text = string.Empty;
        }
        List<SkillData> skills = character.SkillManager.Skills;
        for (int i = 0; i < skills.Count; i++)
        {
            skillNameList[i].text = skills[i].name;
        }
    }

    private void OnChangedCharacter()
    {
        UpdateStatus();
    }
}
