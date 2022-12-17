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

    int soulCount = 0;
    int totalSouls;
    public TMP_Text soulText;

    public TMP_Text enemyText;

    public Slider healthBarSlider;

    public GameObject soul;
    public GameObject particlePrefab;
    public Transform firePoint;
    public Transform firePoint2;

    private float timeToFire;
    public float fireSpeed;
    
    public GameObject floatingTextPrefab;
    public GameObject tali;
    public GameObject tali2;
    public GameObject taliActor;
    public bool destroyTali;
    public GameObject taliParticles;
    public GameObject emitter;

    public GameObject handGun;
    bool canShoot;

    public GameObject projectile;
    public float projectileSpeed;

    public Animator iceAnim;
    public AudioSource gunShot;
    //Animator gunAnim;
    void Start()
    {
        //gunAnim = GetComponent<Animator>();
        floatingTextPrefab.SetActive(false);
        destroyTali = (false);
        handGun.SetActive(false);
        canShoot = false;
    }
    void Update()
    {
        //Player Movement 
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

        if (Input.GetButtonDown("Fire2"))
        {
            
            InstantiateProjectile(firePoint2);
        }
        void InstantiateProjectile(Transform firePoint2)
        {
            var projectileObj = Instantiate(projectile, firePoint2.position, Quaternion.identity) as GameObject;
            projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint2.position).normalized * projectileSpeed;
        }

        //Shoot Gun
        if (Input.GetButtonDown("Fire1") && Time.time >= timeToFire && canShoot == true)
        {
            timeToFire = Time.time + 1 / fireSpeed;
            gunShot.Play();
            Shoot();
            //gunAnim.SetTrigger("Shoot");
            GameObject party = Instantiate(particlePrefab, firePoint.transform.position, firePoint.transform.rotation);
            Destroy(party, 2f);
            
        }
        healthBarSlider.value = playerHealh;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        totalEnemies = enemyCount;
        enemyText.text = "Enemies Remaining: " + enemyCount.ToString(); /*+ "/" + totalEnemies.ToString();*/
        soulText.text = "X" + soulCount;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3))
        {
            if (Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("Soul"))
            {
                print("Hit");
                Destroy(hit.transform.gameObject);
                soulCount += 1;
                //+1 to the soul counter
            }
            if (hit.collider.CompareTag("Ice") && Input.GetKeyDown(KeyCode.E) && soulCount >= 5)
            {
                //print("Ice");
                iceAnim.SetTrigger("IceDrop");
            }
            if (hit.collider.CompareTag("Tali") && destroyTali == false)
            {
                
                floatingTextPrefab.SetActive(true);
                //print("hit");
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //tali.GetComponent<Animator>().enabled = false;
                    //tali2.GetComponent<Animator>().enabled = false;
                    
                    Destroy(tali, 2f);
                    Instantiate(taliParticles, tali.transform.position, emitter.transform.rotation);
                    destroyTali = true;
                    floatingTextPrefab.SetActive(false);
                }
            }
            else
            {
                floatingTextPrefab.SetActive(false);
            }
            if (hit.collider.CompareTag("Gun"))
            {
                print("hit");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.transform.gameObject);
                    handGun.SetActive(true);
                    canShoot = true;
                }
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
    
}
