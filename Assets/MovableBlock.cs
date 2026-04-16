using System.Collections;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving;

    public Vector2 ray_dir = Vector2.left;
    public LayerMask obstacleLayer;
    public LayerMask boxLayer;

    public void Update()
    {
        Vector2 origin = transform.position;
        origin.y = origin.y + 0.5f;

        Debug.DrawRay(origin, ray_dir, Color.yellow, 1f);
    }

    public bool TryPush(Vector2 dir)
    {
        if (isMoving)
        {
            Debug.Log("TryPush is " + false);
            return false;
        }

        ray_dir = dir;
        Vector2 origin = (Vector2)transform.position + dir * 0.55f;
        origin.y = origin.y + 0.5f;
        Debug.Log("TryPush target origin is in " + origin);
        float distance = 0.4f;

        // Check forward tile
        RaycastHit2D wallHit = Physics2D.Raycast(origin, ray_dir, distance, obstacleLayer);
        RaycastHit2D boxHit = Physics2D.Raycast(origin, ray_dir, distance, boxLayer);

        Debug.DrawRay(origin, ray_dir, Color.blue, 1f);

        if (wallHit.collider != null) {
            Debug.Log("TryPush is " + false + ", obstacleLayer overlap");
            return false; 
        }
        if (boxHit.collider != null) {
            Debug.Log("TryPush is " + false + ", boxLayer overlap");
            return false; 
        }

        Vector2 target = (Vector2)transform.position + dir;
        StartCoroutine(Move(target));

        return true;

    }

    public IEnumerator Move(Vector2 target)
    {
        isMoving = true;

        while ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
