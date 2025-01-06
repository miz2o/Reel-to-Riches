using System.Collections;
using UnityEngine;

public class BucketScript : MonoBehaviour
{
    public string fishTag; // Tag to identify fish objects
    public ParticleSystem feedbackParticles; // Particle system for feedback
    public GameObject cashHandlerParent; // Parent object handling cash
    public GameObject caughtcanvas; // UI canvas to display on fish caught

    public float moveUpDistance = 100f; // Distance to move the canvas up
    public float moveUpDuration = 1f; // Duration for moving the canvas up
    public float resetDelay = 5f; // Delay before resetting the canvas position

    private void Start()
    {
        // Ensure the caughtcanvas starts inactive
        if (caughtcanvas != null)
        {
            caughtcanvas.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is a fish
        if (collision.gameObject.tag == fishTag)
        {
            caughtcanvas.SetActive(true); // Activate the canvas
            StartCoroutine(MoveCaughtCanvasUp()); // Start moving the canvas up

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
                }

            }

            // Deactivate the fish object
           Destroy(fish);
        }
    }

    private IEnumerator MoveCaughtCanvasUp()
    {
        RectTransform canvasRectTransform = caughtcanvas.GetComponent<RectTransform>();
        if (canvasRectTransform == null)
        {
            Debug.LogError("Caughtcanvas is not a UI element with a RectTransform.");
            yield break;
        }

        Vector3 startPosition = canvasRectTransform.anchoredPosition;
        Vector3 targetPosition = startPosition + new Vector3(0, moveUpDistance, 0);

        float elapsedTime = 0f;

        // Smoothly move the canvas up
        while (elapsedTime < moveUpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsedTime / moveUpDuration);
            canvasRectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Ensure the final position is exactly at the target
        canvasRectTransform.anchoredPosition = targetPosition;

        // Wait for the reset delay before resetting the canvas
        yield return new WaitForSeconds(resetDelay);

        StartCoroutine(ResetCaughtCanvas(startPosition));
    }


    private IEnumerator ResetCaughtCanvas(Vector3 originalPosition)
    {
        yield return new WaitForSeconds(resetDelay);

        RectTransform canvasRectTransform = caughtcanvas.GetComponent<RectTransform>();
        if (canvasRectTransform != null)
        {
            canvasRectTransform.anchoredPosition = originalPosition;
        }

        // Deactivate the canvas
        caughtcanvas.SetActive(false);
    }
}
