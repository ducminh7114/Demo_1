using UnityEngine;

public class ObjectReplacement : MonoBehaviour
{

    [Header("Replacment settings")]
    [SerializeField] private GameObject replacementPrefab;


    [SerializeField] private float percentage = 70f;

    private void ReplaceObject(GameObject replacementObject)
    {
        float roll = Random.Range(0.0f, 100.0f);
        {
            if (replacementObject == null && roll < percentage)
            {
                Instantiate(replacementPrefab, transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);
    }
    
}
