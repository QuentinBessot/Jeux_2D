using UnityEngine;

public class Move_Players : MonoBehaviour
{
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;
    private float horizontalMovement;
    private float verticalMovement;

    public bool isJumping = false;
    public bool isGrounded;
    private bool canJump = true;  // Nouveau bool�en pour g�rer la capacit� � sauter
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundcheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D capsuleCollider;

    private Vector3 velocity = Vector3.zero;

    public static Move_Players instance;

    // Nouvelles variables pour les touches de d�placement
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode climbUpKey;
    public KeyCode climbDownKey;
    public KeyCode jumpKey;
    public KeyCode interact;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Move_Players dans la sc�ne");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        // R�initialiser les mouvements horizontaux et verticaux
        horizontalMovement = 0f;
        verticalMovement = 0f;

        // V�rifie les touches de d�placement horizontales seulement si le joueur n'est pas en train de grimper
        if (!isClimbing)
        {
            if (Input.GetKey(leftKey))
            {
                horizontalMovement = -moveSpeed * Time.fixedDeltaTime;
            }
            if (Input.GetKey(rightKey))
            {
                horizontalMovement = moveSpeed * Time.fixedDeltaTime;
            }
        }

        // V�rifie les touches de d�placement verticales seulement si le joueur est en train de grimper
        if (isClimbing)
        {
            if (Input.GetKey(climbUpKey))
            {
                verticalMovement = climbSpeed * Time.fixedDeltaTime;
            }
            if (Input.GetKey(climbDownKey))
            {
                verticalMovement = -climbSpeed * Time.fixedDeltaTime;
            }
        }

        // V�rifie la touche de saut
        if (Input.GetKeyDown(jumpKey) && isGrounded && canJump)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundcheckRadius, collisionLayers);
        Move(horizontalMovement, verticalMovement);
    }

    void Move(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            // Mouvement horizontal et saut lorsque le joueur n'est pas en train de grimper
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else
        {
            // Mouvement vertical uniquement si le joueur grimpe
            rb.velocity = new Vector2(0, _verticalMovement);
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            canJump = false;  // D�sactiver le saut lorsque le joueur est � proximit� d'une �chelle
        }
        if (collision.CompareTag("Dialogue"))
        {
            canJump = false;  // D�sactiver le saut lorsque le joueur est � proximit�
        }
        if (collision.CompareTag("Shop"))
        {
            canJump = false;  // D�sactiver le saut lorsque le joueur est � proximit�
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            canJump = true;  // R�activer le saut lorsque le joueur quitte la proximit� de l'�chelle
        }
        if (collision.CompareTag("Dialogue"))
        {
            canJump = true;  // R�activer le saut lorsque le joueur quitte la proximit�
        }
        if (collision.CompareTag("Shop"))
        {
            canJump = true;  // R�activer le saut lorsque le joueur quitte la proximit�
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundcheckRadius);
    }
}
