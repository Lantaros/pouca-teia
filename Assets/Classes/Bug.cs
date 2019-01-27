using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [Header("Dependencies")]
    public float spawnX;
    public float[] spawnYLimits;
    public float destinationX;
    public float[] destinationYLimits;
    public float[] speedLimits;
    [HideInInspector]
    public int isOnSurface;
    public AudioClip caughtSound;
    public GameObject score;
    [HideInInspector]
    public static int bugNumber = 0;
    [HideInInspector]
    public static int points = 0;

    private float spawnY;
    private float destinationY;
    private float speed;
    private float passX;
    private string mode = "flying"; //possible values: flying, free and caught
    private AudioSource audioSource;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnY = Random.Range(spawnYLimits[0], spawnYLimits[1]);
        this.gameObject.transform.position = new Vector3(spawnX, spawnY, 0.0f);
        this.destinationY = Random.Range(destinationYLimits[0], destinationYLimits[1]);
        this.speed = Random.Range(speedLimits[0], speedLimits[1]);
        this.passX = Random.Range(spawnX + 5, destinationX - 5);

        this.audioSource = this.GetComponent<AudioSource>();
        this.animator = this.GetComponent<Animator>();

        bugNumber++;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.mode != "caught")
        {
            this.transform.position += 
                Vector3.Normalize(new Vector3(destinationX - spawnX, destinationY - spawnY, 0.0f))
                * speed 
                * Time.deltaTime;
        }

        if (this.transform.position.x >= this.passX && this.mode == "flying")
        {
            if (this.isOnSurface == 1)
            {
                this.mode = "caught";
                this.audioSource.PlayOneShot(this.caughtSound);
                this.animator.SetTrigger("Caught");
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1);
            }
            else
            {
                this.mode = "free";
            }
        }

        if (this.transform.position.x >= this.destinationX)
        {
            Destroy(this.gameObject);
            bugNumber--;

            if (bugNumber == 0)
            {
                this.score.GetComponent<ScoreUIBehaviour>().ready = true;
                this.score.GetComponent<ScoreUIBehaviour>().points = points;
                this.score.GetComponent<ScoreUIBehaviour>().PlayMusic();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && this.mode == "caught") {
            other.GetComponent<PlayerController>().Eat();
            Destroy(this.gameObject);
            bugNumber--;
            points++;

            if (bugNumber == 0)
            {
                this.score.GetComponent<ScoreUIBehaviour>().ready = true;
                this.score.GetComponent<ScoreUIBehaviour>().points = points;
                this.score.GetComponent<ScoreUIBehaviour>().PlayMusic();
            }
        }
    }
}
