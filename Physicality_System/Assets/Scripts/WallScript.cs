using UnityEngine;

public class WallScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit!");
        collision.gameObject.GetComponent<GrapplePointScript>().collided = true;
    }
}
