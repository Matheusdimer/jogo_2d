using System.Collections;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    private static readonly int Coletado = Animator.StringToHash("coletado");
    private static readonly int Walking = Animator.StringToHash("walking");
    private static readonly int Dying = Animator.StringToHash("dying");
    private static readonly int Jumping = Animator.StringToHash("jumping");
    
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
    private bool _isDying = false;

    private Vector3 _spawn;
    
    private Renderer _renderer;

    private Collider2D _collider;

    public FixedJoystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        _spawn = transform.position;
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDying) {
            var horizontal = joystick.Horizontal;
            
            if (horizontal != 0)
            {
                animator.SetBool(Walking, true);
                sprite.flipX = horizontal < 0;
            } else
            {
                animator.SetBool(Walking, false);
            }

            transform.Translate(new Vector3(horizontal, 0, 0) * (Time.deltaTime * playerSpeed));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            rdb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_isDying) return;
        animator.SetBool(Jumping, true);
        _isGrounded = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            animator.SetBool(Jumping, false);
            _isGrounded = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }

        if (other.gameObject.CompareTag("DeadLine"))
        {
            Die();
        }
    }

    private IEnumerator Respawn()
    {
        Jump();
        animator.SetBool(Walking, false);
        animator.SetBool(Jumping, false);
        animator.SetBool(Dying, true);
        _collider.enabled = false;       
        _isDying = true;
        yield return new WaitForSeconds(2f);
        _isDying = false;
        _collider.enabled = true;
        animator.SetBool(Dying, false);
        transform.position = _spawn;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.GetComponent<Animator>().SetBool(Coletado, true);
            other.enabled = false;
            Destroy(other.gameObject, 1f);
            gameController.AddScore();
        }
        if (other.gameObject.CompareTag("Alavanca"))
        {
            gameController.PushAlavanca();
        }
        if (other.gameObject.CompareTag("EndGame"))
        {
            gameController.EndGame();
        }
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            _spawn = gameController.Checkpoint(other.gameObject);
        }
    }

    private void Die()
    {
        if (!_isDying)
        {
            var dead = gameController.Death();
            if (dead)
            {
                Destroy(this);
                return;
            }

            StartCoroutine(Respawn());
        }
    }
}
