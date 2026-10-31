using UnityEngine;

public class ObjectReplacement : MonoBehaviour
{

    [Header("Replacment settings")]
    [SerializeField] private GameObject replacementPrefab;


    [SerializeField] private float percentage = 70f;

    public void TryReplace()
    {
        float roll = Random.Range(0f, 100f);

        if (replacementPrefab != null && roll < percentage)
        {
            Instantiate(
                replacementPrefab,
                transform.position,
                transform.rotation
            );
        }
    }

}
