using UnityEngine;

public class DissableDoor : MonoBehaviour
{
    GameObject[] doors;
    GameObject[] doors2;

    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
        doors2 = GameObject.FindGameObjectsWithTag("Door 2");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plate"))
        {
            SetDoorsActive(false);
        }

        if (other.CompareTag("Plate 2"))
        {
            SetDoors2Active(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plate"))
        {
            SetDoorsActive(true);
        }

        if (other.CompareTag("Plate 2"))
        {
            SetDoors2Active(true);
        }
    }

    void SetDoorsActive(bool state)
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(state);
        }
    }
    void SetDoors2Active(bool state)
    {
        foreach (GameObject door in doors2)
        {
            door.SetActive(state);
        }
    }
}
