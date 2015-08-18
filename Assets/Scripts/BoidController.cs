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

    Vector3 v1 = Vector3.zero;
    Vector3 v2 = Vector3.zero;
    Vector3 v3 = Vector3.zero;
    Vector3 v4 = Vector3.zero;

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
            float rX = Random.Range(-10, 10);
            float rY = Random.Range(-10, 10);
            float rZ = Random.Range(-10, 10);
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
               float rX = Random.Range(0, 10);
               float rY = Random.Range(0, 10);
               float rZ = Random.Range(0, 10);
               bird.transform.position = new Vector3(rX, rY, rZ);
               boids.Add(bird);
           }
    }

    void Update()
    {
        foreach (GameObject cookies in boids)
        {
            v1 = rule1(cookies) * cohesionslider.value * cFac; // Cohesion
            v2 = rule2(cookies) * separtionslider.value * sFac;
            v3 = rule3(cookies) * alignmentslider.value * aFac;
            v4 = boundingBox(cookies);
            cookies.GetComponent<Boid>().Vel = cookies.GetComponent<Boid>().Vel + v1 + v2 + v3 + v4; // Vel + all rules
            l_Vel(cookies);
            cookies.transform.position = cookies.transform.position + cookies.GetComponent<Boid>().Vel; // Adding the pos + Vel

        }

    }
    void l_Vel(GameObject lv) // Velocity Limit
    {
        Vector3 panda = lv.GetComponent<Boid>().Vel;
        if (panda.magnitude > vLim)
        { 
            panda = panda.normalized * vLim;
        }
    }


    Vector3 boundingBox(GameObject bird) // Of the box, it will use force to push back boids.
    {
        Vector3 v = Vector3.zero;
        

        if (bird.transform.position.x < Xmin)
        {
            v.x =  Xmin - bird.transform.position.x;
        }
        else if (bird.transform.position.x > Xmax)
        {
            v.x = Xmax - bird.transform.position.x;
        }
        if (bird.transform.position.y < Ymin)
        {
            v.y = Ymin - bird.transform.position.y;
        }
        else if (bird.transform.position.y > Ymax)
        {
            v.y = Ymax - bird.transform.position.y;
        }
        if (bird.transform.position.z < Zmin)
        {
            v.z = Zmin - bird.transform.position.z;
        }
        else if (bird.transform.position.z > Zmax)
        {
            v.z = Zmax - bird.transform.position.z;
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
        Vector3 center = Vector3.zero; // Center of mass
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
        Vector3 pv = Vector3.zero;
        foreach (GameObject currentboid in boids)
        {
            if (currentboid != Aligment)
                pv += currentboid.GetComponent<Boid>().Vel;
        }
        pv = pv / (boids.Count - 1); // Center of Mass
        return (pv - Aligment.GetComponent<Boid>().Vel) / 1; // if / by 8 is 1/8, if 100, it's 1/100. 1 is GREAT! 
    }

  
}
