using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Alavanca : MonoBehaviour
{

    [SerializeField]
    private Sprite activeSprite;

    [SerializeField]
    private TilemapRenderer tilemap;

    private SpriteRenderer _spriteRenderer;

    private Collider2D _colider;

    private bool _active = false;

    // Start is called before the first frame update
    void Start()
    {
        tilemap.enabled = false;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _colider = GetComponent<Collider2D>();
        Show(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeSprite()
    {
        if (!_active)
        {
            _active = true;
            _spriteRenderer.sprite = activeSprite;
            tilemap.enabled = true;
        }
    }

    public void Show(bool show)
    {
        _spriteRenderer.enabled = show;
        _colider.enabled = show;
    }
}
