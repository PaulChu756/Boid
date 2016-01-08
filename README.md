Paul Chu
AIE Year 2 Programming

I.0: Requirements Documentation
I.1: Description of the Problem
Name: Boids 
Problem Statement: Boids is an Artificial intelligence behavior for flocking/herding behavior. 
Problem Specification:  These behaviors show similar behavior in flocks of birds or herds of cows/zebras. This simulation has three main behaviors that show to replicate life like forms. 

I.2: Input Information
Name: Boids
Description: Input of cohesion, separation, and alignment sliders.
Type: Unity/Visual Studio
Range of Acceptable Values: Values were determined by the math. I multiply it with a small number with all the rules, so the program doesn’t freak/glitch out. 

I.3: Output Information
The program will output a flocking/herding behavior similar to birds or herds of cows.

I.4: User Interface Information
A GUI will display for user after they input information. After, the user can increase/decrease values on via sliders displayed.

II.0: Design Documentation
II.1: System Architecture Description
The program is split into two different scripts for organization. The main script in this project is BoidController.cs, this is the main part of the project, it handles all the rules of this project.


II.2: Information about the functions
File: BoidController.cs

Class:SpawnBoidControllerner
Data Members: MonoBehaviour

Name: boids
Description: A list of GameObject

Name: cohesionslider
Description: A slider

Name: separtionslider
Description: A slider

Name: alignmentslider
Description: A slider

Name: Target
Description: A Gameobject

Name: target
Description: A Toggle

Name: boid
Description: A public Gameobject

Name: Xmin, Ymin, Zmin, Xmax, Ymax, Zmax 
Description: Public integer that all equal zero. 

Name: v1, v2, v3, v4, center, pv
Description: v1 to v1 are all velocities, center is the center velocity, pv is the perceived velocity.


Function: Start
Description: Spawns all the boids as the user begins the program.

Function: Update
Description: Applies all the rules to each boid each frame, and checks if the escape key is pressed to exit out the program.

Function: l_Vel
Parameters: GameObject lv
Description: This is the Limited Velocity, if the boid passes this limit, it will change the velocity so the boids do not break the program.

Function: Bounding Box
Parameters: Gameobject boid
Description: Creates a bounding box for the boids, it will push them back if they hit the bounding area.

Function: Distance
Parameters: Vector3 pos and pos2
Description: Distance formula between two boids

Function: rule1
Parameters: Gameobject Cohesion
Description: Finds the center of mass between all boids that are nearby, and the boids will fly closely to the center of mass.

Function: rule2
Parameters: Gameobject Separation
Description: Sets a distance away from each boid using distance formula

Function: rule3
Parameters: Gameobject Alignment
Description: Boids will fly in the same general direction of each other, instead of flying as individuals.
File: Boid.cs
Class: Boid
Data Members: MonoBehaviour

Name: Vel
Description: Vector3 for velocity

Function: Mag
Parameters: Vector3 pos1
Description: Getting the magnitude by from position and direction / vector3

Function: Update
Description: Gets all the boid gameobjects to face correctly right, and checks if the escape key is pressed to exit program.


