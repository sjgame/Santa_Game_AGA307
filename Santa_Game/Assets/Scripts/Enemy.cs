using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;

    public float speed = 20.0f;

    public float minDist = 1f;
    public float maxdist = 3f;

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
        // face the target
        transform.LookAt(target);
        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);
        //so long as the chaser is further away than the minimum distance, move towards it at rate speed.
        if (distance < minDist && distance > maxdist)
            transform.position += transform.forward * speed * Time.deltaTime;
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
