using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour, IInteractable
{
    private GridPosition gridPosition;

    private void Start()
    {
        gridPosition = GridSystem.Instance.GetGridPosition(transform.position);
        GridSystem.Instance.SetInteractableOnGridPosition(gridPosition, this);
    }

    public void Interact(Character character)
    {
        // Ê¤Àû
        GameMain.Instance.GameVictory();
    }

    public bool IsCanInteract()
    {
        return true;
    }
}
