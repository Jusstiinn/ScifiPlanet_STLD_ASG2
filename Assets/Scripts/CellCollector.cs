using UnityEngine;

public class CellCollector : MonoBehaviour
{
    [HideInInspector] public int score = 0;
    [HideInInspector] public bool collectedCell = false;

    [Header("References")]
    public Camera playerCamera;
    public AudioSource collectSound;

    [Header("Settings")]
    public float interactDistance = 3f;

    private void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (collectSound == null)
        {
            collectSound = GetComponent<AudioSource>();
        }
    }

    public void OnInteract()
    {
        Debug.Log("Interact pressed");

        if (playerCamera == null)
            return;

        //cast ray from center and assign variable
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        
        // Check if the raycast hits an object within the interact distance
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);
            GameObject target = hit.collider.gameObject;

            //check for Cell tags on the object
            if (target.CompareTag("Cell"))
            {
                CollectCell(target);
            }
        }
    }

    // Collect cell function
    void CollectCell(GameObject cell)
    {
        // Prevent collecting the same cell again
        Collider col = cell.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Hide the cell visually
        MeshRenderer meshRenderer = cell.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        // Play collection sound
        if (collectSound != null && collectSound.clip != null)
        {
            collectSound.PlayOneShot(collectSound.clip);
        }

        // Update score
        score++;
        collectedCell = true;
        Debug.Log("Collected: " + score + " cells");

        // Destroy after sound finishes
        if (collectSound != null && collectSound.clip != null)
        {
            Destroy(cell, collectSound.clip.length);
        }
        else
        {
            Destroy(cell);
        }
    }
}
