using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static EventCenter;

public class EventCenter : BaseManager<EventCenter>
{
    public interface IEventInfo { }
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }

    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (!eventDic.ContainsKey(name))
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
        else
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
    }

    public void AddEventListener(string name, UnityAction action)
    {
        if (!eventDic.ContainsKey(name))
        {
            eventDic.Add(name, new EventInfo(action));
        }
        else
        {
            (eventDic[name] as EventInfo).actions += action;
        }
    }

    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }

    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }

    public void EventTrigger<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
        }
    }

    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions?.Invoke();
        }
    }

    public void Clear()
    {
        eventDic.Clear();
    }
}

/*
 * 固定事件
 * 
 * 准备回合开始 OnPrepareTurn
 * 回合开始 OnTurnStart
 * 回合结束 OnTurnEnd
 * 选择技能 OnSelectSkill
 * 
 */

public class OnTurnArgs
{
    public E_Camp_Type turnType;
}
