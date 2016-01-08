using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{

    public Vector3 Vel;

    float Mag(Vector3 pos1) // Magitude
    {
        return Mathf.Sqrt((pos1.x) * (pos1.x) + (pos1.y) * (pos1.y) + (pos1.z) * (pos1.z)); // Direction = Vector / mag // Size == mag = vector3 / direction
    }

    void Update()
    {
        transform.right = Vel.normalized;

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
