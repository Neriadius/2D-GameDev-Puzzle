using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public float smoothSpeed = 3f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Awake()
    {
        Instance = this;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    public void MoveTo(Vector3 newPosition)
    {
        targetPosition = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        isMoving = true;
    }
}