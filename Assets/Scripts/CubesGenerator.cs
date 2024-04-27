using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeRenderer), typeof(Rigidbody), typeof(Cube))]
public class CubesGenerator : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _cubeParent;

    private List<Cube> _createdCubes = new List<Cube>();
    private int _minCounOfCube = 2;
    private int _maxCounOfCube = 6;
    private float _sizingFactor = 2;
    private float _chanceReductionFactor = 0.5f;

    public float ChanceOfSeparation { get; set; } = 1;

    public void Generate()
    {
        if (Random.value <= ChanceOfSeparation)
        {
            int cubesCount = Random.Range(_minCounOfCube, _maxCounOfCube + 1);
            Vector3 cubeScale = gameObject.transform.localScale / _sizingFactor;

            for (int i = 0; i < cubesCount; i++)
            {
                Cube cube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
                cube.transform.SetParent(_cubeParent);
                cube.transform.localScale = new Vector3(cubeScale.x, cubeScale.y, cubeScale.z);
                cube.GetComponent<CubeRenderer>().SetRandomColor();
                cube.GetComponent<CubesGenerator>().ChanceOfSeparation = ChanceOfSeparation * _chanceReductionFactor;
                _createdCubes.Add(cube);
            }
        }
    }

    public List<Rigidbody> GetCubes()
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (var createdCube in _createdCubes)
            rigidbodies.Add(createdCube.GetComponent<Rigidbody>());

        return rigidbodies;
    }
}