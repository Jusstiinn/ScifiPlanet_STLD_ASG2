using UnityEngine;

public class HideFloorScript : MonoBehaviour
{
    public CellCollector cellCollector;
    public GameObject deleteObjects;
    public GameObject blockExit;
    [SerializeField] AudioSource metalGroan;
    bool hasHiddenFloor;

    // To hide the floor when collected is true
    void LateUpdate()
    {
        if (!hasHiddenFloor && cellCollector != null && cellCollector.collectedCell)
        {
            blockExit.SetActive(true);
            StartCoroutine(WaitBeforeDelete());
        }
    }

    //coroutine to wait for the metal groan sound to finish before deleting the objects
    System.Collections.IEnumerator WaitBeforeDelete()
    {
        hasHiddenFloor = true;

        if (metalGroan != null)
        {
            metalGroan.Play();
            //pause coroutine, and check if clip available, else wait 0 seconds
            yield return new WaitForSeconds(metalGroan.clip != null ? 3.8f : 0f);
        }

        if (deleteObjects != null)
        {
            deleteObjects.SetActive(false);
        }
    }
}
