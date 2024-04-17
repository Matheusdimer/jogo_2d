using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class NewBehaviourScript : MonoBehaviour
{
    private static readonly int coletado = Animator.StringToHash("coletado");
    private static readonly int wallking = Animator.StringToHash("wallking");
    
    [SerializeField]
    private float jumpForce;
    
    [SerializeField]
    private float playerSpeed;
    
    [SerializeField]
    private Rigidbody2D rdb;
    
    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private Animator animator;

    private bool _isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        
        
        if (horizontal != 0)
        {
            animator.SetBool(wallking, true);
            sprite.flipX = horizontal < 0;
        } else
        {
            animator.SetBool(wallking, false);
        }

        transform.Translate(new Vector3(horizontal, 0, 0) * (Time.deltaTime * playerSpeed));

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rdb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isGrounded = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.GetComponent<Animator>().SetBool(coletado, true);
            Destroy(other.gameObject, 1f);
            gameController.AddScore();
        }
    }
}
