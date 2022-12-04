using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour

{
    public Transform spawnPoint;
    public GameObject prefab;
    bool enemySpawned;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (enemySpawned == false && other.gameObject.tag == "Player")
        {
            
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            enemySpawned = true;
            Destroy(gameObject);
        }
        
    }
}
