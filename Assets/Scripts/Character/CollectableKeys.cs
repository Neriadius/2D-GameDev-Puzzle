using Unity.VisualScripting;
using UnityEngine;

public class CollectableKeys : MonoBehaviour
{

    [SerializeField] private int keys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keys = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        KeysCollected();
    }

    public void KeysCollected()
    {
        if(keys == 3)
        {
            Debug.Log("All keys collected!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            Debug.Log("Key collected! Total keys: " + keys);
            Destroy(other.gameObject);
            keys++;
        }
    }
}
