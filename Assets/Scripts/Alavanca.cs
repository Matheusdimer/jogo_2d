using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{

    [SerializeField]
    private Sprite inactiveSprite;

    [SerializeField]
    private Sprite activeSprite;

    private SpriteRenderer spriteRenderer;

    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeSprite()
    {
        if (!active)
        {
            active = true;
            spriteRenderer.sprite = activeSprite;
        }
    }
}
