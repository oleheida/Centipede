using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField] int mushrooms; //debug
    [SerializeField] GameObject spiderPrefab;
    [SerializeField] GameObject mushroomPrefab;

    

    public void CountMushrooms() //make mushrooms count themselves
    {
        mushrooms++; 
    }
    public void MushroomDestroyed() 
    {
        mushrooms--;

        if (mushrooms <= 20)
        {
            SpawnSpider();
        }
        if (mushrooms <= 10)
        {
            StartCoroutine(SpawnMushrooms());
        }

    }

    IEnumerator SpawnMushrooms() //spawn mushrooms with delay of 1 sec untill there are 30 on the level
    {
        while (mushrooms <= 30)
        {
            float randomMushroomPosX = Mathf.Round(UnityEngine.Random.Range(0, 31)) + 0.5f;
            float randomMushroomPosY = Mathf.Round(UnityEngine.Random.Range(0, 23)) + 0.5f;
            Vector2 mushroomPos = new Vector2(randomMushroomPosX, randomMushroomPosY);
            Instantiate(mushroomPrefab, mushroomPos, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }

    private void SpawnSpider() //spawns spider when there are 20 or less mushrooms left
    {
        float randomSpiderX = Mathf.Round(UnityEngine.Random.Range(0, 31)) + 0.5f;
        Vector2 spiderRandomPos = new Vector2(randomSpiderX, 24.5f);
        Instantiate(spiderPrefab, spiderRandomPos, Quaternion.identity);
    }
}
