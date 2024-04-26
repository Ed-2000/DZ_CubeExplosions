using System;
using UnityEngine;

public class RayLauncher : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 10;
    
    private Camera _camera;
    private Ray _ray;

    public event Action<Cube> HitInCube;

    private void Start()
    {
        _camera = transform.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ShootRay();
    }

    private void ShootRay()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.green);

        if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.TryGetComponent<Cube>(out Cube cube))
                HitInCube?.Invoke(cube);
        }
    }
}