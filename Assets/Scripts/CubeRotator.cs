using System;
using Unity.VisualScripting;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    [SerializeField] GameObject Cube;
    [SerializeField] float Radius;
    [SerializeField] float RotationSpeed;
    [SerializeField] int CubeCount;
    [SerializeField] bool Follow;
    [SerializeField] float StepAngle;

    private GameObject[] cubesArray;
     
    private void Awake()
    {
        cubesArray = new GameObject[CubeCount];
        for (int i = 0; i < CubeCount; i++)
        {
            cubesArray[i] = Instantiate(Cube);
            cubesArray[i].transform.SetParent(transform);
        }
        SetCubesPosition();
    }
    
  

    private void SetCubesPosition()
    {
        var angle = 0f;
        if(Follow)
        {
            angle = StepAngle * Mathf.Deg2Rad;
        }
        else
            angle = (Mathf.PI * 2f) / CubeCount;
        
        for (int i = 0; i < CubeCount; i++)
        {
            var x = Radius * Mathf.Cos(angle * i);
            var z = Radius * Mathf.Sin(angle * i);

            cubesArray[i].transform.localPosition = new Vector3(x, 0, z);
        }
    }

    private void Update()
    {
        SetCubesPosition();
        transform.Rotate(0f, RotationSpeed * Time.deltaTime,0f);
    }
}
