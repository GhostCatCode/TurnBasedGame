using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 回合系统
public class TurnSystem : BaseSystem<TurnSystem>
{
    private int turnCnt;
    private E_Camp_Type currentTurn;
    private E_Camp_Type nextTurn;

    public E_Camp_Type CurrentTurn => currentTurn;

    private void Start()
    {
        currentTurn = E_Camp_Type.Player;
        EventCenter.Instance.EventTrigger<E_Camp_Type>(E_Event_Type.OnPrepareTurn.ToString(), currentTurn);
        EventCenter.Instance.EventTrigger<E_Camp_Type>(E_Event_Type.OnTurnStart.ToString(), currentTurn);
    }

    public void EndTurn(E_Camp_Type turn)
    {
        if (turn != currentTurn) return;

        EventCenter.Instance.EventTrigger<E_Camp_Type>(E_Event_Type.OnTurnEnd.ToString(), currentTurn);
        switch (currentTurn)
        {
            case E_Camp_Type.None:
                nextTurn = E_Camp_Type.Player;
                break;
            case E_Camp_Type.Player:
                nextTurn = E_Camp_Type.Neutral;
                break;
            case E_Camp_Type.Neutral:
                nextTurn = E_Camp_Type.Enemy;
                break;
            case E_Camp_Type.Enemy:
                nextTurn = E_Camp_Type.Player;
                turnCnt++;
                break;
        }

        StartCoroutine(StartTurn());
    }

    private IEnumerator StartTurn()
    {
        yield return new WaitForSeconds(0.2f);
        if (nextTurn == E_Camp_Type.Player)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 2f;
        }

        currentTurn = nextTurn;
        EventCenter.Instance.EventTrigger<E_Camp_Type>(E_Event_Type.OnPrepareTurn.ToString(), currentTurn);
        EventCenter.Instance.EventTrigger<E_Camp_Type>(E_Event_Type.OnTurnStart.ToString(), currentTurn);
    }

    //外部获取数据
    public int GetTurnCnt() => turnCnt;
    public E_Camp_Type GetCurrentTurn() => currentTurn;
}
