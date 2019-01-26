using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddColliderToLine()
    {
        LineRenderer line = this.GetComponent<LineRenderer>();

        //create the collider for the line
        BoxCollider2D lineCollider = this.gameObject.AddComponent<BoxCollider2D>();

        lineCollider.isTrigger = true;
        //set the collider as a child of your line
        lineCollider.transform.parent = line.transform;
        // get width of collider from line 
        float lineWidth = line.endWidth;
        // get the length of the line using the Distance method
        float lineLength = Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
        // size of collider is set where X is length of line, Y is width of line
        //z will be how far the collider reaches to the sky
        lineCollider.size = new Vector3(lineLength, lineWidth, 1f);
        // get the midPoint
        Vector3 midPoint = (line.GetPosition(0) + line.GetPosition(1)) / 2;
        // move the created collider to the midPoint
        lineCollider.transform.position = midPoint;


        // Following lines calculate the angle between startPos and endPos
        float angle = (Mathf.Abs(line.GetPosition(0).y - line.GetPosition(1).y) / Mathf.Abs(line.GetPosition(0).x - line.GetPosition(1).x));
        if ((line.GetPosition(0).y < line.GetPosition(1).y && line.GetPosition(0).x > line.GetPosition(1).x) || (line.GetPosition(1).y < line.GetPosition(0).y && line.GetPosition(1).x > line.GetPosition(0).x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        lineCollider.transform.Rotate(0, 0, angle);
    }
}
