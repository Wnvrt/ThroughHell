using UnityEngine;

public class PistonBlock : MonoBehaviour
{
    public float duration = 1f;
    public bool startOpen = true;

    private BoxCollider2D boxCol;
    private SpriteRenderer spriteRen;

    void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        PistonManager.Instance.Register(this, duration, startOpen);
        ApplyState(startOpen);
    }

    public void ApplyState(bool open)
    {
        if (boxCol != null)
            boxCol.enabled = open;

        if (spriteRen != null)
            spriteRen.color = open ? Color.green : Color.red;
    }
}