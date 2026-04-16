using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Transform cameraTarget;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player hit CameraTrigger");
        if (other.CompareTag("Player"))
        {
            CameraController.Instance.MoveTo(cameraTarget.position);
            Debug.Log("Moving camera");
        }
    }
}