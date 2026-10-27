using UnityEngine;

public class KeepObjectOnScreen : MonoBehaviour
{
    private Camera mainCamera;
    private float objectHalfWidth;
    private float objectHalfHeight;

    void Start()
    {
        mainCamera = Camera.main;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            objectHalfWidth = sr.bounds.extents.x;
            objectHalfHeight = sr.bounds.extents.y;
        }
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        Vector3 position = transform.position;

        Vector3 min = mainCamera.ViewportToWorldPoint(
            new Vector3(0, 0, mainCamera.nearClipPlane)
        );

        Vector3 max = mainCamera.ViewportToWorldPoint(
            new Vector3(1, 1, mainCamera.nearClipPlane)
        );

        position.x = Mathf.Clamp(
            position.x,
            min.x + objectHalfWidth,
            max.x - objectHalfWidth
        );

        position.y = Mathf.Clamp(
            position.y,
            min.y + objectHalfHeight,
            max.y - objectHalfHeight
        );

        transform.position = position;
    }
}
