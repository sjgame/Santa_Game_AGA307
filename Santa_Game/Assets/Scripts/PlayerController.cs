using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public GameObject player;
    public int playerHealh = 10;
    float playerSpeed = 12f;
    public float playerJump = 2;

    public Transform groundCheck;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private Vector3 destination;
    public Camera cam;
    public float damage = 1f;

    public int enemyCount;
    int totalEnemies;
    public float enemyChunck;
    
    public TMP_Text enemyText;

    public Slider healthBarSlider;

    public GameObject soul;


    //void Start()
    //{

        
    //}


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * playerSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(playerJump * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        healthBarSlider.value = playerHealh;

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        totalEnemies = enemyCount;
        enemyText.text = "Enemies Remaining: " + enemyCount.ToString(); /*+ "/" + totalEnemies.ToString();*/

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("Soul"))
            {
                print("Hit");
                Destroy(hit.transform.gameObject);
                //+1 to the soul counter
            }
        }

           

    }
    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        Enemy enemy = hit.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    void PickupSoul()
    {
        


    }

   
    //public void PlayerDamage(int damageAmount)
    //{
    //    playerHealh -= damageAmount;
    //}
   
}
