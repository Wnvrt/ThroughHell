using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject mainCam;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mainCam.transform.position = new Vector3(mainCam.transform.position.x, gameObject.transform.position.y, mainCam.transform.position.z);
        }
    }
}
