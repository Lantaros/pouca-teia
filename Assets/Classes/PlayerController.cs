using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UnityEngine.UI.Text debugText;

    public float speed;
    public AudioClip eatenSound;

    [HideInInspector]
    public int isOnSurface = 0;

    public GameObject webOriginal;

    public LayerMask layerMask;

    [HideInInspector]
    public GameObject web;
    [SerializeField]
    private float jump;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Animator animator;

    private bool canJump = true;

    [HideInInspector]
    public bool startingWeb = false;
    [HideInInspector]
    public bool makingWeb = false;

    [HideInInspector]
    public Vector3 movement;
    
    public void StartWeb(Vector3 StartPosition)
    {
        this.web.GetComponent<LineRenderer>().SetPosition(0, StartPosition);
    }

    public void EndWeb(Vector3 EndPosition)
    {
        this.web.GetComponent<LineRenderer>().SetPosition(1, EndPosition);
        if(!makingWeb)
        {
            this.web.GetComponent<Web>().AddColliderToLine();
        }
    }

    public void Eat()
    {
        this.audioSource.PlayOneShot(eatenSound);
    }

    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.animator = this.GetComponent<Animator>();
    }
    
    void Update()
    {
        debugText.text = this.isOnSurface.ToString();

        if (this.isOnSurface > 1)
        {
            this.movement = new Vector3(0.0f, 0.0f, 0.0f);
            if (Input.GetKey("a"))
            {
                this.movement += new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);
            }
            if (Input.GetKey("s"))
            {
                this.movement += new Vector3(0.0f, -speed * Time.deltaTime, 0.0f);
            }
            if (Input.GetKey("d"))
            {
                this.movement += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            }
            if (Input.GetKey("w"))
            {
                this.movement += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
            }
            if (Input.GetKeyDown("space") && canJump)
            {
                canJump = false;
                this.animator.SetTrigger("Jump");
                this.GetComponent<Rigidbody2D>().velocity = this.movement * jump;
                if(!makingWeb)
                {
                    this.startingWeb = true;
                    this.web = Instantiate(this.webOriginal);
                }


                StartCoroutine("CooldownJump");
            }

            this.gameObject.transform.position += this.movement;

            this.gameObject.transform.up = this.movement;

        }
        else
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
    /*        if (Input.GetKey("a"))
            {
                this.gameObject.transform.position += new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);
            }
            if (Input.GetKey("d"))
            {
                this.gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            }
            */
        }

        if (this.makingWeb)
        {
            this.EndWeb(this.gameObject.transform.GetChild(0).transform.position);
        }
    }

    IEnumerator CooldownJump()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(0.5f);
        canJump = true;
    }
}
