using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableKeys : MonoBehaviour
{

    [SerializeField] private int keys;
    [SerializeField] private bool allKeys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keys = 0;
        allKeys = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
    }

    public void KeysCollected()
    {
        if(keys >= 1)
        {
            Debug.Log("All keys collected!");
            allKeys = true;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            keys++;
            KeysCollected();
            Debug.Log("Key collected! Total keys: " + keys);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Exit") && allKeys)
        {
            Debug.Log("Level completed! You have collected all keys.");
            SceneManager.LoadScene("EndScreen");
        }
        else if (other.CompareTag("Exit") && !allKeys)
        {
            Debug.Log("You need to collect all keys before exiting!");
        }

        Debug.Log("Triggered with: " + other.name);
    }
}
