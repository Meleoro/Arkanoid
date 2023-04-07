using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetQuiTombe : MonoBehaviour
{
    
    void Update()
    {
        if (transform.position.y < -5)
        {
            transform.position = transform.position + new Vector3(0, 15, 0);
        }
    }
}
