using UnityEngine;

public class Dice : MonoBehaviour
{
    public int Value { get; private set; }
    public bool IsStopped { get; private set; }
    private Vector3[] faceNormals = {
        Vector3.up,
        Vector3.down,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };
    private int[] faceValues = { 1, 6, 5, 2, 4, 3 };
    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.IsSleeping())
        {
            Value = GetTopFace();
            IsStopped = true;
        }

    }

    private int GetTopFace()
    {
        var bestDot = 0f;
        var bestIndex = 0;

        for (var i = 0; i < faceNormals.Length; i++)
        {
            var worldNormal = transform.TransformDirection(faceNormals[i]);
            var dot = Vector3.Dot(worldNormal, Vector3.up);

            if (dot > bestDot)
            {
                bestDot = dot;
                bestIndex = i;
            }
        }
        return faceValues[bestIndex];
    }

    public void IsMoving()
    {
        IsStopped = false;
    }
}
