using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    private bool collided;
    public float damage = 1f;
    public int health;
    public int damageAmount = 1;

    public GameObject prefab;
    //public GameObject prefab;

    public void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //health = GetComponent<PlayerController>().playerHealh;
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

        if(collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.playerHealh -= damageAmount;
        }
    }
}
