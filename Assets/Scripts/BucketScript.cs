using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BucketScript : MonoBehaviour
{
    public string fishTag; // Tag to identify fish objects
    public ParticleSystem feedbackParticles; // Particle system for feedback
    public GameObject cashHandlerParent; // Parent object handling cash


    public float moveUpDistance = 100f; // Distance to move the canvas up
    public float moveUpDuration = 1f; // Duration for moving the canvas up
    public float resetDelay = 5f; // Delay before resetting the canvas position

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is a fish
        if (collision.gameObject.tag == fishTag)
        {


            print("Fish caught!");

            // Check if the fish object is still valid
            if (collision.gameObject != null)
            {
                // Disable the collider before destroying the fish
                Collider fishCollider = collision.gameObject.GetComponent<Collider>();
                if (fishCollider != null)
                {
                    fishCollider.enabled = false; // Disable collider
                }

                // Delay destruction to give Unity a chance to clean up physics interactions
                StartCoroutine(DestroyFish(collision.gameObject));

                // Play feedback particles
                if (feedbackParticles != null)
                {
                    feedbackParticles.Play();
                }
            }
        }
    }

    private IEnumerator DestroyFish(GameObject fish)
    {
        // Wait for one frame before destroying, allowing physics interactions to settle
        yield return new WaitForEndOfFrame();

        if (fish != null)
        {
            // Add the fish's worth to the cash handler, if available
            FishInfo fishInfo = fish.GetComponent<FishInfo>();
            CashHandler cashHandler = cashHandlerParent.GetComponent<CashHandler>();
            if (fishInfo != null && cashHandler != null)
            {
                cashHandler.cash += fishInfo.worth;

                if (fishInfo.Index == 8)
                {
                    print("Win");
                    SceneManager.LoadScene("WinScreen");
                }

            }

            // Deactivate the fish object
           Destroy(fish);
        }
    }

}
