using UnityEngine;

public class PigTrig : MonoBehaviour
{

    public Triggers triggers;
    public GameObject pig;
    public GameObject T3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("T3"))
        {
            pig.gameObject.SetActive(false);
            triggers.pigMoving = false;
            T3.SetActive(false);
            triggers.pigMoving = false;
        }
    }
}
