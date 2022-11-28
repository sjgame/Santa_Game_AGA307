using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    //sets the distance the object will move
    [SerializeField] float distanceToCover;
    //sets the speed the object will move
    [SerializeField] float speed;
    //sets where the object will start moving from 
    private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 x = startingPosition;
        x.y += distanceToCover * Mathf.Sin(Time.time * speed);
        transform.position = x;
    }
}
