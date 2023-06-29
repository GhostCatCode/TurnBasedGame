using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public ItemData itemData;
    public Button btnBuy;
    public Image image;
    public Text itemName;
    public Text info;
    public Text price;

    public void Setup(ItemData itemData)
    {
        this.itemData = itemData;
        SpriteAtlas sa = ResMgr.Instance.Load<SpriteAtlas>("Sprite/SpriteAtlas");
        image.sprite = sa.GetSprite(itemData.spriteName);
        itemName.text = itemData.name;
        info.text = itemData.introduction;
        price.text = itemData.price.ToString();
    }

    private void Start()
    {
        btnBuy.onClick.AddListener(() =>
        {
            if (ShoppingSystem.Instance.TryBuyItem(itemData))
            {
                ShoppingSystem.Instance.BuyItem(itemData);
                btnBuy.interactable = false;
            }
        });
    }
}
