using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var pin = collision.gameObject.GetComponent<Pin>();
        if (pin != null)
        {
            Destroy(transform.gameObject, 2f);
        }
    }

}
