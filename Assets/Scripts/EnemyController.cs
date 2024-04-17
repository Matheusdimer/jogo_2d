using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private bool _esquerda = true;

    private float _xInicial;
    
    [SerializeField]
    private float velocidade;
    
    [SerializeField]
    private float deslocamento;
    
    // Start is called before the first frame update
    void Start()
    {
        _xInicial = transform.position.x;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_esquerda)
        {
            _spriteRenderer.flipX = false;
            transform.Translate(new Vector2(- velocidade, 0) * Time.deltaTime);
            if (transform.position.x < _xInicial - deslocamento) {
                _esquerda = false;
            }
        }
        else
        {
            _spriteRenderer.flipX = true;
            transform.Translate(new Vector2(velocidade, 0) * Time.deltaTime);
            if (transform.position.x > _xInicial + deslocamento)
            {
                _esquerda = true;
            }
        }
    }
}