III.0: Implementation Documentation
III.1 Program Code
#File: BoidController.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoidController : MonoBehaviour
{
    public List<GameObject> boids = new List<GameObject>();
    public Slider cohesionslider;
    public Slider separtionslider;
    public Slider alignmentslider;
    public GameObject Target;
    public Toggle target;
    public GameObject boid;
    public float cFac, sFac, aFac, vLim;
    public int Xmin = 0, Ymin = 0, Zmin = 0, Xmax = 0, Ymax = 0, Zmax = 0; // Draws Box

    Vector3 v1;
    Vector3 v2;
    Vector3 v3;
    Vector3 v4;
    Vector3 center;
    Vector3 pv;

    /// <summary>
    /// create the list of boids in random positions
    /// </summary>
    [ContextMenu("populate")] // Matthew's code
    void Populate()
    {
        GameObject bird;
        for (int i = 0; i < 25; i++) // Control how many number of boids on Screen
        {
            bird = Instantiate(boid) as GameObject;
            float rX = Random.Range(0, 20);
            float rY = Random.Range(0, 20);
            float rZ = Random.Range(0, 20);
            bird.transform.position = new Vector3(rX, rY, rZ);
            boids.Add(bird);
        }
    }

    [ContextMenu("Unpopulate")] // Matthew's code
    void UnPopulate()
    {
        foreach(GameObject go in boids)
        {
            DestroyImmediate(go);
        }
        boids.Clear();
    }

    void Start()
    {
           GameObject bird;
           for (int i = 0; i < 25; i++) // Control how many number of boids on Screen
           {
               bird = Instantiate(boid) as GameObject;
               float rX = Random.Range(0, 20);
               float rY = Random.Range(0, 20);
               float rZ = Random.Range(0, 20);
               bird.transform.position = new Vector3(rX, rY, rZ);
               boids.Add(bird);
           }
    }

    void Update()
    {
        foreach (GameObject boid in boids)
        {
            v1 = rule1(boid) * cohesionslider.value * cFac; // Cohesion
            v2 = rule2(boid) * separtionslider.value * sFac;
            v3 = rule3(boid) * alignmentslider.value * aFac;
            v4 = boundingBox(boid);
            boid.GetComponent<Boid>().Vel = boid.GetComponent<Boid>().Vel + v1 + v2 + v3 + v4; // Vel + all rules
            l_Vel(boid);
            boid.transform.position = boid.transform.position + boid.GetComponent<Boid>().Vel; // Adding the pos + Vel
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void l_Vel(GameObject lv) // Velocity Limit
    {
        Vector3 velocity = lv.GetComponent<Boid>().Vel;
        if (velocity.magnitude > vLim)
        {
            velocity = velocity.normalized * vLim;
        }
    }


    Vector3 boundingBox(GameObject boid) // Of the box, it will use force to push back boids.
    {
        Vector3 v = Vector3.zero;
        

        if (boid.transform.position.x < Xmin)
        {
            v.x =  Xmin - boid.transform.position.x;
        }
        else if (boid.transform.position.x > Xmax)
        {
            v.x = Xmax - boid.transform.position.x;
        }
        if (boid.transform.position.y < Ymin)
        {
            v.y = Ymin - boid.transform.position.y;
        }
        else if (boid.transform.position.y > Ymax)
        {
            v.y = Ymax - boid.transform.position.y;
        }
        if (boid.transform.position.z < Zmin)
        {
            v.z = Zmin - boid.transform.position.z;
        }
        else if (boid.transform.position.z > Zmax)
        {
            v.z = Zmax - boid.transform.position.z;
        }
        return v;
    }

    public float Distance(Vector3 pos1, Vector3 pos2) // Distance Formula
    {
        return Mathf.Sqrt((pos2.x - pos1.x) * (pos2.x - pos1.x)
        + (pos2.y - pos1.y) * (pos2.y - pos1.y) + (pos2.z - pos1.z) * (pos2.z - pos1.z));
    }

    public Vector3 rule1(GameObject Cohesion) // Rule1  = COHESION!
    {
         // Center of mass
        if (target.isOn)
        {
            return (Target.transform.position - Cohesion.transform.position) / 100; // Moving 1% of boids to Target
        }

        else
            foreach (GameObject currentboid in boids)
            {
                if (currentboid != Cohesion)
                    center = center + currentboid.transform.position;
            }
        center = center / (boids.Count - 1); // Center of Mass
        return (center - Cohesion.transform.position) / 100; // Cohesion, Moving 1%
    }

   public Vector3 rule2(GameObject separtion)
    {
        Vector3 center = new Vector3(0,0,0);
        foreach (GameObject currentboid in boids)
        {
            if(currentboid != separtion)
            {
                if (Distance(currentboid.transform.position, separtion.transform.position) < 7) // Set Distance away from each other
                {
                    center = center - (currentboid.transform.position - separtion.transform.position);
                }
            }
        }
        return center;
    }

    public Vector3 rule3(GameObject Aligment)
    {
        
        foreach (GameObject currentboid in boids)
        {
            if (currentboid != Aligment)
                pv += currentboid.GetComponent<Boid>().Vel;
        }
        pv = pv / (boids.Count - 1); // Center of Mass
        return (pv - Aligment.GetComponent<Boid>().Vel) / 1; // if / by 8 is 1/8, if 100, it's 1/100. 1 is GREAT!
    }
}

#File: Boid.cs
class Boid:
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
