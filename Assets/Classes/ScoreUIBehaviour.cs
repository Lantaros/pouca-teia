using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine;

public class ScoreUIBehaviour : MonoBehaviour
{
    public GameObject reference;
    public bool ready = true;
    public int points;

    private UnityEngine.UI.Text UIText;

    public void PlayMusic()
    {
        this.GetComponent<AudioSource>().Play();
    }

    void Start()
    {
        this.UIText = this.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.ready)
        {
            this.GetComponent<Image>().enabled = true;
            this.UIText.text = this.points + " bugs";
            this.transform.position += (this.reference.transform.position - this.transform.position) / 10.0f;

            if (Input.GetButton("Play"))
            {
                SceneManager.LoadScene(1);
                Bug.points = 0;
            }
        }
    }
}
