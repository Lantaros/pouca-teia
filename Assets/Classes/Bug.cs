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
    public int isOnSurface;
    public AudioClip caughtSound;

    private float spawnY;
    private float destinationY;
    private float speed;
    private float passX;
    private string mode = "flying"; //possible values: flying, free and caught
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnY = Random.Range(spawnYLimits[0], spawnYLimits[1]);
        this.gameObject.transform.position = new Vector3(spawnX, spawnY, 0.0f);
        this.destinationY = Random.Range(destinationYLimits[0], destinationYLimits[1]);
        this.speed = Random.Range(speedLimits[0], speedLimits[1]);
        this.passX = Random.Range(spawnX, destinationX);

        this.audioSource = this.GetComponent<AudioSource>();
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
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1);
            }
            else
            {
                this.mode = "free";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && this.mode == "caught") {
            other.GetComponent<PlayerController>().Eat();
            Destroy(this.gameObject);
        }
    }
}
