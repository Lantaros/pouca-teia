using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        if (other.GetComponent<PlayerController>().makingWeb)
        {
            other.GetComponent<PlayerController>().makingWeb = false;
            other.GetComponent<PlayerController>().EndWeb(other.gameObject.transform.GetChild(1).transform.position);
        }

        other.GetComponent<PlayerController>().isOnSurface++;
        
        Debug.Log("Is on surface: " + other.GetComponent<PlayerController>().isOnSurface);
        other.GetComponent<Rigidbody2D>().gravityScale = 0;
        other.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        other.GetComponent<PlayerController>().isOnSurface--;

        Debug.Log("Is on surface: " + other.GetComponent<PlayerController>().isOnSurface);

        if(other.GetComponent<PlayerController>().isOnSurface == 1
            && other.GetComponent<PlayerController>().startingWeb)
        {
            Debug.Log("Starting web.");
            other.GetComponent<PlayerController>().startingWeb = false;
            other.GetComponent<PlayerController>().makingWeb = true;
            other.GetComponent<PlayerController>().StartWeb();
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
        }

    }
}
