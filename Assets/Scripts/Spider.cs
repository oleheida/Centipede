using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    [SerializeField] float currentSpeed = 3f;
    float spawnCounter;
    [SerializeField] float minTimebetweenSpawns = 1f;
    [SerializeField] float maxTimeBetweenSpawns = 4f;
    [SerializeField] GameObject mushroomPrefab;

    private void Start() 
    {
        spawnCounter = UnityEngine.Random.Range(minTimebetweenSpawns, maxTimeBetweenSpawns); //preventing zerg of spiders to be spawn at the same frame when mushrooms go below 20
    }

    void Update()
    {
        transform.Translate(Vector2.down * currentSpeed * Time.deltaTime); //making spider to move
        CountDownAndSpawnMushroom(); //prevents from spawning mushroom every frame
    }

    private void CountDownAndSpawnMushroom() //countdown for mushroom spawning
    {
        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0f)
        {
            SpawnMushroom();
            spawnCounter = UnityEngine.Random.Range(minTimebetweenSpawns, maxTimeBetweenSpawns);
        }
    }

    private void SpawnMushroom() //spawning mushrooms on countdown at the rounded position on grid
    {
        float spiderPosY = Mathf.Round(this.transform.position.y) +0.5f;
        Vector2 spawnedMushroomPos = new Vector2(transform.position.x, spiderPosY);
        Instantiate(mushroomPrefab, spawnedMushroomPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision) //destroyes spider 1 grid unit before the bottom of the screen
    {
        if(collision.name == "Spider Killer")
        {
            Destroy(gameObject);
        }
    }
}

