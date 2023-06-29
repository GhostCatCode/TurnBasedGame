using UnityEngine;

public class SpriteOutline : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Color color;
    [SerializeField] private bool outline;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        outline = false;
    }

    private void Update()
    {
        Color newColor = color;
        bool newOutline = outline;
        if (character.IsActionable && TurnSystem.Instance.CurrentTurn == E_Camp_Type.Player)
        {
            newOutline = true;
            newColor = Color.white;
            if ( CharacterActionSystem.Instance?.SelectedCharacter == character )
            {
                newColor = Color.green;
            }
        }
        else
        {
            newOutline = false;
        }

        if (newOutline != outline || newColor != color)
        {
            outline = newOutline;
            color = newColor;
            UpdateOutline(outline);
        }
    }

    private void OnDestroy()
    {
        UpdateOutline(false);
    }

    private void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}
