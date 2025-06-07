using UnityEngine;

public class WorldShift : MonoBehaviour
{
    public GameObject world;
    public GameObject hell;

    public AudioClip hellStart;
    public AudioClip hellHold;
    public AudioClip hellEnd;

    void Update()
    {
        if (!PauseManager.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SFXManager.instance.PlaySFXClip(hellStart, transform, 1f);
                SFXManager.instance.LoopSFXClip(hellHold, transform, 0.5f); // Start loop
            }

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

            if (Input.GetKeyUp(KeyCode.Space))
            {
                SFXManager.instance.StopLoop(); // Stop loop
                SFXManager.instance.PlaySFXClip(hellEnd, transform, 1f);
            }
        }
    }
}
