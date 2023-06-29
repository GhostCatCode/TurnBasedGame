using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISystem : BaseSystem<AISystem>
{
    private E_Camp_Type campType;
    private List<Character> characters;
    private int characterIndex;
    private bool isBusy;

    private void Start()
    {
        EventCenter.Instance.AddEventListener<E_Camp_Type>(E_Event_Type.OnTurnStart.ToString(), OnTurnStart);
    }

    private void Update()
    {
        if (isBusy) return;

        SetBusy();
        TryCharacterAI();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventCenter.Instance.RemoveEventListener<E_Camp_Type>(E_Event_Type.OnTurnStart.ToString(), OnTurnStart);
    }

    private void OnTurnStart(E_Camp_Type campType)
    {
        if (campType == E_Camp_Type.Player) return;

        this.campType = campType;
        characters = CharacterSystem.Instance.GetcharacterListOnCampType(campType);
        characterIndex = 0;
        ClearBusy();
    }

    private void TryCharacterAI()
    {
        if (characters != null && characterIndex < characters.Count && characters[characterIndex] != null)
        {
            characters[characterIndex].AutoAction(() => {
                ClearBusy();
                characterIndex++;
            });
        }
        else
        {
            TurnSystem.Instance.EndTurn(campType);
        }
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    private void SetBusy()
    {
        isBusy = true;
    }
}
