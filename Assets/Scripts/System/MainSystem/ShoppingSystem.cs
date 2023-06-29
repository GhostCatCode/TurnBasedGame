using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingSystem : BaseSystem<ShoppingSystem>
{
    private List<ItemData> itemDatas;
    public List<ItemData> ItemDatas => itemDatas;

    private List<SkillData> skillDatas;
    public List<SkillData> SkillDatas => skillDatas;

    private List<Character> characters;
    public List<Character> CharacterDatas => characters;

    public void UpdateShoppingData()
    {
        characters = CharacterSystem.Instance.GetcharacterListOnCampType(E_Camp_Type.Player);
        skillDatas = SkillSystem.Instance.GetShopSkillList(3);
        itemDatas = ItemSystem.Instance.GetShopItemList(3);
    }

    public bool TryBuySkill(SkillData skillData)
    {
        if (skillData.price > PlayerBagSystem.Instance.GoldCnt)
        {
            return false;
        }

        // 技能不可以大于6个
        if (CharacterActionSystem.Instance.SelectedCharacter?.SkillManager.Skills.Count >= 6)
        {
            return false;
        }

        return true;
    }

    public void BuySkill(SkillData skillData)
    {
        PlayerBagSystem.Instance.SpendGold(skillData.price);
        CharacterActionSystem.Instance.SelectedCharacter.SkillManager.AddSkill(skillData);
    }

    public bool TryBuyItem(ItemData itemData)
    {
        if (itemData.price > PlayerBagSystem.Instance.GoldCnt)
        {
            return false;
        }
        return PlayerBagSystem.Instance.TryAddItem(itemData);
    }

    public void BuyItem(ItemData itemData)
    {
        PlayerBagSystem.Instance.SpendGold(itemData.price);
        PlayerBagSystem.Instance.AddItem(itemData);
    }
}
