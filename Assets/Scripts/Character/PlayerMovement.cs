using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private float minMoveInput = 0.1f;
    private bool isRunning = false;
    private bool isFront;
    private bool isBack;
    private bool isSide;

    private bool isMoving;
    private RaycastHit2D hit;
    private Vector2 facingDirection = Vector2.down;
    private Vector2 input;

    public LayerMask obstacleLayer;
    public LayerMask boxLayer;


    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        hit = Physics2D.Raycast(transform.position, facingDirection, 1f, boxLayer);

        Debug.DrawRay(transform.position, facingDirection * hit.distance, Color.red);


        
        float x = UnityEngine.Input.GetAxisRaw("Horizontal");
        float y = UnityEngine.Input.GetAxisRaw("Vertical");

        if (x != 0) y = 0;

        if (x != 0 || y != 0)
        {
            facingDirection = new Vector2(x, y);
            Debug.Log(facingDirection);
        }

        // Push action
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Input Space is recieved");
            TryInteract();
        }

        /*if (input.x != 0) input.y = 0;

        if (input != Vector2.zero)
        {
            facingDirection = input;
            AttemptMove(input);
        }*/
    }

    void TryInteract()
    {
        Debug.Log("TryInteract inited");
        Vector2 origin = transform.position;
        //float distance = 10f;

        //RaycastHit2D hit = Physics2D.Raycast(origin, facingDirection, distance, boxLayer);

        //Debug.DrawRay(origin, facingDirection * hit.distance, Color.red);

        if (hit.collider != null)
        {
            MovableBlock box = hit.collider.GetComponent<MovableBlock>();

            if (box != null)
            {
                if (box.TryPush(facingDirection))
                {
                    Debug.Log("TryPush inited");
                    //Vector2 playerTarget = (Vector2)transform.position + facingDirection;
                    //StartCoroutine(Move(playerTarget));
                }
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(moveInput.x) > minMoveInput || Mathf.Abs(moveInput.y) > minMoveInput)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }


        switch (moveInput)
        {
            case Vector2 v when v.y > 0:
                isFront = true;
                isBack = false;
                isSide = false;
                break;
            case Vector2 v when v.y < 0:
                isFront = false;
                isBack = true;
                isSide = false;
                break;
            case Vector2 v when Mathf.Abs(v.x) > minMoveInput:
                isFront = false;
                isBack = false;
                isSide = true;
                break;
            default:
                isFront = false;
                isBack = true;
                isSide = false;
                break;
        }
    }

    public bool IsRunning()
    {
            return isRunning;
    }

    public bool IsFront()
        {
            return isFront;
    }

    public bool IsBack()
    {
        return isBack;
    }

    public bool IsSide()
    {
        return isSide;
    }

    // PlayerMovement
    public float GetVelocityX()
    {
        return rb.linearVelocity.x;
    }

    void AttemptMove(Vector2 dir)
    {
        Vector2 targetPos = (Vector2)transform.position + dir;

        // Check for box in front
        Collider2D box = Physics2D.OverlapCircle(targetPos, 0.4f, boxLayer);

        if (box != null)
        {
            // Try to push box
            MovableBlock pushable = box.GetComponent<MovableBlock>();
            if (pushable != null)
            {
                pushable.TryPush(dir);
                
            }
            return;
        }
    }
}

