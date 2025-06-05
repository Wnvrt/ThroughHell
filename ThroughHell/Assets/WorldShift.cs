using UnityEngine;

public class WorldShift : MonoBehaviour
{
    public GameObject world;
    public GameObject hell;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            world.SetActive(false);
            hell.SetActive(true);
        }
        else
        {
            world.SetActive(true);
            hell.SetActive(false);
        }
    }
}
