using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeRenderer), typeof(Rigidbody), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _cubeParent;
    [SerializeField] private RayLauncher _rayLauncher;

    private List<Rigidbody> _createdCubes = new List<Rigidbody>();
    private float _chanceReductionFactor = 0.5f;
    private float _sizingFactor = 2;
    private int _minCounOfCube = 2;
    private int _maxCounOfCube = 6;

    public float ChanceOfSeparation { get; set; } = 1;

    public event Action<List<Rigidbody>> WillDisappear;

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
            GenerateCubes();
            WillDisappear?.Invoke(_createdCubes);
            Destroy(gameObject);
        }
    }

    private void GenerateCubes()
    {
        if (UnityEngine.Random.value <= ChanceOfSeparation)
        {
            int cubesCount = UnityEngine.Random.Range(_minCounOfCube, _maxCounOfCube + 1);
            Vector3 cubeScale = gameObject.transform.localScale / _sizingFactor;

            for (int i = 0; i < cubesCount; i++)
            {
                Cube cube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
                cube.transform.SetParent(_cubeParent);
                cube.transform.localScale = new Vector3(cubeScale.x, cubeScale.y, cubeScale.z);
                cube.GetComponent<CubeRenderer>().SetRandomColor();
                cube.ChanceOfSeparation = ChanceOfSeparation * _chanceReductionFactor;
                _createdCubes.Add(cube.GetComponent<Rigidbody>());
            }
        }
    }
}