using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIBlood : MonoBehaviour
{
    public Character character;

    public GameObject healthBar;
    public Image fontBar;
    public Image backBar;

    private void LateUpdate()
    {
        if (character == null)
        {
            PoolMgr.Instance.PushObj("UI/UIBlood", gameObject);
            return;
        }

        transform.position = character.transform.position;
        float fillAmount = (float) character.Status.hp / character.Status.maxHp;
        healthBar.SetActive(fillAmount < 0.99);

        fontBar.fillAmount = fillAmount;
        backBar.fillAmount = Mathf.Lerp(backBar.fillAmount, fillAmount, Time.deltaTime * 10);
    }

    public void Setup(Character character)
    {
        this.character = character;
    }
}
