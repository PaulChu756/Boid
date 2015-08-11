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
    public GameObject boid;
    public GameObject Target;
    public float Cohesion = 0;
    public float Separtion = 0;
    public float Aligment = 0;
    public int Xmin = 0, Ymin = 0, Zmin = 0, Xmax = 0, Ymax = 0, Zmax = 0; // Draws Box

    Vector3 v1 = Vector3.zero;
    Vector3 v2 = Vector3.zero;
    Vector3 v3 = Vector3.zero;
    Vector3 v4 = Vector3.zero;

    Vector3 boundingBox(GameObject box) // Of the box, it will use force to push back boids.
    {
        Vector3 v = Vector3.zero;

        if (box.transform.position.x < Xmin)
        {
            v.x = 10;
        }
        else if (box.transform.position.x > Xmax)
        {
            v.x = -10;
        }
        if (box.transform.position.y < Ymin)
        {
            v.y = 10;
        }
        else if (box.transform.position.y > Ymax)
        {
            v.y = -10;
        }
        if(box.transform.position.z < Zmin)
        {
            v.z = 10;
        }
        else if (box.transform.position.z > Zmax)
        {
            v.z = -10;
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
        if (Target)
        {
            return (Target.transform.position - Cohesion.transform.position) / 100.0f;
        }

        else
            foreach (GameObject currentboid in boids)
            {
                if (currentboid != Cohesion)
                    center = center + currentboid.transform.position;
            }
        center = center / (boids.Count - 1); // Center of Mass
        return (center - Cohesion.transform.position) / 100.0f * 1.0f; // Cohesion, Moving 1% of 1%
    }

   public Vector3 rule2(GameObject separtion)
    {
        Vector3 center = Vector3.zero;
        foreach (GameObject currentboid in boids)
        {
            if(currentboid != separtion)
            {
                if (Distance(currentboid.transform.position, separtion.transform.position) < 10) // Set Distance away from each other
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
        return (pv - Aligment.GetComponent<Boid>().Vel) / 100;
    }

    void l_Vel(GameObject lv) // Velocity Limit
    {
        Vector3 panda = lv.GetComponent<Boid>().Vel;
        if(panda.magnitude > 2)
        {
            panda = panda.normalized * 0.15f;
        }
    }
 
    void Start()
    {
        GameObject bird2;
        for (int i = 0; i < 200; i++) // Control how many number of boids on Screen
        {
            bird2 = Instantiate(boid) as GameObject;
            boids.Add(bird2);
            transform.position = new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
        }
    }

    void Update()
    {
        foreach(GameObject cookies in boids)
        {
            v1 = rule1(cookies) * Cohesion * cohesionslider.value; // Cohesion
            v2 = rule2(cookies) * Separtion * separtionslider.value;
            v3 = rule3(cookies) * Aligment * alignmentslider.value;
            cookies.GetComponent<Boid>().Vel = cookies.GetComponent<Boid>().Vel + v1 + v2 + v3 + v4; // Velocity + the first rule = Cohesion
            l_Vel(cookies);
            cookies.transform.position += cookies.GetComponent<Boid>().Vel; // Adding the pos + Vel
            cookies.GetComponent<Boid>().Vel = cookies.GetComponent<Boid>().Vel.normalized * 0.15f; // Don't get rid of this, boids will start to circle
            boundingBox(cookies);

        }
        
    }
}
