using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    Vector3 direction;
    Quaternion rotGoal;
    public float turnSpeed;

    public float speed = 20.0f;

    public float minDist = 1f;
    public float maxdist = 3f;

    public GameObject projectile;
    public Transform firePoint;
    
    private Vector3 destination;
    //private Vector3 player = GameObject.Find("Player");


    public float projectileSpeed = 30f;
    private float timeToFire;
    public float fireSpeed;

    public float health = 2f;


    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);
        //so long as the chaser is further away than the minimum distance, move towards it at rate speed.
        if (distance < minDist && distance > maxdist)
        {   
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        //If statement for the rotation of the enemy, to look at the player.
        if (distance < minDist)
        {
            direction = (target.position - transform.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
        }
        //If statment for when the enemy can shoot.
        if (distance < minDist && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireSpeed;
            Shoot();
        }
    }
    void Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile(firePoint);

        void InstantiateProjectile(Transform firePoint)
        {
            var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
            projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        }
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
