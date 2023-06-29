using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ButtonCharacter : MonoBehaviour
{
    public Character character;
    public Button btn;
    public Image img;
    public Image selected;
    public Text characterName;

    private void Start()
    {
        EventCenter.Instance.AddEventListener<Character>(E_Event_Type.OnSelectCharacter.ToString(), OnSelectCharacter);
        EventCenter.Instance.AddEventListener(E_Event_Type.OnChangedCharacter.ToString(), OnChangedCharacter);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<Character>(E_Event_Type.OnSelectCharacter.ToString(), OnSelectCharacter);
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnChangedCharacter.ToString(), OnChangedCharacter);
    }

    public void SetInfo(Character character)
    {
        this.character = character;
        characterName.text = character.Status.name;
        SpriteAtlas sa = ResMgr.Instance.Load<SpriteAtlas>("Sprite/SpriteAtlas");
        img.sprite = sa.GetSprite(character.Status.imgName);
        selected.enabled = false;

        btn.onClick.AddListener(() =>
        {
            CharacterActionSystem.Instance.SetSelectedCharacter(character);
        });
    }

    private void OnSelectCharacter(Character character)
    {
        selected.enabled = this.character == character;
    }

    private void OnChangedCharacter()
    {
        SpriteAtlas sa = ResMgr.Instance.Load<SpriteAtlas>("Sprite/SpriteAtlas");
        img.sprite = sa.GetSprite(character.Status.imgName);
    }

}
