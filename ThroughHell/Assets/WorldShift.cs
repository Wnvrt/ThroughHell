using UnityEngine;

public class WorldShift : MonoBehaviour
{
    public GameObject world;
    public GameObject hell;

    public AudioClip hellStart;
    public AudioClip hellHold;
    public AudioClip hellEnd;

    public SpriteRenderer eyes;

    void Update()
    {
        if (!PauseManager.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SFXManager.instance.PlaySFXClip(hellStart, transform, 1f);
                SFXManager.instance.LoopSFXClip(hellHold, transform, 0.5f); // Start loop
                eyes.color = new Color(201f / 255f, 11f / 255f, 0f, 1f);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 175f / 255f, 175f / 255f, 1f);
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
                eyes.color = new Color(0f, 155f / 255f, 201f / 255f, 1f);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            }
        }
    }
}
