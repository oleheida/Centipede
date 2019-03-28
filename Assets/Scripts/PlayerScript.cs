using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {



    [Header("Player")]
    [SerializeField] float padding = 0.5f; 
    [SerializeField] public float moveSpeed = 10f;
    [SerializeField] int health =3;
    

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject oldProjectilePrefab;
    [SerializeField] GameObject newProjectilePrefab;
    [SerializeField] public float projectileFiringPeriod = 0.1f;
    [SerializeField] float projectileSpeed = 15f;
    [Header("Sound")]
    [SerializeField] AudioClip playerShootingSound;
    [SerializeField] [Range(0, 1)] float shootingVolume = 0.5f;
    [SerializeField] AudioClip powerUpSound;


    float moveSpeedBonus = 10f;
    float shootSpeedBonus = 0.05f;
    int buffDuration = 3;

    Coroutine firingCoroutine;
    public float xMin;
    public float xMax;
    public float yMax;
    public float yMin;


    GameObject mSBuff;
    GameObject sSBuff;


    void Start () {
        SetUpBoundaries();
    }
	

	void Update () {
        Move();
        Fire();
    }

    public void SetUpBoundaries() //creating min and max positions on the screen
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move() //set up player movement
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; //Time.deltaTime to make the movement framerate independent
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); // prevent player from moving beyond the created boundaries via Mathf.Clamp 
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXpos, newYpos);

    }

    private void Fire() //starting corouting so the player shoots while spacebar or lctrl is pressed, stops on release
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously() //the corouting for firing projectile, instantiates projectile and sends it to the top every given period of time
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(playerShootingSound, Camera.main.transform.position, shootingVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //makes player lose health and reset his position after collision with centipede or spider
    {
        if (collision.gameObject.tag == "Centipede")
        {
            transform.position = new Vector2(15.5f, 1.5f);
            health--;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            transform.position = new Vector2(15.5f, 1.5f);
            health--;
        }
        if(health <= 0) //ends game when health is equal or below 0
        {
            EndGame();
        }
    }

    public int GetHealth() //it says everything itself
    {
        return health;
    }

    private void EndGame() //destroys player and loads end scene
    {
        FindObjectOfType<SceneLoader>().LoadEndScene();
        Destroy(gameObject);
    }


    #region buffs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "SpeedBuff")
        {
            AudioSource.PlayClipAtPoint(powerUpSound, Camera.main.transform.position, 0.5f);
            //moveSpeed += mSBuff.GetComponent<MoveSpeedBuff>().moveSpeedBonus; can't solve problem with finishing the code after destroying buff gameobject
            moveSpeed += moveSpeedBonus;
            StartCoroutine(buffTimerMS());
            Destroy(collision.gameObject);
            
        }
        else if(collision.gameObject.tag == "ShootBuff")
        {
            AudioSource.PlayClipAtPoint(powerUpSound, Camera.main.transform.position, 0.5f);
            projectileFiringPeriod -= shootSpeedBonus;
            projectilePrefab = newProjectilePrefab;
            StartCoroutine(buffTimerSS());
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "HealthBuff")
        {
            health+=1;
            Destroy(collision.gameObject);
        }
    }
    IEnumerator buffTimerMS()
    {
        yield return new WaitForSeconds(buffDuration);
        mSBuffUndo();
    }
    IEnumerator buffTimerSS()
    {
        yield return new WaitForSeconds(buffDuration);
        sSBuffUndo();
    }

    private void mSBuffUndo()
    {
        //moveSpeed -= mSBuff.GetComponent<MoveSpeedBuff>().moveSpeedBonus; can't solve problem with finishing the code after destroying buff gameobject
        moveSpeed -= moveSpeedBonus;
    }
    private void sSBuffUndo()
    {
        //moveSpeed -= mSBuff.GetComponent<MoveSpeedBuff>().moveSpeedBonus; can't solve problem with finishing the code after destroying buff gameobject
        projectilePrefab = oldProjectilePrefab;
        projectileFiringPeriod += shootSpeedBonus;
        
    }
}
#endregion buffs