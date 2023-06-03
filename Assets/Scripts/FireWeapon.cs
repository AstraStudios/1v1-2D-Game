using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 origin = transform.position; // Change this to your desired starting point
            Vector2 direction = (mouseWorldPosition - origin).normalized;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction);

            if (hit.collider != null)
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                // Do something with the hit object

                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, origin);
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
    }
}
