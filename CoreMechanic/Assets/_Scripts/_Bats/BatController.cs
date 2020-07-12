using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public int spawnBatAmount;
    public ProjectileBehaviour batPrefab;
    
    private HouseSpawner houseSpawner;
    private ProjectileBehaviour projectileInstance;
    private GameObject player;
    private float timer = 1f;

    private void Awake()
    {
        houseSpawner = GetComponent<HouseSpawner>();
        player = GameObject.Find("Player");
    }
    void Start()
    {
        GameMode2();
    }

    private void Update()
    {
        GameMode1();
    }

    void OnTargetReachedEvent(ProjectileBehaviour thisProjectile)
    {
        Vector2 playerPos = player.transform.position;
        Vector2 playerStep = playerPos - new Vector2(thisProjectile.gameObject.transform.position.x, thisProjectile.gameObject.transform.position.y);
        playerStep = playerStep.normalized * 20f;

        thisProjectile.SetTarget(playerStep);
    }
    
    public void GameMode2()
    {
        for (int i = 0; i < spawnBatAmount; i++)
        {
            SpawnSettings();
        }
    }

    public void GameMode1()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnSettings();
            timer = 2f;
        }
    }

    private void SpawnSettings()
    {
        projectileInstance = Instantiate(batPrefab, houseSpawner.houses[Random.Range(0, houseSpawner.houses.Count)], Quaternion.identity);
        projectileInstance.SetTarget(houseSpawner.houses[Random.Range(0, houseSpawner.houses.Count)]);
        projectileInstance.TargetReachedEvent += OnTargetReachedEvent;
    }
}
