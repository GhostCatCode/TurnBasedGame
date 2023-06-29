using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ShoppingPanel : BasePanel
{
    private List<ItemData> itemDatas;
    public Transform shopItemParent;
    private List<SkillData> skillDatas;
    public Transform shopSkillParent;
    private List<Character> characters;
    public Transform characterParent;

    public ShopStatusUI shopStatusUI;

    public Button btnNext;

    protected override void Init()
    {
        ShoppingSystem.Instance.UpdateShoppingData();

        characters = ShoppingSystem.Instance.CharacterDatas;
        for(int i = 0; i < characters.Count; i++)
        {
            GameObject obj = ResMgr.Instance.Load<GameObject>("UI/btnCharacter");
            obj.transform.SetParent(characterParent, false);
            obj.GetComponent<ButtonCharacter>().SetInfo(characters[i]);
        }

        skillDatas = ShoppingSystem.Instance.SkillDatas;
        for (int i = 0; i < skillDatas.Count; i++)
        {
            GameObject obj = ResMgr.Instance.Load<GameObject>("UI/ShopSkillUI");
            obj.transform.SetParent(shopSkillParent, false);
            obj.GetComponent<ShopSkillUI>().Setup(skillDatas[i]);
        }

        itemDatas = ShoppingSystem.Instance.ItemDatas;
        for (int i = 0; i < itemDatas.Count; i++)
        {
            GameObject obj = ResMgr.Instance.Load<GameObject>("UI/ShopItemUI");
            obj.transform.SetParent(shopItemParent, false);
            obj.GetComponent<ShopItemUI>().Setup(itemDatas[i]);
        }

        EventCenter.Instance.AddEventListener<Character>(E_Event_Type.OnSelectCharacter.ToString(), OnSelectCharacter);
        EventCenter.Instance.AddEventListener(E_Event_Type.OnChangedCharacter.ToString(), OnChangedCharacter);
        shopStatusUI.gameObject.SetActive(false);

        btnNext.onClick.AddListener(() =>
        {
            PoolMgr.Instance.Clear();
            GameDataMgr.Instance.LoadData();
            SceneMgr.Instance.LoadScene("GameScene", null);
            UIMgr.Instance.HidePanel<ShoppingPanel>();
        });
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<Character>(E_Event_Type.OnSelectCharacter.ToString(), OnSelectCharacter);
        EventCenter.Instance.RemoveEventListener(E_Event_Type.OnChangedCharacter.ToString(), OnChangedCharacter);
    }

    private void OnChangedCharacter()
    {
        Character character = CharacterActionSystem.Instance.SelectedCharacter;
        if (character == null)
        {
            shopStatusUI.gameObject.SetActive(false);
        }
        else
        {
            shopStatusUI.gameObject.SetActive(true);
            shopStatusUI.Setup(character);
        }
    }

    private void OnSelectCharacter(Character character)
    {
        if (character == null)
        {
            shopStatusUI.gameObject.SetActive(false);
        }
        else
        {
            shopStatusUI.gameObject.SetActive(true);
            shopStatusUI.Setup(character);
        }
    }
}
