using System.Collections;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving;

    public Vector2 ray_dir = Vector2.up;
    public LayerMask obstacleLayer;
    public LayerMask boxLayer;

    public void Update()
    {
        Vector2 origin = transform.position;
        float distance = 1f;
        origin.y = origin.y + 1f;

        // Check forward tile
        Debug.DrawRay(origin, ray_dir, Color.blue, 1f);
    }

    public bool TryPush(Vector2 dir)
    {
        if (isMoving)
        {
            Debug.Log("TryPush is " + false);
            return false;
        }

        /*Vector2 targetPos = (Vector2)transform.position + dir;

        // Check for wall
        Collider2D wall = Physics2D.OverlapCircle(targetPos, 0.2f, obstacleLayer);
        if (wall != null) {
            Debug.Log("TryPush is " + false + "obstacleLayer overlap");
            return false;
        }

        // Check for another box
        Collider2D otherBox = Physics2D.OverlapCircle(targetPos, 0.2f, boxLayer);
        if (otherBox != null) {
            Debug.Log("TryPush is " + false + "boxLayer overlap");
            return false; 
        }

        StartCoroutine(Move(targetPos));

        Debug.Log("TryPush is " + true);
        return true;*/

        ray_dir = dir;
        Vector2 origin = transform.position;
        float distance = 1f;
        origin.y = origin.y + 1f;

        // Check forward tile
        RaycastHit2D wallHit = Physics2D.Raycast(origin, ray_dir, distance, obstacleLayer);
        RaycastHit2D boxHit = Physics2D.Raycast(origin, ray_dir, distance, boxLayer);

        Debug.DrawRay(origin, ray_dir, Color.blue, 1f);

        if (wallHit.collider != null) {
            Debug.Log("TryPush is " + false + "obstacleLayer overlap");
            return false; 
        }
        if (boxHit.collider != null) {
            Debug.Log("TryPush is " + false + "boxLayer overlap");
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
