using System.Collections; // Make sure this line is included at the top
using UnityEngine;

public class BucketScript : MonoBehaviour
{
    public string fishTag;
    public ParticleSystem feedbackParticles;
    public GameObject cashHandlerParent;

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

        // After waiting, destroy the fish object


        if (fish != null)
        {

            if(fish.GetComponent<FishInfo>() != null && cashHandlerParent.GetComponent<CashHandler>() != null)
            {
                cashHandlerParent.GetComponent<CashHandler>().cash = fish.GetComponent<FishInfo>().worth;
            }




            fish.SetActive(false);

        }
    }
}
