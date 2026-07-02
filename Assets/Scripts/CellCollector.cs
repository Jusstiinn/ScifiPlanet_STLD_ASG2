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
    [SerializeField] private KeyCode interactKey = KeyCode.E;

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

    private void Update()
    {
        if (playerCamera == null)
        {
            return;
        }

        // Create a raycast from center of player camera
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // Check if the object has the correct tag
            if (hit.collider.CompareTag("Cell"))
            {
                // Press E to collect
                if (Input.GetKeyDown(interactKey))
                {
                    CollectCell(hit.collider.gameObject);
                }
            }
        }
    }

    // Collect cell function
    void CollectCell(GameObject cell)
    {
        //get MeshRenderer and hide cell visually
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

        // Update score and collected boolean
        score += 1;
        collectedCell = true;
        Debug.Log("Collected: " + score + " cells");

        // Destroy the cell when collected
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
