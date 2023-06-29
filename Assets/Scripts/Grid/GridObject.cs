using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridMap gridMap;
    private GridPosition gridPosition;

    private Character character;
    public IInteractable interactable;
    //public IDamageable damageable;

    private bool isUnStandable;

    public GridObject() { }
    public GridObject(GridMap gridMap, GridPosition gridPosition)
    {
        this.gridMap = gridMap;
        this.gridPosition = gridPosition;
    }

    public Character GetCharacter() => character;
    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    public IInteractable GetInteractable()
    {
        if (interactable != null && interactable.IsCanInteract())
        {
            return interactable;
        }
        return null;
    }
    public void SetInteractable(IInteractable interactable)
    {
        this.interactable = interactable;
    }

    //public IDamageable GetDamageable() => damageable;
    //public void SetIDanageable(IDamageable damageable)
    //{
    //    this.damageable = damageable;
    //}

    public bool IsUnStandable => isUnStandable;
    public void SetIsUnStandable(bool isUnStandable)
    {
        this.isUnStandable = isUnStandable;
    }
}
