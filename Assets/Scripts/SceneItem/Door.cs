using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private GridPosition gridPosition;

    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject openDoor;
    [SerializeField] private GameObject closeDoor;

    private void Start()
    {
        gridPosition = GridSystem.Instance.GetGridPosition(transform.position);
        GridSystem.Instance.SetInteractableOnGridPosition(gridPosition, this);
        UpdateDoor();
    }

    public bool IsCanInteract()
    {
        return !GridSystem.Instance.TryGetCharacterOnGridPosition(gridPosition, out Character character);
    }

    public void Interact(Character character)
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            MusicMgr.Instance.PlaySound("doorOpen", false, null);
        }
        else
        {
            MusicMgr.Instance.PlaySound("doorClose", false, null);
        }
        UpdateDoor();
    }

    private void UpdateDoor()
    {
        GridSystem.Instance.SetIsUnStandable(gridPosition, !isOpen);
        openDoor.SetActive(isOpen);
        closeDoor.SetActive(!isOpen);
    }
}
