using UnityEngine;

public class PathPoints : MonoBehaviour
{
    public static Transform[] points;

    void Awake()
    {
        // Automatically fill the points array with child transforms
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
