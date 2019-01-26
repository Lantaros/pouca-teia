using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UnityEngine.UI.Text debugText;

    public float speed;
    [HideInInspector]
    public int isOnSurface = 0;

    public GameObject webOriginal;
    [HideInInspector]
    public GameObject web;
    [SerializeField]
    private float jump;

    private Rigidbody2D rb;

    [HideInInspector]
    public bool startingWeb = false;
    [HideInInspector]
    public bool makingWeb = false;

    [HideInInspector]
    public Vector3 movement;
    
    public void StartWeb()
    {
        this.web.GetComponent<LineRenderer>().SetPosition(0, this.gameObject.transform.GetChild(0).transform.position);
    }

    public void EndWeb(Vector3 EndPosition)
    {
        this.web.GetComponent<LineRenderer>().SetPosition(1, EndPosition);
        if(!makingWeb)
        {
            this.web.GetComponent<Web>().AddColliderToLine();
        }
    }

    void Start()
    {
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
            if (Input.GetKeyDown("space"))
            {
                //this.GetComponent<PlayerController>().isOnSurface = false;
                //this.GetComponent<Rigidbody2D>().gravityScale = 1;
                this.GetComponent<Rigidbody2D>().velocity = this.movement * jump;
                this.startingWeb = true;

                this.web = Instantiate(this.webOriginal);
            }

            this.gameObject.transform.position += this.movement;

            this.gameObject.transform.up = this.movement;

        }
        else
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
            if (Input.GetKey("a"))
            {
                this.gameObject.transform.position += new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);
            }
            if (Input.GetKey("d"))
            {
                this.gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            }
        }

        if (this.makingWeb)
        {
            this.EndWeb(this.gameObject.transform.GetChild(0).transform.position);
        }
    }
}
