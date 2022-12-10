using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossAI : MonoBehaviour
{
    public Transform target;
    Vector3 direction;
    Quaternion rotGoal;
    public float turnSpeed;
    public float speed = 20.0f;

    public bool isDead;

    public float minDist = 1f;
    public float maxdist = 3f;

    public GameObject projectile;
    public Transform firePoint;
    public float fireSpeed;
    public float health = 2f;

    public float jumpSpeed = 15f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float playerJump = 2;


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
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);
        //so long as the chaser is further away than the minimum distance, move towards it at rate speed.
        //if (distance < minDist && distance > maxdist)
        //{
        //    transform.position += transform.forward * speed * Time.deltaTime;
        //}
        if (distance < minDist)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        //If statement for the rotation of the enemy, to look at the player.
        if (distance < minDist)
        {
            direction = (target.position - transform.position).normalized;
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed * Time.deltaTime);
        }
        if(distance >= maxdist)
        {
            transform.position += transform.forward * jumpSpeed * Time.deltaTime;
            //velocity.y = Mathf.Sqrt(playerJump * -2f * gravity);
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {   //If the targets health = 0 destroy it and change the colour of it. 
            Die();
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
    //Enemy die
    public void Die()
    {
        Destroy(gameObject);
        isDead = true;
        
    }
}
