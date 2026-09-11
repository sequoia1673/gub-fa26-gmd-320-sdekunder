using UnityEngine;

public class GrapplePointScript : MonoBehaviour
{
    public float xpos;
    public float ypos;

    public Vector2 mousePosition;
    public Vector2 movementDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xpos = transform.position.x;
        ypos = transform.position.y;


        movementDirection.x = mousePosition.x - xpos;
        movementDirection.y = mousePosition.y - ypos;


        //if (moving)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, mousePosition * 10, Time.deltaTime * 50);
        //}

        transform.position = Vector3.MoveTowards(transform.position, mousePosition, Time.deltaTime * 50);

    }

    public void SetPos(float x, float y)
    {
        Vector2 newPos = new Vector2(x, y);

        transform.position = newPos;
        //transform.SetPositionAndRotation(newPos, Quaternion.identity);
    }

    public float GetXPos()
    {
        return xpos;
    }
    public float GetYPos()
    {
        return ypos;
    }
}
