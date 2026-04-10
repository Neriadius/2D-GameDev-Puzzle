using UnityEngine;
using UnityEngine.InputSystem;

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


    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;  
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

}
