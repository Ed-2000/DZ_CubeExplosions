using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action WillDisappear;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _cubeParent;
    [SerializeField] private RayLauncher _rayLauncher;

    private float _chanceReductionFactor = 0.5f;
    private int _minCounOfCube = 2;
    private int _maxCounOfCube = 6;

    public float ChanceOfSeparation { get; set; } = 1;

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
            WillDisappear?.Invoke();
            Destroy(gameObject);
        }
    }

    private void GenerateCubes()
    {
        if (UnityEngine.Random.value <=  ChanceOfSeparation)
        {
            int cubesCount = UnityEngine.Random.Range(_minCounOfCube, _maxCounOfCube + 1);
            Vector3 cubeScale = gameObject.transform.localScale / 2;

            for (int i = 0; i < cubesCount; i++)
            {
                Cube cube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
                cube.transform.SetParent(_cubeParent);
                cube.transform.localScale = new Vector3(cubeScale.x, cubeScale.y, cubeScale.z);
                cube.GetComponent<Renderer>().material.color = GetRandomColor();
                cube.ChanceOfSeparation = ChanceOfSeparation * _chanceReductionFactor;
                cube.GetComponent<Marker>().Creator = gameObject;
            }
        }
    }

    private Color GetRandomColor()
    {
        float red = UnityEngine.Random.value;
        float green = UnityEngine.Random.value;
        float blue = UnityEngine.Random.value;

        Color color = new Color(red, green, blue);

        return color;
    }
}