using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        // enemy health will always look at the main camera
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }
}
