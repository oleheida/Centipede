using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeNew : MonoBehaviour {

    [SerializeField]string moveDirection = "right";
    [SerializeField] GameObject mushroomPrefab;

    DifficultyPhase dPhase;

    private void Start()
    {
        CountCentipedeParts();
    }

    private void Update()
    {
        GameObject dPhase = GameObject.Find("DifficultyPhase");
        DifficultyPhase difficultyPhase = dPhase.GetComponent<DifficultyPhase>(); //refference to Difficulty Phase script
        Move(difficultyPhase);
        
    }

    private void CountCentipedeParts() //counts centipede objects using method from D Phase script
    {
        dPhase = FindObjectOfType<DifficultyPhase>(); 
        if(tag == "Centipede")
        {
            dPhase.CountCentipede();
        }
        
    }



    private void Move(DifficultyPhase difficultyPhase)
    {
        transform.Translate(Vector2.right * difficultyPhase.cenMoveSpeed * Time.deltaTime); //takes value for speed from Difficulty Phase script and 
        if (moveDirection == "right") //checks in which direction centipede goes and changes its rotation in case centipede will have sprite in future
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveDirection == "left")
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //changes centipede direction on collision with mushroom or wall
    {
        //Debug.Log("Collided with " + collision.gameObject.name);

        if(collision.gameObject.tag == "Mushroom" && moveDirection == "right" || collision.gameObject.tag == "Wall" && moveDirection == "right") 
        {
            transform.position = new Vector2(transform.position.x, transform.position.y -1f);
            moveDirection = "left";
        }
        else if (collision.gameObject.tag == "Mushroom" && moveDirection == "left" || collision.gameObject.tag == "Wall" && moveDirection == "left")
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1f); 
            moveDirection = "right";
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile") //if collides with projectile, instantiates a mushroom and raises difficulty from D Phase. Also rounds the current position for mushroom to be grid-like
        {
            float cPartPosX = Mathf.Round(transform.position.x)-0.5f;
            float cPartPosY = Mathf.Round(transform.position.y)-0.5f;
            Vector2 cPartPos = new Vector2 (cPartPosX, cPartPosY); 
            Instantiate(mushroomPrefab, cPartPos, Quaternion.identity);
            Destroy(gameObject);
            GameObject dPhase = GameObject.Find("DifficultyPhase");
            DifficultyPhase difficultyPhase = dPhase.GetComponent<DifficultyPhase>();
            difficultyPhase.KillCentipedePart();
        }
        if (collision.gameObject.tag == "Lose Trigger") //when reaches Lose Trigger on the bottom of the screen, loads end scene
        {
            FindObjectOfType<SceneLoader>().LoadEndScene();
        }
    }

    

    
}
