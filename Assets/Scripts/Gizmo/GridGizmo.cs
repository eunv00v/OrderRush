using UnityEngine;

public class GridGizmo : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public Color lineColor = new Color(1, 1, 1, 0.3f);

    void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        for (int x = 0; x <= width; x++)
            Gizmos.DrawLine(
                new Vector3(x, 0, 0),
                new Vector3(x, 0, height));

        for (int z = 0; z <= height; z++)
            Gizmos.DrawLine(
                new Vector3(0, 0, z),
                new Vector3(width, 0, z));
    }
}
