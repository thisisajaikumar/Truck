using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Truck"))
        {
            GameManager.canPickDebrisEvent.Invoke(true, transform.parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Truck"))
        {
            GameManager.canPickDebrisEvent.Invoke(false, null);
        }
    }
}
