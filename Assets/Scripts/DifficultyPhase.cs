using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyPhase : MonoBehaviour {

    float difficultyFactor = 1.15f;
    [SerializeField] int centipedeLength = 4;
    GameObject[] bodyCount;
    [SerializeField] public float cenMoveSpeed = 4f;
    

    [SerializeField] GameObject bodyPrefab;
    [SerializeField] GameObject headPrefab;
    

    [SerializeField] int centipedeParts; //debug

    private void Start()
    {
        
        StartCoroutine(SpawnCentipede());
    }

    public void CountCentipede()
    {
        centipedeParts++;
    }

    public void KillCentipedePart()
    {
        centipedeParts--;
        if (centipedeParts <= 0) //when centipede is killed starts spawning of the new centipede
        {
            RaiseDifficulty();
            StartCoroutine(SpawnNewCentipede());
        }
        
    }

    public void RaiseDifficulty()
    {
        cenMoveSpeed *= difficultyFactor;
        centipedeLength += 1;
    }

    public IEnumerator SpawnCentipede() //spawns centipede with given length
    {
        float spawnDelay = cenMoveSpeed / 40; //making sure centipede parts spawn on the same distance between each other
        bodyCount = new GameObject[centipedeLength]; //I tried to spawn centipede as an array to make the next object in array to appear as a new head, but didn't manage to do so yet
        Instantiate(headPrefab, new Vector2(0.5f, 23.5f), Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        for (int i=0; i<centipedeLength; i++) //spawn given number of centipede body parts
        {
            bodyCount[i] = Instantiate(bodyPrefab, new Vector2(-0.5f, 23.5f), Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public IEnumerator SpawnNewCentipede()
    {
        StartCoroutine(SpawnCentipede());
        yield return null;
    }


}
