using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    private bool collided;
    public float damage = 1f;

    
    public GameObject prefab;

    private void OnCollisionEnter(Collision collision)
    {
        //If the projectile collides with anything other then the projectile or player it will be destroyed
        if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Enemy" && !collided)
        {
            //Instantiates the projectiles prefab, position and rotation.
            //GameObject snowball = Instantiate(prefab, transform.position, transform.rotation);
            //Destroy(snowball, 2f);

            //When the projectile collides with anything other then another projectile or payer, it will be destroyd.
            collided = true;
            Destroy(gameObject);
        }
    }
}
