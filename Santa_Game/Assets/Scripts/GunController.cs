using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Animator gunAnim;
    private float timeToFire;
    public float fireSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gunAnim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireSpeed;
            gunAnim.SetTrigger("Shoot");
        }
    }
}
