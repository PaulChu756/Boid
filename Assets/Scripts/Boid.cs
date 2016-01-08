using UnityEngine;
using System.Collections;
using System.Collections.Generic;

<<<<<<< HEAD
public class Boid : MonoBehaviour 
=======
public class Boid : MonoBehaviour
>>>>>>> origin/master
{

    public Vector3 Vel;

<<<<<<< HEAD

    float Mag(Vector3 pos1) // Magitude
    {
        return Mathf.Sqrt((pos1.x) * (pos1.x) + (pos1.y) * (pos1.y) + (pos1.z) * (pos1.z)); // Direction = Vector / mag
    }


    void Start()
    {
        //Vel = new Vector3(1f, 1f, 1f); // Sets all boids start Speed;
    }


=======
    float Mag(Vector3 pos1) // Magitude
    {
        return Mathf.Sqrt((pos1.x) * (pos1.x) + (pos1.y) * (pos1.y) + (pos1.z) * (pos1.z)); // Direction = Vector / mag // Size == mag = vector3 / direction
    }

>>>>>>> origin/master
    void Update()
    {
        transform.right = Vel.normalized;

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
<<<<<<< HEAD

  


=======
>>>>>>> origin/master
}
