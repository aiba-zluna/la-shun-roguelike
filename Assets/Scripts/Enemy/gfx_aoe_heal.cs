using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HealAoEVisual : MonoBehaviour
{
    [SerializeField] private int segments = 40;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = segments;

        Hide();
    }

    public void Show(Transform target, float radius)
    {
        if (target == null)
            return;

        gameObject.SetActive(true);

        UpdatePosition(target, radius);
    }

    public void UpdatePosition(Transform target, float radius)
    {
        if (target == null)
            return;

        Vector3 center = target.position;

        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2f;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            lineRenderer.SetPosition(
                i,
                center + new Vector3(x, y, 0f)
            );
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
