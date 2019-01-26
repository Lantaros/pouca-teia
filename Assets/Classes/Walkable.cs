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
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            player.isOnSurface++;

            Debug.Log("Is on surface: " + player.isOnSurface);
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
        else if (other.tag == "WebButt")
        {
            PlayerController player = other.gameObject.transform.parent.GetComponent<PlayerController>();

            if (player.makingWeb)
            {
                player.makingWeb = false;
                player.EndWeb(other.gameObject.transform.position);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();

            player.isOnSurface--;

            Debug.Log("Is on surface: " + player.isOnSurface);
        }
        else if (other.tag == "WebButt")
        {
            PlayerController player = other.gameObject.transform.parent.GetComponent<PlayerController>();

            if (player.isOnSurface == 1
            && player.startingWeb)
            {
                Debug.Log("Starting web.");
                player.startingWeb = false;
                player.makingWeb = true;
                player.StartWeb();
                other.gameObject.transform.parent.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
    }
}
