using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {

    [SerializeField] int timesHit; //for debug
    [SerializeField] Sprite[] damageSprite; //array of sprites for damage levels
    [SerializeField] List<GameObject> buffPrefabs;
    [SerializeField] int scoreValue = 1;
    [SerializeField] AudioClip mushroomDestructionSound;

    Level level;

    private void Start()
    {
        CountMushrooms();
    }

    private void CountMushrooms()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Mushroom")
        {
            level.CountMushrooms(); //counting mushrooms present on the level
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        HandleHit(projectile);
    }

    private void HandleHit(Projectile projectile) //takes hit and checks if exceeds the max hit value, if not shows next sprite in damage sprites array, if yes, calling destroy method
    {
        int maxHits = damageSprite.Length;
        timesHit++;
        if (projectile != null) { projectile.Hit(); }
        
        if (maxHits <= timesHit)
        {
            DestroyMushroom();
        }
        else
        {
            ShowNextSprite(); 
        }
    }
    
    private void ShowNextSprite()  //shows next sprite
    {
        int spriteIndex = timesHit; 
        if (damageSprite[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = damageSprite[spriteIndex]; 
        }
    }

    private void DestroyMushroom() //destroys mushroom and instantiates random buff
    {
        level.MushroomDestroyed();

        if (Random.value > 0.8f) //rolling random value for buff
        {
            int prefabIndex = Random.Range(0, buffPrefabs.Count); //choosing random buff from buff list
            Instantiate(buffPrefabs[prefabIndex], transform.position, Quaternion.identity); //instantiating chosen buff
            AudioSource.PlayClipAtPoint(mushroomDestructionSound, Camera.main.transform.position, 0.1f); //deathsound
            FindObjectOfType<GameSession>().AddToScore(scoreValue); //adding scoreValue to score int in GameSession
            Destroy(gameObject);
        }
        else
        {
            FindObjectOfType<GameSession>().AddToScore(scoreValue); //adding score value to score in Game Session object
            AudioSource.PlayClipAtPoint(mushroomDestructionSound, Camera.main.transform.position, 0.4f);
            Destroy(gameObject);
        }
    }

}
