using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ShopSkillUI : MonoBehaviour
{
    public SkillData skillData;
    public Button btnBuy;
    public Text skillName;
    public Text info;
    public Text price;

    public void Setup(SkillData skillData)
    {
        this.skillData = skillData;
        skillName.text = skillData.name;
        info.text = skillData.skillIntroduction;
        price.text = skillData.price.ToString();
    }

    private void Start()
    {
        btnBuy.onClick.AddListener(() =>
        {
            if (ShoppingSystem.Instance.TryBuySkill(skillData))
            {
                ShoppingSystem.Instance.BuySkill(skillData);
                btnBuy.interactable = false;
            }
        });
    }
}
