using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float xpos;
    public float ypos;

    public Vector2 mousePositionOnClick;
    public int grapplePointToFireNext = 0;

    public GameObject[] GrapplePoints;
    const int GRAPLLE_POINT_NUM = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GrapplePoints = new GameObject[GRAPLLE_POINT_NUM];

        for (int i = 0; i < GRAPLLE_POINT_NUM; i++)
        {
            GrapplePoints[i] = Instantiate(Resources.Load<GameObject>("GrapplePoint"), Vector2.zero, Quaternion.identity);
            GrapplePoints[i].GetComponent<GrapplePointScript>().targetPosition = new Vector2(i, i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePostion();

        CheckMouse();
    }

    void UpdatePostion()
    {
        float newXPos = 0;
        float newYPos = 0;

        //averaging position of all grapple points
        for (int i = 0; i < GRAPLLE_POINT_NUM; i++)
        {
            newXPos += GrapplePoints[i].GetComponent<GrapplePointScript>().GetXPos();
            newYPos += GrapplePoints[i].GetComponent<GrapplePointScript>().GetYPos();
        }

        newXPos /= GRAPLLE_POINT_NUM;
        newYPos /= GRAPLLE_POINT_NUM;


        Vector3 newPos = new Vector2(newXPos, newYPos);

        //cool MoveTowards function! for "animating" in straight lines
        transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * 10); //https://discussions.unity.com/t/2d-mouse-point-click-movement-system-quick-tutorial/523253

        xpos = transform.position.x;
        ypos = transform.position.y;
    }

    void CheckMouse()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePositionOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); //https://discussions.unity.com/t/2d-mouse-point-click-movement-system-quick-tutorial/523253


            GrapplePoints[grapplePointToFireNext].GetComponent<GrapplePointScript>().targetPosition = mousePositionOnClick;
            GrapplePoints[grapplePointToFireNext].GetComponent<GrapplePointScript>().SetPos(xpos, ypos);
            GrapplePoints[grapplePointToFireNext].GetComponent<GrapplePointScript>().collided = false;
            grapplePointToFireNext++;
            if(grapplePointToFireNext >= GRAPLLE_POINT_NUM)
            {
                grapplePointToFireNext = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            mousePositionOnClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i < GRAPLLE_POINT_NUM; i++)
            {
                GrapplePoints[i].GetComponent<GrapplePointScript>().targetPosition = mousePositionOnClick;
                GrapplePoints[i].GetComponent<GrapplePointScript>().SetPos(xpos, ypos);
                GrapplePoints[grapplePointToFireNext].GetComponent<GrapplePointScript>().collided = false;
            }
        }
    }
}
