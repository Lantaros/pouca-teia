using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIBehaviour : MonoBehaviour
{
    public GameObject reference;
    public bool ready = true;
    public int points;

    private UnityEngine.UI.Text UIText;

    void Start()
    {
        this.UIText = this.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.ready)
        {
            this.UIText.text = this.points + " bugs";
            this.transform.position += (this.reference.transform.position - this.transform.position) / 10.0f;
        }
    }
}
