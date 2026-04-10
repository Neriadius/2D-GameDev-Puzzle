using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private const string IS_RUNNING = "IsRunning";
    private const string IS_FRONT = "IsFront";
    private const string IS_BACK = "IsBack";
    private const string IS_SIDE = "IsSide";


    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        animator.SetBool(IS_RUNNING, PlayerMovement.Instance.IsRunning());
        animator.SetBool(IS_FRONT, PlayerMovement.Instance.IsFront());
        animator.SetBool(IS_BACK, PlayerMovement.Instance.IsBack());
        animator.SetBool(IS_SIDE, PlayerMovement.Instance.IsSide());
        PlayerFlip();
    }

    void PlayerFlip()
    {
        if (PlayerMovement.Instance.IsSide())
        {
            float velocityX = PlayerMovement.Instance.GetVelocityX();

            if (velocityX > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (velocityX < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}
