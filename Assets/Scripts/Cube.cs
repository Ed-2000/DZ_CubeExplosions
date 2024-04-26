using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubesGenerator))]
public class Cube : MonoBehaviour
{
    [SerializeField] private RayLauncher _rayLauncher;

    private CubesGenerator _cubesGenerator;

    public event Action<List<Rigidbody>> WillDisappear;

    private void Start()
    {
        _cubesGenerator = GetComponent<CubesGenerator>();
    }

    private void OnEnable()
    {
        _rayLauncher.HitInCube += HitHandler;
    }

    private void OnDisable()
    {
        _rayLauncher.HitInCube -= HitHandler;
    }

    private void HitHandler(Cube outCube)
    {
        if (outCube.gameObject == gameObject)
        {
            _cubesGenerator.Generate();
            WillDisappear?.Invoke(_cubesGenerator.GetCubes());
            Destroy(gameObject);
        }
    }
}